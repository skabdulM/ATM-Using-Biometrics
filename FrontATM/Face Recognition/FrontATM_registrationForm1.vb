Imports System.Collections
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Threading
Imports DPUruNet
Imports DPUruNet.Constants
Imports MySql.Data.MySqlClient
Public Class FrontATM_registrationForm1


    Dim conn As MySqlConnection
    Dim COMMAND As MySqlCommand
    ''' <summary>
    ''' Holds the main form with many functions common to all of SDK actions.
    ''' </summary>
    Public _sender As FrontATM_form_main1
    Private _pin As FrontATM_PinTest1
    Private faceVerify As Form1

    Private Const PROBABILITY_ONE As Integer = &H7FFFFFFF
    Private fFinger As Fmd
    Private sFinger As Fmd
    Private tFinger As Fmd
    Private lFinger As Fmd
    Private MainFinger As Fmd
    Private count As Integer
    Public acc_no As integer

    ''' <summary>
    ''' Initialize the form.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Verification_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        fFinger = Nothing
        sFinger = Nothing
        tFinger = Nothing
        fFinger = Nothing
        MainFinger = Nothing
        count = 0

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

            Dim resultConversion As DataResult(Of Fmd) = FeatureExtraction.CreateFmdFromFid(captureResult.Data, Formats.Fmd.ANSI)

            If resultConversion.ResultCode <> Constants.ResultCode.DP_SUCCESS Then
                _sender.Reset = True
                Throw New Exception("" & resultConversion.ResultCode.ToString())
            End If
            MainFinger = resultConversion.Data

            verifyFinger()


        Catch ex As Exception
            MsgBox("Error:  " & ex.Message)
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

    Public Sub verifyFinger()
        conn = New MySqlConnection
        conn.ConnectionString = "server=localhost;userid=root;password='skabdul';database=fingertest"
        conn.Open()
        Dim cmd As New MySqlCommand
        Dim da As New MySqlDataAdapter
        Dim dr As MySqlDataReader
        Dim ds, ds1 As New DataSet
        Try
            ds.Clear()
            cmd = New MySqlCommand("select * from fingerprints WHERE account_no", conn)
            da = New MySqlDataAdapter(cmd)
            da.Fill(ds, "Subject_Detail")
            Dim cnt As Integer = ds.Tables(0).Rows.Count
            For i As Integer = 0 To cnt - 1
                fFinger = Fmd.DeserializeXml(ds.Tables(0).Rows(i).Item(1))
                sFinger = Fmd.DeserializeXml(ds.Tables(0).Rows(i).Item(2))
                tFinger = Fmd.DeserializeXml(ds.Tables(0).Rows(i).Item(3))
                lFinger = Fmd.DeserializeXml(ds.Tables(0).Rows(i).Item(4))

                Dim CompareResult = Comparison.Compare(MainFinger, 0, fFinger, 0)

                If CompareResult.ResultCode <> Constants.ResultCode.DP_SUCCESS Then
                    _sender.Reset = True
                    Throw New Exception("" & CompareResult.ResultCode.ToString())
                End If


                If CompareResult.Score < (PROBABILITY_ONE / 100000) Then
                    Console.WriteLine("Comparison resulted in a dissimilarity score of " & CompareResult.Score.ToString())
                    MsgBox("Finger Print Matched!")
                    acc_no = CInt(ds.Tables(0).Rows(i).Item(0))
                    faceVerify = New Form1(acc_no)
                    faceVerify.ShowDialog()
                    faceVerify.Dispose()
                    faceVerify = Nothing
                    Return
                End If

                CompareResult = Comparison.Compare(MainFinger, 0, sFinger, 0)

                If CompareResult.ResultCode <> Constants.ResultCode.DP_SUCCESS Then
                    _sender.Reset = True
                    Throw New Exception("" & CompareResult.ResultCode.ToString())
                End If


                If CompareResult.Score < (PROBABILITY_ONE / 100000) Then
                    Console.WriteLine("Comparison resulted in a dissimilarity score of " & CompareResult.Score.ToString())
                    MsgBox("Finger Print Matched!")
                    acc_no = CInt(ds.Tables(0).Rows(i).Item(0))
                    faceVerify = New Form1(acc_no)
                    faceVerify.ShowDialog()
                    faceVerify.Dispose()
                    faceVerify = Nothing
                    Return
                End If

                CompareResult = Comparison.Compare(MainFinger, 0, tFinger, 0)

                If CompareResult.ResultCode <> Constants.ResultCode.DP_SUCCESS Then
                    _sender.Reset = True
                    Throw New Exception("" & CompareResult.ResultCode.ToString())
                End If


                If CompareResult.Score < (PROBABILITY_ONE / 100000) Then
                    Console.WriteLine("Comparison resulted in a dissimilarity score of " & CompareResult.Score.ToString())
                    MsgBox("Finger Print Matched!")
                    acc_no = CInt(ds.Tables(0).Rows(i).Item(0))
                    faceVerify = New Form1(acc_no)
                    faceVerify.ShowDialog()
                    faceVerify.Dispose()
                    faceVerify = Nothing

                    Return
                End If

                CompareResult = Comparison.Compare(MainFinger, 0, lFinger, 0)

                If CompareResult.ResultCode <> Constants.ResultCode.DP_SUCCESS Then
                    _sender.Reset = True
                    Throw New Exception("" & CompareResult.ResultCode.ToString())
                End If


                If CompareResult.Score < (PROBABILITY_ONE / 100000) Then
                    Console.WriteLine("Comparison resulted in a dissimilarity score of " & CompareResult.Score.ToString())
                    MsgBox("Finger Print Matched!")
                    acc_no = CInt(ds.Tables(0).Rows(i).Item(0))
                    faceVerify = New Form1(acc_no)
                    faceVerify.ShowDialog()
                    faceVerify.Dispose()
                    faceVerify = Nothing

                    Return
                End If

                If i = ds.Tables(0).Rows.Count - 1 Then
                    MsgBox("Finger Print was not found. Please try again.")
                    Return
                End If
            Next


        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label1.Visible = Not Label1.Visible
    End Sub

    Public Enum Action
        SendMessage
    End Enum
    Private Delegate Sub SendMessageCallback(ByVal action As Action, ByVal payload As String)



End Class