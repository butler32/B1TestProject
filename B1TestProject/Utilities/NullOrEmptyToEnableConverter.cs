using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace B1TestProject.Utilities
{
    public class NullOrEmptyToEnableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || String.IsNullOrEmpty(value as string))
            {
                return false;
            }

            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
