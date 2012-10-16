using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Zuehlke.Zmapp.Wpf.Tools
{
    public static class EnumerableExtensions
    {
        public static void ReplaceAllItemsWith<T>(this ObservableCollection<T> existingCollection, IEnumerable<T> newValues)
        {
            existingCollection.Clear();
            foreach (var newValue in newValues)
            {
                existingCollection.Add(newValue);
            }
        }
    }
}