using System;
using System.Globalization;
using Xamarin.Forms;

namespace RPM_X_2.Converters
{
    public class InverseBoolConverter : IValueConverter
    {
        public static InverseBoolConverter Instance { get; } = new();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            value is false;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            value is false;
    }
}