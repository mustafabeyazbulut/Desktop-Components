using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

public class RoundedButton : Button
{
    private int radius = 20; // Köşe yarıçapını ayarlayın
    private int padding = 10; // Sol ve sağ kenarlardan içeri doğru uzaklık
    private Color borderColor = Color.Black; // Kenar çizgisi rengi
    private int borderWidth = 2; // Kenar çizgisi kalınlığı
    private bool showBorder = true; // Kenar çizgisini göster/gizle
    private bool borderOnRadius = true; // Radius köşelere kenar çizgisi eklesin/eklemesin

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

    [Category("Özellikler")]
    [Browsable(true)]
    public Color BorderColor
    {
        get { return borderColor; }
        set
        {
            borderColor = value;
            this.Invalidate();
        }
    }

    [Category("Özellikler")]
    [Browsable(true)]
    public int BorderWidth
    {
        get { return borderWidth; }
        set
        {
            borderWidth = value;
            this.Invalidate();
        }
    }

    [Category("Özellikler")]
    [Browsable(true)]
    public bool ShowBorder
    {
        get { return showBorder; }
        set
        {
            showBorder = value;
            this.Invalidate();
        }
    }

    [Category("Özellikler")]
    [Browsable(true)]
    public bool BorderOnRadius
    {
        get { return borderOnRadius; }
        set
        {
            borderOnRadius = value;
            this.Invalidate();
        }
    }

    public RoundedButton()
    {
        UpdateRegion();
        this.Font = new System.Drawing.Font(this.Font.FontFamily, 14); // İhtiyaca göre değiştirilebilir
        this.Size = new Size(150, 50); // İhtiyaca göre değiştirilebilir
        this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
    }

    private void UpdateRegion()
    {
        System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();

        if (borderOnRadius)
        {
          path=  GetRoundedRectanglePath();
        }
        else
        {
            // Düz bir dikdörtgenin yolunu oluşturun
            path.AddRectangle(new Rectangle(0, 0, this.Width, this.Height));
        }

        // Yolu kullanarak Button'u çizin
        this.Region = new System.Drawing.Region(path);

        this.Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        if (showBorder)
        {
            // Kenar çizgisini çizin
            using (Pen pen = new Pen(borderColor, borderWidth))
            {
                e.Graphics.DrawPath(pen, GetRoundedRectanglePath());
            }
        }
    }

    private System.Drawing.Drawing2D.GraphicsPath GetRoundedRectanglePath()
    {
        System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();

        // Köşeleri yuvarlanmış bir dikdörtgenin yolunu oluşturun
        path.AddArc(0, 0, radius * 2, radius * 2, 180, 90); // sol üst kenar
        path.AddLine(radius-10, 0, this.Width - radius, 0);
        path.AddArc(this.Width - 2 * radius, 0, radius * 2, radius * 2, 270, 90);// sağ üst kenar
        path.AddLine(this.Width, radius, this.Width, this.Height - radius);
        path.AddArc(this.Width - 2 * radius, this.Height - 2 * radius, radius * 2, radius * 2, 0, 90); // sağ alt kenar
        path.AddLine(this.Width - radius, this.Height, radius, this.Height);
        path.AddArc(0, this.Height - 2 * radius, radius * 2, radius * 2, 90, 90);// sol alt kenar
        path.AddLine(0, this.Height - radius, 0, radius);

        return path;
    }

    protected override void OnSizeChanged(EventArgs e)
    {
        base.OnSizeChanged(e);
        UpdateRegion();
    }
}
