'Author : Ali Hussian (AKA Abo Mahdi)
'intial starting date : around october 2017
'there is alot to do here.. hehe....
Option Strict Off
Option Explicit On
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D





Public Module bGraphics

    Public ScrnBufferBmp As Bitmap
    Public GraphicsBuffer As Graphics
    Public clientWidth As Integer
    Public clientHeight As Integer
    Public hostForm As Form = Form1
    Public frameCount As UInt32
    Private isMainForm As Boolean
    Private makeFullScreen As Boolean
    Private xOff As Double
    Private yOff As Double
    Private mH As Integer
    Private mV As Integer
    Private r As New Random
    Private formRec As Rectangle
    Private mouseIsClicked As Boolean = False
    Private messurmentMode As String = RADIANIS
    Public Const PI As Integer = 180
    Public Const TWO_PI As Integer = 360
    Public Const HALF_PI As Integer = 90
    Public Const QUARTER_PI As Integer = 45
    Public Const TAU As Integer = TWO_PI
    Public Const DEGREES As String = "d"
    Public Const RADIANIS As String = "r"
    Public Const infinity As UInt32 = UInt32.MaxValue


#Region "propeties"
    Public ReadOnly Property isMousePressed
        Get
            Return mouseIsClicked
        End Get
    End Property
    Public ReadOnly Property centerWidth
        Get
            Return hostForm.Width / 2
        End Get
    End Property
    Public ReadOnly Property centerHeight
        Get
            Return hostForm.Height / 2
        End Get
    End Property
    Public ReadOnly Property mouseX
        Get
            Return mH - xOff
        End Get
    End Property
    Public ReadOnly Property mouseY
        Get
            Return mV - yOff
        End Get
    End Property
    Public ReadOnly Property GREEN
        Get
            Return Color.Lime
        End Get
    End Property
    Public ReadOnly Property RED
        Get
            Return Color.Red
        End Get
    End Property
    Public ReadOnly Property YELLOW
        Get
            Return Color.Yellow
        End Get
    End Property
    Public ReadOnly Property PURPLE
        Get
            Return Color.Purple
        End Get
    End Property
    Public ReadOnly Property ORANGE
        Get
            Return Color.Orange
        End Get
    End Property
    Public ReadOnly Property PINK
        Get
            Return Color.Pink
        End Get
    End Property
    Public ReadOnly Property BLUE
        Get
            Return Color.Aqua
        End Get
    End Property
    Public ReadOnly Property BROWN
        Get
            Return Color.Brown
        End Get
    End Property
    Public ReadOnly Property WHITE
        Get
            Return Color.White
        End Get
    End Property
    Public ReadOnly Property BLACK
        Get
            Return Color.Black
        End Get
    End Property
    Public ReadOnly Property GREY
        Get
            Return Color.Gray
        End Get
    End Property

#End Region


    Public Class bVector
        Public x As Double
        Public y As Double

        Public Sub New(ByVal x As Double, ByVal y As Double)
            Me.x = x
            Me.y = y

        End Sub

        Public Sub add(ByVal v As bVector)
            x += v.x
            y += v.y

        End Sub
        Public Sub subt(ByVal v As bVector)
            x -= v.x
            y -= v.y

        End Sub
        Public Sub multi(ByVal v As Double)
            x *= v
            y *= v

        End Sub



        Public Sub div(ByVal v As Double)
            x /= v
            y /= v

        End Sub

        Public Function Mag() As Double
            Return dist(0, 0, x, y)
        End Function
        Public Sub setMag(ByVal val As Double)
            normalize()
            multi(val)
        End Sub

        Public Sub normalize()
            div(Mag())
        End Sub
        Public Sub copyFrom(ByVal v As bVector)


            x = v.x
            y = v.y

        End Sub
        Public Shared Function random2D() As bVector
            Return New bVector(randomInt(-1, 1), randomInt(-1, 1))



        End Function
        Public Shared Function fromAngle(ByVal angle) As bVector
            Dim x As Double = cos(angle)
            Dim y As Double = sin(angle)
            Return New bVector(x, y)

        End Function

    End Class
    Public Class bArray
        Private array As IList
        Private _t As Type

        Public Sub New()
        End Sub
        Public Sub initialize(Of T)()
            array = New List(Of T)
        End Sub
        Public Sub push(ByVal ob As Object)
            array.Add(ob)
        End Sub
        Public Function getAt(ByVal index As Integer, Optional ByVal index1 As Integer = -1) As Object
            Dim toReturn As Integer = 0
            If index1 <> -1 Then
                toReturn = index + index1 * (length()) / 10
            Else
                toReturn = index
            End If
            Return array(toReturn)
        End Function
        Public Sub splice(ByVal index As Integer, Optional ByVal index1 As Integer = -1)
            Dim toReturn As Integer = 0
            If index1 <> -1 Then
                toReturn = index + index1 * (length()) / 10
            Else
                toReturn = index
            End If
            array.RemoveAt(toReturn)
        End Sub
        Public Function length() As Integer
            Return array.Count
        End Function
    End Class

