using System;
using System.Drawing;
using FacebookDPApp.Backend;

namespace FacebookDPApp.CustomControls
{
    public class SortingControlFactory
    {
        public static SortingControl<MyPost> CreatePostSortingControl(Point i_Location, Size i_Size)
        {
            SortingControl<MyPost> control = new SortingControl<MyPost>();

            control.Location = i_Location;
            control.Size = i_Size;
            control.LabelText = "Sort posts by:";

            control.AddSortOption(
                "Alphabet (Ascending)",
                new PropertySorter<MyPost, string>(post => post.Message, true, "Message"));
            control.AddSortOption(
                "Alphabet (Descending)",
                new PropertySorter<MyPost, string>(post => post.Message, false, "Message"));

            control.AddSortOption(
                "Creation Time (Ascending)",
                new PropertySorter<MyPost, DateTime>(post => post.CreatedTime, true, "CreatedTime"));

            control.AddSortOption(
                "Creation Time (Descending)",
                new PropertySorter<MyPost, DateTime>(post => post.CreatedTime, false, "CreatedTime"));

            control.AddSortOption(
                "Likes Amount (Ascending)",
                new PropertySorter<MyPost, int>(post => post.LikesCount, true, "LikesCount"));

            control.AddSortOption(
                "Likes Amount (Descending)",
                new PropertySorter<MyPost, int>(post => post.LikesCount, false, "LikesCount"));

            CompositeSorter<MyPost> compositeSorter = new CompositeSorter<MyPost>();

            compositeSorter.Add(new PropertySorter<MyPost, string>(post => post.Message, false));
            compositeSorter.Add(new PropertySorter<MyPost, int>(post => post.LikesCount, false));
            control.AddSortOption("Message & Likes (Descending)", compositeSorter);

            compositeSorter = new CompositeSorter<MyPost>();

            compositeSorter.Add(new PropertySorter<MyPost, string>(post => post.Message, true));
            compositeSorter.Add(new PropertySorter<MyPost, int>(post => post.LikesCount, true));
            control.AddSortOption("Message & Likes (Ascending)", compositeSorter);

            return control;
        }
    }
}