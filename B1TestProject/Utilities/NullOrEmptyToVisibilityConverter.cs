using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace B1TestProject.Utilities
{
    public class NullOrEmptyToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool invert = parameter != null && parameter.ToString() == "Invert";

            if (!invert)
            {
                if (value == null || String.IsNullOrEmpty(value as string))
                {
                    return Visibility.Visible;
                }
            }
            else
            {
                if (value == null || String.IsNullOrEmpty(value as string))
                {
                    return Visibility.Collapsed;
                }
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
