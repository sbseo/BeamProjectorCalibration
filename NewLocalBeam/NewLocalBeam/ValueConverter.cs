using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;


namespace NewLocalBeam
{
    public partial class ValueConverter : Window
    {
        public ValueConverter()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }
    }

    public class BorderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (System.Convert.ToDouble(value)) + 5.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Cannot convert back");
        }
    }
}