Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient

Public Class FrontATM_PinTest1

    Dim conn As MySqlConnection
    Dim COMMAND As MySqlCommand
    Public acc_no As Integer
    Dim count As Integer = 3
    Private _Home As FrontATM_HomeATM1

    Dim cmd As New MySqlCommand
    Dim da As New MySqlDataAdapter
    Dim dr As MySqlDataReader
    Dim ds, ds1 As New DataSet
    Dim chck As New Integer
    Public Sub New(ByVal acc As Integer)

        ' This call is required by the designer.
        InitializeComponent()
        acc_no = acc
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox1.Text = TextBox1.Text.ToString() + "1"
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox1.Text = TextBox1.Text.ToString() + "2"
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TextBox1.Text = TextBox1.Text.ToString() + "3"
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        TextBox1.Text = TextBox1.Text.ToString() + "4"
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        TextBox1.Text = TextBox1.Text.ToString() + "5"
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        TextBox1.Text = TextBox1.Text.ToString() + "6"
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        TextBox1.Text = TextBox1.Text.ToString() + "7"
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        TextBox1.Text = TextBox1.Text.ToString() + "8"
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        TextBox1.Text = TextBox1.Text.ToString() + "9"
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        TextBox1.Text = TextBox1.Text.ToString() + "0"
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        TextBox1.Text = ""
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        conn = New MySqlConnection
        conn.ConnectionString = "server=localhost;userid=root;password='skabdul';database=fingertest"
        conn.Open()

        Try
            ds.Clear()
            cmd = New MySqlCommand("select * from account_details WHERE account_no = " + acc_no.ToString(), conn)

            da = New MySqlDataAdapter(cmd)
            da.Fill(ds, "Subject_Detail")

            chck = ds.Tables(0).Rows(0).Item(1).ToString()
            If chck = TextBox1.Text Then
                If _Home Is Nothing Then
                    _Home = New FrontATM_HomeATM1(acc_no)
                End If
                Me.Hide()
                _Home.ShowDialog()
                _Home.Dispose()
                _Home = Nothing
                Return
            End If

            If count = 0 Then
                MsgBox("Maximum Attempt reached")
                Me.Close()
            Else
                MsgBox("Wrong Pin. You Have " + count.ToString() + " attempt left")
                TextBox1.Text = ""
                count = count - 1
            End If
            conn.Close()
        Catch ex As Exception
            MsgBox(ex)
        End Try
    End Sub

    Private Sub PinTest_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class