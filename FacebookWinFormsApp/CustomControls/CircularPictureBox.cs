using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FacebookDPApp.CustomControls
{
    public class CircularPictureBox : PictureBox
    {
        private int m_BorderSize = 2;
        private Color m_FirstBorderColor = Color.DimGray;
        private Color m_SecondBorderColor = Color.Black;
        private DashStyle m_BorderLineStyle = DashStyle.Solid;
        private DashCap m_BorderCapStyle = DashCap.Flat;
        private float m_GradientAngle = 50F;

        public CircularPictureBox()
        {
            this.Size = new Size(150, 150);
            this.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public int BorderSize
        {
            get
            {
                return m_BorderSize;
            }
            set
            {
                m_BorderSize = value;
                this.Invalidate();
            }
        }

        public DashStyle BorderLineStyle
        {
            get
            {
                return m_BorderLineStyle;
            }
            set
            {
                m_BorderLineStyle = value;
                this.Invalidate();
            }
        }

        public DashCap BorderCapStyle
        {
            get
            {
                return m_BorderCapStyle;
            }
            set
            {
                m_BorderCapStyle = value;
                this.Invalidate();
            }
        }

        public float GradientAngle
        {
            get
            {
                return m_GradientAngle;
            }
            set
            {
                m_GradientAngle = value;
                this.Invalidate();
            }
        }

        public Color FirstBorderColor
        {
            get
            {
                return m_FirstBorderColor;
            }
            set
            {
                m_FirstBorderColor = value;
                this.Invalidate();
            }
        }

        public Color SecondBorderColor
        {
            get
            {
                return m_SecondBorderColor;
            }
            set
            {
                m_SecondBorderColor = value;
                this.Invalidate();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Size = new Size(this.Width, this.Width);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Graphics graph = pe.Graphics;
            Rectangle rectContourSmooth = Rectangle.Inflate(this.ClientRectangle, 0, 0);
            Rectangle rectBorder = Rectangle.Inflate(rectContourSmooth, 0, 0);
            int smoothSize = m_BorderSize > 0 ? m_BorderSize * 3 : 1;
            LinearGradientBrush borderGColor = new LinearGradientBrush(
                rectBorder,
                m_FirstBorderColor,
                m_SecondBorderColor,
                m_GradientAngle);
            GraphicsPath pathRegion = new GraphicsPath();
            Pen penSmooth = new Pen(this.Parent.BackColor, smoothSize);
            Pen penBorder = new Pen(borderGColor, m_BorderSize);

            graph.SmoothingMode = SmoothingMode.AntiAlias;

            try
            {
                penBorder.DashStyle = BorderLineStyle;
                penBorder.DashCap = BorderCapStyle;
                pathRegion.AddEllipse(rectContourSmooth);

                this.Region = new Region(pathRegion);

                graph.DrawEllipse(penSmooth, rectContourSmooth);

                if (m_BorderSize > 0)
                {
                    graph.DrawEllipse(penBorder, rectBorder);
                }
            }
            finally
            {
                borderGColor.Dispose();
                pathRegion.Dispose();
                penSmooth.Dispose();
                penBorder.Dispose();
            }
        }
    }
}