using System;
using System.Globalization;
using Xamarin.Forms;
namespace ChatHub.Mobile.Converters
{
    public class StackLayoutAlignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isTheSameUser)
            {
                return isTheSameUser ? LayoutOptions.Start : LayoutOptions.End;
            }

            return LayoutOptions.Center;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}