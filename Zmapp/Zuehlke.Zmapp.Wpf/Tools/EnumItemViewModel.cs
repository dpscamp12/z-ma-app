using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Zuehlke.Zmapp.Wpf.Tools
{
	public class EnumItemViewModel<TEnum> : NotificationObject where TEnum : struct
	{
		private readonly TEnum value;
		private readonly FieldInfo enumValueFieldInfo;
		private bool isSelectable = true;
		private bool isSelected;

		public EnumItemViewModel(TEnum value)
		{
			this.value = value;
			this.enumValueFieldInfo = value.GetType().GetField(value.ToString());
		}

		public TEnum Value
		{
			get { return this.value; }
		}

		public string DisplayName
		{
			get { return this.enumValueFieldInfo.ToString(); }
		}

		public bool IsSelectable
		{
			get { return this.isSelectable; }
			set
			{
				if (value == this.isSelectable)
					return;

				this.isSelectable = value;
				RaisePropertyChanged(() => this.IsSelectable);
			}
		}

		public bool IsSelected
		{
			get
			{
				return this.isSelected;
			}
			set
			{
				if (this.isSelected != value)
				{
					this.isSelected = value;
					this.RaisePropertyChanged(() => this.IsSelected);
				}
			}
		}

		public override string ToString()
		{
			return this.value.ToString();
		}

		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			var viewModel = obj as EnumItemViewModel<TEnum>;
			return viewModel != null && this.value.Equals(viewModel.Value);
		}

		#region IItemMetadata members

		/// <summary>
		/// Occurs when the metadata has been changed, used for the filtered collection.
		/// </summary>
		public event EventHandler MetadataChanged;

		public void RaiseMetadataChanged()
		{
			EventHandler handler = this.MetadataChanged;
			if (handler != null)
			{
				handler(this, EventArgs.Empty);
			}
		}

		#endregion

		#region Factory Methods

		public static IEnumerable<EnumItemViewModel<TEnum>> GetViewModels()
		{
			return Enum.GetValues(typeof(TEnum))
				.OfType<TEnum>()
				.Select(value => new EnumItemViewModel<TEnum>(value))
				.ToArray();
		}

		public static IEnumerable<EnumItemViewModel<TEnum>> GetViewModels(IEnumerable<TEnum> values)
		{
			return values
				.Select(value => new EnumItemViewModel<TEnum>(value))
				.ToArray();
		}

		#endregion
	}

}
