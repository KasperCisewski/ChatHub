using System;
using System.Globalization;
using Xamarin.Forms;
namespace ChatHub.Mobile.Converters
{
    public class DatetimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return string.Empty;

            var datetime = (DateTime)value;
            
            return datetime.ToString("HH:mm:ss", new CultureInfo("en-US"));
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}