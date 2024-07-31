Public Class Control

    Public frmForm1 As Form1

    Sub UpdateControls(ByRef Sender As Form1, ByVal intControlState As Integer, ByVal dblDeltaTime As Double)

        Select Case intControlState
            Case 0
                'update ALL controls to textboxes
                Me.txtScaleX.Enabled = True
                Me.txtScaleY.Enabled = True
                Me.txtScaleZ.Enabled = True
                Me.txtRotationX.Enabled = True
                Me.txtRotationY.Enabled = True
                Me.txtRotationZ.Enabled = True
                Me.txtTranslationX.Enabled = True
                Me.txtTranslationY.Enabled = True
                Me.txtTranslationZ.Enabled = True
                Me.txtCameraRotationX.Enabled = True
                Me.txtCameraRotationY.Enabled = True
                Me.txtCameraRotationZ.Enabled = True
                Me.txtCameraTranslationX.Enabled = True
                Me.txtCameraTranslationY.Enabled = True
                Me.txtCameraTranslationZ.Enabled = True

                Sender.sngScaleX = Val(Me.txtScaleX.Text)
                Sender.sngScaleY = Val(Me.txtScaleY.Text)
                Sender.sngScaleZ = Val(Me.txtScaleZ.Text)

                Sender.sngRotationX = Val(Me.txtRotationX.Text)
                Sender.sngRotationY = Val(Me.txtRotationY.Text)
                Sender.sngRotationZ = Val(Me.txtRotationZ.Text)

                Sender.sngTranslationX = Val(Me.txtTranslationX.Text)
                Sender.sngTranslationY = Val(Me.txtTranslationY.Text)
                Sender.sngTranslationZ = Val(Me.txtTranslationZ.Text)

                Sender.sngCameraTranslationX = Val(Me.txtCameraTranslationX.Text)
                Sender.sngCameraTranslationY = Val(Me.txtCameraTranslationY.Text)
                Sender.sngCameraTranslationZ = Val(Me.txtCameraTranslationZ.Text)

                Sender.sngCameraRotationX = Val(Me.txtCameraRotationX.Text)
                Sender.sngCameraRotationY = Val(Me.txtCameraRotationY.Text)
                Sender.sngCameraRotationZ = Val(Me.txtCameraRotationZ.Text)
            Case 1
                'update object textboxes to values
                'update Camera values
                Me.txtScaleX.Enabled = False
                Me.txtScaleY.Enabled = False
                Me.txtScaleZ.Enabled = False
                Me.txtRotationX.Enabled = False
                Me.txtRotationY.Enabled = False
                Me.txtRotationZ.Enabled = False
                Me.txtTranslationX.Enabled = False
                Me.txtTranslationY.Enabled = False
                Me.txtTranslationZ.Enabled = False
                Me.txtCameraRotationX.Enabled = True
                Me.txtCameraRotationY.Enabled = True
                Me.txtCameraRotationZ.Enabled = True
                Me.txtCameraTranslationX.Enabled = True
                Me.txtCameraTranslationY.Enabled = True
                Me.txtCameraTranslationZ.Enabled = True


                Me.txtScaleX.Text = Sender.sngScaleX
                Me.txtScaleY.Text = Sender.sngScaleY
                Me.txtScaleZ.Text = Sender.sngScaleZ

                Me.txtRotationX.Text = Sender.sngRotationX
                Me.txtRotationY.Text = Sender.sngRotationY
                Me.txtRotationZ.Text = Sender.sngRotationZ

                Me.txtTranslationX.Text = Sender.sngTranslationX
                Me.txtTranslationY.Text = Sender.sngTranslationY
                Me.txtTranslationZ.Text = Sender.sngTranslationZ

                Sender.sngCameraTranslationX = Val(Me.txtCameraTranslationX.Text)
                Sender.sngCameraTranslationY = Val(Me.txtCameraTranslationY.Text)
                Sender.sngCameraTranslationZ = Val(Me.txtCameraTranslationZ.Text)

                Sender.sngCameraRotationX = Val(Me.txtCameraRotationX.Text)
                Sender.sngCameraRotationY = Val(Me.txtCameraRotationY.Text)
                Sender.sngCameraRotationZ = Val(Me.txtCameraRotationZ.Text)
            Case 2
                'update Camera textboxes to values
                'update object values
                Me.txtScaleX.Enabled = True
                Me.txtScaleY.Enabled = True
                Me.txtScaleZ.Enabled = True
                Me.txtRotationX.Enabled = True
                Me.txtRotationY.Enabled = True
                Me.txtRotationZ.Enabled = True
                Me.txtTranslationX.Enabled = True
                Me.txtTranslationY.Enabled = True
                Me.txtTranslationZ.Enabled = True
                Me.txtCameraRotationX.Enabled = False
                Me.txtCameraRotationY.Enabled = False
                Me.txtCameraRotationZ.Enabled = False
                Me.txtCameraTranslationX.Enabled = False
                Me.txtCameraTranslationY.Enabled = False
                Me.txtCameraTranslationZ.Enabled = False

                Sender.sngScaleX = Val(Me.txtScaleX.Text)
                Sender.sngScaleY = Val(Me.txtScaleY.Text)
                Sender.sngScaleZ = Val(Me.txtScaleZ.Text)

                Sender.sngRotationX = Val(Me.txtRotationX.Text)
                Sender.sngRotationY = Val(Me.txtRotationY.Text)
                Sender.sngRotationZ = Val(Me.txtRotationZ.Text)

                Sender.sngTranslationX = Val(Me.txtTranslationX.Text)
                Sender.sngTranslationY = Val(Me.txtTranslationY.Text)
                Sender.sngTranslationZ = Val(Me.txtTranslationZ.Text)

                Me.txtCameraTranslationX.Text = Sender.sngCameraTranslationX
                Me.txtCameraTranslationY.Text = Sender.sngCameraTranslationY
                Me.txtCameraTranslationZ.Text = Sender.sngCameraTranslationZ

                Me.txtCameraRotationX.Text = Sender.sngCameraRotationX
                Me.txtCameraRotationY.Text = Sender.sngCameraRotationY
                Me.txtCameraRotationZ.Text = Sender.sngCameraRotationZ
            Case 3
                'update camera values
                'object values are set to 0
                Me.txtScaleX.Enabled = False
                Me.txtScaleY.Enabled = False
                Me.txtScaleZ.Enabled = False
                Me.txtRotationX.Enabled = False
                Me.txtRotationY.Enabled = False
                Me.txtRotationZ.Enabled = False
                Me.txtTranslationX.Enabled = False
                Me.txtTranslationY.Enabled = False
                Me.txtTranslationZ.Enabled = False
                Me.txtCameraRotationX.Enabled = True
                Me.txtCameraRotationY.Enabled = True
                Me.txtCameraRotationZ.Enabled = True
                Me.txtCameraTranslationX.Enabled = True
                Me.txtCameraTranslationY.Enabled = True
                Me.txtCameraTranslationZ.Enabled = True


                Sender.sngScaleX = 1
                Sender.sngScaleY = 1
                Sender.sngScaleZ = 1

                Sender.sngRotationX = 0
                Sender.sngRotationY = 0
                Sender.sngRotationZ = 0

                Sender.sngTranslationX = 0
                Sender.sngTranslationY = 0
                Sender.sngTranslationZ = 0

                Sender.sngCameraTranslationX = Val(Me.txtCameraTranslationX.Text)
                Sender.sngCameraTranslationY = Val(Me.txtCameraTranslationY.Text)
                Sender.sngCameraTranslationZ = Val(Me.txtCameraTranslationZ.Text)

                Sender.sngCameraRotationX = Val(Me.txtCameraRotationX.Text)
                Sender.sngCameraRotationY = Val(Me.txtCameraRotationY.Text)
                Sender.sngCameraRotationZ = Val(Me.txtCameraRotationZ.Text)
        End Select

        Sender.sngFocalLength = Val(Me.txtFocalLength.Text)
        Sender.sngFramerate = Math.Min(Math.Max(1, Val(Me.txtFramerate.Text)), 1000)

        Sender.blnBackCulling = blnCheckBoxes(Checkboxes.BackCulling)

        If blnCheckBoxes(Checkboxes.RotateX) Then
            Sender.sngRotationX += 30 * dblDeltaTime
            Sender.sngRotationX = Sender.sngRotationX Mod 360
            Me.txtRotationX.Text = Sender.sngRotationX
        End If

        If blnCheckBoxes(Checkboxes.RotateY) Then
            Sender.sngRotationY += 30 * dblDeltaTime
            Sender.sngRotationY = Sender.sngRotationY Mod 360
            Me.txtRotationY.Text = Sender.sngRotationY
        End If

        If blnCheckBoxes(Checkboxes.RotateZ) Then
            Sender.sngRotationZ += 30 * dblDeltaTime
            Sender.sngRotationZ = Sender.sngRotationZ Mod 360
            Me.txtRotationZ.Text = Sender.sngRotationZ
        End If

        Sender.sngLightX = Val(Me.txtLightX.Text)
        Sender.sngLightY = Val(Me.txtLightY.Text)
        Sender.sngLightZ = Val(Me.txtLightZ.Text)

    End Sub

    Private Sub txtFramerate_Leave(sender As Object, e As EventArgs) Handles txtFramerate.Leave

        If Val(Me.txtFocalLength.Text) < 1 Then
            Me.txtFocalLength.Text = 1
        End If

    End Sub

    Enum Checkboxes
        BackCulling
        RotateX
        RotateY
        RotateZ
    End Enum

    Public blnCheckBoxes(4) As Boolean

    Private Sub chkCheckbox_Click(sender As Object, e As EventArgs) Handles chkForceBackCulling.Click, chkRotateX.Click, chkRotateY.Click, chkRotateZ.Click

        Dim chkCheckbox As CheckBox = sender

        blnCheckBoxes(Val(chkCheckbox.Tag)) = chkCheckbox.Checked

    End Sub

End Class