# bGraphics
مودل لرسم الجرافكس بسهولة على فيجوال بيسك

# Usage / طريقة الاستخدام
```VB
  Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
     
        loadGraphics() 'يتم استدعاء وتشييد لوحة الجرافكس
  
    End Sub
      Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
      'هنا يتم رسم الجرافكس
        Background(51)  هذا كود يغير لود الخلفية
        
    End Sub
```
