using System;
using Microsoft.Practices.Prism.Commands;

namespace Zuehlke.Zmapp.Wpf
{
	/// <summary>
	/// This class is needed to prevent a binding issue inside Prism.
	/// see http://baboon.eu/post/2011/11/27/Unable-to-cast-object-of-type-MSInternalNamedObject-to-%E2%80%A6.aspx
	/// </summary>
	public class PrismReplacementDelegateCommand<T> : DelegateCommandBase where T : class
	{
		public PrismReplacementDelegateCommand(Action<T> executeMethod)
			: this(executeMethod, (o) => true)
		{
		}

		public PrismReplacementDelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
			: base((o) => executeMethod(GetObjectSafeCastedAs<T>(o)), (o) => canExecuteMethod(GetObjectSafeCastedAs<T>(o)))
		{
			if (executeMethod == null || canExecuteMethod == null)
				throw new ArgumentNullException("executeMethod", "Delegates cannot be null");

#if !WINDOWS_PHONE
			Type genericType = typeof(T);

			if (genericType.IsValueType)
			{
				if ((!genericType.IsGenericType) || (!typeof(Nullable<>).IsAssignableFrom(genericType.GetGenericTypeDefinition())))
				{
					throw new InvalidCastException("DelegateCommand Invalid Generic Payload Type");
				}
			}
#endif
		}

		public bool CanExecute(T parameter)
		{
			return base.CanExecute(parameter);
		}

		public void Execute(T parameter)
		{
			base.Execute(parameter);
		}

		private static T GetObjectSafeCastedAs<T>(object objToCast)
		{
			if (objToCast == null) return default(T);

			if (objToCast.GetType().IsAssignableFrom(typeof(T)))
			{
				return (T)objToCast;
			}
			else return default(T);
		}
	}
}