using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FacebookDPApp.CustomControls
{
    public class RoundedButton : Button
    {
        private int m_BorderSize = 0;
        private int m_BorderRadius = 40;
        private Color m_BorderColor = Color.Transparent;

        public int BorderSize
        {
            get { return m_BorderSize; }
            set
            {
                m_BorderSize = value;
                this.Invalidate();
            }
        }

        public int BorderRadius
        {
            get { return m_BorderRadius; }
            set
            {
                if (this.Height >= value)
                {
                    m_BorderRadius = value;
                }
                else
                {
                    m_BorderRadius = this.Height;
                }

                this.Invalidate();
            }
        }

        public Color BorderColor
        {
            get { return m_BorderColor; }
            set
            {
                m_BorderColor = value;
                this.Invalidate();
            }
        }

        public Color BackgroundColor
        {
            get { return this.BackColor; }
            set
            {
                this.BackColor = value;
                this.Invalidate();
            }
        }

        public Color TextColor
        {
            get { return this.ForeColor; }
            set
            {
                this.ForeColor = value;
                this.Invalidate();
            }
        }

        public RoundedButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 1;
            this.Size = new Size(150, 40);
            this.BackgroundColor = SystemColors.ButtonHighlight;
            this.TextColor = Color.DodgerBlue;
            this.Resize += new EventHandler(Button_Resize);
        }

        private void Button_Resize(object sender, EventArgs e)
        {
            if (m_BorderRadius > this.Height)
            {
                BorderRadius = this.Height;
            }
        }

        private GraphicsPath getFigurePath(RectangleF i_Rect, float i_Radius)
        {
            GraphicsPath path = new GraphicsPath();

            path.StartFigure();
            path.AddArc(i_Rect.X, i_Rect.Y, i_Radius, i_Radius, 180, 90);
            path.AddArc(i_Rect.Width - i_Radius, i_Rect.Y, i_Radius, i_Radius, 270, 90);
            path.AddArc(i_Rect.Width - i_Radius, i_Rect.Height - i_Radius, i_Radius, i_Radius, 0, 90);
            path.AddArc(i_Rect.X, i_Rect.Height - i_Radius, i_Radius, i_Radius, 90, 90);
            path.CloseFigure();

            return path;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            RectangleF rectSurface = new RectangleF(0, 0, this.Width, this.Height);
            RectangleF rectBorder = new RectangleF(1, 1, this.Width - 0.8F, this.Height - 1);
            Pen penBorder = null;

            if (m_BorderRadius > 2)
            {
                GraphicsPath pathSurface = null;
                GraphicsPath pathBorder = null;
                Pen penSurface = null;

                try
                {
                    pathSurface = getFigurePath(rectSurface, m_BorderRadius);
                    pathBorder = getFigurePath(rectBorder, m_BorderRadius - 1);
                    penSurface = new Pen(this.Parent.BackColor, 2);
                    penBorder = new Pen(m_BorderColor, m_BorderSize);

                    this.Region = new Region(pathSurface);

                    pe.Graphics.DrawPath(penSurface, pathSurface);

                    if (m_BorderSize >= 1)
                    {
                        pe.Graphics.DrawPath(penBorder, pathBorder);
                    }
                }
                finally
                {
                    penBorder?.Dispose();
                    penSurface?.Dispose();
                    pathBorder?.Dispose();
                    pathSurface?.Dispose();
                }
            }
            else
            {
                this.Region = new Region(rectSurface);

                if (m_BorderSize >= 1)
                {
                    try
                    {
                        penBorder = new Pen(m_BorderColor, m_BorderSize);

                        penBorder.Alignment = PenAlignment.Inset;
                        pe.Graphics.DrawRectangle(penBorder, 0, 0, this.Width - 1, this.Height - 1);
                    }
                    finally
                    {
                        penBorder?.Dispose();
                    }
                }
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            this.Parent.BackColorChanged += new EventHandler(Container_BackColorChanged);
        }

        private void Container_BackColorChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}