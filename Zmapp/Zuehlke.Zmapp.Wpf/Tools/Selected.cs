using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Zuehlke.Zmapp.Wpf.Tools
{
    /// <summary>
    /// Static Class that holds all Dependency Properties and Static methods to allow 
    /// the Selected event of the Selector class to be attached to a Command. 
    /// </summary>
    public static class Selected
    {
        private static readonly DependencyProperty SelectedCommandBehaviorProperty = DependencyProperty.RegisterAttached(
            "SelectedCommandBehavior",
            typeof(SelectorSelectedCommandBehavior),
            typeof(Selected),
            null);

        /// <summary>
        /// Command to execute on selected event.
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached(
            "Command",
            typeof(ICommand),
            typeof(Selected),
            new PropertyMetadata(OnSetCommandCallback));

        /// <summary>
        /// Command parameter to supply on command execution.
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.RegisterAttached(
            "CommandParameter",
            typeof(object),
            typeof(Selected),
            new PropertyMetadata(OnSetCommandParameterCallback));


        /// <summary>
        /// Sets the <see cref="ICommand"/> to execute on the selected event.
        /// </summary>
        /// <param name="selector">Selector dependency object to attach command</param>
        /// <param name="command">Command to attach</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Only works for buttonbase")]
        public static void SetCommand(Selector selector, ICommand command)
        {
            if (selector == null) throw new System.ArgumentNullException("selector");
            selector.SetValue(CommandProperty, command);
        }

        /// <summary>
        /// Retrieves the <see cref="ICommand"/> attached to the <see cref="ButtonBase"/>.
        /// </summary>
        /// <param name="selector">Selector containing the Command dependency property</param>
        /// <returns>The value of the command attached</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Only works for buttonbase")]
        public static ICommand GetCommand(Selector selector)
        {
            if (selector == null) throw new System.ArgumentNullException("selector");
            return selector.GetValue(CommandProperty) as ICommand;
        }

        /// <summary>
        /// Sets the value for the CommandParameter attached property on the provided <see cref="ButtonBase"/>.
        /// </summary>
        /// <param name="selector">Selector to attach CommandParameter</param>
        /// <param name="parameter">Parameter value to attach</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Only works for buttonbase")]
        public static void SetCommandParameter(Selector selector, object parameter)
        {
            if (selector == null) throw new System.ArgumentNullException("selector");
            selector.SetValue(CommandParameterProperty, parameter);
        }

        /// <summary>
        /// Gets the value in CommandParameter attached property on the provided <see cref="ButtonBase"/>
        /// </summary>
        /// <param name="selector">Selector that has the CommandParameter</param>
        /// <returns>The value of the property</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Only works for buttonbase")]
        public static object GetCommandParameter(Selector selector)
        {
            if (selector == null) throw new System.ArgumentNullException("selector");
            return selector.GetValue(CommandParameterProperty);
        }

        private static void OnSetCommandCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var selector = dependencyObject as Selector;
            if (selector != null)
            {
                SelectorSelectedCommandBehavior behavior = GetOrCreateBehavior(selector);
                behavior.Command = e.NewValue as ICommand;
            }
        }

        private static void OnSetCommandParameterCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var selector = dependencyObject as Selector;
            if (selector != null)
            {
                SelectorSelectedCommandBehavior behavior = GetOrCreateBehavior(selector);
                behavior.CommandParameter = e.NewValue;
            }
        }

        private static SelectorSelectedCommandBehavior GetOrCreateBehavior(Selector selector)
        {
            var behavior = selector.GetValue(SelectedCommandBehaviorProperty) as SelectorSelectedCommandBehavior;
            if (behavior == null)
            {
                behavior = new SelectorSelectedCommandBehavior(selector);
                selector.SetValue(SelectedCommandBehaviorProperty, behavior);
            }

            return behavior;
        }
    }
}