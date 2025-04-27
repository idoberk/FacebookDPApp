using System.Collections.Generic;

namespace FacebookDPApp.Backend
{
    public abstract class SortComponent<T> : IComparer<T>
    {
        public abstract int Compare(T i_First, T i_Second);
    }
}