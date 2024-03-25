<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrontATM_HomeATM1
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
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(FrontATM_HomeATM1))
        Label1 = New Label()
        Button1 = New Button()
        Button3 = New Button()
        Button5 = New Button()
        Button2 = New Button()
        Button4 = New Button()
        Button6 = New Button()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.BackColor = Color.Transparent
        Label1.Font = New Drawing.Font("Times New Roman", 24.0F, FontStyle.Bold, GraphicsUnit.Point)
        Label1.ForeColor = Color.Cornsilk
        Label1.Location = New Point(213, 48)
        Label1.Name = "Label1"
        Label1.Size = New Size(138, 45)
        Label1.TabIndex = 0
        Label1.Text = "Label1"
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(187, 187)
        Button1.Name = "Button1"
        Button1.Size = New Size(164, 59)
        Button1.TabIndex = 1
        Button1.Text = "500"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(187, 350)
        Button3.Name = "Button3"
        Button3.Size = New Size(164, 60)
        Button3.TabIndex = 3
        Button3.Text = "5000"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Button5
        ' 
        Button5.Location = New Point(187, 267)
        Button5.Name = "Button5"
        Button5.Size = New Size(164, 62)
        Button5.TabIndex = 5
        Button5.Text = "2000"
        Button5.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(418, 187)
        Button2.Name = "Button2"
        Button2.Size = New Size(164, 59)
        Button2.TabIndex = 6
        Button2.Text = "1000"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button4
        ' 
        Button4.Location = New Point(418, 270)
        Button4.Name = "Button4"
        Button4.Size = New Size(164, 59)
        Button4.TabIndex = 7
        Button4.Text = "10000"
        Button4.UseVisualStyleBackColor = True
        ' 
        ' Button6
        ' 
        Button6.Location = New Point(418, 348)
        Button6.Name = "Button6"
        Button6.Size = New Size(164, 62)
        Button6.TabIndex = 8
        Button6.Text = "custom"
        Button6.UseVisualStyleBackColor = True
        ' 
        ' FrontATM_HomeATM1
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), Image)
        ClientSize = New Size(631, 489)
        Controls.Add(Button6)
        Controls.Add(Button4)
        Controls.Add(Button2)
        Controls.Add(Button5)
        Controls.Add(Button3)
        Controls.Add(Button1)
        Controls.Add(Label1)
        ForeColor = SystemColors.ControlText
        Name = "FrontATM_HomeATM1"
        Text = "FrontATM_HomeATM1"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button6 As Button
End Class
