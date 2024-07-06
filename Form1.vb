Imports System.IO
Imports System.Numerics

Public Class Form1
    'Kya Beaudry
    'CS10 Final - Directors Cut
    'Version 1.0.0
    'Started June 26 2024

#Region "Core functionality"

    'Object Transforms
    Public sngTranslationX As Single = 0
    Public sngTranslationY As Single = 0
    Public sngTranslationZ As Single = 0
    Public sngRotationY As Single = 0
    Public sngRotationX As Single = 0
    Public sngRotationZ As Single = 0
    Public sngScaleX As Single = 1
    Public sngScaleY As Single = 1
    Public sngScaleZ As Single = 1
    'Camera Transforms
    Public sngCameraTranslationX As Single = 0
    Public sngCameraTranslationY As Single = 0
    Public sngCameraTranslationZ As Single = -3
    Public sngCameraRotationY As Single = 0
    Public sngCameraRotationX As Single = 0
    Public sngCameraRotationZ As Single = 0
    'Behavior Settings
    Public sngFocalLength As Single = 500
    Public sngFramerate As Single = 24
    Public blnForceBackCulling As Boolean = False
    'shading settings
    Public sngLightX As Single = 1
    Public sngLightY As Single = 1
    Public sngLightZ As Single = 1

    Structure Scene
        Dim strName As String
        Dim strDescription As String
        Dim vec4Points() As Vector4 'oooo i like this :) (vec4 bc quaternions)
        'not storing transformed points, will just use one i make at compute time, more memory efficient
        Dim vec2Edges() As Vector2
        Dim vec3Tris() As Vector3
        Dim blnBackCulled As Boolean
    End Structure

    Public stcSceneArray() As Scene = {
        New Scene With {
            .strName = "Cube",
            .strDescription = "Simple cube model. Important to know, the number of edges and the number of tris are for two different views. 8 points, 12 edges, 12 tris.",
            .vec4Points = {
                New Vector4(-0.5, -0.5, -0.5, 0),
                New Vector4(-0.5, -0.5, 0.5, 0),
                New Vector4(-0.5, 0.5, -0.5, 0),
                New Vector4(-0.5, 0.5, 0.5, 0),
                New Vector4(0.5, -0.5, -0.5, 0),
                New Vector4(0.5, -0.5, 0.5, 0),
                New Vector4(0.5, 0.5, -0.5, 0),
                New Vector4(0.5, 0.5, 0.5, 0)
            }, 'Vector4 because of rotations (I LOVE QUATERNIONS!!!!!!!!!)
            .vec2Edges = {
                New Vector2(0, 1),
                New Vector2(0, 2),
                New Vector2(1, 3),
                New Vector2(2, 3),
                New Vector2(4, 5),
                New Vector2(4, 6),
                New Vector2(5, 7),
                New Vector2(6, 7),
                New Vector2(0, 4),
                New Vector2(1, 5),
                New Vector2(2, 6),
                New Vector2(3, 7)
            },
            .vec3Tris = { 'points must be declared in a clockwise order, will use to find the tri's normal direction
                New Vector3(0, 2, 1),
                New Vector3(1, 2, 3),
                New Vector3(4, 5, 6),
                New Vector3(5, 7, 6),
                New Vector3(0, 1, 4),
                New Vector3(1, 5, 4),
                New Vector3(2, 6, 3),
                New Vector3(3, 6, 7),
                New Vector3(2, 0, 4),
                New Vector3(2, 4, 6),
                New Vector3(1, 3, 7),
                New Vector3(3, 7, 5)
            },
            .blnBackCulled = True
        },
        New Scene With {
            .strName = "Tri",
            .strDescription = "Single triangle. used to test back culling. 3 pints, 3 edges, 1 tri.",
            .vec4Points = {
                New Vector4(0, -0.5, 0, 0),
                New Vector4(0.5, 0.5, 0, 0),
                New Vector4(-0.5, 0.5, 0, 0)
            },
            .vec2Edges = {
                New Vector2(0, 1),
                New Vector2(1, 2),
                New Vector2(2, 0)
            },
            .vec3Tris = {
                New Vector3(0, 1, 2)
            },
            .blnBackCulled = False
        }
    }
    Public intCurrentSceneIndex = 0
    Public blnUpdate As Boolean = True

    Public dblDeltaTime As Double = 0

    Public WithEvents frmViewport As Viewport
    Public WithEvents frmControls As Control

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        frmViewport = New Viewport
        frmViewport.Show(Me)

    End Sub

    Private Sub tmrUpdate_Tick(sender As Object, e As EventArgs) Handles tmrUpdate.Tick

        Static dtmLastTime As DateTime = DateTime.UtcNow
        Dim spnTimeTaken As TimeSpan

        Me.tmrUpdate.Interval = 1000 \ sngFramerate
        spnTimeTaken = DateTime.UtcNow - dtmLastTime
        dblDeltaTime = spnTimeTaken.TotalSeconds
        dtmLastTime = DateTime.UtcNow

        If blnUpdate Then
            If frmControls IsNot Nothing Then
                frmControls.UpdateControls(Me, intControlState, dblDeltaTime)
            End If

            frmViewport.UpdateFrame(Me, dblDeltaTime, intControlState, intViewState, stcSceneArray(intCurrentSceneIndex))
        Else
            frmViewport.ShowPaused()
        End If
    End Sub

#End Region

