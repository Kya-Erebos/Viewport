
Imports System.Numerics

Class vportLayer 'empty layer, basically just a container for a graphics object. can specify buffer clear colour

    Protected Property bmpBuffer As Bitmap
    Protected Property grphBuffer As Graphics
    Protected Property grphFrame As Graphics
    Protected Property clrClearColour As Color

    Public Sub New(ByRef picSurface As PictureBox, Optional ByVal clrBufferClearColour As Color = Nothing)

        bmpBuffer = New Bitmap(picSurface.Width, picSurface.Height)
        grphBuffer = Graphics.FromImage(bmpBuffer)
        grphFrame = picSurface.CreateGraphics()

        If IsNothing(clrBufferClearColour) Then
            clrClearColour = Color.Transparent
        Else
            clrClearColour = clrBufferClearColour
        End If

    End Sub

    Public Sub Buffer()
        grphBuffer.Clear(clrClearColour)
    End Sub

    Public Sub DrawFrame()
        grphFrame.DrawImage(bmpBuffer, 0, 0)
    End Sub

End Class

Class vportNaiveDrawSolidLayer
    Inherits vportLayer

    Public Sub New(ByRef picSurface As PictureBox, Optional ByVal clrBufferClearColour As Color = Nothing)
        MyBase.New(picSurface, clrBufferClearColour)
    End Sub

    'all the objects to be drawn
    '(may also contain any subclass of vportEmpty)
    Protected Children As New List(Of IvportModelData)

    Public camera As vportCamera

    Public vec3LightDirection As Vector3 = New Vector3(2, 3, 1)

#Region "managing children"
    'adds a object to the layer
    Public Overloads Sub AddChildren(ByRef Child As IvportModelData)

        Children.Add(Child)

    End Sub

    'adds multiple objects to the layer
    Public Overloads Sub AddChildren(ByRef Children() As IvportModelData)

        Dim intLength As Integer
        Dim intLoop As Integer

        intLength = Children.Length - 1

        For intLoop = 0 To intLength
            Me.Children.Add(Children(intLoop))
        Next

    End Sub

    'removes a object from the layer
    Public Overloads Sub RemoveChildren(ByRef Child As IvportModelData)

        If Not Children.Contains(Child) Then
            Return
        End If

        Children.Remove(Child)

    End Sub

    'removes multiple objects from the layer
    Public Overloads Sub RemoveChildren(ByRef Children() As IvportModelData)

        Dim intLength As Integer
        Dim intLoop As Integer

        intLength = Children.Length - 1

        For intLoop = 0 To intLength
            If Not Me.Children.Contains(Children(intLoop)) Then
                Continue For
            End If

            Me.Children.Remove(Children(intLoop))
        Next

    End Sub
