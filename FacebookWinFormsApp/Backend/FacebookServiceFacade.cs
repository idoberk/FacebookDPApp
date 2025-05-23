using FacebookWrapper.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacebookDPApp.Backend
{
    public sealed class FacebookServiceFacade
    {
        // private static FacebookServiceFacade s_Instance;
        private static readonly object sr_LockObject = new object();

        private readonly List<IDataFetchedObserver> r_DataFetchedObservers = new List<IDataFetchedObserver>();

        public User LoggedInUser { get; private set; }

        public FacebookObjectCollection<string> UserInfo { get; private set; }

        public FacebookObjectCollection<User> UserFriends { get; private set; }

        public string UserName { get; private set; }

        public string UserLocation { get; private set; }

        public string UserBirthday { get; private set; }

        public string UserGender { get; private set; }

        public string UserProfilePicURL { get; private set; }

        public string UserCoverPicURL { get; private set; }

        public int UserAge { get; private set; }

        private readonly List<MyPost> r_PostsList = new List<MyPost>();

        private AlbumSlideShow m_AlbumSlideShowManager;

        public event EventHandler<PhotoChangedEventArgs> PhotoChanged;

        public FacebookServiceFacade()
        {
            
        }

        //private FacebookServiceFacade()
        //{
        //    UserInfo = new FacebookObjectCollection<string>();
        //    UserFriends = new FacebookObjectCollection<User>();
        //}

        //public static FacebookServiceFacade Instance
        //{
        //    get
        //    {
        //        if (s_Instance == null)
        //        {
        //            lock (sr_LockObject)
        //            {
        //                if (s_Instance == null)
        //                {
        //                    s_Instance = new FacebookServiceFacade();
        //                }
        //            }
        //        }

        //        return s_Instance;
        //    }
        //}

        public void AttachObserver(IDataFetchedObserver i_DataFetchedObserver)
        {
            r_DataFetchedObservers.Add(i_DataFetchedObserver);
        }

        public void DetachObserver(IDataFetchedObserver i_DataFetchedObserver)
        {
            r_DataFetchedObservers.Remove(i_DataFetchedObserver);
        }

        private void doWhenDataFetched()
        {
            notifyAllDataFetchedObservers();
        }

        private void notifyAllDataFetchedObservers()
        {
            foreach (IDataFetchedObserver observer in r_DataFetchedObservers)
            {
                observer.AllDataFetched();
            }
        }

        public void InitFacebookServiceFacade(User i_LoggedInUser)
        {
            if (i_LoggedInUser == null)
            {
                throw new ArgumentException(nameof(i_LoggedInUser), "User cannot be null");
            }

            try
            {
                LoggedInUser = i_LoggedInUser;
                UserName = LoggedInUser.Name ?? string.Empty;
                UserProfilePicURL = LoggedInUser.PictureNormalURL;

                UserCoverPicURL = setCoverPhoto();

                setUserLocation();
                setUserBirthdayAndAge();
                setUserGender();
                // setProfilePhoto();
                setUserFriendsList();
                loadUserData();
                setUserPostsList();
                setUserAlbumsList();
                doWhenDataFetched();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to initialize user data", ex);
            }
        }

        private void setUserLocation()
        {
            try
            {
                UserLocation = LoggedInUser.Location?.Name ?? string.Empty;
            } catch(Exception)
            {
                UserLocation = string.Empty;
            }
        }

        private void setUserBirthdayAndAge()
        {
            try
            {
                UserBirthday = LoggedInUser.Birthday;
                DateTime birthDate = DateTime.ParseExact(UserBirthday, "MM/dd/yyyy", null);
                DateTime today = DateTime.Today;

                UserAge = today.Year - birthDate.Year;

                if(birthDate.Month > today.Month || (birthDate.Month == today.Month && birthDate.Day > today.Day))
                {
                    UserAge--;
                }
            } catch(Exception)
            {
                UserAge = 0;
                UserBirthday = DateTime.Now.ToShortDateString();
            }
        }

        private void setUserGender()
        {
            try
            {
                UserGender = LoggedInUser.Gender.ToString();
            } catch(Exception)
            {
                UserGender = "Not specified";
            }
        }

        private string setCoverPhoto()
        {
            string coverPhotoURL = string.Empty;

            foreach(Album photoAlbum in LoggedInUser.Albums)
            {
                if(photoAlbum.Name == "Cover photos" || photoAlbum.Name == "תמונות נושא")
                {
                    coverPhotoURL = photoAlbum.Photos[0].PictureNormalURL;
                    break;
                }
            }

            return coverPhotoURL;
        }

        private void setProfilePhoto()
        {
            UserProfilePicURL = LoggedInUser.PictureNormalURL;
        }

        private void setUserFriendsList()
        {
            UserFriends = new FacebookObjectCollection<User>();

            try
            {
                foreach(User friend in LoggedInUser.Friends)
                {
                    UserFriends.Add(friend);
                }
            } catch(Exception)
            {
                UserFriends = null;
            }
        }

        public FacebookObjectCollection<User> GetUserFriendsList()
        {
            return UserFriends;
        }

        public List<string> GetUserFriendsNameList()
        {
            List<string> userFriendsNameList;

            if(UserFriends.Count == 0)
            {
                userFriendsNameList = new List<string> { "No friends found!" };
            } else
            {
                userFriendsNameList = UserFriends.Select(friend => friend.Name).ToList();
            }

            return userFriendsNameList;
        }

        private void loadUserData()
        {
            UserInfo = new FacebookObjectCollection<string>
                           {
                               $"Lives in: {UserLocation}",
                               $"Birthday: {UserBirthday}",
                               $"Age: {UserAge}",
                               $"Gender: {UserGender}"
                           };
        }

        private void setUserAlbumsList()
        {
            m_AlbumSlideShowManager = new AlbumSlideShow();
            m_AlbumSlideShowManager.PhotoChanged += OnAlbumSlideShowPhotoChanged;
        }

        private void OnAlbumSlideShowPhotoChanged(object sender, PhotoChangedEventArgs e)
        {
            PhotoChanged?.Invoke(this, new PhotoChangedEventArgs(e.Photo));
        }

        private void setUserPostsList()
        {
            if (LoggedInUser.Posts.Count != 0)
            {
                foreach (Post post in LoggedInUser.Posts)
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
            return LoggedInUser.Albums;
        }

        public void StopSlideShow()
        {
            m_AlbumSlideShowManager?.StopSlideshow();
        }

        public void AddPost(string i_PostText)
        {
            r_PostsList.Insert(0, new MyPost(i_PostText, DateTime.Now));
        }

        public void Sort(SortComponent<MyPost> i_Sorter)
        {
            r_PostsList.Sort(i_Sorter);
        }

        public async void StartSlideShow(Album i_SelectedAlbum)
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