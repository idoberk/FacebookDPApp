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
            FacebookService.s_CollectionLimit = 25;
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
                    "EAAJfwfccn2sBOxL5G4JepPMQnNvGaiT5MbpZAuUzspFpE2fQh2Jz7QH736Fp6kO40gfsBQIJ8kefWlWchzykzP9jppfrG9i2glBUzxeLpJ9fwaY3UlnUDEnP0EDJPlXlm5InxLTprWnKQ5AOlijgDMiGKLhaiXSObdX1JE8GVra9ZCg7u5wTH8vcwAmfVrQOAYkFEaO5EI69McugZDZD");
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