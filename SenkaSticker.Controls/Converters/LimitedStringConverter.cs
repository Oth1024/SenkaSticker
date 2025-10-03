using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenkaSticker.Controls.Converters
{
    public class LimitedStringConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string toLimitString)
            {
                var defaultLimit = 50;
                if (parameter is string param)
                {
                    try
                    {
                        defaultLimit = int.Parse(param);
                    }
                    catch
                    {
                        // DO NOTHING
                    }
                }
                if (toLimitString.Length > defaultLimit)
                {
                    var toDisplay = toLimitString.Substring(0, 46);
                    return toDisplay + "...";
                }
                else
                {
                    return toLimitString;
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
