using System;
using FacebookWrapper.ObjectModel;

namespace FacebookDPApp.Backend
{
    public class PageSelectedEventArgs : EventArgs
    {
        public Page FirstPage { get; }
        public Page SecondPage { get; }

        public PageSelectedEventArgs(Page i_FirstPage, Page i_SecondPage)
        {
            FirstPage = i_FirstPage;
            SecondPage = i_SecondPage;
        }
    }
}
