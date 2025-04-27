using System.Collections.Generic;

namespace FacebookDPApp.Backend
{
    public class CompositeSorter<T> : SortComponent<T>
    {
        private readonly List<SortComponent<T>> r_Sorters = new List<SortComponent<T>>();

        public string Name { get; private set; }

        public CompositeSorter(string i_Name = "Composite Sorter")
        {
            Name = i_Name;
        }

        public void Add(SortComponent<T> i_Sorter)
        {
            r_Sorters.Add(i_Sorter);
        }

        public override int Compare(T i_First, T i_Second)
        {
            int result = 0;

            if (i_First == null && i_Second == null)
            {
                result = 0;
            }
            else if (i_First == null)
            {
                result = -1;
            }
            else if (i_Second == null)
            {
                result = 1;
            }
            else
            {
                foreach (SortComponent<T> sorter in r_Sorters)
                {
                    int sorterResult = sorter.Compare(i_First, i_Second);

                    if (sorterResult != 0)
                    {
                        result = sorterResult;
                        break;
                    }
                }
            }

            return result;
        }
    }
}