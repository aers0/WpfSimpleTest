using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfSimpleTest.Helpers
{
    class IntervalToFrameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return  1 / (double)value * 1000;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double) 1000 /(int)value;
        }
    }
}
