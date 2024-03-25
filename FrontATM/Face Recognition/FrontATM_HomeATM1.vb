Imports MySql.Data.MySqlClient

Public Class FrontATM_HomeATM1
    Public acc_no As Integer
    Private ac_balance As Integer
    Dim conn As MySqlConnection
    Dim COMMAND As MySqlCommand
    Dim cmd As New MySqlCommand
    Dim da As New MySqlDataAdapter
    Dim dr As MySqlDataReader
    Dim ds, ds1 As New DataSet
    Public Sub New(ByVal acc As Integer)

        ' This call is required by the designer.
        InitializeComponent()
        acc_no = acc
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub HomeATM_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        conn = New MySqlConnection
        conn.ConnectionString = "server=localhost;userid=root;password='skabdul';database=fingertest"
        conn.Open()

        Try
            ds.Clear()
            cmd = New MySqlCommand("select * from personaldetails WHERE account_no = " + acc_no.ToString(), conn)

            da = New MySqlDataAdapter(cmd)
            da.Fill(ds, "Subject_Detail")
            Label1.Text = "Welcome " + ds.Tables(0).Rows(0).Item(1).ToString() + " " + ds.Tables(0).Rows(0).Item(2).ToString()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub DeduductBalance(int)
        Try
            conn = New MySqlConnection
            conn.ConnectionString = "server=localhost;userid=root;password='skabdul';database=fingertest"
            conn.Open()
            ds.Clear()
            cmd = New MySqlCommand("select * from account_details WHERE account_no = " + acc_no.ToString(), conn)
            da = New MySqlDataAdapter(cmd)
            da.Fill(ds, "Subject_Detail")
            ac_balance = ds.Tables(0).Rows(0).Item("ac_balance")
            If int < ac_balance Then
                ac_balance = ac_balance - int
                cmd = New MySqlCommand("update account_details Set ac_balance=" + ac_balance.ToString() + " WHERE account_no = " + acc_no.ToString(), conn)
                da = New MySqlDataAdapter(cmd)
                da.Fill(ds, "Subject_Detail")
                MsgBox("Transaction sucessfull")
                Me.Close()
            Else
                MsgBox("Insufficient Funds")
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        DeduductBalance(500)
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        DeduductBalance(5000)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        DeduductBalance(2000)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DeduductBalance(1000)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        DeduductBalance(10000)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim transaction_ammount As Integer
        transaction_ammount = Microsoft.VisualBasic.InputBox("Enter Ammount, Note! transaction  only upto 15,000", "Input transaction ammount", 0, 220, 200)
        If transaction_ammount <= 15000 Then
            DeduductBalance(transaction_ammount)
        Else
            MessageBox.Show("Enter a valid ammount")
        End If
    End Sub
End Class