using System;

namespace FacebookDPApp.Backend
{
    public class GuessResultEventArgs : EventArgs
    {
        public int FirstPageLikesCount { get; }
        public int SecondPageLikesCount { get; }
        public bool IsCorrect { get; }
        public bool IsFirstPageHigher { get; }

        public GuessResultEventArgs(int i_FirstPageLikesCount, int i_SecondPageLikesCount, bool i_IsCorrect, bool i_IsFirstPageHigher)
        {
            FirstPageLikesCount = i_FirstPageLikesCount;
            SecondPageLikesCount = i_SecondPageLikesCount;
            IsCorrect = i_IsCorrect;
            IsFirstPageHigher = i_IsFirstPageHigher;
        }
    }
}
