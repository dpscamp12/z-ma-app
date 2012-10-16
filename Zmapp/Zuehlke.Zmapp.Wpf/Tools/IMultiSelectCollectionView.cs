using System.Windows.Controls.Primitives;

namespace Zuehlke.Zmapp.Wpf.Tools
{
    public interface IMultiSelectCollectionView
    {
        void AddControl(Selector selector);
        void RemoveControl(Selector selector);
    }
}