#Region "Closing logic"

    Dim blnClosed As Boolean = False

    Private Sub frmViewport_Closed(sender As Object, e As EventArgs) Handles frmViewport.Closed

        If Not blnClosed Then
            Me.Close()
        End If

    End Sub

    Private Sub Form1_Closing(sender As Object, e As EventArgs) Handles Me.Closing

        blnClosed = True

    End Sub

    Private Sub frmControls_Closing(sender As Object, e As EventArgs) Handles frmControls.Closing

        frmControls = Nothing

    End Sub

#End Region

#Region "Menu items"

    Private Sub mnuExit_Click(sender As Object, e As EventArgs) Handles mnuExit.Click

        End

    End Sub

    Private Sub mnuLoadScene_Click(sender As Object, e As EventArgs) Handles mnuLoadScene.Click

        Dim okCancel As DialogResult
        Dim fStream As FileStream
        Dim textFile As StreamReader

        okCancel = opnFileDialog.ShowDialog()
        If okCancel = DialogResult.OK Then
            fStream = opnFileDialog.OpenFile()
            textFile = New StreamReader(fStream)
            LoadScene(textFile)
            textFile.Close()
            fStream.Close()
        End If

    End Sub

    Private Sub mnuSaveScene_Click(sender As Object, e As EventArgs) Handles mnuSaveScene.Click

        MessageBox.Show("Not Yet Implemented")

    End Sub

#End Region

#Region "Buttons"

    Public intControlState As Integer = 0
    Public intViewState As Integer = 0

    'some buttons use the MouseDown events, this lets them process right clicks

    Private Sub btnChangeControlMode_MouseDown(sender As Object, e As MouseEventArgs) Handles btnChangeControlMode.MouseDown

        If e.Button = MouseButtons.Left Then
            intControlState = (intControlState + 1) Mod 4
        ElseIf e.Button = MouseButtons.Right Then
            intControlState = (intControlState + 3) Mod 4 'uses +3 because if i used -1 it would be a negative number, which i dont want
        End If

        Select Case intControlState
            Case 0
                Me.btnChangeControlMode.Text = "Control Mode: None"
            Case 1
                Me.btnChangeControlMode.Text = "Control Mode: Object"
            Case 2
                Me.btnChangeControlMode.Text = "Control Mode: Camera"
            Case 3
                Me.btnChangeControlMode.Text = "Control Mode: Live Edit"
        End Select

    End Sub

    Private Sub btnChangeViewMode_MouseDown(sender As Object, e As MouseEventArgs) Handles btnChangeViewMode.MouseDown

        If e.Button = MouseButtons.Left Then
            intViewState = (intViewState + 1) Mod 5
        ElseIf e.Button = MouseButtons.Right Then
            intViewState = (intViewState + 4) Mod 5 'uses +4 because if i used -1 it would be a negative number, which i dont want
        End If

        Select Case intViewState
            Case 0
                Me.btnChangeViewMode.Text = "View Mode: Wireframe"
            Case 1
                Me.btnChangeViewMode.Text = "View Mode: Solid - Depth"
            Case 2
                Me.btnChangeViewMode.Text = "View Mode: Solid - Screenspace Normals"
            Case 3
                Me.btnChangeViewMode.Text = "View Mode: Solid - Worldspace Normals"
            Case 4
                Me.btnChangeViewMode.Text = "View Mode: Solid - Directional Lighting"
        End Select

    End Sub

    Private Sub btnOpenControls_Click(sender As Object, e As EventArgs) Handles btnOpenControls.Click

        If frmControls IsNot Nothing Then
            frmControls.Close()
        End If

        frmControls = New Control

        frmControls.frmForm1 = Me

        frmControls.txtScaleX.Text = sngScaleX
        frmControls.txtScaleY.Text = sngScaleY
        frmControls.txtScaleZ.Text = sngScaleZ

        frmControls.txtRotationX.Text = sngRotationX
        frmControls.txtRotationY.Text = sngRotationY
        frmControls.txtRotationZ.Text = sngRotationZ

        frmControls.txtTranslationX.Text = sngTranslationX
        frmControls.txtTranslationY.Text = sngTranslationY
        frmControls.txtTranslationZ.Text = sngTranslationZ

        frmControls.txtCameraTranslationX.Text = sngCameraTranslationX
        frmControls.txtCameraTranslationY.Text = sngCameraTranslationY
        frmControls.txtCameraTranslationZ.Text = sngCameraTranslationZ

        frmControls.txtCameraRotationX.Text = sngCameraRotationX
        frmControls.txtCameraRotationY.Text = sngCameraRotationY
        frmControls.txtCameraRotationZ.Text = sngCameraRotationZ

        frmControls.txtFocalLength.Text = sngFocalLength
        frmControls.txtFramerate.Text = sngFramerate

        frmControls.blnCheckBoxes(Control.Checkboxes.ForceBackCulling) = blnForceBackCulling
        frmControls.chkForceBackCulling.Checked = blnForceBackCulling

        frmControls.txtLightX.Text = sngLightX
        frmControls.txtLightY.Text = sngLightY
        frmControls.txtLightZ.Text = sngLightZ

        frmControls.Show(Me)

    End Sub

#End Region

#Region "File I/O"

    '
    'NOT YET IMPLEMENTED
    '
    '
    Private Sub LoadScene(ByRef File As StreamReader)

    End Sub



#End Region

End Class
