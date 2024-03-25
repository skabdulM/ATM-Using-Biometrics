<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FingerBasedATM__form_main1
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
        Me.components = New System.ComponentModel.Container()
        Me.btnVerify = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.FnameText = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LnameText = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PhoneText = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.MailText = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.BalText = New System.Windows.Forms.TextBox()
        Me.PanText = New System.Windows.Forms.TextBox()
        Me.AdhaarText = New System.Windows.Forms.TextBox()
        Me.AccText = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.RePinText = New System.Windows.Forms.TextBox()
        Me.PinText = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Button2 = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnVerify
        '
        Me.btnVerify.Location = New System.Drawing.Point(268, 182)
        Me.btnVerify.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnVerify.Name = "btnVerify"
        Me.btnVerify.Size = New System.Drawing.Size(143, 28)
        Me.btnVerify.TabIndex = 0
        Me.btnVerify.Text = "Register Finger Print"
        Me.btnVerify.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 28)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "First Name"
        '
        'FnameText
        '
        Me.FnameText.Location = New System.Drawing.Point(73, 26)
        Me.FnameText.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.FnameText.Name = "FnameText"
        Me.FnameText.Size = New System.Drawing.Size(95, 20)
        Me.FnameText.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 55)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Last Name"
        '
        'LnameText
        '
        Me.LnameText.Location = New System.Drawing.Point(72, 53)
        Me.LnameText.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.LnameText.Name = "LnameText"
        Me.LnameText.Size = New System.Drawing.Size(95, 20)
        Me.LnameText.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 81)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Phone"
        '
        'PhoneText
        '
        Me.PhoneText.Location = New System.Drawing.Point(73, 81)
        Me.PhoneText.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.PhoneText.Name = "PhoneText"
        Me.PhoneText.Size = New System.Drawing.Size(95, 20)
        Me.PhoneText.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 107)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(25, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "mail"
        '
        'MailText
        '
        Me.MailText.Location = New System.Drawing.Point(72, 107)
        Me.MailText.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.MailText.Name = "MailText"
        Me.MailText.Size = New System.Drawing.Size(95, 20)
        Me.MailText.TabIndex = 8
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.PhoneText)
        Me.GroupBox1.Controls.Add(Me.MailText)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.FnameText)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.LnameText)
        Me.GroupBox1.Location = New System.Drawing.Point(17, 19)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox1.Size = New System.Drawing.Size(188, 138)
        Me.GroupBox1.TabIndex = 9
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Personal Details"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.BalText)
        Me.GroupBox2.Controls.Add(Me.PanText)
        Me.GroupBox2.Controls.Add(Me.AdhaarText)
        Me.GroupBox2.Controls.Add(Me.AccText)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Location = New System.Drawing.Point(257, 19)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox2.Size = New System.Drawing.Size(241, 138)
        Me.GroupBox2.TabIndex = 10
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Account Details"
        '
        'BalText
        '
        Me.BalText.Location = New System.Drawing.Point(110, 111)
        Me.BalText.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.BalText.Name = "BalText"
        Me.BalText.Size = New System.Drawing.Size(128, 20)
        Me.BalText.TabIndex = 14
        '
        'PanText
        '
        Me.PanText.Location = New System.Drawing.Point(110, 80)
        Me.PanText.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.PanText.Name = "PanText"
        Me.PanText.Size = New System.Drawing.Size(128, 20)
        Me.PanText.TabIndex = 13
        '
        'AdhaarText
        '
        Me.AdhaarText.Location = New System.Drawing.Point(110, 50)
        Me.AdhaarText.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.AdhaarText.Name = "AdhaarText"
        Me.AdhaarText.Size = New System.Drawing.Size(128, 20)
        Me.AdhaarText.TabIndex = 12
        '
        'AccText
        '
        Me.AccText.Enabled = False
        Me.AccText.Location = New System.Drawing.Point(110, 21)
        Me.AccText.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.AccText.Name = "AccText"
        Me.AccText.ReadOnly = True
        Me.AccText.Size = New System.Drawing.Size(128, 20)
        Me.AccText.TabIndex = 9
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(16, 111)
        Me.Label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(85, 13)
        Me.Label8.TabIndex = 11
        Me.Label8.Text = "Starting Balance"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(16, 81)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(66, 13)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "Pan Number"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(16, 53)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(81, 13)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Adhaar Number"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 26)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(87, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Account Number"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.RePinText)
        Me.GroupBox3.Controls.Add(Me.PinText)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Location = New System.Drawing.Point(17, 175)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox3.Size = New System.Drawing.Size(232, 75)
        Me.GroupBox3.TabIndex = 11
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Set Pin"
        '
        'RePinText
        '
        Me.RePinText.Location = New System.Drawing.Point(80, 50)
        Me.RePinText.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.RePinText.Name = "RePinText"
        Me.RePinText.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.RePinText.Size = New System.Drawing.Size(128, 20)
        Me.RePinText.TabIndex = 15
        '
        'PinText
        '
        Me.PinText.Location = New System.Drawing.Point(80, 24)
        Me.PinText.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.PinText.Name = "PinText"
        Me.PinText.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.PinText.Size = New System.Drawing.Size(128, 20)
        Me.PinText.TabIndex = 15
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(8, 50)
        Me.Label10.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(66, 13)
        Me.Label10.TabIndex = 15
        Me.Label10.Text = "Re-enter Pin"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(8, 29)
        Me.Label9.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(50, 13)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "Enter Pin"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(268, 226)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(143, 28)
        Me.Button1.TabIndex = 12
        Me.Button1.Text = "Create Account"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(423, 185)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 13
        Me.Button2.Text = "Button2"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'FingerBasedATM__form_main1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(522, 293)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnVerify)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "FingerBasedATM__form_main1"
        Me.Text = "FingerBasedATM__form_main1"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnVerify As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents FnameText As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents LnameText As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents PhoneText As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents MailText As TextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents BalText As TextBox
    Friend WithEvents PanText As TextBox
    Friend WithEvents AdhaarText As TextBox
    Friend WithEvents AccText As TextBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents RePinText As TextBox
    Friend WithEvents PinText As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents ErrorProvider1 As ErrorProvider
    Friend WithEvents Button2 As Button
End Class
