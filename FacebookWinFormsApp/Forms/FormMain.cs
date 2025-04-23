using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FacebookDPApp.Backend;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;

namespace FacebookDPApp.Forms
{
    public partial class FormMain : Form
    {
        private const string k_TextBoxPostPlaceHolderText = "What's on your mind? Share your thoughts with the community!";
        private const string k_TextBoxSearchFriendsPlaceHolderText = "Search Friends...";

        private readonly User r_LoggedInUser;
        private readonly List<MyPost> r_PostsList = new List<MyPost>();
        private readonly AlbumSlideShow r_AlbumSlideShowManager;
        private HigherLowerGameManager m_GameManager;
        private UserDataManager m_UserDataManager;

        public FormMain(User i_LoggedInUser)
        {
            InitializeComponent();
            r_LoggedInUser = i_LoggedInUser;

            textBoxFillStatus.LostFocus += textBoxFillStatus_LostFocus;

            initUserDataManager();
            r_AlbumSlideShowManager = new AlbumSlideShow(pictureBoxAlbums);
        }

        private void initUserDataManager()
        {
            m_UserDataManager = UserDataManager.Instance;

            m_UserDataManager.InitUserData(r_LoggedInUser);
            fetchFriends();
        }

        private void fetchAlbums()
        {
            if (r_LoggedInUser.Albums != null)
            {
                listBoxAlbums.DisplayMember = "Name";
                foreach (Album album in r_LoggedInUser.Albums)
                {
                    listBoxAlbums.Items.Add(album);
                }
            }
        }

        private void fetchPosts()
        {
            if (r_LoggedInUser.Posts.Count == 0)
            {
                MessageBox.Show("No Posts to load");
            }
            else
            {
                foreach (Post post in r_LoggedInUser.Posts)
                {
                    if (post.Message != null)
                    {
                        r_PostsList.Add(new MyPost(post.Message, post.CreatedTime ?? DateTime.Now));
                    }
                }

                listBoxPosts.DisplayMember = "Name";
                updatePostList();
            }
        }

        private void fetchFriends()
        {
            if (m_UserDataManager.UserFriends.Count == 0)
            {
                listBoxFriendsList.Items.Add("No friends found!");

                return;
            }

            listBoxFriendsList.Items.Clear();

            foreach (User friend in m_UserDataManager.UserFriends)
            {
                listBoxFriendsList.Items.Add(friend.Name);
            }
        }

        private void updatePostList()
        {
            listBoxPosts.Items.Clear();

            foreach (MyPost myPost in r_PostsList)
            {
                if (myPost.Message != null)
                {
                    listBoxPosts.Items.Add(myPost.Message);
                }
            }
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            FacebookService.Logout();
            // FacebookService.LogoutWithUI();
            r_AlbumSlideShowManager.StopSlideshow();
            pictureBoxAlbums.Visible = false;
            this.Invoke(new Action(this.Close));
        }

        private async void listBoxAlbums_SelectedIndexChanged(object sender, EventArgs e)
        {
            r_AlbumSlideShowManager.StopSlideshow();

            if (listBoxAlbums.SelectedItem is Album selectedAlbum)
            {
                List<Photo> photos = await fetchPhotosFromSelectedAlbum(selectedAlbum);
                r_AlbumSlideShowManager.StartSlideshow(photos);
            }
        }

        private void textBoxFillStatus_Clicked(object sender, MouseEventArgs e)
        {
            if (textBoxFillStatus.Text == k_TextBoxPostPlaceHolderText)
            {
                textBoxFillStatus.Text = string.Empty;
                textBoxFillStatus.ForeColor = Color.Black;
            }
        }

        private void buttonSubmitPost_Click(object sender, EventArgs e)
        {
            string newPostText = textBoxFillStatus.Text.Trim();

            if (string.IsNullOrEmpty(newPostText) || newPostText == k_TextBoxPostPlaceHolderText)
            {
                MessageBox.Show("Cannot add an empty post.");

                return;
            }

            r_PostsList.Insert(0, new MyPost(newPostText, DateTime.Now));
            updatePostList();
            resetTextBoxFillStatusStyle();
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

        private void resetTextBoxFillStatusStyle()
        {
            textBoxFillStatus.Text = k_TextBoxPostPlaceHolderText;
            textBoxFillStatus.ForeColor = SystemColors.ScrollBar;
        }

        private void comboBoxSortingOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            r_PostsList.Sort(new PostSorter(comboBoxSortingOptions.SelectedIndex));
            updatePostList();
        }

