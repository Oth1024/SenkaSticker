using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using SenkaSticker.Common.TypeDef.Enum;
using System.Globalization;

namespace SenkaSticker.Controls.Converters
{
    public class IssueTypeIconConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is IssueType issueType)
            {
                var icon = issueType switch
                {
                    IssueType.Issue => new Bitmap(AssetLoader.Open(new Uri("avares://SenkaSticker.Controls/Assets/Issue/Issue.png"))),
                    IssueType.Test => new Bitmap(AssetLoader.Open(new Uri("avares://SenkaSticker.Controls/Assets/Issue/Test.png"))),
                    IssueType.Requirement => new Bitmap(AssetLoader.Open(new Uri("avares://SenkaSticker.Controls/Assets/Issue/Requirement.png"))),
                    _ => null
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
