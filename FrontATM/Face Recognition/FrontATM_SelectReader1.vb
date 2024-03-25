Imports System.Diagnostics.Eventing
Imports DPUruNet
Public Class FrontATM_SelectReader1
    Private _readers As ReaderCollection



    Private Sub SelectReader_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cboReaders.Text = String.Empty
        cboReaders.Items.Clear()
        cboReaders.SelectedIndex = -1


        _readers = ReaderCollection.GetReaders()


        For Each Reader As Reader In _readers
            cboReaders.Items.Add(Reader.Description.SerialNumber)
            cboReaders.SelectedIndex = 0
            FrontATM_form_main1._currentReader = _readers(0)
            Me.Close()
        Next


    End Sub

    Private Sub register_Click(sender As Object, e As EventArgs) Handles register.Click
        Dim callpg As FrontATM_registrationForm1 = FrontATM_registrationForm1
        callpg.Show()
    End Sub
End Class