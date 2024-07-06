Imports System.Numerics

Public Class Viewport

    Dim bmpFrameBuffer As Bitmap
    Dim grphFrameBuffer As Graphics
    Dim grphFrame As Graphics

    Dim vec4WorldTranslation As Vector4  ' .add, W is always 0
    Dim quatWorldRotation As Quaternion ' Vector4.Transform(vec, quat)
    Dim vec4WorldScale As Vector4 ' .multiply is component-wise, letting me use this

    Dim vec4CameraTranslation As Vector4  ' .add, W is always 0
    'will use the raw values to rotate the camera, quaternion's order is wrong in this use case.

    'technically W can be any value because im not using it anywhere but eh

    Dim vec3LightDirection As Vector3

    Dim blnDebug As Boolean 'enables/disables debug visuals

    Dim vec3OrderedTris() As Vector3

    Private Sub Viewport_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        bmpFrameBuffer = New Bitmap(Me.picViewport.Width, Me.picViewport.Height)
        grphFrameBuffer = Graphics.FromImage(bmpFrameBuffer)
        grphFrame = Me.picViewport.CreateGraphics()

    End Sub

    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    'Causes the current state to update and draws the frame to the screen
    '
    'post: Current scene is appropiately transformed and drawn to the screen
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Sub UpdateFrame(ByRef Sender As Form1, ByVal dblDeltaTime As Double, ByVal intControlMode As Integer, ByVal intViewMode As Integer, ByRef stcCurrentScene As Form1.Scene)

        Dim vec4TransformedPoints() As Vector4
        Dim vec4CameraTransformedPoints() As Vector4

        ReDim vec4TransformedPoints(stcCurrentScene.vec4Points.Length - 1)
        ReDim vec4CameraTransformedPoints(stcCurrentScene.vec4Points.Length - 1)
        Array.Copy(stcCurrentScene.vec4Points, vec4TransformedPoints, stcCurrentScene.vec4Points.Length)

        ReDim vec3OrderedTris(stcCurrentScene.vec3Tris.Length - 1)
        Array.Copy(stcCurrentScene.vec3Tris, vec3OrderedTris, stcCurrentScene.vec3Tris.Length)

        Select Case intControlMode
            Case 0
                'do nothing, values will be correct
                blnCameraControl = False
            Case 1
                'object key control
                blnCameraControl = False
                HandleObjectInput(Sender.sngTranslationX, Sender.sngTranslationY, Sender.sngTranslationZ,
                                  Sender.sngRotationX, Sender.sngRotationY, Sender.sngRotationZ,
                                  dblDeltaTime)
            Case 2
                'camera key control
                blnCameraControl = True
                HandleCameraInput(Sender.sngCameraTranslationX, Sender.sngCameraTranslationY, Sender.sngCameraTranslationZ,
                                  Sender.sngCameraRotationX, Sender.sngCameraRotationY, Sender.sngCameraRotationZ,
                                  dblDeltaTime)
                HandleMouseInput(Sender.sngCameraRotationX, Sender.sngCameraRotationY,
                                 dblDeltaTime)
            Case 3
                'Live Edit
                blnCameraControl = False
        End Select

        vec4WorldTranslation = New Vector4(Sender.sngTranslationX, Sender.sngTranslationY, Sender.sngTranslationZ, 0)
        quatWorldRotation = Quaternion.CreateFromYawPitchRoll(degreesToRadians(Sender.sngRotationY), degreesToRadians(Sender.sngRotationX), degreesToRadians(Sender.sngRotationZ))
        vec4WorldScale = New Vector4(Sender.sngScaleX, Sender.sngScaleY, Sender.sngScaleZ, 1)
        vec4CameraTranslation = New Vector4(-Sender.sngCameraTranslationX, -Sender.sngCameraTranslationY, -Sender.sngCameraTranslationZ, 0)
        vec3LightDirection = New Vector3(Sender.sngLightX, Sender.sngLightY, Sender.sngLightZ)


        'world space transform
        vec4TransformedPoints = ScaleScene(vec4TransformedPoints, vec4WorldScale)
        vec4TransformedPoints = RotateScene(vec4TransformedPoints, quatWorldRotation)
        vec4TransformedPoints = TranslateScene(vec4TransformedPoints, vec4WorldTranslation)

        'camera space transform
        Array.Copy(vec4TransformedPoints, vec4CameraTransformedPoints, vec4TransformedPoints.Length)
        vec4CameraTransformedPoints = TranslateScene(vec4CameraTransformedPoints, vec4CameraTranslation)
        vec4CameraTransformedPoints = CameraRotateScene(vec4CameraTransformedPoints, Sender.sngCameraRotationX, Sender.sngCameraRotationY, Sender.sngCameraRotationZ)

        'draw frame
        Select Case intViewMode
            Case 0
                DrawWireframe(vec4CameraTransformedPoints, stcCurrentScene, Sender.sngFocalLength, dblDeltaTime)
            Case Else
                DrawTris(vec4CameraTransformedPoints, stcCurrentScene, vec4TransformedPoints, Sender.sngFocalLength, dblDeltaTime, intViewMode, Sender.blnForceBackCulling)
        End Select

    End Sub

    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    'shows a pause icon in the top right of the screen
    '
    'post: pause icon is drawn in the top right of the screen
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Sub ShowPaused()

        'easiest part of the renderer so far:
        grphFrame.FillRectangle(Brushes.LimeGreen, Me.picViewport.Width - 40 + 0, 10, 10, 40)
        grphFrame.FillRectangle(Brushes.LimeGreen, Me.picViewport.Width - 40 + 20, 10, 10, 40)

    End Sub

