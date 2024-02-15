using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;

namespace WindowsFormsApplication1
{
    public partial class ImageGroup : UserControl
    {
        private const int shadowOffset = 5;

        public ImageGroup()
        {
            InitializeComponent();
            this.Resize += new EventHandler(ImageGroup_Resize);
        }

        private void ImageGroup_Load(object sender, EventArgs e)
        {
            RoundCorners();
        }

        private void ImageGroup_Resize(object sender, EventArgs e)
        {
            RoundCorners();
        }

        private void RoundCorners()
        {
            GraphicsPath path = new GraphicsPath();
            path.AddLine(radius, 0, this.Width - radius, 0);
            path.AddArc(this.Width - radius, 0, radius, radius, 270, 90);
            path.AddLine(this.Width, radius, this.Width, this.Height - radius);
            path.AddArc(this.Width - radius, this.Height - radius, radius, radius, 0, 90);
            path.AddLine(this.Width - radius, this.Height, radius, this.Height);
            path.AddArc(0, this.Height - radius, radius, radius, 90, 90);
            path.AddLine(0, this.Height - radius, 0, radius);
            path.AddArc(0, 0, radius, radius, 180, 90);
            this.Region = new Region(path);
        }

        #region  İmport

        private int radius = 50;
        [Category("Özellikler")]
        [Browsable(true)]
        public int Radius
        {
            get { return radius; }
            set
            {
                radius = value;
                ImageGroup_Resize(this, EventArgs.Empty);
            }
        }

        private int labelSize = 15;
        [Category("Özellikler")]
        [Browsable(true)]
        public int LabelSize
        {
            get { return labelSize; }
            set
            {
                labelSize = value;
                label1.Font = new Font(label1.Font.FontFamily, labelSize);
            }
        }

        private Image resim;
        [Category("Özellikler")]
        [Browsable(true)]
        public Image Resim
        {
            get { return resim; }
            set
            {
                resim = value;
                ovalPictureBox1.Image = resim;
            }
        }

        private string text = "ImageGroup";
        [Category("Özellikler")]
        [Browsable(true)]
        public string Texti
        {
            get { return text; }
            set
            {
                text = value;
                label1.Text = text;
            }
        }
        private Color imageBackColor = Color.White;
        [Category("Özellikler")]
        [Browsable(true)]
        public Color ImageBackColor
        {
            get { return imageBackColor; }
            set
            {
                imageBackColor = value;
                ovalPictureBox1.BackColor = value;
            }
        }

        private Color imageGroupBackColor = Color.White;
        [Category("Özellikler")]
        [Browsable(true)]
        public Color ImageGroupBackColor
        {
            get { return imageGroupBackColor; }
            set
            {
                imageGroupBackColor = value;
                this.BackColor = value;
            }
        }
        private Color imageGroupHoverColor = Color.Red;
        [Category("Özellikler")]
        [Browsable(true)]
        public Color ImageGroupHoverColor
        {
            get { return imageGroupHoverColor; }
            set
            {
                imageGroupHoverColor = value;
            }
        }

        private Color textColor = Color.Black; // or any default color
        [Category("Özellikler")]
        [Browsable(true)]
        public Color TextColor
        {
            get { return textColor; }
            set
            {
                textColor = value;
                label1.ForeColor = value;
            }
        }
        private PictureBoxSizeMode pictureBoxSizeMode = PictureBoxSizeMode.Zoom;
        [Category("Özellikler")]
        [Browsable(true)]
        public PictureBoxSizeMode PictureBoxSizeMode
        {
            get { return pictureBoxSizeMode; }
            set
            {
                pictureBoxSizeMode = value;
                ovalPictureBox1.SizeMode = pictureBoxSizeMode;
            }
        }
       
        #endregion

        #region mause event
        private void ChangeColors(Color color)
        {
            this.BackColor = color;
            ovalPictureBox1.BackColor = color;
            label1.BackColor = color;
            if (imageGroupBackColor==color)
            {
                ovalPictureBox1.BackColor = imageBackColor;
            }
        }
        private void ImageGroup_MouseMove(object sender, MouseEventArgs e)
        {
            ChangeColors(imageGroupHoverColor);
        }

        private bool IsMouseOverControl(Control control)
        {
            Point localCursor = control.PointToClient(Cursor.Position);
            return control.ClientRectangle.Contains(localCursor);
        }
        private void ImageGroup_MouseLeave(object sender, EventArgs e)
        {
            if (IsMouseOverControl(this))
            {
                ChangeColors(imageGroupHoverColor);
            }
            else
            {
                ChangeColors(imageGroupBackColor);
            }
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            ChangeColors(imageGroupHoverColor);

        }
        private void label1_MouseLeave(object sender, EventArgs e)
        {
            ChangeColors(imageGroupBackColor);

        }

        private void ovalPictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            ChangeColors(imageGroupHoverColor);

        }

        private void ovalPictureBox1_MouseLeave(object sender, EventArgs e)
        {
            ChangeColors(imageGroupBackColor);

        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            ChangeColors(imageGroupBackColor);

        }

        private void label2_MouseMove(object sender, MouseEventArgs e)
        {
            ChangeColors(imageGroupHoverColor);

        }
        private void ovalPictureBox1_Click(object sender, EventArgs e)
        {
            OnClick(e);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            OnClick(e);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            OnClick(e);

        }
        #endregion
    }
}

class OvalPictureBox : PictureBox
{
    private bool isMouseOver = false;
    private int originalWidth;
    private int originalHeight;

    public OvalPictureBox()
    {
        this.BackColor = Color.White;
        this.Paint += OvalPictureBox_Paint;
        this.SizeMode = PictureBoxSizeMode.Zoom;
        this.MouseEnter += OvalPictureBox_MouseEnter;
        this.MouseLeave += OvalPictureBox_MouseLeave;
    }

    private void OvalPictureBox_MouseEnter(object sender, EventArgs e)
    {
        isMouseOver = true;
        SaveOriginalSize();
        ScalePictureBox(1.2f);
    }

    private void OvalPictureBox_MouseLeave(object sender, EventArgs e)
    {
        isMouseOver = false;
        ScalePictureBox(1.0f);
    }

    private void SaveOriginalSize()
    {
        originalWidth = this.Width;
        originalHeight = this.Height;
    }

    private void ScalePictureBox(float scaleFactor)
    {
        int newSizeWidth = (int)(originalWidth * scaleFactor);
        int newSizeHeight = (int)(originalHeight * scaleFactor);

        using (var gp = new GraphicsPath())
        {
            gp.AddEllipse(new Rectangle(0, 0, newSizeWidth - 1, newSizeHeight - 1));
            this.Region = new Region(gp);
        }
    }
    private void OvalPictureBox_Paint(object sender, PaintEventArgs e)
    {
        if (!isMouseOver)
        {
            using (var gp = new GraphicsPath())
            {
                gp.AddEllipse(new Rectangle(0, 0, this.Width - 1, this.Height - 1));
                this.Region = new Region(gp);
            }
        }
        else
        {
            ScalePictureBox(1.2f);
        }
    }
}