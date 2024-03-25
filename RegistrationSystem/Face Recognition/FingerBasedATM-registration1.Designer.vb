<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FingerBasedATM_registration1
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
        txtVerify = New TextBox()
        SuspendLayout()
        ' 
        ' txtVerify
        ' 
        txtVerify.Location = New Point(27, 12)
        txtVerify.Multiline = True
        txtVerify.Name = "txtVerify"
        txtVerify.Size = New Size(163, 210)
        txtVerify.TabIndex = 0
        ' 
        ' FingerBasedATM_registration1
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(txtVerify)
        Name = "FingerBasedATM_registration1"
        Text = "FingerBasedATM_registration1"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents txtVerify As TextBox
End Class
