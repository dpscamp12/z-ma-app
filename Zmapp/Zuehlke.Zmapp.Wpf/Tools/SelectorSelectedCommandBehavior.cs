using Microsoft.Practices.Prism.Commands;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Zuehlke.Zmapp.Wpf.Tools
{
    /// <summary>
    /// Behavior that allows controls that derrive from <see cref="Selector"/> to hook up with <see cref="ICommand"/> objects. 
    /// </summary>
    public class SelectorSelectedCommandBehavior : CommandBehaviorBase<Selector>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectorSelectedCommandBehavior"/> class.
        /// </summary>
        /// <param name="targetObject">The target object.</param>
        public SelectorSelectedCommandBehavior(Selector targetObject)
            : base(targetObject)
        {
            targetObject.SelectionChanged += this.TargetObject_SelectionChanged;
        }

        private void TargetObject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            base.ExecuteCommand();
        }
    }
}