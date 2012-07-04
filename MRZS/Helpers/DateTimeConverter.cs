using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Data;

namespace MRZS.Helpers
{
    public class DateTimeConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DateTime dateTime;
            var nullableDateTime = value as Nullable<DateTime>;
            if (nullableDateTime != null && nullableDateTime.HasValue)
            {
                dateTime = nullableDateTime.Value;
            }
            else
            {
                dateTime = (DateTime)value;
            }
            return dateTime.ToString("dd.MM.yyyy H:mm:ss");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