#Region "Math Functions"
    Private Function DegreeToRadian(ByVal x As Double)
        Return (x * Math.PI) / 180
    End Function
    Public Function sqrt(ByVal val As Double)
        Return Math.Sqrt(val)
    End Function
    Public Function sin(ByVal val As Double)
        If messurmentMode = DEGREES Then
            Return Math.Sin(DegreeToRadian(val))
        End If
        Return Math.Sin(val)
    End Function
    Public Function cos(ByVal val As Double)
        If messurmentMode = DEGREES Then
            Return Math.Cos(DegreeToRadian(val))
        End If
        Return Math.Cos(val)
    End Function
    Public Function tan(ByVal val As Double)
        If messurmentMode = DEGREES Then
            Return Math.Tan(DegreeToRadian(val))
        End If
        Return Math.Tan(val)
    End Function
    Public Function asin(ByVal val As Double)
        If messurmentMode = DEGREES Then
            Return Math.Asin(DegreeToRadian(val))
        End If
        Return Math.Asin(val)
    End Function
    Public Function acos(ByVal val As Double)
        If messurmentMode = DEGREES Then
            Return Math.Acos(DegreeToRadian(val))
        End If
        Return Math.Acos(val)
    End Function
    Public Function atan(ByVal val As Double)
        If messurmentMode = DEGREES Then
            Return Math.Atan(DegreeToRadian(val))
        End If
        Return Math.Atan(val)
    End Function

    Public Function abs(ByVal val As Decimal)
        Return Math.Abs(val)
    End Function
    Public Function floor(ByVal val As Double)
        Return Math.Floor(val)
    End Function
    Public Function max(ByVal val1 As Integer, ByVal val2 As Integer)
        Return Math.Max(val1, val2)
    End Function
    Public Function min(ByVal val1 As Integer, ByVal val2 As Integer)
        Return Math.Min(val1, val2)
    End Function
