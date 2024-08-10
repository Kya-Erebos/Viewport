Imports System.Numerics
Imports vportObjects
Imports vportLayers

Public Class Viewport

    Dim vplrTest As vportNaiveDrawSolidLayer
    Dim Camera As New vportCamera(New Vector3(0, 0, -3), New Vector3(1, 1, 1), Quaternion.CreateFromYawPitchRoll(0, 0, 0))
    Dim vportCube As New vportModel(vec3InitialScale:=New Vector3(1, 1, 1), quatInitialRotation:=Quaternion.CreateFromYawPitchRoll(0, 0, 0))
    Dim vportCube2 As New vportModel(vec3InitialTranslation:=New Vector3(1.5, 0, 0), vec3InitialScale:=New Vector3(1, 1, 1), quatInitialRotation:=Quaternion.CreateFromYawPitchRoll(0, 0, 0))
    Dim vportCube3 As New vportModel(vec3InitialTranslation:=New Vector3(-1.5, 0, 0), vec3InitialScale:=New Vector3(1, 1, 1), quatInitialRotation:=Quaternion.CreateFromYawPitchRoll(0, 0, 0))
    Dim vportGroundPlane As New vportModel(New Vector3(0, 2, 0), New Vector3(2, 1, 1), Quaternion.CreateFromYawPitchRoll(0, 0, 0))

    Private Sub Viewport_Load(sender As Object, e As EventArgs) Handles Me.Load

        vplrTest = New vportNaiveDrawSolidLayer(Me.picViewport, Color.Black)

        vplrTest.AddChildren(Camera)

        Camera.AddChildren({vportCube, vportCube2, vportCube3, vportGroundPlane})

        vportCube.Vertecies = {New Vector3(-0.5, -0.5, -0.5),
                               New Vector3(-0.5, -0.5, 0.5),
                               New Vector3(-0.5, 0.5, -0.5),
                               New Vector3(-0.5, 0.5, 0.5),
                               New Vector3(0.5, -0.5, -0.5),
                               New Vector3(0.5, -0.5, 0.5),
                               New Vector3(0.5, 0.5, -0.5),
                               New Vector3(0.5, 0.5, 0.5)
                              }.ToList()
        vportCube.vec3ReferenceTris = {New Vector3(0, 2, 1), 'L
                                       New Vector3(1, 2, 3), 'L
                                       New Vector3(4, 5, 6), 'R
                                       New Vector3(5, 7, 6), 'R
                                       New Vector3(0, 1, 4), 'U
                                       New Vector3(1, 5, 4), 'U
                                       New Vector3(3, 2, 6), 'D
                                       New Vector3(3, 6, 7), 'D
                                       New Vector3(2, 0, 4), 'F
                                       New Vector3(2, 4, 6), 'F
                                       New Vector3(3, 5, 1), 'B
                                       New Vector3(3, 7, 5)  'B
                                      }.ToList()
        vportCube.blnIsBackCulled = True

        vportCube2.Vertecies = {New Vector3(-0.5, -0.5, -0.5),
                               New Vector3(-0.5, -0.5, 0.5),
                               New Vector3(-0.5, 0.5, -0.5),
                               New Vector3(-0.5, 0.5, 0.5),
                               New Vector3(0.5, -0.5, -0.5),
                               New Vector3(0.5, -0.5, 0.5),
                               New Vector3(0.5, 0.5, -0.5),
                               New Vector3(0.5, 0.5, 0.5)
                              }.ToList()
        vportCube2.vec3ReferenceTris = {New Vector3(0, 2, 1), 'L
                                       New Vector3(1, 2, 3), 'L
                                       New Vector3(4, 5, 6), 'R
                                       New Vector3(5, 7, 6), 'R
                                       New Vector3(0, 1, 4), 'U
                                       New Vector3(1, 5, 4), 'U
                                       New Vector3(3, 2, 6), 'D
                                       New Vector3(3, 6, 7), 'D
                                       New Vector3(2, 0, 4), 'F
                                       New Vector3(2, 4, 6), 'F
                                       New Vector3(3, 5, 1), 'B
                                       New Vector3(3, 7, 5)  'B
                                      }.ToList()
        vportCube2.blnIsBackCulled = True

        vportCube3.Vertecies = {New Vector3(-0.5, -0.5, -0.5),
                               New Vector3(-0.5, -0.5, 0.5),
                               New Vector3(-0.5, 0.5, -0.5),
                               New Vector3(-0.5, 0.5, 0.5),
                               New Vector3(0.5, -0.5, -0.5),
                               New Vector3(0.5, -0.5, 0.5),
                               New Vector3(0.5, 0.5, -0.5),
                               New Vector3(0.5, 0.5, 0.5)
                              }.ToList()
        vportCube3.vec3ReferenceTris = {New Vector3(2, 0, 1), 'L
                                        New Vector3(2, 1, 3), 'L
                                        New Vector3(5, 4, 6), 'R
                                        New Vector3(7, 5, 6), 'R
                                        New Vector3(1, 0, 4), 'U
                                        New Vector3(5, 1, 4), 'U
                                        New Vector3(2, 3, 6), 'D
                                        New Vector3(6, 3, 7), 'D
                                        New Vector3(0, 2, 4), 'F
                                        New Vector3(4, 2, 6), 'F
                                        New Vector3(5, 3, 1), 'B
                                        New Vector3(7, 3, 5)  'B
                                       }.ToList()
        vportCube3.blnIsBackCulled = True

        vportGroundPlane.Vertecies = {New Vector3(-1.5, 0, 1.5),
                                      New Vector3(1.5, 0, 1.5),
                                      New Vector3(-1.5, 0, -1.5),
                                      New Vector3(1.5, 0, -1.5)}.ToList()
        vportGroundPlane.vec3ReferenceTris = {New Vector3(0, 1, 3), New Vector3(0, 3, 2)}.ToList()
        vportGroundPlane.blnIsBackCulled = False

        actionTest = New Action(AddressOf vplrTest.Buffer)

        vplrTest.camera = Camera

    End Sub

    Dim actionTest As Action
    Dim tskTest As Task

    Sub main()

        HandleMouseInput()
        handleKeyboardInput()

        vportCube.Rotate(Quaternion.CreateFromYawPitchRoll(0.061, 0.047, 0.111))

        tskTest = Task.Run(actionTest)

        While (Not tskTest.IsCompleted)

        End While

        vplrTest.DrawFrame()

    End Sub