#End Region

    Public Overloads Sub Buffer()
        'clear buffer
        grphBuffer.Clear(clrClearColour)

        'all the processing stuff
        Const intFOCALLENGTH = 500

        Dim stclstTris As New List(Of Tri)
        Dim lnglstDistances As New List(Of Long)
        Dim stcTris() As Tri
        Dim lngDistances() As Long
        Dim intLength As Integer
        Dim intLoop As Integer
        Dim vec3Normal As Vector3
        Dim sngTheta As Single
        Dim intColourValue As Integer
        Dim pntA As PointF
        Dim pntB As PointF
        Dim pntC As PointF

        For Each Child As IvportModelData In Children
            Child.PropogateTransforms()
            stclstTris.AddRange(Child.FetchTris())
        Next Child

        For Each Face As Tri In stclstTris
            lnglstDistances.Add(BitConverter.DoubleToInt64Bits((Face.vec3A.Length() + Face.vec3B.Length() + Face.vec3C.Length()) / 3))
        Next Face

        stcTris = stclstTris.ToArray()
        lngDistances = lnglstDistances.ToArray()
        LSDRadixSortTris(stcTris, lngDistances)

        intLength = stcTris.Length - 1

        For intLoop = 0 To intLength

            vec3Normal = CalculateNormal(stcTris(intLoop))

            If (stcTris(intLoop).vec3A.Z <= 0 Or stcTris(intLoop).vec3B.Z <= 0 Or stcTris(intLoop).vec3C.Z <= 0) Or
               (Vector3.Dot(stcTris(intLoop).vec3A, vec3Normal) >= 0 And stcTris(intLoop).blnBackCulled) Then
                Continue For
            End If

            If Vector3.Dot(stcTris(intLoop).vec3A, vec3Normal) >= 0 And Not stcTris(intLoop).blnBackCulled Then
                vec3Normal = -vec3Normal
            End If

            If IsNothing(Me.camera) Then
                sngTheta = radiansToDegrees(Math.Acos(Vector3.Dot(Me.vec3LightDirection, vec3Normal) / (Me.vec3LightDirection.Length() * vec3Normal.Length)))
            Else
                sngTheta = radiansToDegrees(Math.Acos(Vector3.Dot(Vector3.Transform(Me.vec3LightDirection, Me.camera.m4x4Rotation), vec3Normal) / (Me.vec3LightDirection.Length() * vec3Normal.Length)))
            End If

            If Single.IsNaN(sngTheta) Then
                intColourValue = 0
            Else
                intColourValue = Map(sngTheta, 0, 180, 10, 200)
            End If

            Color.FromArgb(intColourValue, intColourValue, intColourValue)

            pntA = New Point With {
                .X = bmpBuffer.Width / 2 + stcTris(intLoop).vec3A.X * intFOCALLENGTH / stcTris(intLoop).vec3A.Z,
                .Y = bmpBuffer.Height / 2 + stcTris(intLoop).vec3A.Y * intFOCALLENGTH / stcTris(intLoop).vec3A.Z
            }
            pntB = New Point With {
                .X = bmpBuffer.Width / 2 + stcTris(intLoop).vec3B.X * intFOCALLENGTH / stcTris(intLoop).vec3B.Z,
                .Y = bmpBuffer.Height / 2 + stcTris(intLoop).vec3B.Y * intFOCALLENGTH / stcTris(intLoop).vec3B.Z
            }
            pntC = New Point With {
                .X = bmpBuffer.Width / 2 + stcTris(intLoop).vec3C.X * intFOCALLENGTH / stcTris(intLoop).vec3C.Z,
                .Y = bmpBuffer.Height / 2 + stcTris(intLoop).vec3C.Y * intFOCALLENGTH / stcTris(intLoop).vec3C.Z
            }

            grphBuffer.FillPolygon(New SolidBrush(Color.FromArgb(intColourValue, intColourValue, intColourValue)),
                                   {pntA, pntB, pntC}, Drawing2D.FillMode.Winding)

        Next intLoop

    End Sub

    Public Sub DrawFrame()

        'bush buffer to screen
        grphFrame.DrawImage(bmpBuffer, 0, 0)

    End Sub

    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    'LSD radix sort, operating on both the Tris and their distances, sorting based on the distances
    '
    'pre: both input arrays are of a fixed size, and distances are precomputed for the function
    'post:both input arrays are sorted based on the distances (furthest to closest)
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Protected Sub LSDRadixSortTris(ByRef Tris() As Tri, ByRef Distances() As Long)

        Dim lngBitMask As Long = 1
        Dim intLength As Integer
        Dim intLoop As Integer
        Dim lngRadixLeft As New List(Of Long)
        Dim lngRadixRight As New List(Of Long)
        Dim stcRadixLeft As New List(Of Tri)
        Dim stcRadixRight As New List(Of Tri)

        intLength = Tris.Length - 1

        Do Until lngBitMask = 0

            For intLoop = 0 To intLength

                If Distances(intLoop) And lngBitMask Then
                    lngRadixLeft.Add(Distances(intLoop))
                    stcRadixLeft.Add(Tris(intLoop))
                Else
                    lngRadixRight.Add(Distances(intLoop))
                    stcRadixRight.Add(Tris(intLoop))
                End If

            Next intLoop

            lngRadixLeft.CopyTo(Distances, 0)
            stcRadixLeft.CopyTo(Tris, 0)

            lngRadixRight.CopyTo(Distances, lngRadixLeft.Count)
            stcRadixRight.CopyTo(Tris, stcRadixLeft.Count)

            lngRadixLeft.Clear()
            stcRadixLeft.Clear()
            lngRadixRight.Clear()
            stcRadixRight.Clear()

            lngBitMask <<= 1

        Loop

    End Sub

    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    'calculate the normal direction of a tri
    '
    'post: returns a vec3 containing the normal direction
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Function CalculateNormal(ByRef face As Tri) As Vector3

        Dim vec3U As Vector3
        Dim vec3V As Vector3
        Dim vec3Normal As Vector3

        vec3V = face.vec3B - face.vec3A
        vec3U = face.vec3C - face.vec3A

        vec3Normal = New Vector3 With {
            .X = vec3U.Y * vec3V.Z - vec3U.Z * vec3V.Y,
            .Y = vec3U.Z * vec3V.X - vec3U.X * vec3V.Z,
            .Z = vec3U.X * vec3V.Y - vec3U.Y * vec3V.X}

        Return vec3Normal

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

End Class

Class vportDrawWireframeLayer
    Inherits vportLayer

    Public Sub New(ByRef picSurface As PictureBox, Optional ByVal clrBufferClearColour As Color = Nothing)
        MyBase.New(picSurface, clrBufferClearColour)
    End Sub

    'all the objects to be drawn
    '(may also contain any subclass of vportEmpty)
    Private Children As New List(Of IvportModelData)

    Public vec3LightDirection As Vector3 = New Vector3(2, 3, 1)

