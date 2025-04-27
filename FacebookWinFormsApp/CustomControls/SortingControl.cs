using System;
using System.Collections.Generic;
using FacebookDPApp.Backend;

namespace FacebookDPApp.CustomControls
{
    public class SortingControl<T> : BaseSortingControl
    {
        public delegate void SortingChangeEventHandler(object sender, SortComponent<T> sorter);

        public event SortingChangeEventHandler SortingChanged;

        private readonly Dictionary<string, SortComponent<T>> r_SortOptions =
            new Dictionary<string, SortComponent<T>>();

        public SortComponent<T> SelectedSorter
        {
            get
            {
                SortComponent<T> selectedSorter = null;

                if (r_ComboBoxSortingOptions.SelectedItem != null)
                {
                    string selectedOption = r_ComboBoxSortingOptions.SelectedItem.ToString();

                    if (r_SortOptions.ContainsKey(selectedOption))
                    {
                        selectedSorter = r_SortOptions[selectedOption];
                    }
                }

                return selectedSorter;
            }
        }

        public SortingControl()
            : base()
        {
            r_ComboBoxSortingOptions.SelectedIndexChanged += r_ComboBoxSortingOptions_SelectedIndexChanged;
        }

        private void r_ComboBoxSortingOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedSorter != null)
            {
                OnSortingChanged(SelectedSorter);
            }
        }

        protected virtual void OnSortingChanged(SortComponent<T> i_Sorter)
        {
            SortingChanged?.Invoke(this, i_Sorter);
        }

        public void AddSortOption(string i_OptionText, SortComponent<T> i_Sorter)
        {
            if (!r_SortOptions.ContainsKey(i_OptionText))
            {
                r_SortOptions.Add(i_OptionText, i_Sorter);
                AddSortOptionText(i_OptionText);
            }
        }

        public bool SortList(List<T> i_List)
        {
            bool isSorted = false;

            if (SelectedSorter != null && i_List != null)
            {
                i_List.Sort(SelectedSorter);
                isSorted = true;
            }

            return isSorted;
        }

        public void ClearSortOptions()
        {
            r_SortOptions.Clear();
            ClearSortingOptions();
        }

        public bool SelectSortOption(string i_OptionText)
        {
            return SelectSortOptionText(i_OptionText);
        }

        public bool SelectSortOption(int i_Index)
        {
            return SelectSortOptionIndex(i_Index);
        }
    }
}
