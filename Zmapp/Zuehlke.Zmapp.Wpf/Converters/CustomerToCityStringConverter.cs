using System;
using System.Globalization;
using System.Windows.Data;
using Zuehlke.Zmapp.Services.Contracts.Employee;

namespace Zuehlke.Zmapp.Wpf.Converters
{
    public class CustomerToCityStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CustomerInfo info = value as CustomerInfo;

            if (info == null)
            {
                throw new ArgumentException("not expected type", "value");
            }

            return string.Format(culture, "{0} {1}", info.ZipCode, info.City);

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
