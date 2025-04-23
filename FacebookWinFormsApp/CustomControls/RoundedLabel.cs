using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FacebookDPApp.CustomControls
{
    public class RoundedLabel : Label
    {
        private int m_BorderRadius = 10;
        private Color m_BorderColor = Color.DodgerBlue;
        private int m_BorderSize = 1;
        private bool m_BorderVisible = true;
        private Color m_BackgroundColor = Color.White;

        public int BorderRadius
        {
            get { return m_BorderRadius; }
            set
            {
                if (value >= 0)
                {
                    m_BorderRadius = value;
                    Invalidate();
                }
            }
        }

        public Color BorderColor
        {
            get { return m_BorderColor; }
            set
            {
                m_BorderColor = value;
                Invalidate();
            }
        }

        public int BorderSize
        {
            get { return m_BorderSize; }
            set
            {
                if (value >= 0)
                {
                    m_BorderSize = value;
                    Invalidate();
                }
            }
        }

        public bool BorderVisible
        {
            get { return m_BorderVisible; }
            set
            {
                m_BorderVisible = value;
                Invalidate();
            }
        }

        public Color BackgroundColor
        {
            get { return m_BackgroundColor; }
            set
            {
                m_BackgroundColor = value;
                Invalidate();
            }
        }

        public RoundedLabel()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);

            BackColor = Color.Transparent;
            ForeColor = Color.Black;
            Size = new Size(100, 30);
            TextAlign = ContentAlignment.MiddleCenter;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            GraphicsPath path = createRoundedRectanglePath(ClientRectangle, m_BorderRadius);

            using (SolidBrush brush = new SolidBrush(m_BackgroundColor))
            {
                e.Graphics.FillPath(brush, path);
            }

            if (m_BorderVisible && m_BorderSize > 0)
            {
                using (Pen pen = new Pen(m_BorderColor, m_BorderSize))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }

            TextRenderer.DrawText(e.Graphics, Text, Font, ClientRectangle, ForeColor, getTextFormatFlags(TextAlign));

            path.Dispose();
        }

        private GraphicsPath createRoundedRectanglePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();

            int adjustedBorderSize = m_BorderVisible ? m_BorderSize : 0;
            Rectangle adjustedRect = new Rectangle(
                rect.X + adjustedBorderSize,
                rect.Y + adjustedBorderSize,
                rect.Width - (adjustedBorderSize * 2),
                rect.Height - (adjustedBorderSize * 2));

            int limitedRadius = Math.Min(radius, Math.Min(adjustedRect.Width, adjustedRect.Height) / 2);

            if (limitedRadius == 0)
            {
                path.AddRectangle(adjustedRect);
                return path;
            }

            int diameter = limitedRadius * 2;

            Rectangle topLeftCorner = new Rectangle(adjustedRect.X, adjustedRect.Y, diameter, diameter);
            Rectangle topRightCorner = new Rectangle(
                adjustedRect.X + adjustedRect.Width - diameter,
                adjustedRect.Y,
                diameter,
                diameter);
            Rectangle bottomLeftCorner = new Rectangle(
                adjustedRect.X,
                adjustedRect.Y + adjustedRect.Height - diameter,
                diameter,
                diameter);
            Rectangle bottomRightCorner = new Rectangle(
                adjustedRect.X + adjustedRect.Width - diameter,
                adjustedRect.Y + adjustedRect.Height - diameter,
                diameter,
                diameter);

            path.AddArc(topLeftCorner, 180, 90);
            path.AddArc(topRightCorner, 270, 90);
            path.AddArc(bottomRightCorner, 0, 90);
            path.AddArc(bottomLeftCorner, 90, 90);

            path.CloseAllFigures();

            return path;
        }

        private TextFormatFlags getTextFormatFlags(ContentAlignment alignment)
        {
            TextFormatFlags flags = TextFormatFlags.WordBreak;

            if ((alignment & (ContentAlignment.TopLeft | ContentAlignment.MiddleLeft | ContentAlignment.BottomLeft))
                != 0)
                flags |= TextFormatFlags.Left;
            else if ((alignment & (ContentAlignment.TopRight | ContentAlignment.MiddleRight
                                                             | ContentAlignment.BottomRight)) != 0)
                flags |= TextFormatFlags.Right;
            else
                flags |= TextFormatFlags.HorizontalCenter;

            if ((alignment & (ContentAlignment.TopLeft | ContentAlignment.TopCenter | ContentAlignment.TopRight)) != 0)
                flags |= TextFormatFlags.Top;
            else if ((alignment & (ContentAlignment.BottomLeft | ContentAlignment.BottomCenter
                                                               | ContentAlignment.BottomRight)) != 0)
                flags |= TextFormatFlags.Bottom;
            else
                flags |= TextFormatFlags.VerticalCenter;

            return flags;
        }
    }
}