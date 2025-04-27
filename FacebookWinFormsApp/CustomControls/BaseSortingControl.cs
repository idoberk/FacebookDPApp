using System.Drawing;
using System.Windows.Forms;

namespace FacebookDPApp.CustomControls
{
    public partial class BaseSortingControl : UserControl
    {
        protected readonly ComboBox r_ComboBoxSortingOptions = new ComboBox();
        protected readonly Label r_LabelSortBy = new Label();
        protected const string k_DefaultLabelText = "Sort by:";

        public string LabelText
        {
            get { return r_LabelSortBy.Text; }
            set { r_LabelSortBy.Text = value; }
        }

        public string SelectedOptionText
        {
            get { return r_ComboBoxSortingOptions.SelectedItem?.ToString(); }
        }

        public int SelectedIndex
        {
            get { return r_ComboBoxSortingOptions.SelectedIndex; }
        }

        public BaseSortingControl()
        {
            InitializeComponent();
            setupControls();
        }

        private void setupControls()
        {
            setupLabel();
            setupComboBox();
        }

        private void setupLabel()
        {
            r_LabelSortBy.AutoSize = true;
            r_LabelSortBy.Font = new Font("Arial", 12F);
            r_LabelSortBy.Location = new Point(0, 0);
            r_LabelSortBy.Name = "labelSortBy";
            r_LabelSortBy.Size = new Size(63, 18);
            r_LabelSortBy.Text = k_DefaultLabelText;
            this.Controls.Add(r_LabelSortBy);
        }

        private void setupComboBox()
        {
            r_ComboBoxSortingOptions.DropDownStyle = ComboBoxStyle.DropDownList;
            r_ComboBoxSortingOptions.Font = new Font("Arial", 9F);
            r_ComboBoxSortingOptions.Location = new Point(0, 25);
            r_ComboBoxSortingOptions.Name = "comboBoxSortingOptions";
            r_ComboBoxSortingOptions.Size = new Size(250, 23);
            this.Controls.Add(r_ComboBoxSortingOptions);
        }

        protected void ClearSortingOptions()
        {
            r_ComboBoxSortingOptions.Items.Clear();
            r_ComboBoxSortingOptions.SelectedIndex = -1;
        }

        protected void AddSortOptionText(string i_OptionText)
        {
            if (!r_ComboBoxSortingOptions.Items.Contains(i_OptionText))
            {
                r_ComboBoxSortingOptions.Items.Add(i_OptionText);
            }
        }

        protected bool SelectSortOptionIndex(int i_Index)
        {
            bool isSelected = false;

            if (i_Index >= 0 && i_Index < r_ComboBoxSortingOptions.Items.Count)
            {
                r_ComboBoxSortingOptions.SelectedIndex = i_Index;
                isSelected = true;
            }

            return isSelected;
        }

        protected bool SelectSortOptionText(string i_OptionText)
        {
            bool isSelected = false;
            int index = r_ComboBoxSortingOptions.Items.IndexOf(i_OptionText);

            if(index >= 0)
            {
                r_ComboBoxSortingOptions.SelectedIndex = index;
                isSelected = true;
            }

            return isSelected;
        }

        public string[] GetSortingOptionTexts()
        {
            string[] options = new string[r_ComboBoxSortingOptions.Items.Count];

            for (int i = 0; i < r_ComboBoxSortingOptions.Items.Count; i++)
            {
                options[i] = r_ComboBoxSortingOptions.Items[i].ToString();
            }

            return options;
        }
    }
}