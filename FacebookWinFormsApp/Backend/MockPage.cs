using System;
using FacebookWrapper.ObjectModel;

namespace FacebookDPApp.Backend
{
    public class MockPage
    {
        private readonly long r_FictionalLikesCount;
        private readonly Page r_OriginalPage;
        private static readonly Random sr_RandomLikesCount = new Random();

        public long LikesCount
        {
            get { return r_FictionalLikesCount; }
        }

        public string Id
        {
            get { return r_OriginalPage.Id; }
        }

        public MockPage(Page i_OriginalPage)
        {
            r_OriginalPage = i_OriginalPage;

            if (i_OriginalPage.LikesCount == null)
            {
                r_FictionalLikesCount = sr_RandomLikesCount.Next(0, int.MaxValue);
            }
            else
            {
                r_FictionalLikesCount = (long)i_OriginalPage.LikesCount;
            }
        }

        public Page GetOriginalPage()
        {
            return r_OriginalPage;
        }
    }
}