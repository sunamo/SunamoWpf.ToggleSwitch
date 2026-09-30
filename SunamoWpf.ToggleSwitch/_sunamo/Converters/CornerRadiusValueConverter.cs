using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SunamoWpf.ToggleSwitch._sunamo;

internal class CornerRadiusValueConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var radius = double.Parse(value.ToString(), culture);
        if (parameter != null)
        {
            radius *= double.Parse(parameter.ToString(), culture);
        }
        return new CornerRadius(radius);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return false;
    }
}