#Region "managing children"
    'adds a object to the layer
    Public Overloads Sub AddChildren(ByRef Child As IvportModelData)

        Children.Add(Child)

    End Sub

    'adds multiple objects to the layer
    Public Overloads Sub AddChildren(ByRef Children() As IvportModelData)

        Dim intLength As Integer
        Dim intLoop As Integer

        intLength = Children.Length - 1

        For intLoop = 0 To intLength
            Me.Children.Add(Children(intLoop))
        Next

    End Sub

    'removes a object from the layer
    Public Overloads Sub RemoveChildren(ByRef Child As IvportModelData)

        If Not Children.Contains(Child) Then
            Return
        End If

        Children.Remove(Child)

    End Sub

    'removes multiple objects from the layer
    Public Overloads Sub RemoveChildren(ByRef Children() As IvportModelData)

        Dim intLength As Integer
        Dim intLoop As Integer

        intLength = Children.Length - 1

        For intLoop = 0 To intLength
            If Not Me.Children.Contains(Children(intLoop)) Then
                Continue For
            End If

            Me.Children.Remove(Children(intLoop))
        Next

    End Sub
#End Region

    Public Overloads Sub Buffer()
        'clear buffer
        grphBuffer.Clear(clrClearColour)

        'all the processing stuff
        Const intFOCALLENGTH = 500

        Dim stclstTris As New List(Of Tri)
        Dim intLength As Integer
        Dim intLoop As Integer
        Dim pntA As PointF
        Dim pntB As PointF
        Dim pntC As PointF
        Dim penPen As New Pen(Brushes.White, 2.5)

        For Each Child As IvportModelData In Children
            Child.PropogateTransforms()
            stclstTris.AddRange(Child.FetchTris())
        Next Child

        intLength = stclstTris.Count - 1

        For intLoop = 0 To intLength

            If stclstTris(intLoop).vec3A.Z <= 0 Or stclstTris(intLoop).vec3B.Z <= 0 Or stclstTris(intLoop).vec3C.Z <= 0 Then
                Continue For
            End If

            pntA = New Point With {
                .X = bmpBuffer.Width / 2 + stclstTris(intLoop).vec3A.X * intFOCALLENGTH / stclstTris(intLoop).vec3A.Z,
                .Y = bmpBuffer.Height / 2 + stclstTris(intLoop).vec3A.Y * intFOCALLENGTH / stclstTris(intLoop).vec3A.Z
            }
            pntB = New Point With {
                .X = bmpBuffer.Width / 2 + stclstTris(intLoop).vec3B.X * intFOCALLENGTH / stclstTris(intLoop).vec3B.Z,
                .Y = bmpBuffer.Height / 2 + stclstTris(intLoop).vec3B.Y * intFOCALLENGTH / stclstTris(intLoop).vec3B.Z
            }
            pntC = New Point With {
                .X = bmpBuffer.Width / 2 + stclstTris(intLoop).vec3C.X * intFOCALLENGTH / stclstTris(intLoop).vec3C.Z,
                .Y = bmpBuffer.Height / 2 + stclstTris(intLoop).vec3C.Y * intFOCALLENGTH / stclstTris(intLoop).vec3C.Z
            }

            grphBuffer.DrawLine(penPen, pntA, pntB)
            grphBuffer.DrawLine(penPen, pntA, pntC)
            grphBuffer.DrawLine(penPen, pntC, pntB)

        Next intLoop

    End Sub

    Public Sub DrawFrame()

        'push buffer to screen
        grphFrame.DrawImage(bmpBuffer, 0, 0)

    End Sub

End Class

Class vportDrawSolidLayer
    Inherits vportNaiveDrawSolidLayer

    Public Sub New(ByRef picSurface As PictureBox, Optional ByVal clrBufferClearColour As Color = Nothing)
        MyBase.New(picSurface, clrBufferClearColour)
        ReDim ZBuffer(picSurface.Width, picSurface.Height)
    End Sub

    Protected ZBuffer(,) As distIdPair

    Structure distIdPair
        Dim dblDistance As Double ' = Double.PositiveInfinity
        Dim intID As Integer
    End Structure

    Protected dblInternalNearClipDistance As Double
    Property dblNearClipDistance As Double
        Get
            Return dblInternalNearClipDistance
        End Get
        Set(value As Double)
            If value > 0 Then
                dblInternalNearClipDistance = value
            Else
                Console.WriteLine("Warning: Distance to near clipping plane must be a positive value")
            End If
        End Set
    End Property

    Public Sub Buffer() 'differs from the naive approach because this one /should/ handle clipping faces, and have a propper near-clipping plane

        'clear buffer
        grphBuffer.Clear(clrClearColour)

        'all the processing stuff
        Const intFOCALLENGTH = 500

        Dim stclstTris As New List(Of Tri)
        Dim stcTris() As Tri

        For Each Child As IvportModelData In Children
            Child.PropogateTransforms()
            stclstTris.AddRange(Child.FetchTris())
        Next Child

        stcTris = stclstTris.ToArray()

    End Sub

End Class