#Region "Transforms"

    Function ScaleScene(ByVal vec4Points() As Vector4, ByVal vec4Scale As Vector4) As Vector4()

        Parallel.For(0, vec4Points.Length, Sub(ByVal intLoop As Integer)

                                               vec4Points(intLoop) = Vector4.Multiply(vec4Points(intLoop), vec4Scale)

                                           End Sub)

        Return vec4Points

    End Function

    Function RotateScene(ByVal vec4Points() As Vector4, ByVal quatRotation As Quaternion) As Vector4()

        Parallel.For(0, vec4Points.Length, Sub(ByVal intLoop As Integer)

                                               vec4Points(intLoop) = Vector4.Transform(vec4Points(intLoop), quatRotation)

                                           End Sub)

        Return vec4Points

    End Function

    Function TranslateScene(ByVal vec4Points() As Vector4, ByVal vec4Translation As Vector4) As Vector4()

        Parallel.For(0, vec4Points.Length, Sub(ByVal intLoop As Integer)

                                               vec4Points(intLoop) = Vector4.Add(vec4Points(intLoop), vec4Translation)

                                           End Sub)

        Return vec4Points

    End Function

    Function CameraRotateScene(ByVal vec4Points() As Vector4, ByVal sngCameraRotationX As Single, ByVal sngCameraRotationY As Single, ByVal sngCameraRotationZ As Single) As Vector4()

        Dim intLength As Integer
        Dim intLoop As Integer
        Dim vec3TempPoint As Vector3

        intLength = vec4Points.Length - 1

        For intLoop = 0 To intLength
            vec3TempPoint = New Vector3(vec4Points(intLoop).X, vec4Points(intLoop).Y, vec4Points(intLoop).Z)
            vec3TempPoint = CameraRotate(vec3TempPoint, sngCameraRotationX, sngCameraRotationY, sngCameraRotationZ)
            vec4Points(intLoop) = New Vector4(vec3TempPoint, 0)
        Next intLoop

        Return vec4Points

    End Function

    Function CameraRotate(ByVal vec3Point As Vector3, ByVal sngCameraRotationX As Single, ByVal sngCameraRotationY As Single, ByVal sngCameraRotationZ As Single) As Vector3

        Dim mtr4RotationMatrixX As Matrix4x4
        Dim mtr4RotationMatrixY As Matrix4x4
        Dim mtr4RotationMatrixZ As Matrix4x4

        mtr4RotationMatrixX = New Matrix4x4(1, 0, 0, 0,
                                            0, Math.Cos(degreesToRadians(sngCameraRotationX)), -Math.Sin(degreesToRadians(sngCameraRotationX)), 0,
                                            0, Math.Sin(degreesToRadians(sngCameraRotationX)), Math.Cos(degreesToRadians(sngCameraRotationX)), 0,
                                            0, 0, 0, 1)

        mtr4RotationMatrixY = New Matrix4x4(Math.Cos(degreesToRadians(sngCameraRotationY)), 0, Math.Sin(degreesToRadians(sngCameraRotationY)), 0,
                                            0, 1, 0, 0,
                                            -Math.Sin(degreesToRadians(sngCameraRotationY)), 0, Math.Cos(degreesToRadians(sngCameraRotationY)), 0,
                                            0, 0, 0, 1)

        mtr4RotationMatrixZ = New Matrix4x4(Math.Cos(degreesToRadians(sngCameraRotationZ)), -Math.Sin(degreesToRadians(sngCameraRotationZ)), 0, 0,
                                            Math.Sin(degreesToRadians(sngCameraRotationZ)), Math.Cos(degreesToRadians(sngCameraRotationZ)), 0, 0,
                                            0, 0, 1, 0,
                                            0, 0, 0, 1)

        vec3Point = Vector3.Transform(vec3Point, mtr4RotationMatrixY)
        vec3Point = Vector3.Transform(vec3Point, mtr4RotationMatrixX)
        vec3Point = Vector3.Transform(vec3Point, mtr4RotationMatrixZ)

        Return vec3Point

    End Function

