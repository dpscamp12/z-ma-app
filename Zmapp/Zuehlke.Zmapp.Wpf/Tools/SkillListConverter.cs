using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Zuehlke.Zmapp.Services.Contracts.Employees;

namespace Zuehlke.Zmapp.Wpf.Tools
{
	public class SkillListConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var skills = value as IEnumerable<Skill>;

			string result = string.Empty;

			if (skills != null)
			{
				foreach (var skill in skills)
				{
					result += " " + skill;
				}
			}

			return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return DependencyProperty.UnsetValue;
		}
	}
}