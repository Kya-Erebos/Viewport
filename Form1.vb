Imports System.Numerics
Imports vportLayers
Imports vportObjects

Public Class Form1
    'Kya Beaudry
    'CS10 Final - Directors Cut
    'Version 2.0.0
    'Started July 28 2024

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
    Public blnBackCulling As Boolean = True 'always loads on the cube
    'shading settings
    Public sngLightX As Single = 2
    Public sngLightY As Single = 3
    Public sngLightZ As Single = 1

    Public WithEvents frmViewport As Viewport
    Dim Layer As vportNaiveDrawSolidLayer
    Dim fauxCamera As vportEmpty
    Dim meshA As vportModel

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load

        frmViewport = New Viewport()
        frmViewport.Show()

    End Sub

    Private Sub tmrUpdate_Tick(sender As Object, e As EventArgs) Handles tmrUpdate.Tick

        frmViewport.main()

    End Sub

#End Region

#Region "Menu items"

    Private Sub mnuExit_Click(sender As Object, e As EventArgs) Handles mnuExit.Click

        End

    End Sub

#End Region

#Region "closing logic"

    Private Sub frmViewport_Closing() Handles frmViewport.Closing

        Me.Close()

    End Sub

#End Region

End Class
