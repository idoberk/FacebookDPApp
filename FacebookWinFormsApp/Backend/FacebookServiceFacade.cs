using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;

namespace FacebookDPApp.Backend
{
    public class FacebookServiceFacade
    {
        private readonly User r_LoggedInUser;
        private List<MyPost> m_PostsList;

        public FacebookServiceFacade(User i_LoggedInUser)
        {
            r_LoggedInUser = i_LoggedInUser;
            m_PostsList = new List<MyPost>();
        }

        public List<Album> GetUserAlbums()
        {
            List<Album> albumsList = new List<Album>();

            try
            {
                if (r_LoggedInUser.Albums != null)
                {
                    foreach (Album album in r_LoggedInUser.Albums)
                    {
                        albumsList.Add(album);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching albums: {ex.Message}");
            }

            return albumsList;
        }

        public List<MyPost> GetUserPosts()
        {
            m_PostsList.Clear();

            try
            {
                if (r_LoggedInUser.Posts != null && r_LoggedInUser.Posts.Count > 0)
                {
                    foreach (Post post in r_LoggedInUser.Posts)
                    {
                        if (post.Message != null)
                        {
                            m_PostsList.Add(new MyPost(post.Message, post.CreatedTime ?? DateTime.Now));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching posts: {ex.Message}");
            }

            return m_PostsList;
        }

        //public List<User> GetUserFriends()
        //{
        //    List<User> friendsList = new List<User>();

        //    try
        //    {
        //        if (r_LoggedInUser.Friends != null && r_LoggedInUser.Friends.Count > 0)
        //        {
        //            foreach (User friend in r_LoggedInUser.Friends)
        //            {
        //                friendsList.Add(friend);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error fetching friends: {ex.Message}");
        //    }

        //    return friendsList;
        //}
    }
}