<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FingerBasedATM_selectreader1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        cboReaders = New ComboBox()
        register = New Button()
        SuspendLayout()
        ' 
        ' cboReaders
        ' 
        cboReaders.AccessibleName = "cboReaders"
        cboReaders.FormattingEnabled = True
        cboReaders.Location = New Point(290, 45)
        cboReaders.Name = "cboReaders"
        cboReaders.Size = New Size(151, 28)
        cboReaders.TabIndex = 2
        cboReaders.Tag = "cboReaders"
        ' 
        ' register
        ' 
        register.Location = New Point(314, 104)
        register.Name = "register"
        register.Size = New Size(94, 29)
        register.TabIndex = 3
        register.Text = "Go Home"
        register.UseVisualStyleBackColor = True
        ' 
        ' FingerBasedATM_selectreader1
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(register)
        Controls.Add(cboReaders)
        Name = "FingerBasedATM_selectreader1"
        Text = "FingerBasedATM_selectreader1"
        ResumeLayout(False)
    End Sub
    Friend WithEvents cboReaders As ComboBox
    Public WithEvents register As Button
End Class
