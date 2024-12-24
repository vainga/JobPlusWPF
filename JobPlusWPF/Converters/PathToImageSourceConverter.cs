using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace JobPlusWPF.Converters
{
    public class PathToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string filePath && !string.IsNullOrEmpty(filePath))
            {
                return new BitmapImage(new Uri(filePath));
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
