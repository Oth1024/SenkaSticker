using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenkaSticker.Controls.Converters
{
    public class EmptyStringConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string mayEmptyString)
            {
                if (string.IsNullOrEmpty(mayEmptyString) || value == null)
                {
                    return "Null";
                }
                else
                {
                    return mayEmptyString;
                }
            }
            else
            {
                return null;
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class EmptyStringFontStyleConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string mayEmptyString)
            {
                if (string.IsNullOrEmpty(mayEmptyString))
                {
                    return FontStyle.Italic;
                }
                else
                {
                    return FontStyle.Normal;
                }
            }
            else
            {
                return null;
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
