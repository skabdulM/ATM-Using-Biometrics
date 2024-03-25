Imports System.Collections
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Threading
Imports DPUruNet
Imports DPUruNet.Constants
Imports MySql.Data.MySqlClient
Public Class FingerBasedATM_registration1


    Dim conn As MySqlConnection
    Dim COMMAND As MySqlCommand
    ''' <summary>
    ''' Holds the main form with many functions common to all of SDK actions.
    ''' </summary>
    Public _sender As FingerBasedATM__form_main1

    Private Const PROBABILITY_ONE As Integer = &H7FFFFFFF
    Private fFinger As Fmd
    Private sFinger As Fmd
    Private tFinger As Fmd
    Private lFinger As Fmd
    Private count As Integer
    Public acc_no As Integer

    Public Sub New(ByVal acc As Integer)

        ' This call is required by the designer.
        InitializeComponent()
        acc_no = acc
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    ''' <summary>
    ''' Initialize the form.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Verification_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtVerify.Text = String.Empty
        fFinger = Nothing
        sFinger = Nothing
        tFinger = Nothing
        fFinger = Nothing
        count = 0

        SendMessage(Action.SendMessage, "Place a finger on the reader.")

        If Not _sender.OpenReader() Then
            Me.Close()
        End If

        If Not _sender.StartCaptureAsync(AddressOf Me.OnCaptured) Then
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Handler for when a fingerprint is captured.
    ''' </summary>
    ''' <param name="captureResult">contains info and data on the fingerprint capture</param>
    Public Sub OnCaptured(ByVal captureResult As CaptureResult)
        conn = New MySqlConnection
        conn.ConnectionString = "server=localhost;userid=root;password='skabdul';database=fingertest"
        conn.Open()


        Try
            ' Check capture quality and throw an error if bad.
            If Not _sender.CheckCaptureResult(captureResult) Then Return

            SendMessage(Action.SendMessage, "A finger was captured. (" + (count + 1).ToString() + "/4)")

            Dim resultConversion As DataResult(Of Fmd) = FeatureExtraction.CreateFmdFromFid(captureResult.Data, Formats.Fmd.ANSI)

            If resultConversion.ResultCode <> Constants.ResultCode.DP_SUCCESS Then
                _sender.Reset = True
                Throw New Exception("" & resultConversion.ResultCode.ToString())
            End If


            If count = 0 Then
                fFinger = resultConversion.Data
                count = count + 1
            ElseIf count = 1 Then
                sFinger = resultConversion.Data
                count = count + 1
            ElseIf count = 2 Then
                tFinger = resultConversion.Data
                count = count + 1
            ElseIf count = 3 Then
                lFinger = resultConversion.Data
                count = 0
            End If

            If count = 0 Then
                Dim InsertCommand As String
                InsertCommand = "INSERT INTO fingerprints(account_no,test1,test2,test3,test4) VALUES(@account_no,@test1,@test2,@test3,@test4)"
                Dim cmd = New MySqlCommand(InsertCommand, conn)
                cmd.Parameters.AddWithValue("@account_no", acc_no)
                cmd.Parameters.AddWithValue("@test1", Fmd.SerializeXml(fFinger))
                cmd.Parameters.AddWithValue("@test2", Fmd.SerializeXml(sFinger))
                cmd.Parameters.AddWithValue("@test3", Fmd.SerializeXml(tFinger))
                cmd.Parameters.AddWithValue("@test4", Fmd.SerializeXml(lFinger))
                cmd.Prepare()
                cmd.ExecuteNonQuery()
                MsgBox("Record inserted successfully")

            End If

        Catch ex As Exception
            SendMessage(Action.SendMessage, "Error:  " & ex.Message)
        End Try
        conn.Close()
    End Sub

    ''' <summary>
    ''' Close window.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    ''' <summary>
    ''' Close window.
    ''' </summary>
    Private Sub Verification_Closed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        _sender.CancelCaptureAndCloseReader(AddressOf Me.OnCaptured)
    End Sub
    Public Enum Action
        SendMessage
    End Enum
    Private Delegate Sub SendMessageCallback(ByVal action As Action, ByVal payload As String)
    Private Sub SendMessage(ByVal action As Action, ByVal payload As String)
        On Error Resume Next

        If Me.txtVerify.InvokeRequired Then
            Dim d As New SendMessageCallback(AddressOf SendMessage)
            Me.Invoke(d, New Object() {action, payload})
        Else
            Select Case action
                Case Action.SendMessage
                    txtVerify.Text += payload & vbCrLf & vbCrLf
                    txtVerify.SelectionStart = txtVerify.TextLength
                    txtVerify.ScrollToCaret()
            End Select
        End If
    End Sub


End Class