using System;
using System.Drawing;
using System.Windows.Forms;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;

namespace FacebookDPApp.Forms
{
    public partial class FormLogin : Form
    {
        private const string k_AppID = "668236632596331";
        private static readonly string[] sr_AppPermissions =
            {
                "public_profile", "email", "user_friends", "user_birthday", "user_posts", "user_gender", "user_photos", "user_age_range", "user_likes", "user_location", "user_hometown"
            };
        private LoginResult m_LoginResult;
        private User m_LoggedInUser;

        public FormLogin()
        {
            InitializeComponent();
            centerButton();
            FacebookService.s_CollectionLimit = 5;
        }

        private void centerButton()
        {
            int x = (ClientSize.Width - buttonLogin.Width) / 2;
            int y = (ClientSize.Height - buttonLogin.Height) / 2;

            buttonLogin.Location = new Point(x, y);
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            login();
        }

        private void login()
        {
            try
            {
                m_LoginResult = FacebookService.Connect(
                    "EAAJfwfccn2sBO6MMBFf7zqQLhvxLSuZBr7ZAtRWvBfaF0phRDXRY628rbWKjHH6JUbOpZCDFOIReSrVhYcsxxRDhtqB7ZCnskvrlRGadYjBZAa7iF2RGLRt3OPJuvJpm0QO6d8KVyy7Uyz8Oj4T4mfSxhaDbPWdjf10fB2uEX5UCE3LHHfOZBhZCVKqfL4zjZCspB0mGETvLMqlZB8bPRfNlCDsQZD");
                //m_LoginResult = FacebookService.Connect(
                //    "EAAJfwfccn2sBOZBPnPk4si1JHuyz0GxAjTSEX6J1fBj7E8SUtZAw1w0LryvPtdz8f6hOZAwjzdwoUQQ4vEfWlKQjBw0BvRbcEcYwZA9Lc4RhCQ7hfrmCb7i1H4Xbgz0MPmC3MPjNKAGUTSYz7KB7JSxqSae4YvfhDN1HisZCyQi2ZBvmSy0LuhOSSTZA7VvbxBuXbxgiym1A2drT5IJdghFVOZAGLY1wnQQYYdlxhg5xLRe6Vb7hZAZBcI6dQZD");
                //m_LoginResult = FacebookService.Login(k_AppID, sr_AppPermissions);

                if (!string.IsNullOrEmpty(m_LoginResult.AccessToken))
                {
                    m_LoggedInUser = m_LoginResult.LoggedInUser;
                    FormMain formMain = new FormMain(m_LoggedInUser);
                    this.Hide();
                    formMain.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show(
                        "Login failed. Please try again.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}