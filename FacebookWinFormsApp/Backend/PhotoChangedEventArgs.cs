using System;
using FacebookWrapper.ObjectModel;

namespace FacebookDPApp.Backend
{
    public class PhotoChangedEventArgs : EventArgs
    {
        public Photo Photo { get; }

        public PhotoChangedEventArgs(Photo i_Photo)
        {
            Photo = i_Photo;
        }
    }
}
