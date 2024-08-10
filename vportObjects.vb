Imports System.Linq.Expressions
Imports System.Numerics

Public Interface IvportModelData
    Function FetchTris() As List(Of Tri)
    Overloads Sub PropogateTransforms()
    Overloads Sub PropogateTransforms(ByVal vec3InputTranslation As Vector3,
                                      ByVal vec3InputScale As Vector3,
                                      ByVal quatInputRotation As Quaternion)
End Interface

Public Class vportEmpty 'empty object, not drawn and contains no data. has children
    Implements IvportModelData
    Public Sub New(Optional ByVal vec3InitialTranslation As Vector3 = Nothing,
                   Optional ByVal vec3InitialScale As Vector3 = Nothing,
                   Optional ByVal quatInitialRotation As Quaternion = Nothing)

        If Not IsNothing(vec3InitialTranslation) Then
            vec3Translation = vec3InitialTranslation
        Else
            vec3Translation = New Vector3(0, 0, 0)
        End If

        If Not IsNothing(vec3InitialScale) Then
            vec3Scale = vec3InitialScale
        Else
            vec3Scale = New Vector3(1, 1, 1)
        End If

        If Not IsNothing(quatInitialRotation) Then
            quatRotation = quatInitialRotation
        Else
            quatRotation = Quaternion.CreateFromYawPitchRoll(0, 0, 0)
        End If

    End Sub

    Property vec3Translation As Vector3 = New Vector3(0, 0, 0)
    Property vec3Scale As Vector3 = New Vector3(1, 1, 1)
    Property quatRotation As Quaternion = Quaternion.CreateFromYawPitchRoll(0, 0, 0)

    Property vec3PropogatedTranslation As Vector3 = New Vector3(0, 0, 0)
    Property vec3PropogatedScale As Vector3 = New Vector3(1, 1, 1)
    Property quatPropogatedRotation As Quaternion = Quaternion.CreateFromYawPitchRoll(0, 0, 0)

#Region "Transforms"
    Public Sub Translate(ByVal vec3InputTranslation As Vector3)
        vec3Translation += vec3InputTranslation
    End Sub

    Public Sub Scale(ByVal vec3InputScale As Vector3)
        vec3Scale *= vec3InputScale
    End Sub

    Public Sub Rotate(ByVal quatInputRotation As Quaternion)
        quatRotation = Quaternion.Multiply(quatRotation, quatInputRotation)
    End Sub

#End Region

#Region "runtime inheritance"

    Protected internalParent As vportEmpty
    Property Parent As vportEmpty
        Get
            If Not IsNothing(internalParent) Then
                Return internalParent
            Else
                Return Nothing
            End If
        End Get
        Set(value As vportEmpty)
            If Not IsNothing(internalParent) Then
                internalParent.RemoveChildren(Me)
            End If
            internalParent = value
        End Set
    End Property

    Protected Children As New List(Of vportEmpty)

    Public Overloads Sub AddChildren(ByRef Child As vportEmpty)

        Children.Add(Child)

    End Sub

    Public Overloads Sub AddChildren(ByRef Children() As vportEmpty)

        Dim intLength As Integer
        Dim intLoop As Integer

        intLength = Children.Length - 1

        For intLoop = 0 To intLength
            Me.Children.Add(Children(intLoop))
        Next

    End Sub

    Public Overloads Sub RemoveChildren(ByRef Child As vportEmpty)

        If Not Children.Contains(Child) Then
            Return
        End If

        Children.Remove(Child)

    End Sub

    Public Overloads Sub RemoveChildren(ByRef Children() As vportEmpty)

        Dim intLength As Integer
        Dim intLoop As Integer

        intLength = Children.Length - 1

        For intLoop = 0 To intLength
            Me.RemoveChildren(Children(intLoop))
        Next

    End Sub

    Public Overloads Sub PropogateTransforms() Implements IvportModelData.PropogateTransforms

        Dim intLength As Integer
        Dim intLoop As Integer

        intLength = Me.Children.Count - 1

        For intLoop = 0 To intLength
            Me.Children(intLoop).PropogateTransforms(Me.vec3Translation, Me.vec3Scale, Me.quatRotation)
        Next intLoop

    End Sub

    Public Overloads Sub PropogateTransforms(ByVal vec3InputTranslation As Vector3,
                                             ByVal vec3InputScale As Vector3,
                                             ByVal quatInputRotation As Quaternion) Implements IvportModelData.PropogateTransforms

        Dim intLength As Integer
        Dim intLoop As Integer

        intLength = Me.Children.Count - 1

        Me.vec3PropogatedTranslation = vec3InputTranslation
        Me.vec3PropogatedScale = vec3InputScale
        Me.quatPropogatedRotation = quatInputRotation

        For intLoop = 0 To intLength
            Me.Children(intLoop).PropogateTransforms(Me.vec3Translation + Me.vec3PropogatedTranslation,
                                                     Me.vec3Scale * Me.vec3PropogatedScale,
                                                     Quaternion.Multiply(Me.quatRotation, Me.quatPropogatedRotation))
        Next intLoop

    End Sub

