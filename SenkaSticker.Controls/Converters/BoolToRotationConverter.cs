using Avalonia.Data.Converters;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using SenkaSticker.Common.TypeDef.Enum;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenkaSticker.Controls.Converters
{
    public class BoolToRotationConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool isExpand)
            {
                if (isExpand)
                {
                    return 90;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return 0;
        }
    }
}