#Region "Input Handling"

#Region "Keyboard"

    'Key into array using enum, used for readability
    Enum Keys
        W
        A
        S
        D
        Q
        E
        T
        G
        F
        H
        R
        Y
    End Enum
    Dim blnKeys(11) As Boolean

    Private Sub Viewport_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        Select Case e.KeyCode
            Case Windows.Forms.Keys.W
                blnKeys(Keys.W) = True
            Case Windows.Forms.Keys.A
                blnKeys(Keys.A) = True
            Case Windows.Forms.Keys.S
                blnKeys(Keys.S) = True
            Case Windows.Forms.Keys.D
                blnKeys(Keys.D) = True
            Case Windows.Forms.Keys.Q
                blnKeys(Keys.Q) = True
            Case Windows.Forms.Keys.E
                blnKeys(Keys.E) = True
            Case Windows.Forms.Keys.T
                blnKeys(Keys.T) = True
            Case Windows.Forms.Keys.G
                blnKeys(Keys.G) = True
            Case Windows.Forms.Keys.F
                blnKeys(Keys.F) = True
            Case Windows.Forms.Keys.H
                blnKeys(Keys.H) = True
            Case Windows.Forms.Keys.R
                blnKeys(Keys.R) = True
            Case Windows.Forms.Keys.Y
                blnKeys(Keys.Y) = True
        End Select

    End Sub

    Private Sub Viewport_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp

        Select Case e.KeyCode
            Case Windows.Forms.Keys.W
                blnKeys(Keys.W) = False
            Case Windows.Forms.Keys.A
                blnKeys(Keys.A) = False
            Case Windows.Forms.Keys.S
                blnKeys(Keys.S) = False
            Case Windows.Forms.Keys.D
                blnKeys(Keys.D) = False
            Case Windows.Forms.Keys.Q
                blnKeys(Keys.Q) = False
            Case Windows.Forms.Keys.E
                blnKeys(Keys.E) = False
            Case Windows.Forms.Keys.T
                blnKeys(Keys.T) = False
            Case Windows.Forms.Keys.G
                blnKeys(Keys.G) = False
            Case Windows.Forms.Keys.F
                blnKeys(Keys.F) = False
            Case Windows.Forms.Keys.H
                blnKeys(Keys.H) = False
            Case Windows.Forms.Keys.R
                blnKeys(Keys.R) = False
            Case Windows.Forms.Keys.Y
                blnKeys(Keys.Y) = False
        End Select

    End Sub

    Sub handleKeyboardInput()

        Dim m4x4Rotate As Matrix4x4
        Dim vec3movement As Vector3

        m4x4Rotate = New Matrix4x4(Math.Sin(Camera.degreeToRadian(Camera.sngRotationY)), 0, Math.Cos(Camera.degreeToRadian(Camera.sngRotationY)), 0,
                                   0, 1, 0, 0,
                                   Math.Cos(Camera.degreeToRadian(Camera.sngRotationY)), 0, -Math.Sin(Camera.degreeToRadian(Camera.sngRotationY)), 0,
                                   0, 0, 0, 1)

        vec3movement = New Vector3((blnKeys(Me.Keys.A) - blnKeys(Me.Keys.D)) * 0.2,
                                   (blnKeys(Me.Keys.E) - blnKeys(Me.Keys.Q)) * 0.2,
                                   (blnKeys(Me.Keys.W) - blnKeys(Me.Keys.S)) * 0.2)

        Camera.Translate(Vector3.Transform(vec3movement, m4x4Rotate))

    End Sub

#End Region

#Region "Mouse"

    Dim blnMouseHeld As Boolean

    Private Sub picViewport_MouseDown(sender As Object, e As MouseEventArgs) Handles picViewport.MouseDown

        blnMouseHeld = True
        Cursor.Position = picViewport.PointToScreen(New Point(picViewport.Width / 2, picViewport.Height / 2))
        Cursor.Hide()

    End Sub

    Private Sub picViewport_MouseUp(sender As Object, e As MouseEventArgs) Handles picViewport.MouseUp

        blnMouseHeld = False
        Cursor.Show()

    End Sub

    Sub HandleMouseInput()

        Dim pntDeltaMousePosition As Point

        If blnMouseHeld Then
            pntDeltaMousePosition = Cursor.Position - picViewport.PointToScreen(New Point(picViewport.Width / 2, picViewport.Height / 2))

            Camera.sngRotationX += 0.2 * pntDeltaMousePosition.Y
            Camera.sngRotationY += 0.2 * pntDeltaMousePosition.X

            Cursor.Position = picViewport.PointToScreen(New Point(picViewport.Width / 2, picViewport.Height / 2))
        End If

    End Sub

#End Region

#End Region

End Class