#End Region

#Region "Rendering backend"
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    'Draws a scene to the screen
    '
    'post: scene is drawn to the screen using it's edge data, optionally drawing debug information as well
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Sub DrawWireframe(ByVal vec4Points() As Vector4, ByVal stcCurrentScene As Form1.Scene, ByVal sngFocalLength As Single, ByVal dblDeltaTime As Double)

        Dim pntPoints() As Point = {}
        Dim pntStart As Point
        Dim pntEnd As Point
        Dim intLength As Integer
        Dim intLoop As Integer

        grphFrameBuffer.Clear(Color.Black)

        intLength = stcCurrentScene.vec2Edges.Length - 1

        'because i'm using vec2 for the edges, i needed to use .X and .Y, these are actually the start and end points :)
        For intLoop = 0 To intLength
            If Not (vec4Points(stcCurrentScene.vec2Edges(intLoop).X).Z <= 0 Or vec4Points(stcCurrentScene.vec2Edges(intLoop).Y).Z <= 0) Then
                pntStart.X = Me.picViewport.Width / 2 + vec4Points(stcCurrentScene.vec2Edges(intLoop).X).X * sngFocalLength / vec4Points(stcCurrentScene.vec2Edges(intLoop).X).Z
                pntStart.Y = Me.picViewport.Height / 2 + vec4Points(stcCurrentScene.vec2Edges(intLoop).X).Y * sngFocalLength / vec4Points(stcCurrentScene.vec2Edges(intLoop).X).Z

                pntEnd.X = Me.picViewport.Width / 2 + vec4Points(stcCurrentScene.vec2Edges(intLoop).Y).X * sngFocalLength / vec4Points(stcCurrentScene.vec2Edges(intLoop).Y).Z
                pntEnd.Y = Me.picViewport.Height / 2 + vec4Points(stcCurrentScene.vec2Edges(intLoop).Y).Y * sngFocalLength / vec4Points(stcCurrentScene.vec2Edges(intLoop).Y).Z

                grphFrameBuffer.DrawLine(New Pen(Color.White, 1), pntStart, pntEnd)
            End If
        Next intLoop

        If blnDebug Then
            DrawDebug(stcCurrentScene, dblDeltaTime)
        End If

        grphFrame.DrawImage(bmpFrameBuffer, 0, 0)

    End Sub

    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    'Draws a scene to the screen
    '
    'post: scene is drawn to the screen using it's tri data, optionally drawing debug information as well
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Sub DrawTris(ByVal vec4Points() As Vector4, ByRef stcCurrentScene As Form1.Scene, ByRef vec4WorldSpacePoints() As Vector4,
                 ByVal sngFocalLength As Single, ByVal dblDeltaTime As Double, ByVal intViewMode As Integer, ByVal blnForceBackCulling As Boolean)

        Dim intDistances() As Integer
        Dim intLoop As Integer
        Dim intLength As Integer
        Dim vec3Normal As Vector3
        Dim vec3BackCullCheck As Vector3
        Dim sngTheta As Single
        Dim intColorValue As Integer
        Dim DrawColor As Color
        Dim pntA As Point
        Dim pntB As Point
        Dim pntC As Point

        intDistances = sortTris(vec4Points)

        grphFrameBuffer.Clear(Color.Black)

        intLength = vec3OrderedTris.Length - 1

        For intLoop = 0 To intLength
            vec3Normal = CalculateNormal(vec4Points, vec3OrderedTris(intLoop))
            vec3BackCullCheck = New Vector3 With {
                .X = vec4Points(vec3OrderedTris(intLoop).X).X,
                .Y = vec4Points(vec3OrderedTris(intLoop).X).Y,
                .Z = vec4Points(vec3OrderedTris(intLoop).X).Z
            }

            If Vector3.Dot(vec3BackCullCheck, vec3Normal) >= 0 And (stcCurrentScene.blnBackCulled Or blnForceBackCulling) Or
               (vec4Points(vec3OrderedTris(intLoop).X).Z <= 0 Or vec4Points(vec3OrderedTris(intLoop).Y).Z <= 0 Or vec4Points(vec3OrderedTris(intLoop).Z).Z <= 0) Then
                Continue For
            End If

            Select Case intViewMode 'case 0 is handled elsewhere
                Case 1
                    intColorValue = Map(Math.Max(Math.Min(intDistances(intLoop) / 1000000, 10), 0), 0, 10, 255, 0)
                    DrawColor = Color.FromArgb(intColorValue, intColorValue, intColorValue)
                Case 2
                    DrawColor = Color.FromArgb(
                    Map(vec3Normal.X, -1, 1, 0, 255),
                    Map(vec3Normal.Y, -1, 1, 0, 255),
                    Map(vec3Normal.Z, -1, 1, 0, 255)
                    )
                Case 3
                    vec3Normal = CalculateNormal(vec4WorldSpacePoints, vec3OrderedTris(intLoop))
                    DrawColor = Color.FromArgb(
                        Map(vec3Normal.X, -1, 1, 0, 255),
                        Map(vec3Normal.Y, -1, 1, 0, 255),
                        Map(vec3Normal.Z, -1, 1, 0, 255)
                    )
                Case 4
                    'Directional Lighting
                    'Get world-space normal
                    vec3Normal = CalculateNormal(vec4WorldSpacePoints, vec3OrderedTris(intLoop))
                    sngTheta = radiansToDegrees(Math.Acos(Vector3.Dot(vec3LightDirection, vec3Normal) / (vec3LightDirection.Length * vec3Normal.Length)))
                    If Single.IsNaN(sngTheta) Then
                        intColorValue = 0
                    Else
                        intColorValue = Map(sngTheta, 0, 180, 0, 255)
                    End If
                    DrawColor = Color.FromArgb(intColorValue, intColorValue, intColorValue)
            End Select

            pntA.X = Me.picViewport.Width / 2 + vec4Points(vec3OrderedTris(intLoop).X).X * sngFocalLength / vec4Points(vec3OrderedTris(intLoop).X).Z
            pntA.Y = Me.picViewport.Height / 2 + vec4Points(vec3OrderedTris(intLoop).X).Y * sngFocalLength / vec4Points(vec3OrderedTris(intLoop).X).Z

            pntB.X = Me.picViewport.Width / 2 + vec4Points(vec3OrderedTris(intLoop).Y).X * sngFocalLength / vec4Points(vec3OrderedTris(intLoop).Y).Z
            pntB.Y = Me.picViewport.Height / 2 + vec4Points(vec3OrderedTris(intLoop).Y).Y * sngFocalLength / vec4Points(vec3OrderedTris(intLoop).Y).Z

            pntC.X = Me.picViewport.Width / 2 + vec4Points(vec3OrderedTris(intLoop).Z).X * sngFocalLength / vec4Points(vec3OrderedTris(intLoop).Z).Z
            pntC.Y = Me.picViewport.Height / 2 + vec4Points(vec3OrderedTris(intLoop).Z).Y * sngFocalLength / vec4Points(vec3OrderedTris(intLoop).Z).Z

            grphFrameBuffer.FillPolygon(New SolidBrush(DrawColor), {pntA, pntB, pntC}, Drawing2D.FillMode.Alternate)
        Next

        If blnDebug Then
            DrawDebug(stcCurrentScene, dblDeltaTime)
        End If

        grphFrame.DrawImage(bmpFrameBuffer, 0, 0)

    End Sub

    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    'Draws debug information to the frame buffer
    '
    'post: debug info is drawn in the frame buffer
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Sub DrawDebug(ByRef stcCurrentScene As Form1.Scene, ByVal dblDeltaTime As Double)

        grphFrameBuffer.DrawString("DeltaTime: " & dblDeltaTime & "s" & vbCrLf &
                                   "Scene Name: " & stcCurrentScene.strName & vbCrLf, New Font(FontFamily.GenericSerif, 10), Brushes.Cyan, 0, 0)

    End Sub

    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    'Sorts tris by their distance from the camera. uses radixLSD sort
    '
    'post: vec3OrderedTris() is sorted by the average length of each component point and the distances are returned in an array
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Function sortTris(ByVal vec4Points() As Vector4) As Integer()

        Dim intDistances(vec3OrderedTris.Length - 1) As Integer 'i dont really want to implement radix sort for floats, so i do this instead
        Dim intLength As Integer
        Dim intLoop As Integer
        Dim intBitMask As Integer = 1
        Dim vec3lstRadixLeft As New List(Of Vector3)()
        Dim vec3lstRadixRight As New List(Of Vector3)()
        Dim intlstRadixLeft As New List(Of Integer)()
        Dim intlstRadixRight As New List(Of Integer)()

        Parallel.For(0, vec3OrderedTris.Length,
        Sub(ByVal intLoopInner)
            intDistances(intLoopInner) = ((vec4Points(vec3OrderedTris(intLoopInner).X).Length() _
                                         + vec4Points(vec3OrderedTris(intLoopInner).Y).Length() _
                                         + vec4Points(vec3OrderedTris(intLoopInner).Z).Length()) / 3) * 1000000 'technically just a fixed point value now :)
        End Sub)

        Do
            intLength = vec3OrderedTris.Length - 1

            For intLoop = 0 To intLength
                If intDistances(intLoop) And intBitMask Then
                    intlstRadixLeft.Add(intDistances(intLoop))
                    vec3lstRadixLeft.Add(vec3OrderedTris(intLoop))
                Else
                    intlstRadixRight.Add(intDistances(intLoop))
                    vec3lstRadixRight.Add(vec3OrderedTris(intLoop))
                End If
            Next intLoop

            intlstRadixLeft.CopyTo(intDistances, 0)
            vec3lstRadixLeft.CopyTo(vec3OrderedTris, 0)

            intlstRadixRight.CopyTo(intDistances, intlstRadixLeft.Count)
            vec3lstRadixRight.CopyTo(vec3OrderedTris, vec3lstRadixLeft.Count)

            intBitMask = intBitMask << 1

            intlstRadixLeft.Clear()
            vec3lstRadixLeft.Clear()
            intlstRadixRight.Clear()
            vec3lstRadixRight.Clear()

        Loop While intBitMask

        Return intDistances

    End Function

    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    'calculate the normal direction of a tri
    '
    'post: returns a vec3 containing the normal direction
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Function CalculateNormal(ByRef vec4Points() As Vector4, ByVal vec3Tri As Vector3) As Vector3

        Dim vec3A As New Vector3 With {
            .X = vec4Points(vec3Tri.X).X,
            .Y = vec4Points(vec3Tri.X).Y,
            .Z = vec4Points(vec3Tri.X).Z}
        Dim vec3B As New Vector3 With {
            .X = vec4Points(vec3Tri.Y).X,
            .Y = vec4Points(vec3Tri.Y).Y,
            .Z = vec4Points(vec3Tri.Y).Z}
        Dim vec3C As New Vector3 With {
            .X = vec4Points(vec3Tri.Z).X,
            .Y = vec4Points(vec3Tri.Z).Y,
            .Z = vec4Points(vec3Tri.Z).Z}
        Dim vec3U As Vector3
        Dim vec3V As Vector3
        Dim vec3Normal As Vector3

        vec3V = vec3B - vec3A
        vec3U = vec3C - vec3A

        vec3Normal = New Vector3 With {
            .X = vec3U.Y * vec3V.Z - vec3U.Z * vec3V.Y,
            .Y = vec3U.Z * vec3V.X - vec3U.X * vec3V.Z,
            .Z = vec3U.X * vec3V.Y - vec3U.Y * vec3V.X}

        Return vec3Normal

    End Function

