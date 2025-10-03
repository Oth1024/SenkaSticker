using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.TextFormatting;
using Avalonia.Styling;
using Avalonia.VisualTree;
using IEnumerable = System.Collections.IEnumerable;

namespace SenkaSticker.Controls.Components.PartViews
{
    public partial class CollectionsTree : UserControl
    {
        #region Constructor
        public CollectionsTree()
        {
            InitializeComponent();
        }
        #endregion

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Styled Properties
        public double IconSize
        {
            get { return GetValue(IconSizeProperty); }
            set { SetValue(IconSizeProperty, value); }
        }

        public static readonly StyledProperty<double> IconSizeProperty
            = AvaloniaProperty.Register<SeperateTimePicker, double>(nameof(IconSize), 10);

        public double HeaderHeight
        {
            get { return GetValue(HeaderHeightProperty); }
            set { SetValue(HeaderHeightProperty, value); }
        }

        public static readonly StyledProperty<double> HeaderHeightProperty
            = AvaloniaProperty.Register<SeperateTimePicker, double>(nameof(HeaderHeight), 20);

        public double HeaderFontSize
        {
            get { return GetValue(HeaderFontSizeProperty); }
            set { SetValue(HeaderFontSizeProperty, value); }
        }

        public static readonly StyledProperty<double> HeaderFontSizeProperty
            = AvaloniaProperty.Register<SeperateTimePicker, double>(nameof(HeaderFontSize), 14);

        public string Header
        {
            get { return GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public static readonly StyledProperty<string> HeaderProperty
            = AvaloniaProperty.Register<SeperateTimePicker, string>(nameof(Header), "Header");

        public IBrush HeaderForeground
        {
            get { return GetValue(HeaderForegroundProperty); }
            set { SetValue(HeaderForegroundProperty, value); }
        }

        public static readonly StyledProperty<IBrush> HeaderForegroundProperty
            = AvaloniaProperty.Register<SeperateTimePicker, IBrush>(nameof(HeaderForeground), Brushes.Gray);

        public FontStyle HeaderFontStyle
        {
            get { return GetValue(HeaderFontStyleProperty); }
            set { SetValue(HeaderFontStyleProperty, value); }
        }

        public static readonly StyledProperty<FontStyle> HeaderFontStyleProperty
            = AvaloniaProperty.Register<SeperateTimePicker, FontStyle>(nameof(HeaderFontStyle), FontStyle.Normal);

        public bool IsExpanded
        {
            get { return GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        public static readonly StyledProperty<bool> IsExpandedProperty
            = AvaloniaProperty.Register<SeperateTimePicker, bool>(nameof(IsExpanded), false);

        public IEnumerable? ItemsSource
        {
            get { return GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly StyledProperty<IEnumerable?> ItemsSourceProperty =
            AvaloniaProperty.Register<ItemsControl, IEnumerable?>(nameof(ItemsSource));
        #endregion

        #region Event Callback
        public void OnButtonClicked(object sender, RoutedEventArgs args)
        {
            IsExpanded = !IsExpanded;
        }
        #endregion

        #region Private
        #endregion
    }
}