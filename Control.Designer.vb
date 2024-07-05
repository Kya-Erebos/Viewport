<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Control
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
        Me.grpObjectSettings = New System.Windows.Forms.GroupBox()
        Me.chkRotateZ = New System.Windows.Forms.CheckBox()
        Me.txtScaleZ = New System.Windows.Forms.TextBox()
        Me.chkRotateY = New System.Windows.Forms.CheckBox()
        Me.txtScaleY = New System.Windows.Forms.TextBox()
        Me.chkRotateX = New System.Windows.Forms.CheckBox()
        Me.txtScaleX = New System.Windows.Forms.TextBox()
        Me.txtRotationZ = New System.Windows.Forms.TextBox()
        Me.txtRotationY = New System.Windows.Forms.TextBox()
        Me.txtRotationX = New System.Windows.Forms.TextBox()
        Me.txtTranslationZ = New System.Windows.Forms.TextBox()
        Me.txtTranslationY = New System.Windows.Forms.TextBox()
        Me.txtTranslationX = New System.Windows.Forms.TextBox()
        Me.lblScaleZ = New System.Windows.Forms.Label()
        Me.lblScaleY = New System.Windows.Forms.Label()
        Me.lblScaleX = New System.Windows.Forms.Label()
        Me.lblRotationZ = New System.Windows.Forms.Label()
        Me.lblRotationY = New System.Windows.Forms.Label()
        Me.lblRotationX = New System.Windows.Forms.Label()
        Me.lblTranslationZ = New System.Windows.Forms.Label()
        Me.lblTranslationY = New System.Windows.Forms.Label()
        Me.lblTranslationX = New System.Windows.Forms.Label()
        Me.grpCameraSettings = New System.Windows.Forms.GroupBox()
        Me.txtCameraRotationZ = New System.Windows.Forms.TextBox()
        Me.lblCameraRotationZ = New System.Windows.Forms.Label()
        Me.txtCameraRotationY = New System.Windows.Forms.TextBox()
        Me.lblCameraTranslationX = New System.Windows.Forms.Label()
        Me.txtCameraRotationX = New System.Windows.Forms.TextBox()
        Me.lblCameraRotationY = New System.Windows.Forms.Label()
        Me.txtCameraTranslationZ = New System.Windows.Forms.TextBox()
        Me.lblCameraTranslationY = New System.Windows.Forms.Label()
        Me.txtCameraTranslationY = New System.Windows.Forms.TextBox()
        Me.lblCameraRotationX = New System.Windows.Forms.Label()
        Me.txtCameraTranslationX = New System.Windows.Forms.TextBox()
        Me.lblCameraTranslationZ = New System.Windows.Forms.Label()
        Me.txtFocalLength = New System.Windows.Forms.TextBox()
        Me.lblFocalLength = New System.Windows.Forms.Label()
        Me.grpBehaviorSettings = New System.Windows.Forms.GroupBox()
        Me.txtFramerate = New System.Windows.Forms.TextBox()
        Me.lblFramerate = New System.Windows.Forms.Label()
        Me.chkForceBackCulling = New System.Windows.Forms.CheckBox()
        Me.btnPreviousScene = New System.Windows.Forms.Button()
        Me.btnPause = New System.Windows.Forms.Button()
        Me.btnPlay = New System.Windows.Forms.Button()
        Me.btnNextScene = New System.Windows.Forms.Button()
        Me.grpShadingSettings = New System.Windows.Forms.GroupBox()
        Me.txtLightZ = New System.Windows.Forms.TextBox()
        Me.lblLightZ = New System.Windows.Forms.Label()
        Me.txtLightY = New System.Windows.Forms.TextBox()
        Me.txtLightX = New System.Windows.Forms.TextBox()
        Me.lblLightY = New System.Windows.Forms.Label()
        Me.lblLightX = New System.Windows.Forms.Label()
        Me.grpObjectSettings.SuspendLayout()
        Me.grpCameraSettings.SuspendLayout()
        Me.grpBehaviorSettings.SuspendLayout()
        Me.grpShadingSettings.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpObjectSettings
        '
        Me.grpObjectSettings.Controls.Add(Me.chkRotateZ)
        Me.grpObjectSettings.Controls.Add(Me.txtScaleZ)
        Me.grpObjectSettings.Controls.Add(Me.chkRotateY)
        Me.grpObjectSettings.Controls.Add(Me.txtScaleY)
        Me.grpObjectSettings.Controls.Add(Me.chkRotateX)
        Me.grpObjectSettings.Controls.Add(Me.txtScaleX)
        Me.grpObjectSettings.Controls.Add(Me.txtRotationZ)
        Me.grpObjectSettings.Controls.Add(Me.txtRotationY)
        Me.grpObjectSettings.Controls.Add(Me.txtRotationX)
        Me.grpObjectSettings.Controls.Add(Me.txtTranslationZ)
        Me.grpObjectSettings.Controls.Add(Me.txtTranslationY)
        Me.grpObjectSettings.Controls.Add(Me.txtTranslationX)
        Me.grpObjectSettings.Controls.Add(Me.lblScaleZ)
        Me.grpObjectSettings.Controls.Add(Me.lblScaleY)
        Me.grpObjectSettings.Controls.Add(Me.lblScaleX)
        Me.grpObjectSettings.Controls.Add(Me.lblRotationZ)
        Me.grpObjectSettings.Controls.Add(Me.lblRotationY)
        Me.grpObjectSettings.Controls.Add(Me.lblRotationX)
        Me.grpObjectSettings.Controls.Add(Me.lblTranslationZ)
        Me.grpObjectSettings.Controls.Add(Me.lblTranslationY)
        Me.grpObjectSettings.Controls.Add(Me.lblTranslationX)
        Me.grpObjectSettings.Location = New System.Drawing.Point(12, 12)
        Me.grpObjectSettings.Name = "grpObjectSettings"
        Me.grpObjectSettings.Size = New System.Drawing.Size(188, 358)
        Me.grpObjectSettings.TabIndex = 0
        Me.grpObjectSettings.TabStop = False
        Me.grpObjectSettings.Text = "Object Settings"
        '
        'chkRotateZ
        '
        Me.chkRotateZ.AutoSize = True
        Me.chkRotateZ.Location = New System.Drawing.Point(9, 309)
        Me.chkRotateZ.Name = "chkRotateZ"
        Me.chkRotateZ.Size = New System.Drawing.Size(68, 17)
        Me.chkRotateZ.TabIndex = 30
        Me.chkRotateZ.Tag = "3"
        Me.chkRotateZ.Text = "Rotate Z"
        Me.chkRotateZ.UseVisualStyleBackColor = True
        '
        'txtScaleZ
        '
        Me.txtScaleZ.Location = New System.Drawing.Point(89, 216)
        Me.txtScaleZ.Name = "txtScaleZ"
        Me.txtScaleZ.Size = New System.Drawing.Size(93, 20)
        Me.txtScaleZ.TabIndex = 17
        '
        'chkRotateY
        '
        Me.chkRotateY.AutoSize = True
        Me.chkRotateY.Location = New System.Drawing.Point(9, 286)
        Me.chkRotateY.Name = "chkRotateY"
        Me.chkRotateY.Size = New System.Drawing.Size(68, 17)
        Me.chkRotateY.TabIndex = 29
        Me.chkRotateY.Tag = "2"
        Me.chkRotateY.Text = "Rotate Y"
        Me.chkRotateY.UseVisualStyleBackColor = True
        '
        'txtScaleY
        '
        Me.txtScaleY.Location = New System.Drawing.Point(89, 195)
        Me.txtScaleY.Name = "txtScaleY"
        Me.txtScaleY.Size = New System.Drawing.Size(93, 20)
        Me.txtScaleY.TabIndex = 16
        '
        'chkRotateX
        '
        Me.chkRotateX.AutoSize = True
        Me.chkRotateX.Location = New System.Drawing.Point(9, 263)
        Me.chkRotateX.Name = "chkRotateX"
        Me.chkRotateX.Size = New System.Drawing.Size(68, 17)
        Me.chkRotateX.TabIndex = 28
        Me.chkRotateX.Tag = "1"
        Me.chkRotateX.Text = "Rotate X"
        Me.chkRotateX.UseVisualStyleBackColor = True
        '
        'txtScaleX
        '
        Me.txtScaleX.Location = New System.Drawing.Point(89, 174)
        Me.txtScaleX.Name = "txtScaleX"
        Me.txtScaleX.Size = New System.Drawing.Size(93, 20)
        Me.txtScaleX.TabIndex = 15
        '
        'txtRotationZ
        '
        Me.txtRotationZ.Location = New System.Drawing.Point(89, 142)
        Me.txtRotationZ.Name = "txtRotationZ"
        Me.txtRotationZ.Size = New System.Drawing.Size(93, 20)
        Me.txtRotationZ.TabIndex = 14
        '
        'txtRotationY
        '
        Me.txtRotationY.Location = New System.Drawing.Point(89, 121)
        Me.txtRotationY.Name = "txtRotationY"
        Me.txtRotationY.Size = New System.Drawing.Size(93, 20)
        Me.txtRotationY.TabIndex = 13
        '
        'txtRotationX
        '
        Me.txtRotationX.Location = New System.Drawing.Point(89, 100)
        Me.txtRotationX.Name = "txtRotationX"
        Me.txtRotationX.Size = New System.Drawing.Size(93, 20)
        Me.txtRotationX.TabIndex = 12
        '
        'txtTranslationZ
        '
        Me.txtTranslationZ.Location = New System.Drawing.Point(89, 67)
        Me.txtTranslationZ.Name = "txtTranslationZ"
        Me.txtTranslationZ.Size = New System.Drawing.Size(93, 20)
        Me.txtTranslationZ.TabIndex = 11
        '
        'txtTranslationY
        '
        Me.txtTranslationY.Location = New System.Drawing.Point(89, 46)
        Me.txtTranslationY.Name = "txtTranslationY"
        Me.txtTranslationY.Size = New System.Drawing.Size(93, 20)
        Me.txtTranslationY.TabIndex = 10
        '
        'txtTranslationX
        '
        Me.txtTranslationX.Location = New System.Drawing.Point(89, 25)
        Me.txtTranslationX.Name = "txtTranslationX"
        Me.txtTranslationX.Size = New System.Drawing.Size(93, 20)
        Me.txtTranslationX.TabIndex = 9
        '
        'lblScaleZ
        '
        Me.lblScaleZ.Location = New System.Drawing.Point(6, 219)
        Me.lblScaleZ.Name = "lblScaleZ"
        Me.lblScaleZ.Size = New System.Drawing.Size(77, 13)
        Me.lblScaleZ.TabIndex = 8
        Me.lblScaleZ.Text = "Scale Z:"
        '
        'lblScaleY
        '
        Me.lblScaleY.Location = New System.Drawing.Point(6, 198)
        Me.lblScaleY.Name = "lblScaleY"
        Me.lblScaleY.Size = New System.Drawing.Size(77, 13)
        Me.lblScaleY.TabIndex = 7
        Me.lblScaleY.Text = "Scale Y:"
        '
        'lblScaleX
        '
        Me.lblScaleX.Location = New System.Drawing.Point(6, 177)
        Me.lblScaleX.Name = "lblScaleX"
        Me.lblScaleX.Size = New System.Drawing.Size(77, 13)
        Me.lblScaleX.TabIndex = 6
        Me.lblScaleX.Text = "Scale X:"
        '
        'lblRotationZ
        '
        Me.lblRotationZ.Location = New System.Drawing.Point(6, 145)
        Me.lblRotationZ.Name = "lblRotationZ"
        Me.lblRotationZ.Size = New System.Drawing.Size(77, 13)
        Me.lblRotationZ.TabIndex = 5
        Me.lblRotationZ.Text = "Rotation Z:"
        '
        'lblRotationY
        '
        Me.lblRotationY.Location = New System.Drawing.Point(6, 124)
        Me.lblRotationY.Name = "lblRotationY"
        Me.lblRotationY.Size = New System.Drawing.Size(77, 13)
        Me.lblRotationY.TabIndex = 4
        Me.lblRotationY.Text = "Rotation Y:"
        '
        'lblRotationX
        '
        Me.lblRotationX.Location = New System.Drawing.Point(6, 103)
        Me.lblRotationX.Name = "lblRotationX"
        Me.lblRotationX.Size = New System.Drawing.Size(77, 13)
        Me.lblRotationX.TabIndex = 3
        Me.lblRotationX.Text = "Rotation X:"
        '
        'lblTranslationZ
        '
        Me.lblTranslationZ.Location = New System.Drawing.Point(6, 70)
        Me.lblTranslationZ.Name = "lblTranslationZ"
        Me.lblTranslationZ.Size = New System.Drawing.Size(77, 13)
        Me.lblTranslationZ.TabIndex = 2
        Me.lblTranslationZ.Text = "Translation Z:"
        '
        'lblTranslationY
        '
        Me.lblTranslationY.Location = New System.Drawing.Point(6, 49)
        Me.lblTranslationY.Name = "lblTranslationY"
        Me.lblTranslationY.Size = New System.Drawing.Size(77, 13)
        Me.lblTranslationY.TabIndex = 1
        Me.lblTranslationY.Text = "Translation Y:"
        '
        'lblTranslationX
        '
        Me.lblTranslationX.Location = New System.Drawing.Point(6, 28)
        Me.lblTranslationX.Name = "lblTranslationX"
        Me.lblTranslationX.Size = New System.Drawing.Size(77, 13)
        Me.lblTranslationX.TabIndex = 0
        Me.lblTranslationX.Text = "Translation X:"
        '
        'grpCameraSettings
        '
        Me.grpCameraSettings.Controls.Add(Me.txtCameraRotationZ)
        Me.grpCameraSettings.Controls.Add(Me.lblCameraRotationZ)
        Me.grpCameraSettings.Controls.Add(Me.txtCameraRotationY)
        Me.grpCameraSettings.Controls.Add(Me.lblCameraTranslationX)
        Me.grpCameraSettings.Controls.Add(Me.txtCameraRotationX)
        Me.grpCameraSettings.Controls.Add(Me.lblCameraRotationY)
        Me.grpCameraSettings.Controls.Add(Me.txtCameraTranslationZ)
        Me.grpCameraSettings.Controls.Add(Me.lblCameraTranslationY)
        Me.grpCameraSettings.Controls.Add(Me.txtCameraTranslationY)
        Me.grpCameraSettings.Controls.Add(Me.lblCameraRotationX)
        Me.grpCameraSettings.Controls.Add(Me.txtCameraTranslationX)
        Me.grpCameraSettings.Controls.Add(Me.lblCameraTranslationZ)
        Me.grpCameraSettings.Location = New System.Drawing.Point(206, 12)
        Me.grpCameraSettings.Name = "grpCameraSettings"
        Me.grpCameraSettings.Size = New System.Drawing.Size(188, 169)
        Me.grpCameraSettings.TabIndex = 1
        Me.grpCameraSettings.TabStop = False
        Me.grpCameraSettings.Text = "Camera Settings"
        '
        'txtCameraRotationZ
        '
        Me.txtCameraRotationZ.Location = New System.Drawing.Point(89, 142)
        Me.txtCameraRotationZ.Name = "txtCameraRotationZ"
        Me.txtCameraRotationZ.Size = New System.Drawing.Size(93, 20)
        Me.txtCameraRotationZ.TabIndex = 23
        '
        'lblCameraRotationZ
        '
        Me.lblCameraRotationZ.Location = New System.Drawing.Point(6, 145)
        Me.lblCameraRotationZ.Name = "lblCameraRotationZ"
        Me.lblCameraRotationZ.Size = New System.Drawing.Size(77, 13)
        Me.lblCameraRotationZ.TabIndex = 14
        Me.lblCameraRotationZ.Text = "Rotation Z:"
        '
        'txtCameraRotationY
        '
        Me.txtCameraRotationY.Location = New System.Drawing.Point(89, 121)
        Me.txtCameraRotationY.Name = "txtCameraRotationY"
        Me.txtCameraRotationY.Size = New System.Drawing.Size(93, 20)
        Me.txtCameraRotationY.TabIndex = 22
        '
        'lblCameraTranslationX
        '
        Me.lblCameraTranslationX.Location = New System.Drawing.Point(6, 28)
        Me.lblCameraTranslationX.Name = "lblCameraTranslationX"
        Me.lblCameraTranslationX.Size = New System.Drawing.Size(77, 13)
        Me.lblCameraTranslationX.TabIndex = 9
        Me.lblCameraTranslationX.Text = "Translation X:"
        '
        'txtCameraRotationX
        '
        Me.txtCameraRotationX.Location = New System.Drawing.Point(89, 100)
        Me.txtCameraRotationX.Name = "txtCameraRotationX"
        Me.txtCameraRotationX.Size = New System.Drawing.Size(93, 20)
        Me.txtCameraRotationX.TabIndex = 21
        '
        'lblCameraRotationY
        '
        Me.lblCameraRotationY.Location = New System.Drawing.Point(6, 124)
        Me.lblCameraRotationY.Name = "lblCameraRotationY"
        Me.lblCameraRotationY.Size = New System.Drawing.Size(77, 13)
        Me.lblCameraRotationY.TabIndex = 13
        Me.lblCameraRotationY.Text = "Rotation Y:"
        '
        'txtCameraTranslationZ
        '
        Me.txtCameraTranslationZ.Location = New System.Drawing.Point(89, 67)
        Me.txtCameraTranslationZ.Name = "txtCameraTranslationZ"
        Me.txtCameraTranslationZ.Size = New System.Drawing.Size(93, 20)
        Me.txtCameraTranslationZ.TabIndex = 20
        '
        'lblCameraTranslationY
        '
        Me.lblCameraTranslationY.Location = New System.Drawing.Point(6, 49)
        Me.lblCameraTranslationY.Name = "lblCameraTranslationY"
        Me.lblCameraTranslationY.Size = New System.Drawing.Size(77, 13)
        Me.lblCameraTranslationY.TabIndex = 10
        Me.lblCameraTranslationY.Text = "Translation Y:"
        '
        'txtCameraTranslationY
        '
        Me.txtCameraTranslationY.Location = New System.Drawing.Point(89, 46)
        Me.txtCameraTranslationY.Name = "txtCameraTranslationY"
        Me.txtCameraTranslationY.Size = New System.Drawing.Size(93, 20)
        Me.txtCameraTranslationY.TabIndex = 19
        '
        'lblCameraRotationX
        '
        Me.lblCameraRotationX.Location = New System.Drawing.Point(6, 103)
        Me.lblCameraRotationX.Name = "lblCameraRotationX"
        Me.lblCameraRotationX.Size = New System.Drawing.Size(77, 13)
        Me.lblCameraRotationX.TabIndex = 12
        Me.lblCameraRotationX.Text = "Rotation X:"
        '
        'txtCameraTranslationX
        '
        Me.txtCameraTranslationX.Location = New System.Drawing.Point(89, 25)
        Me.txtCameraTranslationX.Name = "txtCameraTranslationX"
        Me.txtCameraTranslationX.Size = New System.Drawing.Size(93, 20)
        Me.txtCameraTranslationX.TabIndex = 18
        '
        'lblCameraTranslationZ
        '
        Me.lblCameraTranslationZ.Location = New System.Drawing.Point(6, 70)
        Me.lblCameraTranslationZ.Name = "lblCameraTranslationZ"
        Me.lblCameraTranslationZ.Size = New System.Drawing.Size(77, 13)
        Me.lblCameraTranslationZ.TabIndex = 11
        Me.lblCameraTranslationZ.Text = "Translation Z:"
        '
        'txtFocalLength
        '
        Me.txtFocalLength.Location = New System.Drawing.Point(89, 25)
        Me.txtFocalLength.Name = "txtFocalLength"
        Me.txtFocalLength.Size = New System.Drawing.Size(93, 20)
        Me.txtFocalLength.TabIndex = 24
        '
        'lblFocalLength
        '
        Me.lblFocalLength.Location = New System.Drawing.Point(6, 28)
        Me.lblFocalLength.Name = "lblFocalLength"
        Me.lblFocalLength.Size = New System.Drawing.Size(77, 13)
        Me.lblFocalLength.TabIndex = 9
        Me.lblFocalLength.Text = "Focal Length:"
        '
        'grpBehaviorSettings
        '
        Me.grpBehaviorSettings.Controls.Add(Me.txtFramerate)
        Me.grpBehaviorSettings.Controls.Add(Me.lblFramerate)
        Me.grpBehaviorSettings.Controls.Add(Me.txtFocalLength)
        Me.grpBehaviorSettings.Controls.Add(Me.lblFocalLength)
        Me.grpBehaviorSettings.Location = New System.Drawing.Point(400, 12)
        Me.grpBehaviorSettings.Name = "grpBehaviorSettings"
        Me.grpBehaviorSettings.Size = New System.Drawing.Size(188, 358)
        Me.grpBehaviorSettings.TabIndex = 1
        Me.grpBehaviorSettings.TabStop = False
        Me.grpBehaviorSettings.Text = "Behavior Settings"
        '
        'txtFramerate
        '
        Me.txtFramerate.Location = New System.Drawing.Point(89, 46)
        Me.txtFramerate.Name = "txtFramerate"
        Me.txtFramerate.Size = New System.Drawing.Size(93, 20)
        Me.txtFramerate.TabIndex = 26
        '
        'lblFramerate
        '
        Me.lblFramerate.Location = New System.Drawing.Point(6, 49)
        Me.lblFramerate.Name = "lblFramerate"
        Me.lblFramerate.Size = New System.Drawing.Size(77, 13)
        Me.lblFramerate.TabIndex = 25
        Me.lblFramerate.Text = "Framerate:"
        '
        'chkForceBackCulling
        '
        Me.chkForceBackCulling.AutoSize = True
        Me.chkForceBackCulling.Location = New System.Drawing.Point(9, 89)
        Me.chkForceBackCulling.Name = "chkForceBackCulling"
        Me.chkForceBackCulling.Size = New System.Drawing.Size(113, 17)
        Me.chkForceBackCulling.TabIndex = 27
        Me.chkForceBackCulling.Tag = "0"
        Me.chkForceBackCulling.Text = "Force back culling"
        Me.chkForceBackCulling.UseVisualStyleBackColor = True
        '
        'btnPreviousScene
        '
        Me.btnPreviousScene.Location = New System.Drawing.Point(12, 376)
        Me.btnPreviousScene.Name = "btnPreviousScene"
        Me.btnPreviousScene.Size = New System.Drawing.Size(137, 62)
        Me.btnPreviousScene.TabIndex = 2
        Me.btnPreviousScene.Tag = "Previous"
        Me.btnPreviousScene.Text = "Previous Scene"
        Me.btnPreviousScene.UseVisualStyleBackColor = True
        '
        'btnPause
        '
        Me.btnPause.Location = New System.Drawing.Point(206, 376)
        Me.btnPause.Name = "btnPause"
        Me.btnPause.Size = New System.Drawing.Size(89, 62)
        Me.btnPause.TabIndex = 3
        Me.btnPause.Tag = "Pause"
        Me.btnPause.Text = "Pause"
        Me.btnPause.UseVisualStyleBackColor = True
        '
        'btnPlay
        '
        Me.btnPlay.Location = New System.Drawing.Point(305, 376)
        Me.btnPlay.Name = "btnPlay"
        Me.btnPlay.Size = New System.Drawing.Size(89, 62)
        Me.btnPlay.TabIndex = 4
        Me.btnPlay.Tag = "Play"
        Me.btnPlay.Text = "Play"
        Me.btnPlay.UseVisualStyleBackColor = True
        '
        'btnNextScene
        '
        Me.btnNextScene.Location = New System.Drawing.Point(451, 376)
        Me.btnNextScene.Name = "btnNextScene"
        Me.btnNextScene.Size = New System.Drawing.Size(137, 62)
        Me.btnNextScene.TabIndex = 5
        Me.btnNextScene.Tag = "Next"
        Me.btnNextScene.Text = "Next Scene"
        Me.btnNextScene.UseVisualStyleBackColor = True
        '
        'grpShadingSettings
        '
        Me.grpShadingSettings.Controls.Add(Me.txtLightZ)
        Me.grpShadingSettings.Controls.Add(Me.lblLightZ)
        Me.grpShadingSettings.Controls.Add(Me.txtLightY)
        Me.grpShadingSettings.Controls.Add(Me.chkForceBackCulling)
        Me.grpShadingSettings.Controls.Add(Me.txtLightX)
        Me.grpShadingSettings.Controls.Add(Me.lblLightY)
        Me.grpShadingSettings.Controls.Add(Me.lblLightX)
        Me.grpShadingSettings.Location = New System.Drawing.Point(206, 186)
        Me.grpShadingSettings.Name = "grpShadingSettings"
        Me.grpShadingSettings.Size = New System.Drawing.Size(188, 184)
        Me.grpShadingSettings.TabIndex = 24
        Me.grpShadingSettings.TabStop = False
        Me.grpShadingSettings.Text = "Shading Settings"
        '
        'txtLightZ
        '
        Me.txtLightZ.Location = New System.Drawing.Point(89, 63)
        Me.txtLightZ.Name = "txtLightZ"
        Me.txtLightZ.Size = New System.Drawing.Size(93, 20)
        Me.txtLightZ.TabIndex = 23
        '
        'lblLightZ
        '
        Me.lblLightZ.Location = New System.Drawing.Point(6, 66)
        Me.lblLightZ.Name = "lblLightZ"
        Me.lblLightZ.Size = New System.Drawing.Size(77, 13)
        Me.lblLightZ.TabIndex = 14
        Me.lblLightZ.Text = "Component Z:"
        '
        'txtLightY
        '
        Me.txtLightY.Location = New System.Drawing.Point(89, 42)
        Me.txtLightY.Name = "txtLightY"
        Me.txtLightY.Size = New System.Drawing.Size(93, 20)
        Me.txtLightY.TabIndex = 22
        '
        'txtLightX
        '
        Me.txtLightX.Location = New System.Drawing.Point(89, 21)
        Me.txtLightX.Name = "txtLightX"
        Me.txtLightX.Size = New System.Drawing.Size(93, 20)
        Me.txtLightX.TabIndex = 21
        '
        'lblLightY
        '
        Me.lblLightY.Location = New System.Drawing.Point(6, 45)
        Me.lblLightY.Name = "lblLightY"
        Me.lblLightY.Size = New System.Drawing.Size(77, 13)
        Me.lblLightY.TabIndex = 13
        Me.lblLightY.Text = "Component Y:"
        '
        'lblLightX
        '
        Me.lblLightX.Location = New System.Drawing.Point(6, 24)
        Me.lblLightX.Name = "lblLightX"
        Me.lblLightX.Size = New System.Drawing.Size(77, 13)
        Me.lblLightX.TabIndex = 12
        Me.lblLightX.Text = "Component X:"
        '
        'Control
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(605, 450)
        Me.Controls.Add(Me.grpShadingSettings)
        Me.Controls.Add(Me.btnNextScene)
        Me.Controls.Add(Me.btnPlay)
        Me.Controls.Add(Me.btnPause)
        Me.Controls.Add(Me.btnPreviousScene)
        Me.Controls.Add(Me.grpBehaviorSettings)
        Me.Controls.Add(Me.grpCameraSettings)
        Me.Controls.Add(Me.grpObjectSettings)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "Control"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "Viewport Control"
        Me.grpObjectSettings.ResumeLayout(False)
        Me.grpObjectSettings.PerformLayout()
        Me.grpCameraSettings.ResumeLayout(False)
        Me.grpCameraSettings.PerformLayout()
        Me.grpBehaviorSettings.ResumeLayout(False)
        Me.grpBehaviorSettings.PerformLayout()
        Me.grpShadingSettings.ResumeLayout(False)
        Me.grpShadingSettings.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents grpObjectSettings As GroupBox
    Friend WithEvents grpCameraSettings As GroupBox
    Friend WithEvents grpBehaviorSettings As GroupBox
    Friend WithEvents lblScaleZ As Label
    Friend WithEvents lblScaleY As Label
    Friend WithEvents lblScaleX As Label
    Friend WithEvents lblRotationZ As Label
    Friend WithEvents lblRotationY As Label
    Friend WithEvents lblRotationX As Label
    Friend WithEvents lblTranslationZ As Label
    Friend WithEvents lblTranslationY As Label
    Friend WithEvents lblTranslationX As Label
    Friend WithEvents lblFocalLength As Label
    Friend WithEvents lblCameraRotationZ As Label
    Friend WithEvents lblCameraTranslationX As Label
    Friend WithEvents lblCameraRotationY As Label
    Friend WithEvents lblCameraTranslationY As Label
    Friend WithEvents lblCameraRotationX As Label
    Friend WithEvents lblCameraTranslationZ As Label
    Friend WithEvents txtScaleZ As TextBox
    Friend WithEvents txtScaleY As TextBox
    Friend WithEvents txtScaleX As TextBox
    Friend WithEvents txtRotationZ As TextBox
    Friend WithEvents txtRotationY As TextBox
    Friend WithEvents txtRotationX As TextBox
    Friend WithEvents txtTranslationZ As TextBox
    Friend WithEvents txtTranslationY As TextBox
    Friend WithEvents txtTranslationX As TextBox
    Friend WithEvents txtFocalLength As TextBox
    Friend WithEvents txtCameraRotationZ As TextBox
    Friend WithEvents txtCameraRotationY As TextBox
    Friend WithEvents txtCameraRotationX As TextBox
    Friend WithEvents txtCameraTranslationZ As TextBox
    Friend WithEvents txtCameraTranslationY As TextBox
    Friend WithEvents txtCameraTranslationX As TextBox
    Friend WithEvents btnPreviousScene As Button
    Friend WithEvents btnPause As Button
    Friend WithEvents btnPlay As Button
    Friend WithEvents btnNextScene As Button
    Friend WithEvents txtFramerate As TextBox
    Friend WithEvents lblFramerate As Label
    Friend WithEvents chkRotateZ As CheckBox
    Friend WithEvents chkRotateY As CheckBox
    Friend WithEvents chkRotateX As CheckBox
    Friend WithEvents chkForceBackCulling As CheckBox
    Friend WithEvents grpShadingSettings As GroupBox
    Friend WithEvents txtLightZ As TextBox
    Friend WithEvents lblLightZ As Label
    Friend WithEvents txtLightY As TextBox
    Friend WithEvents txtLightX As TextBox
    Friend WithEvents lblLightY As Label
    Friend WithEvents lblLightX As Label
End Class
