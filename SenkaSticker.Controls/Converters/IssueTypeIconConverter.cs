using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using SenkaSticker.Common.TypeDef.Enum;
using System.Globalization;

namespace SenkaSticker.Controls.Converters
{
    public class IssueStarIconConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool isStared)
            {
                var icon = isStared switch
                {
                    true => new Bitmap(AssetLoader.Open(new Uri("avares://SenkaSticker.Controls/Assets/Issue/Star.png"))),
                    false => new Bitmap(AssetLoader.Open(new Uri("avares://SenkaSticker.Controls/Assets/Issue/GrayStar.png"))),
                };
                return icon;
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
