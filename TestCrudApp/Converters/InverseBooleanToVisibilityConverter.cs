using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TestApp.Converters;

public class InverseBooleanToVisibilityConverter:IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
            return boolValue? Visibility.Collapsed : Visibility.Visible;

        return Visibility.Visible; // По умолчанию показываем
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Visibility visibility)
            return visibility == Visibility.Visible;

        return false;
    }
}