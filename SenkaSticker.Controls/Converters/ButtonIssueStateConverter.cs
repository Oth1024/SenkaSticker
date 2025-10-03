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
    public class ButtonForegroundIssueStateConverter : IValueConverter
    {
        private IBrush _darkGray = new SolidColorBrush(Color.Parse("#4c4c4c"));

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is IssueState issueType)
            {
                var color = issueType switch
                {
                    IssueState.Backlog => _darkGray,
                    IssueState.Processing => Brushes.White,
                    IssueState.WaitingForVerification => Brushes.White,
                    IssueState.Verifying => Brushes.White,
                    IssueState.Closed => Brushes.White,
                    _ => null
                };
                return color;
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

    public class ButtonBackgroundIssueStateConverter : IValueConverter
    {
        private IBrush _lightBlue = new SolidColorBrush(Color.Parse("#0181FF"));
        private IBrush _lightGray = new SolidColorBrush(Color.Parse("#afafaf"));

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is IssueState issueType)
            {
                IBrush color = issueType switch
                {
                    IssueState.Backlog => _lightGray,
                    IssueState.Processing => _lightBlue,
                    IssueState.WaitingForVerification => _lightBlue,
                    IssueState.Verifying => _lightBlue,
                    IssueState.Closed => Brushes.Green,
                    _ => null
                };
                return color;
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

    public class ButtonItemVisibleIssueStateConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is IssueState issueType)
            {
                var showItem = issueType switch
                {
                    IssueState.Backlog => true,
                    IssueState.Processing => true,
                    IssueState.WaitingForVerification => true,
                    IssueState.Verifying => true,
                    IssueState.Closed => false,
                    _ => false
                };
                return showItem;
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

    public class ButtonFlyoutContentIssueStateConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is IssueState issueType)
            {
                var content = issueType switch
                {
                    IssueState.Backlog => "Start Process",
                    IssueState.Processing => "Finish",
                    IssueState.WaitingForVerification => "Start Verifying",
                    IssueState.Verifying => "Finish",
                    IssueState.Closed => "Restart",
                    _ => ""
                };
                return content;
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
