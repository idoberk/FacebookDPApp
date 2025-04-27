using System;

namespace FacebookDPApp.Backend
{
    public class PropertySorter<T, TKey> : SortComponent<T>
        where TKey : IComparable<TKey>
    {
        private readonly Func<T, TKey> r_PropertySelector;
        private readonly bool r_Ascending;

        public string PropertyName { get; private set; }

        public PropertySorter(Func<T, TKey> i_PropertySelector, bool i_Ascending, string i_PropertyName = "")
        {
            r_PropertySelector = i_PropertySelector;
            r_Ascending = i_Ascending;
            PropertyName = i_PropertyName;
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
                TKey firstValue = r_PropertySelector(i_First);
                TKey secondValue = r_PropertySelector(i_Second);

                if (firstValue == null && secondValue == null)
                {
                    result = 0;
                }
                else if (firstValue == null)
                {
                    result = -1;
                }
                else if (secondValue == null)
                {
                    result = 1;
                }
                else
                {
                    result = firstValue.CompareTo(secondValue);

                    if (!r_Ascending)
                    {
                        result = -result;
                    }
                }
            }

            return result;
        }
    }
}
