using FacebookDPApp.Backend;
using FacebookWrapper.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicFacebookFeatures.Backend
{
    public sealed class FacbookServiceFacade
    {
        private static FacbookServiceFacade s_Instance;
        private static readonly object sr_LockObject = new object();
        public User LoggedInUserFacade { get; private set; }
        private readonly List<MyPost> r_PostsList = new List<MyPost>();
        private AlbumSlideShow m_AlbumSlideShowManager;// Ido- its not readOnly Now, not sure it is suppose to be here


        public static FacbookServiceFacade Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    lock (sr_LockObject)
                    {
                        if (s_Instance == null)
                        {
                            s_Instance = new FacbookServiceFacade();
                        }
                    }
                }

                return s_Instance;
            }
        }

        public void InitFacbookServiceFacade(User i_LoggedInUser, PictureBox i_PictureBoxAlbums)// not good to send in pictureBox
        {
            if (i_LoggedInUser == null)
            {
                throw new ArgumentException(nameof(i_LoggedInUser), "User cannot be null");
            }

            try
            {
                LoggedInUserFacade = i_LoggedInUser;
                setUserPostsList();
                setUserAlbumsList(i_PictureBoxAlbums);
            }
            catch (Exception ex) 
            {
                throw new Exception("Failed to initialize user facade", ex);
            }

        }

        private void setUserAlbumsList(PictureBox i_PictureBoxAlbums)
        {
            m_AlbumSlideShowManager = new AlbumSlideShow(i_PictureBoxAlbums);
        }

        private void setUserPostsList()
        {
            if (LoggedInUserFacade.Posts.Count == 0)
            {
                //???
            }
            else
            {
                foreach (Post post in LoggedInUserFacade.Posts)
                {
                    if (post.Message != null)
                    {
                        r_PostsList.Add(new MyPost(post.Message, post.CreatedTime ?? DateTime.Now));
                    }
                }
            }
        }

        public List<MyPost> GetUserPosts()
        {             
            return r_PostsList;
        }

        public AlbumSlideShow GetAlbumSlideShow()
        {
            return m_AlbumSlideShowManager;
        }

        public FacebookObjectCollection<Album> GetUserAlbums()
        {
            return LoggedInUserFacade.Albums;
        }

        public void StopSlideShow()
        {
            m_AlbumSlideShowManager.StopSlideshow();
        }

        public void AddPost(string i_PostText)
        {
            r_PostsList.Insert(0,new MyPost(i_PostText,DateTime.Now));
        }

        public void Sort( int i_SortingChoice)
        {
            r_PostsList.Sort(new PostSorter(i_SortingChoice));
        }

        //public void StartSlidesShow(List<Photo> photos)
        //{
        //    m_AlbumSlideShowManager.StartSlideshow(photos);
        //}

        public async void StartSlidesShow(Album i_SelectedAlbum)
        {
            List<Photo> photos = await fetchPhotosFromSelectedAlbum(i_SelectedAlbum);
            m_AlbumSlideShowManager.StartSlideshow(photos);
        }

        private async Task<List<Photo>> fetchPhotosFromSelectedAlbum(Album i_SelectedAlbum)
        {
            List<Photo> photosUrl = new List<Photo>();

            if (i_SelectedAlbum != null)
            {
                foreach (Photo photo in i_SelectedAlbum.Photos)
                {
                    photosUrl.Add(photo);
                }
            }

            return photosUrl;
        }
    }
}
