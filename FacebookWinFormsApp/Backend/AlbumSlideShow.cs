using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;

namespace FacebookDPApp.Backend
{
    public class AlbumSlideShow : ListBox
    {
        private readonly Timer r_Timer;
        private List<Photo> m_PhotoList;
        private int m_CurrentPhotoIndex;
        private readonly PictureBox r_PictureBox;
        private const int k_SlideShowIntervalMs = 3000;

        public AlbumSlideShow(PictureBox i_PictureBox)
        {
            r_PictureBox = i_PictureBox;
            m_CurrentPhotoIndex = 0;
            m_PhotoList = new List<Photo>();

            r_Timer = new Timer();

            r_Timer.Interval = k_SlideShowIntervalMs;
            r_Timer.Tick += Timer_Tick;
        }

        public void StartSlideshow(List<Photo> i_PhotoList)
        {
            if (i_PhotoList == null || i_PhotoList.Count == 0)
            {
                MessageBox.Show("No photos available in the album.");
                return;
            }

            m_PhotoList = i_PhotoList;
            m_CurrentPhotoIndex = 0;
            displayCurrentPhoto();
            r_Timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (m_PhotoList.Count > 0)
            {
                m_CurrentPhotoIndex = (m_CurrentPhotoIndex + 1) % m_PhotoList.Count;
                displayCurrentPhoto();
            }
        }

        private void displayCurrentPhoto()
        {
            if (m_PhotoList.Count > 0)
            {
                r_PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                r_PictureBox.LoadAsync(m_PhotoList[m_CurrentPhotoIndex].PictureNormalURL);
            }
        }

        public void StopSlideshow()
        {
            r_Timer.Stop();
        }
    }
}