#End Region

    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    'returns an array of tris, all referencing their points directly
    '
    'post:returns an array of Tris
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Public Function FetchTris() As List(Of Tri) Implements IvportModelData.FetchTris

        Dim Tris As New List(Of Tri)

        For Each Child As IvportModelData In Me.Children
            Tris.AddRange(Child.FetchTris())
        Next Child

        Return Tris

    End Function

End Class

Public Class vportModel
    Inherits vportEmpty
    Implements IvportModelData

    Public blnIsBackCulled As Boolean = True

    Public Sub New(Optional ByVal vec3InitialTranslation As Vector3 = Nothing,
                   Optional ByVal vec3InitialScale As Vector3 = Nothing,
                   Optional ByVal quatInitialRotation As Quaternion = Nothing)

        If Not IsNothing(vec3InitialTranslation) Then
            vec3Translation = vec3InitialTranslation
        Else
            vec3Translation = New Vector3(0, 0, 0)
        End If

        If Not IsNothing(vec3InitialScale) Then
            vec3Scale = vec3InitialScale
        Else
            vec3Scale = New Vector3(1, 1, 1)
        End If

        If Not IsNothing(quatInitialRotation) Then
            quatRotation = quatInitialRotation
        Else
            quatRotation = Quaternion.CreateFromYawPitchRoll(0, 0, 0)
        End If

    End Sub

    ' Drawing will be done by a single "draw layer", that way tris can be collected and drawn correctly.
    ' Also allows me to make different draw methods without needing to do large refactors of the code,
    'just making a new draw layer subclass

    'each vertex of the object, defined in local space. stored as Vector3.
    Public Vertecies As List(Of Vector3)

    'indices of the vertexes. stored as Vector3. 
    Public vec3ReferenceTris As List(Of Vector3)

    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    'returns an array of tris, all referencing their points directly
    '
    'post:returns an array of Tris
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Public Overloads Function FetchTris() As List(Of Tri) Implements IvportModelData.FetchTris

        Dim lambdaTransform As Func(Of Vector3, Vector3) = Function(ByVal vec3Input As Vector3) As Vector3
                                                               Return Vector3.Transform((Vector3.Transform(vec3Input * Me.vec3Scale, Me.quatRotation) + Me.vec3Translation) * Me.vec3PropogatedScale, Me.quatPropogatedRotation) + Me.vec3PropogatedTranslation
                                                           End Function
        Dim intLength As Integer
        Dim intLoop As Integer
        Dim Tris As New List(Of Tri)

        intLength = vec3ReferenceTris.Count - 1

        For intLoop = 0 To intLength
            Tris.Add(New Tri With {.vec3A = lambdaTransform(Vertecies(vec3ReferenceTris(intLoop).X)),
                                   .vec3B = lambdaTransform(Vertecies(vec3ReferenceTris(intLoop).Y)),
                                   .vec3C = lambdaTransform(Vertecies(vec3ReferenceTris(intLoop).Z)),
                                   .blnBackCulled = Me.blnIsBackCulled})
        Next intLoop

        For Each Child As IvportModelData In Me.Children
            Tris.AddRange(Child.FetchTris())
        Next Child

        Return Tris

    End Function

End Class