#End Region

    Public Function createVector(ByVal x As Double, ByVal y As Double) As bVector
        Return New bVector(x, y)
    End Function
    Public Function createVector(ByVal v As bVector) As bVector
        Return New bVector(v.x, v.y)

    End Function
    Public Function createArray(Of t)() As bArray
        Dim a As bArray = New bArray()
        a.initialize(Of t)()
        Return a
    End Function

    Public Sub countFrames()
        If Not frameCount = UInt32.MaxValue Then
            frameCount += 1
        Else
            frameCount = 0
        End If
    End Sub
    Public Sub getMouseXY()
        mH = floor(Cursor.Position.X) - hostForm.Location.X
        mV = floor(Cursor.Position.Y) - hostForm.Location.Y - 20
        mH = constrain(mH, 0, hostForm.Width)
        mV = constrain(mV, 0, hostForm.Height)
    End Sub




    Public Sub loadGraphics()
        If (makeFullScreen) Then
            Fullscreen()
        End If
        ScrnBufferBmp = New Bitmap(hostForm.Width, hostForm.Height, hostForm.CreateGraphics)
        GraphicsBuffer = Graphics.FromImage(ScrnBufferBmp)
        hostForm.Show()
        hostForm.Focus()
        xOff = 0
        yOff = 0
        clientWidth = My.Computer.Screen.Bounds.Width
        clientHeight = My.Computer.Screen.Bounds.Height
        frameCount = 0
        formRec = New Rectangle(0, 0, hostForm.Width, hostForm.Height)

        AddHandler hostForm.Disposed, AddressOf Kill
        AddHandler hostForm.Paint, AddressOf reDraw
        AddHandler hostForm.MouseDown, AddressOf mouseDownChecker
        AddHandler hostForm.MouseUp, AddressOf mouseUpChecker



    End Sub

    Public Sub Run()

        Application.DoEvents()
        hostForm.Invalidate(formRec)
        getMouseXY()
        countFrames()
        reset()

    End Sub
    Public Sub Background(ByVal r As Integer, Optional ByVal g As Integer = -1, Optional ByVal b As Integer = -1, Optional ByVal alpha As Integer = 255)
        If g = -1 Then
            g = r

        End If
        If b = -1 Then
            b = r
        End If
        Dim color As Color = color.FromArgb(alpha, r, g, b)
        GraphicsBuffer.Clear(color)
    End Sub
    Public Sub Background(ByVal clr As Color)
        GraphicsBuffer.Clear(clr)

    End Sub
    Public Function randomColor() As Color
        Return Color.FromArgb(randomInt(0, 255), randomInt(0, 255), randomInt(0, 255))

    End Function
    Public Function m(ByVal rec As Rectangle, ByVal r As Integer, ByVal rmode As Integer) As Matrix
        Dim mt As New Matrix
        Select Case rmode
            Case 0
                mt.RotateAt(r, New PointF(rec.Left + (rec.Width / 2), rec.Top + (rec.Height / 2)))
            Case 1
                mt.RotateAt(r, New PointF(rec.Left, rec.Top))
            Case 2
                mt.RotateAt(r, New PointF(rec.Left, rec.Top + rec.Height))

        End Select
        Return mt
    End Function

    Public Function rect(ByVal x As Integer, ByVal y As Integer, ByVal w As Integer, ByVal h As Integer, ByVal c As Color, Optional ByVal a As Integer = 255, Optional ByVal strokeWidth As Integer = 1, Optional ByVal fromCenterPoint As Boolean = False, Optional ByVal rotation As Integer = Nothing, Optional ByVal rotationMode As Integer = 0) As Rectangle

        Dim rec As Rectangle

        If fromCenterPoint Then
            rec = New Rectangle((x - w / 2) + xOff, (y - h / 2) + yOff, w, h)
        Else
            rec = New Rectangle((x) + xOff, (y) + yOff, w, h)
        End If

        Dim p As New Pen(New SolidBrush(Color.FromArgb(a, c)))
        p.Width = strokeWidth
        If rotation <> Nothing Then
            GraphicsBuffer.Transform = m(rec, rotation, rotationMode)

            GraphicsBuffer.DrawRectangle(p, rec)
            GraphicsBuffer.ResetTransform()

        Else

            GraphicsBuffer.DrawRectangle(p, rec)
        End If

        Return rec
    End Function

    Public Function ellipse(ByVal x As Integer, ByVal y As Integer, ByVal w As Integer, ByVal h As Integer, ByVal c As Color, Optional ByVal a As Integer = 255, Optional ByVal strokeWidth As Integer = 1, Optional ByVal fromCenterPoint As Boolean = True, Optional ByVal rotation As Integer = Nothing, Optional ByVal rotationMode As Integer = 0) As Rectangle
        Dim rec As Rectangle

        If fromCenterPoint Then
            rec = New Rectangle((x - w / 2) + xOff, (y - h / 2) + yOff, w, h)
        Else
            rec = New Rectangle((x) + xOff, (y) + yOff, w, h)
        End If

        Dim p As New Pen(New SolidBrush(Color.FromArgb(a, c)))
        p.Width = strokeWidth

        If rotation <> Nothing Then
            GraphicsBuffer.Transform = m(rec, rotation, rotationMode)
            GraphicsBuffer.DrawEllipse(p, rec)
            GraphicsBuffer.ResetTransform()

        Else
            GraphicsBuffer.DrawEllipse(p, rec)
        End If
        Return rec
    End Function
    Public Function rect(ByVal rec As Rectangle, ByVal c As Color, Optional ByVal a As Integer = 255, Optional ByVal strokeWidth As Integer = 1, Optional ByVal fromCenterPoint As Boolean = False, Optional ByVal rotation As Integer = Nothing, Optional ByVal rotationMode As Integer = 0)

        Return rect(rec.X, rec.Y, rec.Width, rec.Height, c, a, strokeWidth, fromCenterPoint, rotation, rotationMode)
    End Function
    Public Function ellipse(ByVal rec As Rectangle, ByVal c As Color, Optional ByVal a As Integer = 255, Optional ByVal strokeWidth As Integer = 1, Optional ByVal fromCenterPoint As Boolean = True, Optional ByVal rotation As Integer = Nothing, Optional ByVal rotationMode As Integer = 0) As Rectangle
        Return ellipse(rec.X, rec.Y, rec.Width, rec.Height, c, a, strokeWidth, fromCenterPoint, rotation, rotationMode)

    End Function
    Public Function rectFill(ByVal x As Integer, ByVal y As Integer, ByVal w As Integer, ByVal h As Integer, ByVal c As Color,
                                                Optional ByVal p As Color = Nothing, Optional ByVal strokeWidth As Integer = 1, Optional ByVal fromCenterPoint As Boolean = False, Optional ByVal a As Integer = 255, Optional ByVal rotation As Integer = Nothing, Optional ByVal rotationMode As Integer = 0) As Rectangle

        Dim rec As Rectangle

        If fromCenterPoint Then
            rec = New Rectangle((x - w / 2) + xOff, (y - h / 2) + yOff, w, h)
        Else
            rec = New Rectangle((x) + xOff, (y) + yOff, w, h)
        End If


        If rotation <> Nothing Then
            GraphicsBuffer.Transform = m(rec, rotation, rotationMode)
            GraphicsBuffer.FillRectangle(New SolidBrush(Color.FromArgb(a, c)), rec)
            GraphicsBuffer.ResetTransform()

        Else
            GraphicsBuffer.FillRectangle(New SolidBrush(Color.FromArgb(a, c)), rec)

        End If
        If p <> Nothing Then
            rect(x, y, w, h, p, strokeWidth, fromCenterPoint, rotation, rotationMode)

        End If
        Return rec
    End Function
    Public Function ellipseFill(ByVal x As Integer, ByVal y As Integer, ByVal w As Integer, ByVal h As Integer, ByVal c As Color,
                                                Optional ByVal p As Color = Nothing, Optional ByVal strokeWidth As Integer = 1, Optional ByVal fromCenterPoint As Boolean = True, Optional ByVal a As Integer = 255, Optional ByVal rotation As Integer = Nothing, Optional ByVal rotationMode As Integer = 0) As Rectangle

        Dim rec As Rectangle

        If fromCenterPoint Then
            rec = New Rectangle((x - w / 2) + xOff, (y - h / 2) + yOff, w, h)
        Else
            rec = New Rectangle((x) + xOff, (y) + yOff, w, h)
        End If

        If rotation <> Nothing Then
            GraphicsBuffer.Transform = m(rec, rotation, rotationMode)
            GraphicsBuffer.FillEllipse(New SolidBrush(Color.FromArgb(a, c)), rec)
            GraphicsBuffer.ResetTransform()
        Else
            GraphicsBuffer.FillEllipse(New SolidBrush(Color.FromArgb(a, c)), rec)
        End If
        If p <> Nothing Then
            ellipse(x, y, w, h, p, strokeWidth, fromCenterPoint, rotation, rotationMode)
        End If
        Return rec
    End Function

    Public Function rectFill(ByVal rec As Rectangle, ByVal c As Color,
                                                Optional ByVal p As Color = Nothing, Optional ByVal strokeWidth As Integer = 1, Optional ByVal fromCenterPoint As Boolean = False, Optional ByVal a As Integer = 255, Optional ByVal rotation As Integer = Nothing, Optional ByVal rotationMode As Integer = 0) As Rectangle
        Return rectFill(rec.X, rec.Y, rec.Width, rec.Height, c, p, strokeWidth, fromCenterPoint, a, rotation, rotationMode)
    End Function
    Public Function ellipseFill(ByVal rec As Rectangle, ByVal c As Color, Optional ByVal p As Color = Nothing, Optional ByVal strokeWidth As Integer = 1, Optional ByVal fromCenterPoint As Boolean = True,
                                                Optional ByVal a As Integer = 255, Optional ByVal rotation As Integer = Nothing, Optional ByVal rotationMode As Integer = 0) As Rectangle
        Return ellipseFill(rec.X, rec.Y, rec.Width, rec.Height, c, p, strokeWidth, fromCenterPoint, a, rotation, rotationMode)
    End Function
    Public Function line(ByVal x1 As Integer, ByVal y1 As Integer, ByVal x2 As Integer, ByVal y2 As Integer, ByVal c As Color, Optional ByVal rotation As Integer = Nothing,
                             Optional ByVal rotationMode As Integer = 0, Optional ByVal strokeWidth As Integer = 1, Optional ByVal a As Integer = 255)
        x1 = x1 + xOff
        x2 = x2 + xOff
        y1 = y1 + yOff
        y2 = y2 + yOff
        Dim rec As New Rectangle(New Point(x2, y2), New Size(1, (y1) - (y2)))
        Dim p As New Pen(New SolidBrush(Color.FromArgb(a, c)))
        p.Width = strokeWidth
        If rotation <> 0 Then
            GraphicsBuffer.Transform = m(rec, rotation, rotationMode)
            GraphicsBuffer.DrawLine(p, x1, y1, x2, y2)
            GraphicsBuffer.ResetTransform()
        Else

            GraphicsBuffer.DrawLine(p, x1, y1, x2, y2)
        End If
        Return rec


    End Function
    Public Function drawLine(ByVal p As Point, ByVal p2 As Point, ByVal c As Color, Optional ByVal rotation As Integer = Nothing,
                             Optional ByVal rotationMode As Integer = 0, Optional ByVal penWeight As Integer = 1, Optional ByVal a As Integer = 255)

        Return line(p.X, p.Y, p2.X, p2.Y, c, rotation, rotationMode, penWeight, a)


    End Function
    Public Sub drawLineBinary() 'test function

    End Sub
    Public Sub polygon(ByVal c As Color, ByVal points() As Point)
    
        For i = 0 To points.Length - 1
            points(i).X += xOff
            points(i).Y += yOff
        Next

        GraphicsBuffer.DrawPolygon(New Pen(New SolidBrush(c)), points) 'test function

    End Sub
    Public Sub triangle(ByVal x As Integer, ByVal y As Integer, ByVal w As Integer, ByVal h As Integer, ByVal c As Color)
        'test function
        Dim point1 = New Point(x, y)
        Dim point2 = New Point(x - w, y + h)
        Dim point3 = New Point(x + w, y + h)
        Dim curvePoints As Point() = {point1, point2, point3}
        GraphicsBuffer.DrawPolygon(New Pen(New SolidBrush(c)), curvePoints)

    End Sub
    Public Sub arc() 'test function
        GraphicsBuffer.DrawArc(New Pen(New SolidBrush(Color.Red)), New Rectangle(100, 100, 300, 300), 0, 360)

    End Sub
    Public Sub coords(ByVal sys As Integer) ' test function

    End Sub
    Public Sub point(ByVal x As Integer, ByVal y As Integer, ByVal c As Color, Optional ByVal size As Integer = 7, Optional ByVal fromCenterPoint As Boolean = False)
        translate(0.5, 0.5)

        ellipseFill(x, y, size, size, c, , size, fromCenterPoint, , , )

    End Sub

    Public Function randomInt(ByVal min As Integer, ByVal max As Integer, Optional ByVal canHaveZero As Boolean = True) As Integer
        Dim n As Integer = r.Next(min, max + 1)
        If n = 0 AndAlso canHaveZero = False Then
            Return min
        End If
        Return n

    End Function
    Public Sub translate(ByVal x As Double, ByVal y As Double)
        xOff = xOff + x
        yOff = yOff + y
    End Sub


    Public Function screenShot(ByVal sx As Integer, ByVal sy As Integer, ByVal dx As Integer, ByVal dy As Integer, ByVal w As Integer, ByVal h As Integer, Optional ByVal text As String = "", Optional ByVal tx As Integer = 0, Optional ByVal ty As Integer = 0) As Bitmap

        Dim bmpScreenShot As Bitmap
        Dim gfxScreenshot As Graphics

        bmpScreenShot = New Bitmap(w, h, PixelFormat.Gdi)

        gfxScreenshot = Graphics.FromImage(bmpScreenShot)
        gfxScreenshot.CopyFromScreen(sx, sy, dx, dy, New Size(w, h), CopyPixelOperation.SourceCopy)
        If text <> "" Then
            gfxScreenshot.DrawString(text, New Font("Arial", 30), Brushes.Red, tx, ty)
        End If



        Return bmpScreenShot
    End Function
    Public Sub drawImageRec(ByVal m As Image, ByVal x As Integer, ByVal y As Integer, ByVal w As Integer, ByVal h As Integer)
        x = x + xOff
        y = y + yOff

        GraphicsBuffer.DrawImage(m, x, y, w, h)


    End Sub

    Public Sub drawImageRec(ByVal m As Image, ByVal rec As Rectangle)
        drawImageRec(m, rec.X, rec.Y, rec.Width, rec.Height)

    End Sub


    Public Sub drawText(ByVal s As String, ByVal x As Double, ByVal y As Double, ByVal fontSize As Integer, ByVal b As Color, Optional ByVal alpha As Integer = 255, Optional ByVal fontName As String = "Arial", Optional ByVal fontStyle As FontStyle = FontStyle.Regular)
        x = x + xOff
        y = y + yOff
        If s.Length > 1 Then
            x = x - (s.Length - 1)
            y = y - (s.Length - 1)
        End If
        Dim bRush As New SolidBrush(Color.FromArgb(alpha, b))
        GraphicsBuffer.DrawString(s, New Font(fontName, fontSize, fontStyle), bRush, x, y)
    End Sub

    Public Sub formSize(Optional ByVal x As Integer = -1, Optional ByVal y As Integer = -1)
        hostForm.Width = IIf(x > -1, x, hostForm.Width)
        hostForm.Height = IIf(y > -1, y, hostForm.Height)
    End Sub
    Public Sub formBorder(ByVal val As Integer)
        Select Case val
            Case 0
                hostForm.FormBorderStyle = FormBorderStyle.None
            Case 1
                hostForm.FormBorderStyle = FormBorderStyle.Fixed3D
            Case 2
                hostForm.FormBorderStyle = FormBorderStyle.FixedDialog
            Case 3
                hostForm.FormBorderStyle = FormBorderStyle.FixedSingle
            Case 4
                hostForm.FormBorderStyle = FormBorderStyle.FixedToolWindow
            Case 5
                hostForm.FormBorderStyle = FormBorderStyle.Sizable
            Case 6
                hostForm.FormBorderStyle = FormBorderStyle.SizableToolWindow
        End Select
    End Sub
    Public Sub angleMode(ByVal Mode As String)
        If (Mode = DEGREES Or Mode = RADIANIS) Then
            messurmentMode = Mode
        End If
    End Sub

    Public Function map(ByVal n As Double, ByVal start1 As Double, ByVal stop1 As Double, ByVal start2 As Double, ByVal stop2 As Double) As Double
        Return ((n - start1) / (stop1 - start1)) * (stop2 - start2) + start2
    End Function
    Public Function dist(ByVal x1 As Double, ByVal y1 As Double, ByVal x2 As Double, ByVal y2 As Double) As Double
        Dim DeltaX As Double = x2 - x1
        Dim DeltaY As Double = y2 - y1
        Return sqrt((DeltaX ^ 2) + (DeltaY ^ 2))
    End Function
    Public Function constrain(ByVal n As Integer, ByVal low As Integer, ByVal high As Integer) As Integer
        Return max(min(n, high), low)
    End Function
    Public Sub inc(ByRef val As Double, Optional ByVal increment As Double = 1)
        val += increment
    End Sub
    Public Sub dec(ByRef val As Double, Optional ByVal decrement As Double = 1)
        val -= decrement
    End Sub
    Public Sub ConsoleLog(ByVal s As String, ByVal ParamArray args() As Object)
        Debug.Print(String.Format(s, args))
    End Sub

    Public Sub Sleep(Optional ByVal value As Short = 0)
        If (value <= 0) Then
            Exit Sub
        End If
        System.Threading.Thread.Sleep(value)
    End Sub
    Public Sub Fullscreen()

        hostForm.Width = clientWidth
        hostForm.Height = clientHeight
        hostForm.FormBorderStyle = FormBorderStyle.None
        hostForm.WindowState = FormWindowState.Maximized
        ScrnBufferBmp = New Bitmap(hostForm.Width, hostForm.Height, hostForm.CreateGraphics)
        GraphicsBuffer = Graphics.FromImage(ScrnBufferBmp)
    End Sub
    Private Sub reset()

        xOff = 0
        yOff = 0

    End Sub


    Private Sub Kill(ByVal sender As Object, ByVal e As System.EventArgs)
        End
    End Sub


    Private Sub reDraw(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs)
        e.Graphics.DrawImage(ScrnBufferBmp, 0, 0)
        Run()
    End Sub
    Private Sub mouseUpChecker(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        mouseIsClicked = False
    End Sub
    Private Sub mouseDownChecker(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        mouseIsClicked = True
    End Sub


End Module
