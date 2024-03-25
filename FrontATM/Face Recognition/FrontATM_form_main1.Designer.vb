<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrontATM_form_main1
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
        components = New ComponentModel.Container()
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(FrontATM_form_main1))
        Label2 = New Label()
        bankLabel = New Label()
        welcomeLabel = New Label()
        PictureBox1 = New PictureBox()
        Timer1 = New Timer(components)
        Timer2 = New Timer(components)
        Timer3 = New Timer(components)
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.BackColor = Color.Transparent
        Label2.Font = New Drawing.Font("Times New Roman", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point)
        Label2.ForeColor = Color.FromArgb(CByte(255), CByte(128), CByte(128))
        Label2.Location = New Point(349, 476)
        Label2.Name = "Label2"
        Label2.Size = New Size(379, 39)
        Label2.TabIndex = 9
        Label2.Text = "click anywhere to continue"
        ' 
        ' bankLabel
        ' 
        bankLabel.AutoSize = True
        bankLabel.BackColor = Color.Transparent
        bankLabel.Font = New Drawing.Font("Times New Roman", 48.0F, FontStyle.Bold, GraphicsUnit.Point)
        bankLabel.ForeColor = Color.FromArgb(CByte(57), CByte(66), CByte(42))
        bankLabel.Location = New Point(179, 340)
        bankLabel.Name = "bankLabel"
        bankLabel.Size = New Size(693, 90)
        bankLabel.TabIndex = 8
        bankLabel.Text = "STAR BANK ATM"
        ' 
        ' welcomeLabel
        ' 
        welcomeLabel.AutoSize = True
        welcomeLabel.BackColor = Color.Transparent
        welcomeLabel.Font = New Drawing.Font("Times New Roman", 48.0F, FontStyle.Bold, GraphicsUnit.Point)
        welcomeLabel.ForeColor = Color.Tan
        welcomeLabel.Location = New Point(237, 240)
        welcomeLabel.Name = "welcomeLabel"
        welcomeLabel.Size = New Size(606, 90)
        welcomeLabel.TabIndex = 7
        welcomeLabel.Text = "WELCOME TO"
        ' 
        ' PictureBox1
        ' 
        PictureBox1.BackColor = Color.Transparent
        PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), Image)
        PictureBox1.InitialImage = CType(resources.GetObject("PictureBox1.InitialImage"), Image)
        PictureBox1.Location = New Point(598, 14)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(415, 127)
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox1.TabIndex = 6
        PictureBox1.TabStop = False
        ' 
        ' Timer1
        ' 
        ' 
        ' Timer2
        ' 
        ' 
        ' Timer3
        ' 
        ' 
        ' FrontATM_form_main1
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), Image)
        ClientSize = New Size(1002, 578)
        Controls.Add(Label2)
        Controls.Add(bankLabel)
        Controls.Add(welcomeLabel)
        Controls.Add(PictureBox1)
        Name = "FrontATM_form_main1"
        Text = "FrontATM_form_main1"
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label2 As Label
    Friend WithEvents bankLabel As Label
    Friend WithEvents welcomeLabel As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Timer2 As Timer
    Friend WithEvents Timer3 As Timer
End Class
