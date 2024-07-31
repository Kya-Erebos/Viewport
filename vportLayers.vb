
Class vportLayer 'empty layer, basically just a container for a graphics object. can specify buffer clear colour

    Private Property bmpBuffer As Bitmap
    Private Property grphBuffer As Graphics
    Private Property grphFrame As Graphics
    Private Property clrClearColour As Color

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

    Public Sub DrawFrame()
        grphBuffer.Clear(clrClearColour)
        grphFrame.DrawImage(bmpBuffer, 0, 0)
    End Sub

End Class