#End Region

#Region "Miscellaneous Functions"

    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    'converts a value from degrees to radians
    '
    'post: sngDegrees is converted from degrees to radians
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Function degreesToRadians(ByVal sngDegrees As Single) As Single

        Return sngDegrees * Math.PI / 180

    End Function

    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    'converts a value from radians to degrees 
    '
    'post: sngRadians is converted from radians to degrees
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Function radiansToDegrees(ByVal sngRadians As Single) As Single

        Return sngRadians * 180 / Math.PI

    End Function

    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    'Maps a number from one range to another
    '
    'post: number mapped from the original range to the output range
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Function Map(ByVal sngX As Single, ByVal sngMinimumIn As Single, ByVal sngMaximumIn As Single, ByVal sngMinimumOut As Single, ByVal sngMaximumOut As Single) As Single

        Return (sngX - sngMinimumIn) / (sngMaximumIn - sngMinimumIn) * (sngMaximumOut - sngMinimumOut) + sngMinimumOut

    End Function

#End Region

#Region "Input handling"

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

        If e.KeyCode = Windows.Forms.Keys.F3 Then
            blnDebug = Not blnDebug
        End If

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

    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    'modifies transforms according to input, acts like a third person control
    '
    'post: transforms are modified based on user input
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Sub HandleObjectInput(ByRef sngTranslationX As Single, ByRef sngTranslationY As Single, ByRef sngTranslationZ As Single,
                          ByRef sngRotationX As Single, ByRef sngRotationY As Single, ByRef sngRotationZ As Single,
                          ByVal dblDeltaTime As Double)

        ' Q    W       E
        ' Down Forward Up
        ' A    S       D
        ' Left Back    Right
        sngTranslationX -= 1 * blnKeys(Keys.D) * dblDeltaTime
        sngTranslationX += 1 * blnKeys(Keys.A) * dblDeltaTime
        sngTranslationY -= 1 * blnKeys(Keys.Q) * dblDeltaTime
        sngTranslationY += 1 * blnKeys(Keys.E) * dblDeltaTime
        sngTranslationZ -= 1 * blnKeys(Keys.W) * dblDeltaTime
        sngTranslationZ += 1 * blnKeys(Keys.S) * dblDeltaTime

        ' R    T       Y
        ' CCW  Up      CW
        ' F    G       H
        ' Left Down    Right
        sngRotationX += 30 * blnKeys(Keys.T) * dblDeltaTime
        sngRotationX -= 30 * blnKeys(Keys.G) * dblDeltaTime
        sngRotationY += 30 * blnKeys(Keys.H) * dblDeltaTime
        sngRotationY -= 30 * blnKeys(Keys.F) * dblDeltaTime
        sngRotationZ += 30 * blnKeys(Keys.R) * dblDeltaTime
        sngRotationZ -= 30 * blnKeys(Keys.Y) * dblDeltaTime

        sngRotationX = sngRotationX Mod 360
        sngRotationY = sngRotationY Mod 360
        sngRotationZ = sngRotationZ Mod 360

    End Sub

    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    'modifies transforms according to input, acts like a first person control
    '
    'post: transforms are modified based on user input
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Sub HandleCameraInput(ByRef sngTranslationX As Single, ByRef sngTranslationY As Single, ByRef sngTranslationZ As Single,
                          ByRef sngRotationX As Single, ByRef sngRotationY As Single, ByRef sngRotationZ As Single,
                          ByVal dblDeltaTime As Double)

        ' Q    W       E
        ' Down Forward Up
        ' A    S       D
        ' Left Back    Right

        'relative to screen orientation
        sngTranslationZ -= 1 * blnKeys(Keys.W) * dblDeltaTime * Math.Sin(degreesToRadians(sngRotationY + 90))
        sngTranslationX += 1 * blnKeys(Keys.W) * dblDeltaTime * Math.Cos(degreesToRadians(sngRotationY + 90))

        sngTranslationZ += 1 * blnKeys(Keys.S) * dblDeltaTime * Math.Sin(degreesToRadians(sngRotationY + 90))
        sngTranslationX -= 1 * blnKeys(Keys.S) * dblDeltaTime * Math.Cos(degreesToRadians(sngRotationY + 90))

        sngTranslationZ -= 1 * blnKeys(Keys.D) * dblDeltaTime * Math.Sin(degreesToRadians(-sngRotationY))
        sngTranslationX -= 1 * blnKeys(Keys.D) * dblDeltaTime * Math.Cos(degreesToRadians(-sngRotationY))

        sngTranslationZ += 1 * blnKeys(Keys.A) * dblDeltaTime * Math.Sin(degreesToRadians(-sngRotationY))
        sngTranslationX += 1 * blnKeys(Keys.A) * dblDeltaTime * Math.Cos(degreesToRadians(-sngRotationY))


        'not relative to screen orientation
        sngTranslationY -= 1 * blnKeys(Keys.Q) * dblDeltaTime
        sngTranslationY += 1 * blnKeys(Keys.E) * dblDeltaTime

        ' R    T       Y
        ' CCW  Up      CW
        ' F    G       H
        ' Left Down    Right
        sngRotationX -= 30 * blnKeys(Keys.T) * dblDeltaTime
        sngRotationX += 30 * blnKeys(Keys.G) * dblDeltaTime
        sngRotationY -= 30 * blnKeys(Keys.H) * dblDeltaTime
        sngRotationY += 30 * blnKeys(Keys.F) * dblDeltaTime

        sngRotationX = sngRotationX Mod 360
        sngRotationY = sngRotationY Mod 360

    End Sub

    Dim blnMouseHeld As Boolean
    Dim blnCameraControl As Boolean

    Private Sub picViewport_Click(sender As Object, e As MouseEventArgs) Handles picViewport.MouseDown

        If blnCameraControl Then
            blnMouseHeld = True
            Cursor.Position = picViewport.PointToScreen(New Point(picViewport.Width / 2, picViewport.Height / 2))
            Cursor.Hide()
        End If

    End Sub

    Private Sub picViewport_MouseUp(sender As Object, e As MouseEventArgs) Handles picViewport.MouseUp

        If blnCameraControl Then
            blnMouseHeld = False
            Cursor.Show()
        End If

    End Sub

    Sub HandleMouseInput(ByRef sngRotationX As Single, ByRef sngRotationY As Single,
                         ByVal dblDeltaTime As Double)

        Dim pntDeltaMousePosition As Point

        If blnMouseHeld Then
            pntDeltaMousePosition = Cursor.Position - picViewport.PointToScreen(New Point(picViewport.Width / 2, picViewport.Height / 2))

            sngRotationX -= 0.1 * pntDeltaMousePosition.Y
            sngRotationY += 0.1 * pntDeltaMousePosition.X

            Cursor.Position = picViewport.PointToScreen(New Point(picViewport.Width / 2, picViewport.Height / 2))
        End If

    End Sub

#End Region

End Class