using System;
using System.Collections.Generic;

namespace FacebookDPApp.Backend
{
    public class PostSorter : IComparer<MyPost>
    {
        private readonly eComparisonTypes r_ComparisonType;

        private enum eComparisonTypes
        {
            AlphabeticalAscending,
            AlphabeticalDescending,
            CreationTimeAscending,
            CreationTimeDescending,
            LikeCountAscending,
            LikeCountDescending,
        }

        public PostSorter(int i_ComparisonType)
        {
            r_ComparisonType = (eComparisonTypes)i_ComparisonType;
        }

        public int Compare(MyPost i_FirstPost, MyPost i_SecondPost)
        {
            int compareResult = -2;

            if (i_FirstPost == null && i_SecondPost == null)
            {
                compareResult = 0;
            }
            else if (i_FirstPost == null)
            {
                compareResult = -1;
            }
            else if (i_SecondPost == null)
            {
                compareResult = 1;
            }
            else
            {
                switch (r_ComparisonType)
                {
                    case eComparisonTypes.AlphabeticalAscending:
                        compareResult = alphabeticalCompare(i_FirstPost, i_SecondPost);
                        break;

                    case eComparisonTypes.AlphabeticalDescending:
                        compareResult = alphabeticalCompare(i_SecondPost, i_FirstPost);
                        break;

                    case eComparisonTypes.CreationTimeAscending:
                        compareResult = creationTimeCompare(i_FirstPost, i_SecondPost);
                        break;

                    case eComparisonTypes.CreationTimeDescending:
                        compareResult = creationTimeCompare(i_SecondPost, i_FirstPost);
                        break;

                    case eComparisonTypes.LikeCountAscending:
                        compareResult = likeCountCompare(i_FirstPost, i_SecondPost);
                        break;

                    case eComparisonTypes.LikeCountDescending:
                        compareResult = likeCountCompare(i_SecondPost, i_FirstPost);
                        break;
                }
            }

            return compareResult;
        }

        private int alphabeticalCompare(MyPost i_FirstPost, MyPost i_SecondPost)
        {
            string firstPostMessage = i_FirstPost.Message ?? string.Empty;
            string secondPostMessage = i_SecondPost.Message ?? string.Empty;

            return string.Compare(firstPostMessage, secondPostMessage, StringComparison.CurrentCulture);
        }

        private int creationTimeCompare(MyPost i_FirstPost, MyPost i_SecondPost)
        {
            DateTime firstPostCreationTime = i_FirstPost.CreatedTime == null ? DateTime.Now : i_FirstPost.CreatedTime;
            DateTime secondPostCreationTime =
                i_SecondPost.CreatedTime == null ? DateTime.Now : i_SecondPost.CreatedTime;

            return DateTime.Compare(firstPostCreationTime, secondPostCreationTime);
        }

        private int likeCountCompare(MyPost i_FirstPost, MyPost i_SecondPost)
        {
            int firstPostLikesCount = i_FirstPost.LikesCount == 0 ? 0 : i_FirstPost.LikesCount;
            int secondPostLikesCount = i_SecondPost.LikesCount == 0 ? 0 : i_SecondPost.LikesCount;

            return firstPostLikesCount.CompareTo(secondPostLikesCount);
        }
    }
}