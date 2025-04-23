using System.Drawing;
using System.Windows.Forms;

namespace FacebookDPApp.CustomControls
{
    public class CustomTabControl : TabControl
    {
        public CustomTabControl()
        {
            this.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.DrawItem += customTabControl_DrawItem;
        }

        private void customTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabControl tabControl = (TabControl)sender;
            Rectangle tabRect = tabControl.GetTabRect(e.Index);
            SolidBrush backBrush = new SolidBrush(tabControl.TabPages[e.Index].BackColor);

            e.Graphics.FillRectangle(backBrush, tabRect);

            if (ImageList != null)
            {
                int imageIndex = TabPages[e.Index].ImageIndex;

                if (imageIndex >= 0)
                {
                    Image image = tabControl.ImageList.Images[tabControl.TabPages[e.Index].ImageIndex];
                    float x = tabRect.Left + (tabRect.Width - image.Width) / 2.0f;
                    float y = tabRect.Top + (tabRect.Height - image.Height) / 2.0f;

                    e.Graphics.DrawImage(image, x, y);
                }
            }
        }
    }
}