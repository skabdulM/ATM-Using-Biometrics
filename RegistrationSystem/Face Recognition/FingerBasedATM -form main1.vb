Imports System.Collections
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Transactions
Imports DPUruNet
Imports DPUruNet.Constants
Imports MySql.Data.MySqlClient

Public Class FingerBasedATM__form_main1

    Dim entry_flag As Boolean = True
    Dim count_flag As Integer = 0

    Dim conn As MySqlConnection
    Dim COMMAND As MySqlCommand
    Public Property Fmds() As Dictionary(Of Int16, Fmd)
        Get
            Return _fmds
        End Get
        Set(ByVal value As Dictionary(Of Int16, Fmd))
            _fmds = value
        End Set
    End Property
    Private _fmds As Dictionary(Of Int16, Fmd) = New Dictionary(Of Int16, Fmd)
    Public acc_no As Integer
    ''' <summary>
    ''' Reset the UI causing the user to reselect a reader.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Reset() As Boolean
        Get
            Return _reset
        End Get
        Set(ByVal value As Boolean)
            _reset = value
        End Set
    End Property
    Private _reset As Boolean

    Private Sub Form_Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '//////////////////////////////
        getAccountNumber()
        '/////////////////////////////
        If _readerSelect Is Nothing Then
            _readerSelect = New FingerBasedATM_selectreader1
            '_readerSelect._sender = Me
        End If

        _readerSelect.ShowDialog()

        _readerSelect.Dispose()
        _readerSelect = Nothing
    End Sub

    ' When set by child forms, shows s/n and enables buttons.
    Public Property CurrentReader() As Reader
        Get
            Return _currentReader
        End Get
        Set(ByVal value As Reader)
            _currentReader = value
            SendMessage(Action.UpdateReaderState, value)
        End Set
    End Property
    Public _currentReader As Reader

    Private _verification As FingerBasedATM_registration1

    Private Sub btnVerify_Click_1(sender As Object, e As EventArgs) Handles btnVerify.Click

        If entry_flag = False Then
            Return
        End If

        If FnameText.Text = "" Or LnameText.Text = "" Or PhoneText.Text = "" Or MailText.Text = "" Or AdhaarText.Text = "" Or PanText.Text = "" Or BalText.Text = "" Or PinText.Text = "" Or RePinText.Text = "" Then
            MsgBox("All values not filled")
            Return
        End If

        Me.acc_no = CInt(AccText.Text)
        If _verification Is Nothing Then
            _verification = New FingerBasedATM_registration1(acc_no)
            _verification._sender = Me
        End If

        _verification.ShowDialog()
        _verification.Dispose()
        _verification = Nothing
    End Sub

    Private _readerSelect As FingerBasedATM_selectreader1

    ''' <summary>
    ''' Open a device and check result for errors.
    ''' </summary>
    ''' <returns>Returns true if successful; false if unsuccessful</returns>
    Public Function OpenReader() As Boolean
        Reset = False
        Dim result As Constants.ResultCode = Constants.ResultCode.DP_DEVICE_FAILURE

        result = _currentReader.Open(Constants.CapturePriority.DP_PRIORITY_COOPERATIVE)

        If result <> Constants.ResultCode.DP_SUCCESS Then
            MessageBox.Show("Error:  " & result.ToString())
            Reset = True
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Hookup capture handler and start capture.
    ''' </summary>
    ''' <param name="OnCaptured">Delegate to hookup as handler of the On_Captured event</param>
    ''' <returns>Returns true if successful; false if unsuccessful</returns>
    Public Function StartCaptureAsync(ByVal OnCaptured As Reader.CaptureCallback) As Boolean
        AddHandler _currentReader.On_Captured, OnCaptured

        If Not CaptureFingerAsync() Then
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Cancel the capture and then close the reader.
    ''' </summary>
    ''' <param name="OnCaptured">Delegate to unhook as handler of the On_Captured event </param>
    Public Sub CancelCaptureAndCloseReader(ByVal OnCaptured As Reader.CaptureCallback)
        If _currentReader IsNot Nothing Then
            ' Dispose of reader handle and unhook reader events.
            CurrentReader.Dispose()

            If (Reset) Then
                CurrentReader = Nothing
            End If
        End If
    End Sub

    ''' <summary>
    ''' Check the device status before starting capture.
    ''' </summary>
    ''' <returns></returns>
    Public Sub GetStatus()
        Dim result = _currentReader.GetStatus()

        If (result <> ResultCode.DP_SUCCESS) Then
            If CurrentReader IsNot Nothing Then
                Reset = True
                Throw New Exception("" & result.ToString())
            End If
        End If

        If (_currentReader.Status.Status = ReaderStatuses.DP_STATUS_BUSY) Then
            Thread.Sleep(50)
        ElseIf (_currentReader.Status.Status = ReaderStatuses.DP_STATUS_NEED_CALIBRATION) Then
            _currentReader.Calibrate()
        ElseIf (_currentReader.Status.Status <> ReaderStatuses.DP_STATUS_READY) Then
            Throw New Exception("Reader Status - " & CurrentReader.Status.Status.ToString())
        End If
    End Sub

    ''' <summary>
    ''' Check quality of the resulting capture.
    ''' </summary>
    Public Function CheckCaptureResult(ByVal captureResult As CaptureResult) As Boolean
        If captureResult.Data Is Nothing Then
            If captureResult.ResultCode <> Constants.ResultCode.DP_SUCCESS Then
                Reset = True
                Throw New Exception("" & captureResult.ResultCode.ToString())
            End If

            If captureResult.Quality <> Constants.CaptureQuality.DP_QUALITY_CANCELED Then
                Throw New Exception("Quality - " & captureResult.Quality.ToString())
            End If
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Function to capture a finger. Always get status first and calibrate or wait if necessary.  Always check status and capture errors.
    ''' </summary>
    ''' <param name="fid"></param>
    ''' <returns></returns>
    Public Function CaptureFingerAsync() As Boolean
        Try
            GetStatus()

            Dim captureResult = _currentReader.CaptureAsync(Formats.Fid.ANSI,
                                                   CaptureProcessing.DP_IMG_PROC_DEFAULT,
                                                    _currentReader.Capabilities.Resolutions(0))

            If captureResult <> ResultCode.DP_SUCCESS Then
                Reset = True
                Throw New Exception("" + captureResult.ToString())
            End If

            Return True
        Catch ex As Exception
            MessageBox.Show("Error:  " & ex.Message)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Create a bitmap from raw data in row/column format.
    ''' </summary>
    ''' <param name="bytes"></param>
    ''' <param name="width"></param>
    ''' <param name="height"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CreateBitmap(ByVal bytes As [Byte](), ByVal width As Integer, ByVal height As Integer) As Bitmap
        Dim rgbBytes As Byte() = New Byte(bytes.Length * 3 - 1) {}

        For i As Integer = 0 To bytes.Length - 1
            rgbBytes((i * 3)) = bytes(i)
            rgbBytes((i * 3) + 1) = bytes(i)
            rgbBytes((i * 3) + 2) = bytes(i)
        Next
        Dim bmp As New Bitmap(width, height, PixelFormat.Format24bppRgb)

        Dim data As BitmapData = bmp.LockBits(New Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.[WriteOnly], PixelFormat.Format24bppRgb)

        For i As Integer = 0 To bmp.Height - 1
            Dim p As New IntPtr(data.Scan0.ToInt64() + data.Stride * i)
            System.Runtime.InteropServices.Marshal.Copy(rgbBytes, i * bmp.Width * 3, p, bmp.Width * 3)
        Next

        bmp.UnlockBits(data)

        Return bmp
    End Function

#Region "SendMessage"
    Private Enum Action
        UpdateReaderState
    End Enum
    Private Delegate Sub SendMessageCallback(ByVal state As Action, ByVal payload As Object)
    Private Sub SendMessage(ByVal state As Action, ByVal payload As Object)
        On Error Resume Next

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        conn = New MySqlConnection
        conn.ConnectionString = "server=localhost;userid=root;password='skabdul';database=fingertest"
        conn.Open()
        Dim InsertCommand As String

        Try
            InsertCommand = "INSERT INTO account_details(account_no,pin_code,pan_id,adhaar_id,ac_balance,ac_type) VALUES(@account_no,@pin_code,@pan_id,@adhaar_id,@ac_balance,ac_type)"
            Dim cmd1 = New MySqlCommand(InsertCommand, conn)
            cmd1.Parameters.AddWithValue("@account_no", acc_no)
            cmd1.Parameters.AddWithValue("@pin_code", CInt(PinText.Text))
            cmd1.Parameters.AddWithValue("@pan_id", PanText.Text.ToString())
            cmd1.Parameters.AddWithValue("@adhaar_id", AdhaarText.Text.ToString())
            cmd1.Parameters.AddWithValue("@ac_balance", BalText.Text.ToString())
            cmd1.Parameters.AddWithValue("@ac_type", "Savings")
            cmd1.Prepare()
            cmd1.ExecuteNonQuery()
            MsgBox("account details inserted successfully")

            InsertCommand = "INSERT INTO PersonalDetails(account_no,fName,lName,phone,email) VALUES(@account_no,@fName,@lName,@phone,@email)"
            Dim cmd2 = New MySqlCommand(InsertCommand, conn)
            cmd2.Parameters.AddWithValue("@account_no", acc_no)
            cmd2.Parameters.AddWithValue("@fName", FnameText.Text.ToString())
            cmd2.Parameters.AddWithValue("@lName", LnameText.Text.ToString())
            cmd2.Parameters.AddWithValue("@phone", PhoneText.Text.ToString())
            cmd2.Parameters.AddWithValue("@email", MailText.Text.ToString())
            cmd2.Prepare()
            cmd2.ExecuteNonQuery()
            MsgBox("perosnal details inserted successfully")
        Catch ex As Exception
            MsgBox("Error:  " & ex.Message)
        End Try
    End Sub

    'Make Changes in this
    Public Sub getAccountNumber()
        conn = New MySqlConnection
        conn.ConnectionString = "server=localhost;userid=root;password='skabdul';database=fingertest"
        conn.Open()
        Dim cmd As New MySqlCommand
        Dim da As New MySqlDataAdapter
        Dim dr As MySqlDataReader
        Dim ds, ds1 As New DataSet
        Try
            ds.Clear()
            cmd = New MySqlCommand("select * from account_details WHERE account_no", conn)
            da = New MySqlDataAdapter(cmd)
            da.Fill(ds, "Subject_Detail")
            Dim cnt As Integer = ds.Tables(0).Rows.Count
            AccText.Text = (CInt(ds.Tables(0).Rows(cnt - 1).Item(0)) + 1).ToString
        Catch ex As Exception
            MsgBox(ex)
        End Try
    End Sub

    Private Sub FnameText_Focus(sender As Object, e As EventArgs) Handles FnameText.LostFocus
        Dim nameRegex As New Regex("^[A-Za-z]+(?: [A-Za-z]+)*$")
        Dim Name As String = FnameText.Text.ToString()

        If entry_flag = True Then
            count_flag = 1
        End If

        If count_flag = 1 Then
            entry_flag = False
            If Name = "" Then
                ErrorProvider1.SetError(FnameText, "Name cannnot be empty")
                FnameText.Focus()
            ElseIf Not nameRegex.IsMatch(Name) Then
                ErrorProvider1.SetError(FnameText, "Name is not in proper format")
                FnameText.Focus()

            Else
                ErrorProvider1.Clear()
                entry_flag = True
            End If
        End If
    End Sub

    Private Sub LnameText_Focus(sender As Object, e As EventArgs) Handles LnameText.LostFocus
        Dim nameRegex As New Regex("^[A-Za-z]+(?: [A-Za-z]+)*$")
        Dim Name As String = LnameText.Text.ToString()
        If entry_flag = True Then
            count_flag = 2
        End If

        If count_flag = 2 Then
            entry_flag = False
            If Name = "" Then
                ErrorProvider1.SetError(LnameText, "Name cannot be empty")
                LnameText.Focus()
            ElseIf Not nameRegex.IsMatch(Name) Then
                ErrorProvider1.SetError(LnameText, "Name is not in proper format")
                LnameText.Focus()
            Else
                ErrorProvider1.Clear()
                entry_flag = True
            End If
        End If
    End Sub

    Private Sub PhoneText_Focus(sender As Object, e As EventArgs) Handles PhoneText.LostFocus
        Dim phoneRegex As New Regex("^[1-9]\d{9}$")
        Dim phoneNumber As String = PhoneText.Text.ToString()

        If entry_flag = True Then
            count_flag = 3
        End If

        If count_flag = 3 Then
            entry_flag = False
            If phoneNumber = "" Then
                ErrorProvider1.SetError(PhoneText, "Phone Number Cannot be empty")
                PhoneText.Focus()
            ElseIf Not phoneRegex.IsMatch(phoneNumber) Then
                ErrorProvider1.SetError(PhoneText, "Invalid Phone number")
                PhoneText.Focus()
            Else

                ErrorProvider1.Clear()
                entry_flag = True
            End If
        End If


    End Sub

    Private Sub MailText_focus(sender As Object, e As EventArgs) Handles MailText.LostFocus
        Dim emailRegex As New Regex("^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$")
        Dim email As String = MailText.Text.ToString()

        If entry_flag = True Then
            count_flag = 4
        End If
        If count_flag = 4 Then
            entry_flag = False
            If email = "" Then
                ErrorProvider1.SetError(MailText, "E-mail cannot be empty")
                MailText.Focus()
            ElseIf Not emailRegex.IsMatch(email) Then
                ErrorProvider1.SetError(MailText, "Improper e-mail format")
                MailText.Focus()
            Else
                ErrorProvider1.Clear()
                entry_flag = True
            End If
        End If
    End Sub

    Private Sub AdhaarText_focus(sender As Object, e As EventArgs) Handles AdhaarText.LostFocus
        Dim aadhaarRegex As New Regex("^\d{4}\s\d{4}\s\d{4}$")
        Dim aadhaarNumber As String = AdhaarText.Text.ToString()

        If entry_flag = True Then
            count_flag = 5
        End If
        If count_flag = 5 Then
            entry_flag = False
            If aadhaarNumber = "" Then
                ErrorProvider1.SetError(AdhaarText, "Aadhaar number cannot empty")
                AdhaarText.Focus()
            ElseIf Not aadhaarRegex.IsMatch(aadhaarNumber) Then
                ErrorProvider1.SetError(AdhaarText, "Not a valid aadhaar number")
                AdhaarText.Focus()
            Else
                ErrorProvider1.Clear()
                entry_flag = True
            End If
        End If
    End Sub

    Private Sub PanText_focus(sender As Object, e As EventArgs) Handles PanText.LostFocus
        Dim panRegex As New Regex("^[A-Z]{5}[0-9]{4}[A-Z]{1}")
        Dim panNumber As String = PanText.Text.ToString()
        If entry_flag = True Then
            count_flag = 6
        End If
        If count_flag = 6 Then
            entry_flag = False
            If panNumber = "" Then
                ErrorProvider1.SetError(PanText, "Pan no cannot be empty")
                PanText.Focus()
            ElseIf Not panRegex.IsMatch(panNumber) Then
                ErrorProvider1.SetError(PanText, "Not a valid pan number")
                PanText.Focus()
            Else
                ErrorProvider1.Clear()
                entry_flag = True
            End If
        End If
    End Sub
    Private Sub RePinText_Focus(sender As Object, e As EventArgs) Handles RePinText.LostFocus
        If PinText.Text.ToString <> RePinText.Text.ToString Then
            MsgBox("Pin did not match")
            PinText.Text = ""
            RePinText.Text = ""
            PinText.Focus()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim redirect As New Form1
        redirect.Show()
    End Sub
#End Region
End Class