using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FacebookDPApp.Backend;
using FacebookDPApp.CustomControls;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;

namespace FacebookDPApp.Forms
{
    public partial class FormMain : Form
    {
        private const string k_TextBoxPostPlaceHolderText = "What's on your mind? Share your thoughts with the community!";
        private const string k_TextBoxSearchFriendsPlaceHolderText = "Search Friends...";

        private readonly User r_LoggedInUser;
        private HigherLowerGameManager m_GameManager;
        private UserDataManager m_UserDataManager;
        private FacebookServiceFacade m_facebookServiceFacade;
        private SortingControl<MyPost> m_PostSortingControl;

        public FormMain(User i_LoggedInUser)
        {
            InitializeComponent();
            r_LoggedInUser = i_LoggedInUser;

            textBoxFillStatus.LostFocus += textBoxFillStatus_LostFocus;

            initUserDataManager();
            initFacebookServiceFacade();
            initSortingControls();
        }

        private void initUserDataManager()
        {
            m_UserDataManager = UserDataManager.Instance;

            m_UserDataManager.InitUserData(r_LoggedInUser);
            // fetchFriends();
        }

        private void initFacebookServiceFacade()
        {
            m_facebookServiceFacade = FacebookServiceFacade.Instance;
            m_facebookServiceFacade.InitFacebookServiceFacade(r_LoggedInUser, pictureBoxAlbums);

            // fetchPosts();
            // fetchAlbums();
        }

        private void initSortingControls()
        {
            m_PostSortingControl =
                SortingControlFactory.CreatePostSortingControl(new Point(654, 99), new Size(180, 50));

            m_PostSortingControl.SortingChanged += PostSortingControl_SortingChanged;

            // m_PostSortingControl.SelectSortOption(0);
            tabPageHome.Controls.Add(m_PostSortingControl);
        }

        private void PostSortingControl_SortingChanged(object sender, SortComponent<MyPost> sorter)
        {
            if (sorter != null)
            {
                m_facebookServiceFacade.Sort(sorter);
                updatePostList();
            }
            //if(r_PostsList.Count > 0 && sorter != null)
            //{
            //    r_PostsList.Sort(sorter);
            //    updatePostList();
            //}
        }

        private void fetchAlbums()
        {
            if(m_facebookServiceFacade.GetUserAlbums() != null)
            {
                listBoxAlbums.Invoke(new Action(() => listBoxAlbums.DisplayMember = "Name"));

                foreach(Album album in m_facebookServiceFacade.GetUserAlbums())
                {
                    listBoxAlbums.Invoke(new Action(() => listBoxAlbums.Items.Add(album)));
                }
            }
        }

        private void fetchPosts()
        {
            if (m_facebookServiceFacade.GetUserPosts().Count == 0)
            {
                MessageBox.Show("No Posts to load");
            } else
            {
                // listBoxPosts.DisplayMember = "Name";
                updatePostList();
            }
        }

        private void fetchFriends()
        {
            // List<User> friendsList = r_FacebookService.GetUserFriends();

            listBoxFriendsList.Invoke(new Action(() => listBoxFriendsList.Items.Clear()));

            FacebookObjectCollection<User> friendsList = m_UserDataManager.GetUserFriendsList();

            foreach(User friend in friendsList)
            {
                listBoxFriendsList.Invoke(new Action(() => listBoxFriendsList.Items.Add(friend.Name)));
            }
        }

        private void updatePostList()
        {
            listBoxPosts.Invoke(new Action(() => listBoxPosts.Items.Clear()));

            List<MyPost> postsList = m_facebookServiceFacade.GetUserPosts();

            foreach (MyPost myPost in postsList)
            {
                if (myPost.Message != null)
                {
                    listBoxPosts.Invoke(new Action(() => listBoxPosts.Items.Add($"{myPost.Message} | Likes: {myPost.LikesCount} | Posted: {myPost.CreatedTime}")));
                }
            }
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            FacebookService.Logout();
            // FacebookService.LogoutWithUI();
            m_facebookServiceFacade.StopSlideShow();
            pictureBoxAlbums.Visible = false;
            this.Invoke(new Action(this.Close));
        }

        private async void listBoxAlbums_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_facebookServiceFacade.StopSlideShow();

            if(listBoxAlbums.SelectedItem is Album selectedAlbum)
            {
                //List<Photo> photos = await fetchPhotosFromSelectedAlbum(selectedAlbum);
                //r_AlbumSlideShowManager.StartSlideshow(photos);
                m_facebookServiceFacade.StartSlidesShow(selectedAlbum);
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

            m_facebookServiceFacade.AddPost(newPostText);
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
            SortComponent<MyPost> sorter;

            switch (comboBoxSortingOptions.SelectedIndex)
            {
                case 0:
                    sorter = new PropertySorter<MyPost, string>(post => post.Message, i_Ascending: true);
                    break;

                case 1:
                    sorter = new PropertySorter<MyPost, string>(post => post.Message, i_Ascending: false);
                    break;

                case 2:
                    sorter = new PropertySorter<MyPost, DateTime>(post => post.CreatedTime, i_Ascending: true);
                    break;

                case 3:
                    sorter = new PropertySorter<MyPost, DateTime>(post => post.CreatedTime, i_Ascending: false);
                    break;

                case 4:
                    sorter = new PropertySorter<MyPost, int>(post => post.LikesCount, i_Ascending: true);
                    break;

                case 5:
                    sorter = new PropertySorter<MyPost, int>(post => post.LikesCount, i_Ascending: false);
                    break;

                case 6:
                    CompositeSorter<MyPost> compositeSorter = new CompositeSorter<MyPost>();

                    compositeSorter.Add(new PropertySorter<MyPost, DateTime>(post => post.CreatedTime, false));
                    compositeSorter.Add(new PropertySorter<MyPost, int>(post => post.LikesCount, i_Ascending: false));

                    sorter = compositeSorter;
                    break;

                default:
                    sorter = new PropertySorter<MyPost, string>(post => post.Message, true);
                    break;

            }

            m_facebookServiceFacade.Sort(sorter);
            // r_PostsList.Sort(new PostSorter(comboBoxSortingOptions.SelectedIndex));
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
            updateUIWithUserData();
        }

        private void updateUIWithUserData()
        {
            try
            {
                labelUserName.Invoke(new Action(() => labelUserName.Text = m_UserDataManager.UserName));
                new Thread(getProfilePhoto).Start();
                new Thread(getCoverPhoto).Start();
                new Thread(getUserInfo).Start();
                new Thread(fetchFriends).Start();
                new Thread(fetchPosts).Start();
                new Thread(fetchAlbums).Start();
            }
            catch (Exception ex)
            {
                this.Invoke(new Action(() => handleDataLoadingError(ex.Message)));
            }
        }

        private void getCoverPhoto()
        {
            coverPictureBox.Invoke(new Action(() => coverPictureBox.LoadAsync(m_UserDataManager.UserCoverPicURL)));
        }

        private void getUserInfo()
        {
            listBoxUserInfo.Invoke(
                new Action(
                    () =>
                        {
                            listBoxUserInfo.Items.Clear();
                            foreach(string userInfo in m_UserDataManager.UserInfo)
                            {
                                listBoxUserInfo.Items.Add(userInfo);
                            }
                        }));
        }

        private void getProfilePhoto()
        {
            profilePictureBox.Invoke(new Action(() => profilePictureBox.LoadAsync(m_UserDataManager.UserProfilePicURL)));
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