        private void listBox_Leave(object sender, EventArgs e)
        {
            listBoxPosts.ClearSelected();
        }

        private void tabPageHome_MouseDown(object sender, MouseEventArgs e)
        {
            tabPageHome.Focus();
        }

        private void buttonFetchPosts_Click(object sender, EventArgs e)
        {
            fetchPosts();
            buttonFetchPosts.Text = "Posts Fetched!";
            buttonFetchPosts.Enabled = false;
        }

        private void buttonFetchAlbums_Click(object sender, EventArgs e)
        {
            fetchAlbums();
            buttonFetchAlbums.Text = "Albums Fetched!";
            buttonFetchAlbums.Enabled = false;
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabPageHigherLower)
            {
                if (m_GameManager == null)
                {
                    m_GameManager = new HigherLowerGameManager(
                        labelScore,
                        labelTimer,
                        labelRoundFeedback,
                        labelPage1Name,
                        labelPage2Name,
                        labelPage1Likes,
                        labelPage2Likes,
                        pictureBoxPage1,
                        pictureBoxPage2,
                        buttonHigherPage1,
                        buttonHigherPage2,
                        buttonNewGame,
                        this,
                        r_LoggedInUser);
                }

                m_GameManager.Initialize();
            }
            else
            {
                m_GameManager?.Cleanup();
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_GameManager?.Cleanup();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Thread dataLoadingThread = new Thread(loadUserDataInBackground);

            dataLoadingThread.IsBackground = true;
            dataLoadingThread.Start();
        }

        private void loadUserDataInBackground()
        {
            try
            {
                string name = r_LoggedInUser.Name;
                string profilePicUrl = m_UserDataManager.UserProfilePicURL;
                string coverPicUrl = m_UserDataManager.UserCoverPicURL;
                List<string> userInfoItems = new List<string>();

                foreach (string info in m_UserDataManager.UserInfo)
                {
                    userInfoItems.Add(info);
                }

                this.Invoke(new Action(() => updateUIWithUserData(name, profilePicUrl, coverPicUrl, userInfoItems)));
            }
            catch (Exception ex)
            {
                this.Invoke(new Action(() => handleDataLoadingError(ex.Message)));
            }
        }

        private void updateUIWithUserData(
            string i_Name,
            string i_ProfilePicUrl,
            string i_CoverPicUrl,
            List<string> i_UserInfoItems)
        {
            labelUserName.Text = i_Name;
            profilePictureBox.ImageLocation = i_ProfilePicUrl;
            coverPictureBox.ImageLocation = i_CoverPicUrl;

            listBoxUserInfo.Items.Clear();
            foreach (string userInfo in i_UserInfoItems)
            {
                listBoxUserInfo.Items.Add(userInfo);
            }
        }

        private void handleDataLoadingError(string i_ErrorMessage)
        {
            MessageBox.Show($"Error loading user data {i_ErrorMessage}");
        }

        private void textBoxFillStatus_TextChanged(object sender, EventArgs e)
        {
            bool hasText = !string.IsNullOrWhiteSpace(textBoxFillStatus.Text)
                           && textBoxFillStatus.Text != k_TextBoxPostPlaceHolderText;

            buttonSubmitPost.Enabled = hasText;
        }

        private void textBoxFillStatus_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxFillStatus.Text))
            {
                resetTextBoxFillStatusStyle();
            }
        }

        private void textBoxSearchFriends_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxSearchFriends.Text == k_TextBoxSearchFriendsPlaceHolderText)
            {
                textBoxSearchFriends.Text = string.Empty;
                textBoxSearchFriends.ForeColor = Color.Black;
            }
        }

        private void textBoxSearchFriends_TextChanged(object sender, EventArgs e)
        {
            listBoxFriendsList.Items.Clear();

            foreach (User friend in m_UserDataManager.UserFriends)
            {
                if (friend.Name.StartsWith(textBoxSearchFriends.Text, StringComparison.CurrentCultureIgnoreCase))
                {
                    listBoxFriendsList.Items.Add(friend.Name);
                }
            }

            if (listBoxFriendsList.Items.Count == 0)
            {
                listBoxFriendsList.Items.Add("No match found!");
            }
        }

        private void textBoxSearchFriends_Leave(object sender, EventArgs e)
        {
            if (textBoxSearchFriends.Text != k_TextBoxSearchFriendsPlaceHolderText)
            {
                textBoxSearchFriends.Text = k_TextBoxSearchFriendsPlaceHolderText;
                textBoxSearchFriends.ForeColor = SystemColors.ScrollBar;
                fetchFriends();
            }
        }
    }
}