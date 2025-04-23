using System;
using FacebookWrapper.ObjectModel;

namespace FacebookDPApp.Backend
{
    public sealed class UserDataManager
    {
        private static UserDataManager s_Instance;
        private static readonly object sr_LockObject = new object();

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
        
        private UserDataManager()
        {
            UserInfo = new FacebookObjectCollection<string>();
            UserFriends = new FacebookObjectCollection<User>();
        }

        public static UserDataManager Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    lock (sr_LockObject)
                    {
                        if (s_Instance == null)
                        {
                            s_Instance = new UserDataManager();
                        }
                    }
                }

                return s_Instance;
            }
            
        }

        public void InitUserData(User i_LoggedInUser)
        {
            if (i_LoggedInUser == null)
            {
                throw new ArgumentException(nameof(i_LoggedInUser), "User cannot be null");
            }

            try
            {
                LoggedInUser = i_LoggedInUser;
                UserName = LoggedInUser.Name ?? string.Empty;

                setUserLocation();
                setUserBirthdayAndAge();
                setUserGender();
                setCoverPhoto();
                setProfilePhoto();
                setUserFriendsList();
                loadUserData();
            }
            catch (Exception ex)
            {
                initUserWithDefaultValues();
                throw new Exception("Failed to initialize user data", ex);
            }
        }

        private void initUserWithDefaultValues()
        {
            UserName = string.Empty;
            UserLocation = string.Empty;
            UserBirthday = DateTime.Now.ToShortDateString();
            UserGender = "Not specified";
            UserProfilePicURL = string.Empty;
            UserCoverPicURL = string.Empty;
            UserAge = 0;

            UserInfo = new FacebookObjectCollection<string>();
            UserFriends = new FacebookObjectCollection<User>();
        }

        private void setUserLocation()
        {
            try
            {
                UserLocation = LoggedInUser.Location.Name;
            }
            catch (Exception)
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

                if (birthDate.Month > today.Month || (birthDate.Month == today.Month && birthDate.Day > today.Day))
                {
                    UserAge--;
                }
            }
            catch (Exception)
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
            }
            catch (Exception)
            {
                UserGender = "Not specified";
            }
        }

        private void setCoverPhoto()
        {
            string coverPhotoURL = string.Empty;

            foreach(Album photoAlbum in LoggedInUser.Albums)
            {
                if(photoAlbum.Name == "Cover photos")
                {
                    coverPhotoURL = photoAlbum.Photos[0].PictureNormalURL;
                    break;
                }
            }

            UserCoverPicURL = coverPhotoURL;
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
                foreach (User friend in LoggedInUser.Friends)
                {
                    UserFriends.Add(friend);
                }
            }
            catch (Exception)
            {
                UserFriends = null;
            }
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
    }
}