Public Class vportCamera
    Inherits vportEmpty
    Implements IvportModelData

    Public sngRotationX As Single = -90
    Public sngRotationY As Single = 90
    Public m4x4Rotation As Matrix4x4

    Public Sub New(Optional ByVal vec3InitialTranslation As Vector3 = Nothing,
                   Optional ByVal vec3InitialScale As Vector3 = Nothing,
                   Optional ByVal quatInitialRotation As Quaternion = Nothing)

        If Not IsNothing(vec3InitialTranslation) Then
            vec3Translation = vec3InitialTranslation
        Else
            vec3Translation = New Vector3(0, 0, 0)
        End If

        If Not IsNothing(vec3InitialScale) Then
            vec3Scale = vec3InitialScale
        Else
            vec3Scale = New Vector3(1, 1, 1)
        End If

        If Not IsNothing(quatInitialRotation) Then
            quatRotation = quatInitialRotation
        Else
            quatRotation = Quaternion.CreateFromYawPitchRoll(0, 0, 0)
        End If

    End Sub

    Public Overloads Sub PropogateTransforms() Implements IvportModelData.PropogateTransforms

        Dim intLength As Integer
        Dim intLoop As Integer

        intLength = Me.Children.Count - 1

        For intLoop = 0 To intLength
            Me.Children(intLoop).PropogateTransforms()
        Next intLoop

    End Sub

    Public Overloads Sub PropogateTransforms(ByVal vec3InputTranslation As Vector3,
                                             ByVal vec3InputScale As Vector3,
                                             ByVal quatInputRotation As Quaternion) Implements IvportModelData.PropogateTransforms
        Throw New NotImplementedException("Hey, dont do this. i dont know how to make the camera not inherit this method so im doing this instead")
    End Sub

    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    'returns an array of tris, all referencing their points directly
    'differs from the other objects due to operating on the other models' data 
    '
    'post:returns an array of Tris
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Public Function FetchTris() As List(Of Tri) Implements IvportModelData.FetchTris

        Dim Tris As New List(Of Tri)
        Dim lambdaTransform As Func(Of Vector3, Vector3) = Function(ByVal vec3Input As Vector3) As Vector3
                                                               Return Vector3.Transform(vec3Input - Me.vec3Translation, Me.m4x4Rotation) _
                                                                      * New Vector3(1 / Me.vec3Scale.X, 1 / Me.vec3Scale.Y, 1 / Me.vec3Scale.Z)
                                                           End Function
        Dim intLength As Integer
        Dim intLoop As Integer

        'i did the math on paper. more optimized. grug approves. 
        Dim sinY As Double = Math.Sin(degreeToRadian(sngRotationY))
        Dim cosY As Double = Math.Cos(degreeToRadian(sngRotationY))
        Dim sinX As Double = Math.Sin(degreeToRadian(sngRotationX))
        Dim cosX As Double = Math.Cos(degreeToRadian(sngRotationX))
        Me.m4x4Rotation = New Matrix4x4(sinY, cosY * cosX, cosY * sinX, 0,
                                        0, -sinX, cosX, 0,
                                        cosY, -sinY * cosX, -sinY * sinX, 0,
                                        0, 0, 0, 1)


        For Each Child As IvportModelData In Me.Children
            Tris.AddRange(Child.FetchTris())
        Next Child

        If vec3Scale.X = 0 Then
            vec3Scale = New Vector3(0.0001, vec3Scale.Y, vec3Scale.Z)
        End If

        If vec3Scale.Y = 0 Then
            vec3Scale = New Vector3(vec3Scale.X, 0.0001, vec3Scale.Z)
        End If

        If vec3Scale.Z = 0 Then
            vec3Scale = New Vector3(vec3Scale.X, vec3Scale.Y, 0.0001)
        End If

        intLength = Tris.Count - 1

        For intLoop = 0 To intLength
            Tris(intLoop) = New Tri With {.vec3A = lambdaTransform(Tris(intLoop).vec3A),
                                          .vec3B = lambdaTransform(Tris(intLoop).vec3B),
                                          .vec3C = lambdaTransform(Tris(intLoop).vec3C),
                                          .blnBackCulled = Tris(intLoop).blnBackCulled}
        Next intLoop

        Return Tris

    End Function

    Function degreeToRadian(ByVal dblDegrees As Double) As Double
        Return dblDegrees * Math.PI / 180
    End Function

End Class

Public Structure Tri
    Dim vec3A As Vector3
    Dim vec3B As Vector3
    Dim vec3C As Vector3
    Dim blnBackCulled As Boolean
End Structure


