Imports System.Numerics

Public Class vportEmpty 'empty object, not drawn and contains no data. has children

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

    Property vec3Translation As Vector3
    Property vec3Scale As Vector3
    Property quatRotation As Quaternion

    Property vec3PropogatedTranslation As Vector3
    Property vec3PropogatedScale As Vector3
    Property quatPropogatedRotation As Quaternion

#Region "operators"
    ' okay, yes these operators are a bit janky but they work and i kinda like how it feels. 
    'also this way i shouldnt need to reimplement these for each subclass

    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    'not a conventional operator. used to apply transforms to the object
    '
    'post:object's scale is modified appropiately, returns -1 if any error occurs
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Shared Operator *(ByVal left As vportEmpty, ByVal Right As Vector3) As Integer

        Try
            left.vec3Scale = left.vec3Scale * Right
            Return 0
        Catch
            Return -1
        End Try

    End Operator

    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    'not a conventional operator. used to apply transforms to the object
    '
    'post:object's translation is modified appropiately, returns -1 if any error occurs
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Shared Operator +(ByVal left As vportEmpty, ByVal Right As Vector3) As Integer

        Try
            left.vec3Translation = left.vec3Translation + Right
            Return 0
        Catch
            Return -1
        End Try

    End Operator

    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    'not a conventional operator. used to apply transforms to the object
    '
    'post:object's scale is modified appropiately, returns -1 if any error occurs
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Shared Operator *(ByVal left As vportEmpty, ByVal Right As Quaternion) As Integer

        Try
            left.quatRotation = left.quatRotation * Right
            Return 0
        Catch
            Return -1
        End Try

    End Operator
#End Region

#Region "runtime inheritance"

    Property Parent As vportEmpty
        Get
            Return Parent
        End Get
        Set(value As vportEmpty)
            Parent.RemoveChildren(Me)
            Parent = value
        End Set
    End Property

    Private Children As List(Of vportEmpty)

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

    Public Overloads Sub PropogateTransforms()

        Dim intLength As Integer
        Dim intLoop As Integer

        intLength = Me.Children.Count - 1

        For intLoop = 0 To intLength
            Me.Children(intLoop).PropogateTransforms(Me.vec3Translation, Me.vec3Scale, Me.quatRotation)
        Next intLoop

    End Sub

    Public Overloads Sub PropogateTransforms(ByVal vec3InputTranslation As Vector3,
                                             ByVal vec3InputScale As Vector3,
                                             ByVal quatInputRotation As Quaternion)

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

End Class

Public Class vportModel
    Inherits vportEmpty

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
    Property Vertecies() As Vector3

    'indices of the vertexes. stored as Vector3. 
    Property Tris() As Vector3

End Class
