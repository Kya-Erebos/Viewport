Imports System.Numerics
Imports vportObjects
Imports vportLayers

Public Class Viewport

    Dim vplrEmpty As vportLayer

    Private Sub Viewport_Load(sender As Object, e As EventArgs) Handles Me.Load

        vplrEmpty = New vportLayer(Me.picViewport, Color.Black)

    End Sub

    Sub main()

        vplrEmpty.DrawFrame()

    End Sub

End Class