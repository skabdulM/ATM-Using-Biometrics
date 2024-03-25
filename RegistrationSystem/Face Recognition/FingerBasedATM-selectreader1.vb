Imports System.Diagnostics.Eventing
Imports DPUruNet
Public Class FingerBasedATM_selectreader1
    Private _readers As ReaderCollection



    Private Sub SelectReader_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cboReaders.Text = String.Empty
        cboReaders.Items.Clear()
        cboReaders.SelectedIndex = -1


        _readers = ReaderCollection.GetReaders()


        For Each Reader As Reader In _readers
            cboReaders.Items.Add(Reader.Description.SerialNumber)
            cboReaders.SelectedIndex = 0
            FingerBasedATM__form_main1._currentReader = _readers(0)
            Me.Close()
        Next


    End Sub


End Class