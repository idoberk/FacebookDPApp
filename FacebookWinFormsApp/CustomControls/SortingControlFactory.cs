using System;
using System.Drawing;
using FacebookDPApp.Backend;

namespace FacebookDPApp.CustomControls
{
    public class SortingControlFactory
    {
        public static SortingControl<PostWrapper> CreatePostSortingControl(Point i_Location, Size i_Size, string i_LabelText)
        {
            SortingControl<PostWrapper> control = new SortingControl<PostWrapper>();

            control.Location = i_Location;
            control.Size = i_Size;
            control.LabelText = i_LabelText;
            // control.LabelText = "Sort posts by:";

            control.AddSortOption(
                "Alphabet (Ascending)",
                new PropertySorter<PostWrapper, string>(post => post.Message, true, "Message"));
            control.AddSortOption(
                "Alphabet (Descending)",
                new PropertySorter<PostWrapper, string>(post => post.Message, false, "Message"));

            control.AddSortOption(
                "Creation Time (Ascending)",
                new PropertySorter<PostWrapper, DateTime>(post => post.CreatedTime, true, "CreatedTime"));

            control.AddSortOption(
                "Creation Time (Descending)",
                new PropertySorter<PostWrapper, DateTime>(post => post.CreatedTime, false, "CreatedTime"));

            control.AddSortOption(
                "Likes Amount (Ascending)",
                new PropertySorter<PostWrapper, int>(post => post.LikesCount, true, "LikesCount"));

            control.AddSortOption(
                "Likes Amount (Descending)",
                new PropertySorter<PostWrapper, int>(post => post.LikesCount, false, "LikesCount"));

            CompositeSorter<PostWrapper> compositeSorter = new CompositeSorter<PostWrapper>();

            compositeSorter.Add(new PropertySorter<PostWrapper, string>(post => post.Message, false));
            compositeSorter.Add(new PropertySorter<PostWrapper, int>(post => post.LikesCount, false));
            control.AddSortOption("Message & Likes (Descending)", compositeSorter);

            compositeSorter = new CompositeSorter<PostWrapper>();

            compositeSorter.Add(new PropertySorter<PostWrapper, string>(post => post.Message, true));
            compositeSorter.Add(new PropertySorter<PostWrapper, int>(post => post.LikesCount, true));
            control.AddSortOption("Message & Likes (Ascending)", compositeSorter);

            return control;
        }
    }
}