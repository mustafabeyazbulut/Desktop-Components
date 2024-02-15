using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

public class RoundedTextBox : TextBox
{
    private int radius = 20; // Köşe yarıçapını ayarlayın
    private int padding = 10; // Sol ve sağ kenarlardan içeri doğru uzaklık

    [Category("Özellikler")]
    [Browsable(true)]
    public int Radius
    {
        get { return radius; }
        set
        {
            radius = value;
            UpdateRegion();
        }
    }

    public RoundedTextBox()
    {
        UpdateRegion();
        this.Font = new System.Drawing.Font(this.Font.FontFamily, 20);
        this.ForeColor = Color.Black;
        this.Size = new Size(300, 50); // Örnek bir boyut, ihtiyaca göre değiştirilebilir
    }

    private void UpdateRegion()
    {
        System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();

        // Köşeleri yuvarlanmış bir dikdörtgenin yolunu oluşturun
        path.AddArc(0, 0, radius * 2, radius * 2, 180, 90);
        path.AddLine(radius, 0, this.Width - radius, 0);
        path.AddArc(this.Width - 2 * radius, 0, radius * 2, radius * 2, 270, 90);
        path.AddLine(this.Width, radius, this.Width, this.Height - radius);
        path.AddArc(this.Width - 2 * radius, this.Height - 2 * radius, radius * 2, radius * 2, 0, 90);
        path.AddLine(this.Width - radius, this.Height, radius, this.Height);
        path.AddArc(0, this.Height - 2 * radius, radius * 2, radius * 2, 90, 90);
        path.AddLine(0, this.Height - radius, 0, radius);

        // Yolu kullanarak TextBox'u çizin
        this.Region = new System.Drawing.Region(path);


        this.Invalidate();
    }

    protected override void OnSizeChanged(EventArgs e)
    {
        base.OnSizeChanged(e);
        UpdateRegion();
    }
}
