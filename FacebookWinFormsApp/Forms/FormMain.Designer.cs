using System.Windows.Forms;
using FacebookDPApp.CustomControls;

namespace FacebookDPApp.Forms
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

       
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabControl = new FacebookDPApp.CustomControls.CustomTabControl();
            this.tabPageHome = new System.Windows.Forms.TabPage();
            this.buttonStopSlideshow = new FacebookDPApp.CustomControls.RoundedButton();
            this.listBoxPosts = new System.Windows.Forms.ListBox();
            this.buttonSubmitPost = new FacebookDPApp.CustomControls.RoundedButton();
            this.pictureBoxAlbums = new System.Windows.Forms.PictureBox();
            this.textBoxFillStatus = new System.Windows.Forms.TextBox();
            this.listBoxAlbums = new System.Windows.Forms.ListBox();
            this.albumBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.buttonLogout = new FacebookDPApp.CustomControls.RoundedButton();
            this.tabPageProfile = new System.Windows.Forms.TabPage();
            this.textBoxSearchFriends = new System.Windows.Forms.TextBox();
            this.listBoxFriendsList = new System.Windows.Forms.ListBox();
            this.profilePictureBox = new FacebookDPApp.CustomControls.CircularPictureBox();
            this.labelUserName = new FacebookDPApp.CustomControls.RoundedLabel();
            this.coverPictureBox = new System.Windows.Forms.PictureBox();
            this.listBoxUserInfo = new System.Windows.Forms.ListBox();
            this.tabPageHigherLower = new System.Windows.Forms.TabPage();
            this.panelMain = new System.Windows.Forms.Panel();
            this.labelRoundFeedback = new System.Windows.Forms.Label();
            this.panelPage2 = new System.Windows.Forms.Panel();
            this.pictureBoxPage2 = new System.Windows.Forms.PictureBox();
            this.labelPage2Name = new System.Windows.Forms.Label();
            this.buttonHigherPage2 = new FacebookDPApp.CustomControls.RoundedButton();
            this.labelPage2Likes = new System.Windows.Forms.Label();
            this.panelPage1 = new System.Windows.Forms.Panel();
            this.pictureBoxPage1 = new System.Windows.Forms.PictureBox();
            this.labelPage1Name = new System.Windows.Forms.Label();
            this.buttonHigherPage1 = new FacebookDPApp.CustomControls.RoundedButton();
            this.labelPage1Likes = new System.Windows.Forms.Label();
            this.labelTimer = new System.Windows.Forms.Label();
            this.buttonNewGame = new FacebookDPApp.CustomControls.RoundedButton();
            this.labelScore = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabPageHome.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAlbums)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.albumBindingSource)).BeginInit();
            this.tabPageProfile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profilePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.coverPictureBox)).BeginInit();
            this.tabPageHigherLower.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPage2)).BeginInit();
            this.panelPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPage1)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "home-icon.png");
            this.imageList1.Images.SetKeyName(1, "user-solid.png");
            this.imageList1.Images.SetKeyName(2, "game-icon.png");
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageHome);
            this.tabControl.Controls.Add(this.tabPageProfile);
            this.tabControl.Controls.Add(this.tabPageHigherLower);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl.Font = new System.Drawing.Font("Arial", 8.25F);
            this.tabControl.ImageList = this.imageList1;
            this.tabControl.ItemSize = new System.Drawing.Size(150, 27);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.Padding = new System.Drawing.Point(0, 0);
            this.tabControl.SelectedIndex = 0;
            this.tabControl.ShowToolTips = true;
            this.tabControl.Size = new System.Drawing.Size(850, 557);
            this.tabControl.TabIndex = 54;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabPageHome
            // 
            this.tabPageHome.AutoScroll = true;
            this.tabPageHome.BackColor = System.Drawing.Color.Transparent;
            this.tabPageHome.Controls.Add(this.buttonStopSlideshow);
            this.tabPageHome.Controls.Add(this.listBoxPosts);
            this.tabPageHome.Controls.Add(this.buttonSubmitPost);
            this.tabPageHome.Controls.Add(this.pictureBoxAlbums);
            this.tabPageHome.Controls.Add(this.textBoxFillStatus);
            this.tabPageHome.Controls.Add(this.listBoxAlbums);
            this.tabPageHome.Controls.Add(this.buttonLogout);
            this.tabPageHome.ImageIndex = 0;
            this.tabPageHome.Location = new System.Drawing.Point(4, 31);
            this.tabPageHome.Name = "tabPageHome";
            this.tabPageHome.Size = new System.Drawing.Size(842, 522);
            this.tabPageHome.TabIndex = 0;
            this.tabPageHome.ToolTipText = "Home";
            this.tabPageHome.UseVisualStyleBackColor = true;
            this.tabPageHome.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabPageHome_MouseDown);
            // 
            // buttonStopSlideshow
            // 
            this.buttonStopSlideshow.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonStopSlideshow.BackgroundColor = System.Drawing.Color.DodgerBlue;
            this.buttonStopSlideshow.BorderColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonStopSlideshow.BorderRadius = 10;
            this.buttonStopSlideshow.BorderSize = 3;
            this.buttonStopSlideshow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStopSlideshow.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.buttonStopSlideshow.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonStopSlideshow.Location = new System.Drawing.Point(8, 478);
            this.buttonStopSlideshow.Name = "buttonStopSlideshow";
            this.buttonStopSlideshow.Size = new System.Drawing.Size(141, 35);
            this.buttonStopSlideshow.TabIndex = 65;
            this.buttonStopSlideshow.Text = "Stop Slideshow";
            this.buttonStopSlideshow.TextColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonStopSlideshow.UseVisualStyleBackColor = false;
            this.buttonStopSlideshow.Visible = false;
            this.buttonStopSlideshow.Click += new System.EventHandler(this.buttonStopSlideshow_Click);
            // 
            // listBoxPosts
            // 
            this.listBoxPosts.Font = new System.Drawing.Font("Arial", 9F);
            this.listBoxPosts.FormattingEnabled = true;
            this.listBoxPosts.ItemHeight = 15;
            this.listBoxPosts.Location = new System.Drawing.Point(8, 54);
            this.listBoxPosts.Name = "listBoxPosts";
            this.listBoxPosts.Size = new System.Drawing.Size(604, 214);
            this.listBoxPosts.TabIndex = 59;
            this.listBoxPosts.Leave += new System.EventHandler(this.listBox_Leave);
            // 
            // buttonSubmitPost
            // 
            this.buttonSubmitPost.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonSubmitPost.BackgroundColor = System.Drawing.Color.DodgerBlue;
            this.buttonSubmitPost.BorderColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonSubmitPost.BorderRadius = 10;
            this.buttonSubmitPost.BorderSize = 3;
            this.buttonSubmitPost.Enabled = false;
            this.buttonSubmitPost.FlatAppearance.BorderSize = 0;
            this.buttonSubmitPost.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSubmitPost.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSubmitPost.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonSubmitPost.Location = new System.Drawing.Point(439, 15);
            this.buttonSubmitPost.Margin = new System.Windows.Forms.Padding(0);
            this.buttonSubmitPost.Name = "buttonSubmitPost";
            this.buttonSubmitPost.Size = new System.Drawing.Size(100, 30);
            this.buttonSubmitPost.TabIndex = 64;
            this.buttonSubmitPost.Text = "Post";
            this.buttonSubmitPost.TextColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonSubmitPost.UseVisualStyleBackColor = false;
            this.buttonSubmitPost.Click += new System.EventHandler(this.buttonSubmitPost_Click);
            // 
            // pictureBoxAlbums
            // 
            this.pictureBoxAlbums.Location = new System.Drawing.Point(340, 284);
            this.pictureBoxAlbums.Name = "pictureBoxAlbums";
            this.pictureBoxAlbums.Size = new System.Drawing.Size(249, 191);
            this.pictureBoxAlbums.TabIndex = 63;
            this.pictureBoxAlbums.TabStop = false;
            // 
            // textBoxFillStatus
            // 
            this.textBoxFillStatus.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFillStatus.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.textBoxFillStatus.Location = new System.Drawing.Point(8, 15);
            this.textBoxFillStatus.Multiline = true;
            this.textBoxFillStatus.Name = "textBoxFillStatus";
            this.textBoxFillStatus.Size = new System.Drawing.Size(428, 30);
            this.textBoxFillStatus.TabIndex = 61;
            this.textBoxFillStatus.Text = "What\'s on your mind? Share your thoughts with the community!";
            this.textBoxFillStatus.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxFillStatus_Clicked);
            this.textBoxFillStatus.TextChanged += new System.EventHandler(this.textBoxFillStatus_TextChanged);
            // 
            // listBoxAlbums
            // 
            this.listBoxAlbums.DataSource = this.albumBindingSource;
            this.listBoxAlbums.DisplayMember = "Name";
            this.listBoxAlbums.Font = new System.Drawing.Font("Arial", 11F);
            this.listBoxAlbums.FormattingEnabled = true;
            this.listBoxAlbums.ItemHeight = 17;
            this.listBoxAlbums.Location = new System.Drawing.Point(8, 284);
            this.listBoxAlbums.Name = "listBoxAlbums";
            this.listBoxAlbums.Size = new System.Drawing.Size(326, 191);
            this.listBoxAlbums.TabIndex = 56;
            this.listBoxAlbums.SelectedIndexChanged += new System.EventHandler(this.listBoxAlbums_SelectedIndexChanged);
            // 
            // albumBindingSource
            // 
            this.albumBindingSource.DataSource = typeof(FacebookWrapper.ObjectModel.Album);
            // 
            // buttonLogout
            // 
            this.buttonLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonLogout.BackColor = System.Drawing.Color.Red;
            this.buttonLogout.BackgroundColor = System.Drawing.Color.Red;
            this.buttonLogout.BorderColor = System.Drawing.Color.Empty;
            this.buttonLogout.BorderRadius = 25;
            this.buttonLogout.BorderSize = 0;
            this.buttonLogout.FlatAppearance.BorderSize = 0;
            this.buttonLogout.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Brown;
            this.buttonLogout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Firebrick;
            this.buttonLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLogout.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLogout.ForeColor = System.Drawing.Color.White;
            this.buttonLogout.Location = new System.Drawing.Point(657, 474);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Size = new System.Drawing.Size(177, 40);
            this.buttonLogout.TabIndex = 62;
            this.buttonLogout.Text = "Logout";
            this.buttonLogout.TextColor = System.Drawing.Color.White;
            this.buttonLogout.UseVisualStyleBackColor = false;
            this.buttonLogout.Click += new System.EventHandler(this.buttonLogout_Click);
            // 
            // tabPageProfile
            // 
            this.tabPageProfile.AutoScroll = true;
            this.tabPageProfile.BackColor = System.Drawing.Color.Transparent;
            this.tabPageProfile.Controls.Add(this.textBoxSearchFriends);
            this.tabPageProfile.Controls.Add(this.listBoxFriendsList);
            this.tabPageProfile.Controls.Add(this.profilePictureBox);
            this.tabPageProfile.Controls.Add(this.labelUserName);
            this.tabPageProfile.Controls.Add(this.coverPictureBox);
            this.tabPageProfile.Controls.Add(this.listBoxUserInfo);
            this.tabPageProfile.ImageIndex = 1;
            this.tabPageProfile.Location = new System.Drawing.Point(4, 31);
            this.tabPageProfile.Name = "tabPageProfile";
            this.tabPageProfile.Size = new System.Drawing.Size(842, 522);
            this.tabPageProfile.TabIndex = 2;
            this.tabPageProfile.ToolTipText = "Profile";
            this.tabPageProfile.UseVisualStyleBackColor = true;
            // 
            // textBoxSearchFriends
            // 
            this.textBoxSearchFriends.Font = new System.Drawing.Font("Arial", 11F);
            this.textBoxSearchFriends.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.textBoxSearchFriends.Location = new System.Drawing.Point(574, 301);
            this.textBoxSearchFriends.MaxLength = 30;
            this.textBoxSearchFriends.Name = "textBoxSearchFriends";
            this.textBoxSearchFriends.Size = new System.Drawing.Size(260, 24);
            this.textBoxSearchFriends.TabIndex = 67;
            this.textBoxSearchFriends.Text = "Search Friends...";
            this.textBoxSearchFriends.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxSearchFriends_MouseClick);
            this.textBoxSearchFriends.TextChanged += new System.EventHandler(this.textBoxSearchFriends_TextChanged);
            this.textBoxSearchFriends.Leave += new System.EventHandler(this.textBoxSearchFriends_Leave);
            // 
            // listBoxFriendsList
            // 
            this.listBoxFriendsList.Font = new System.Drawing.Font("Arial", 11F);
            this.listBoxFriendsList.FormattingEnabled = true;
            this.listBoxFriendsList.ItemHeight = 17;
            this.listBoxFriendsList.Location = new System.Drawing.Point(574, 331);
            this.listBoxFriendsList.Name = "listBoxFriendsList";
            this.listBoxFriendsList.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBoxFriendsList.Size = new System.Drawing.Size(260, 72);
            this.listBoxFriendsList.TabIndex = 66;
            // 
            // profilePictureBox
            // 
            this.profilePictureBox.BorderCapStyle = System.Drawing.Drawing2D.DashCap.Flat;
            this.profilePictureBox.BorderLineStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.profilePictureBox.BorderSize = 5;
            this.profilePictureBox.FirstBorderColor = System.Drawing.Color.DodgerBlue;
            this.profilePictureBox.GradientAngle = 270F;
            this.profilePictureBox.Location = new System.Drawing.Point(286, 169);
            this.profilePictureBox.Name = "profilePictureBox";
            this.profilePictureBox.SecondBorderColor = System.Drawing.Color.Transparent;
            this.profilePictureBox.Size = new System.Drawing.Size(150, 150);
            this.profilePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.profilePictureBox.TabIndex = 63;
            this.profilePictureBox.TabStop = false;
            // 
            // labelUserName
            // 
            this.labelUserName.AutoSize = true;
            this.labelUserName.BackColor = System.Drawing.Color.Transparent;
            this.labelUserName.BackgroundColor = System.Drawing.Color.Transparent;
            this.labelUserName.BorderColor = System.Drawing.Color.DodgerBlue;
            this.labelUserName.BorderRadius = 25;
            this.labelUserName.BorderSize = 0;
            this.labelUserName.BorderVisible = true;
            this.labelUserName.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUserName.ForeColor = System.Drawing.Color.Black;
            this.labelUserName.Location = new System.Drawing.Point(442, 241);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Padding = new System.Windows.Forms.Padding(5);
            this.labelUserName.Size = new System.Drawing.Size(142, 37);
            this.labelUserName.TabIndex = 64;
            this.labelUserName.Text = "User Name";
            this.labelUserName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // coverPictureBox
            // 
            this.coverPictureBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.coverPictureBox.Location = new System.Drawing.Point(0, 0);
            this.coverPictureBox.Name = "coverPictureBox";
            this.coverPictureBox.Size = new System.Drawing.Size(842, 226);
            this.coverPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.coverPictureBox.TabIndex = 57;
            this.coverPictureBox.TabStop = false;
            // 
            // listBoxUserInfo
            // 
            this.listBoxUserInfo.Font = new System.Drawing.Font("Arial", 11F);
            this.listBoxUserInfo.FormattingEnabled = true;
            this.listBoxUserInfo.ItemHeight = 17;
            this.listBoxUserInfo.Location = new System.Drawing.Point(8, 245);
            this.listBoxUserInfo.Name = "listBoxUserInfo";
            this.listBoxUserInfo.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBoxUserInfo.Size = new System.Drawing.Size(260, 72);
            this.listBoxUserInfo.TabIndex = 65;
            // 
            // tabPageHigherLower
            // 
            this.tabPageHigherLower.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageHigherLower.Controls.Add(this.panelMain);
            this.tabPageHigherLower.ImageIndex = 2;
            this.tabPageHigherLower.Location = new System.Drawing.Point(4, 31);
            this.tabPageHigherLower.Name = "tabPageHigherLower";
            this.tabPageHigherLower.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHigherLower.Size = new System.Drawing.Size(842, 522);
            this.tabPageHigherLower.TabIndex = 1;
            this.tabPageHigherLower.ToolTipText = "HigherLower";
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.AliceBlue;
            this.panelMain.Controls.Add(this.labelRoundFeedback);
            this.panelMain.Controls.Add(this.panelPage2);
            this.panelMain.Controls.Add(this.panelPage1);
            this.panelMain.Controls.Add(this.labelTimer);
            this.panelMain.Controls.Add(this.buttonNewGame);
            this.panelMain.Controls.Add(this.labelScore);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(3, 3);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(836, 516);
            this.panelMain.TabIndex = 0;
            // 
            // labelRoundFeedback
            // 
            this.labelRoundFeedback.BackColor = System.Drawing.Color.Red;
            this.labelRoundFeedback.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRoundFeedback.ForeColor = System.Drawing.Color.White;
            this.labelRoundFeedback.Location = new System.Drawing.Point(353, 231);
            this.labelRoundFeedback.Name = "labelRoundFeedback";
            this.labelRoundFeedback.Size = new System.Drawing.Size(130, 50);
            this.labelRoundFeedback.TabIndex = 7;
            this.labelRoundFeedback.Text = "TIME OVER!";
            this.labelRoundFeedback.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelPage2
            // 
            this.panelPage2.BackColor = System.Drawing.SystemColors.Control;
            this.panelPage2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPage2.Controls.Add(this.pictureBoxPage2);
            this.panelPage2.Controls.Add(this.labelPage2Name);
            this.panelPage2.Controls.Add(this.buttonHigherPage2);
            this.panelPage2.Controls.Add(this.labelPage2Likes);
            this.panelPage2.Location = new System.Drawing.Point(472, 100);
            this.panelPage2.Name = "panelPage2";
            this.panelPage2.Size = new System.Drawing.Size(340, 340);
            this.panelPage2.TabIndex = 9;
            // 
            // pictureBoxPage2
            // 
            this.pictureBoxPage2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxPage2.BackColor = System.Drawing.Color.White;
            this.pictureBoxPage2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxPage2.Location = new System.Drawing.Point(65, 45);
            this.pictureBoxPage2.Name = "pictureBoxPage2";
            this.pictureBoxPage2.Size = new System.Drawing.Size(210, 210);
            this.pictureBoxPage2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPage2.TabIndex = 9;
            this.pictureBoxPage2.TabStop = false;
            // 
            // labelPage2Name
            // 
            this.labelPage2Name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPage2Name.AutoEllipsis = true;
            this.labelPage2Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPage2Name.Location = new System.Drawing.Point(3, 10);
            this.labelPage2Name.Name = "labelPage2Name";
            this.labelPage2Name.Size = new System.Drawing.Size(332, 23);
            this.labelPage2Name.TabIndex = 8;
            this.labelPage2Name.Text = "Page Name";
            this.labelPage2Name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonHigherPage2
            // 
            this.buttonHigherPage2.BackColor = System.Drawing.Color.RoyalBlue;
            this.buttonHigherPage2.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.buttonHigherPage2.BorderColor = System.Drawing.Color.PaleGreen;
            this.buttonHigherPage2.BorderRadius = 40;
            this.buttonHigherPage2.BorderSize = 0;
            this.buttonHigherPage2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonHigherPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonHigherPage2.ForeColor = System.Drawing.Color.White;
            this.buttonHigherPage2.Location = new System.Drawing.Point(90, 290);
            this.buttonHigherPage2.Name = "buttonHigherPage2";
            this.buttonHigherPage2.Size = new System.Drawing.Size(160, 40);
            this.buttonHigherPage2.TabIndex = 7;
            this.buttonHigherPage2.Text = "Higher";
            this.buttonHigherPage2.TextColor = System.Drawing.Color.White;
            this.buttonHigherPage2.UseVisualStyleBackColor = false;
            // 
            // labelPage2Likes
            // 
            this.labelPage2Likes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPage2Likes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPage2Likes.Location = new System.Drawing.Point(3, 261);
            this.labelPage2Likes.Name = "labelPage2Likes";
            this.labelPage2Likes.Size = new System.Drawing.Size(332, 23);
            this.labelPage2Likes.TabIndex = 6;
            this.labelPage2Likes.Text = "1,000,000 likes";
            this.labelPage2Likes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelPage1
            // 
            this.panelPage1.BackColor = System.Drawing.SystemColors.Control;
            this.panelPage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPage1.Controls.Add(this.pictureBoxPage1);
            this.panelPage1.Controls.Add(this.labelPage1Name);
            this.panelPage1.Controls.Add(this.buttonHigherPage1);
            this.panelPage1.Controls.Add(this.labelPage1Likes);
            this.panelPage1.Location = new System.Drawing.Point(24, 100);
            this.panelPage1.Name = "panelPage1";
            this.panelPage1.Size = new System.Drawing.Size(340, 340);
            this.panelPage1.TabIndex = 8;
            // 
            // pictureBoxPage1
            // 
            this.pictureBoxPage1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxPage1.BackColor = System.Drawing.Color.White;
            this.pictureBoxPage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxPage1.Location = new System.Drawing.Point(65, 45);
            this.pictureBoxPage1.Name = "pictureBoxPage1";
            this.pictureBoxPage1.Size = new System.Drawing.Size(210, 210);
            this.pictureBoxPage1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPage1.TabIndex = 9;
            this.pictureBoxPage1.TabStop = false;
            // 
            // labelPage1Name
            // 
            this.labelPage1Name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPage1Name.AutoEllipsis = true;
            this.labelPage1Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPage1Name.Location = new System.Drawing.Point(3, 10);
            this.labelPage1Name.Name = "labelPage1Name";
            this.labelPage1Name.Size = new System.Drawing.Size(332, 23);
            this.labelPage1Name.TabIndex = 8;
            this.labelPage1Name.Text = "Page Name";
            this.labelPage1Name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonHigherPage1
            // 
            this.buttonHigherPage1.BackColor = System.Drawing.Color.RoyalBlue;
            this.buttonHigherPage1.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.buttonHigherPage1.BorderColor = System.Drawing.Color.PaleGreen;
            this.buttonHigherPage1.BorderRadius = 40;
            this.buttonHigherPage1.BorderSize = 0;
            this.buttonHigherPage1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonHigherPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonHigherPage1.ForeColor = System.Drawing.Color.White;
            this.buttonHigherPage1.Location = new System.Drawing.Point(90, 290);
            this.buttonHigherPage1.Name = "buttonHigherPage1";
            this.buttonHigherPage1.Size = new System.Drawing.Size(160, 40);
            this.buttonHigherPage1.TabIndex = 7;
            this.buttonHigherPage1.Text = "Higher";
            this.buttonHigherPage1.TextColor = System.Drawing.Color.White;
            this.buttonHigherPage1.UseVisualStyleBackColor = false;
            // 
            // labelPage1Likes
            // 
            this.labelPage1Likes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPage1Likes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPage1Likes.Location = new System.Drawing.Point(3, 261);
            this.labelPage1Likes.Name = "labelPage1Likes";
            this.labelPage1Likes.Size = new System.Drawing.Size(332, 23);
            this.labelPage1Likes.TabIndex = 6;
            this.labelPage1Likes.Text = "1,000,000 likes";
            this.labelPage1Likes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelTimer
            // 
            this.labelTimer.AutoSize = true;
            this.labelTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTimer.Location = new System.Drawing.Point(375, 67);
            this.labelTimer.Name = "labelTimer";
            this.labelTimer.Size = new System.Drawing.Size(86, 20);
            this.labelTimer.TabIndex = 6;
            this.labelTimer.Text = "Time: 15s";
            this.labelTimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonNewGame
            // 
            this.buttonNewGame.BackColor = System.Drawing.Color.RoyalBlue;
            this.buttonNewGame.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.buttonNewGame.BorderColor = System.Drawing.Color.PaleGreen;
            this.buttonNewGame.BorderRadius = 30;
            this.buttonNewGame.BorderSize = 0;
            this.buttonNewGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNewGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNewGame.ForeColor = System.Drawing.Color.White;
            this.buttonNewGame.Location = new System.Drawing.Point(682, 10);
            this.buttonNewGame.Name = "buttonNewGame";
            this.buttonNewGame.Size = new System.Drawing.Size(130, 46);
            this.buttonNewGame.TabIndex = 5;
            this.buttonNewGame.Text = "New Game";
            this.buttonNewGame.TextColor = System.Drawing.Color.White;
            this.buttonNewGame.UseVisualStyleBackColor = false;
            // 
            // labelScore
            // 
            this.labelScore.AutoSize = true;
            this.labelScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelScore.Location = new System.Drawing.Point(20, 26);
            this.labelScore.Name = "labelScore";
            this.labelScore.Size = new System.Drawing.Size(76, 20);
            this.labelScore.TabIndex = 4;
            this.labelScore.Text = "Score: 0";
            this.labelScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(850, 557);
            this.Controls.Add(this.tabControl);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Facebook DP Course";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.tabControl.ResumeLayout(false);
            this.tabPageHome.ResumeLayout(false);
            this.tabPageHome.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAlbums)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.albumBindingSource)).EndInit();
            this.tabPageProfile.ResumeLayout(false);
            this.tabPageProfile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profilePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.coverPictureBox)).EndInit();
            this.tabPageHigherLower.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPage2)).EndInit();
            this.panelPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPage1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private RoundedButton buttonLogout;
        private System.Windows.Forms.TabPage tabPageHigherLower;
        private CustomTabControl tabControl;
        private System.Windows.Forms.TabPage tabPageHome;
        private ListBox listBoxAlbums;
        private TextBox textBoxFillStatus;
        private PictureBox pictureBoxAlbums;
        private ImageList imageList1;
        private CustomControls.RoundedButton buttonSubmitPost;
        private ListBox listBoxPosts;
        private TabPage tabPageProfile;
        private System.Windows.Forms.PictureBox coverPictureBox;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label labelScore;
        private RoundedButton buttonNewGame;
        private System.Windows.Forms.Label labelTimer;
        private System.Windows.Forms.Label labelRoundFeedback;
        private System.Windows.Forms.Panel panelPage2;
        private System.Windows.Forms.PictureBox pictureBoxPage2;
        private System.Windows.Forms.Label labelPage2Name;
        private RoundedButton buttonHigherPage2;
        private System.Windows.Forms.Label labelPage2Likes;
        private System.Windows.Forms.Panel panelPage1;
        private System.Windows.Forms.PictureBox pictureBoxPage1;
        private System.Windows.Forms.Label labelPage1Name;
        private RoundedButton buttonHigherPage1;
        private System.Windows.Forms.Label labelPage1Likes;
        private RoundedLabel labelUserName;
        private CircularPictureBox profilePictureBox;
        private System.Windows.Forms.ListBox listBoxUserInfo;
        private ListBox listBoxFriendsList;
        private TextBox textBoxSearchFriends;
        private BindingSource albumBindingSource;
        private RoundedButton buttonStopSlideshow;
    }
 }

