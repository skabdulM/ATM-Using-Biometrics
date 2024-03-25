Imports System.Collections
Imports System.Collections.Generic
Imports System.Diagnostics.Eventing
Imports System.Diagnostics.Metrics
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Runtime.Intrinsics.X86
Imports System.Text.RegularExpressions
Imports System.Threading
Imports DPUruNet
Imports DPUruNet.Constants

Public Class FrontATM_form_main1

    Dim len, len2 As Integer
    Dim counter As Integer
    Dim txt, txt2 As String
    Dim CountUp As Boolean = False
    Dim Red As Integer = 255
    Dim form_main As FrontATM_form_main1
    Public Property Fmds() As Dictionary(Of Int16, Fmd)
        Get
            Return _fmds
        End Get
        Set(ByVal value As Dictionary(Of Int16, Fmd))
            _fmds = value
        End Set
    End Property
    Private _fmds As Dictionary(Of Int16, Fmd) = New Dictionary(Of Int16, Fmd)

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
    Private _verification As FrontATM_registrationForm1

    Private Sub Form_Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Counter = 0
        Len = welcomeLabel.Text.Length
        txt = welcomeLabel.Text
        welcomeLabel.Text = ""
        len2 = bankLabel.Text.Length
        txt2 = bankLabel.Text
        bankLabel.Text = ""
        Label2.Visible = False
        Timer1.Start()

        If _readerSelect Is Nothing Then
            _readerSelect = New FrontATM_SelectReader1

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



    Private _readerSelect As FrontATM_SelectReader1

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

    Private Enum Action
        UpdateReaderState
    End Enum
    Private Delegate Sub SendMessageCallback(ByVal state As Action, ByVal payload As Object)
    Private Sub SendMessage(ByVal state As Action, ByVal payload As Object)
        On Error Resume Next

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        counter += 1

        If counter > len Then
            Timer1.Stop()
            counter = 0

            Timer2.Start()

        Else
            welcomeLabel.Text = txt.Substring(0, counter)
        End If


    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        counter += 1

        If counter > len2 Then
            Timer2.Stop()
            Label2.Visible = True
            Timer3.Start()

        Else
            bankLabel.Text = txt2.Substring(0, counter)
        End If

    End Sub

    Private Sub ClickHandler(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseClick, bankLabel.MouseClick, welcomeLabel.MouseClick, MyBase.MouseClick
        If _verification Is Nothing Then
            _verification = New FrontATM_registrationForm1
            _verification._sender = Me
        End If
        Me.Hide()
        _verification.ShowDialog()
        Me.Show()
        _verification.Dispose()
        _verification = Nothing


    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        If CountUp = True Then
            If Red < 255 Then
                Red = Red + 1
            Else
                CountUp = False
            End If

        End If

        If CountUp = False Then
            If Red > 0 Then
                Red = Red - 1
            Else
                CountUp = True
            End If

        End If

        Label2.ForeColor = Color.FromArgb(Red, 128, 128)

    End Sub

End Class