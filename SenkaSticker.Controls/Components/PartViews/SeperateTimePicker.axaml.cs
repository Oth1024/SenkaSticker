using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.VisualTree;

namespace SenkaSticker.Controls.Components.PartViews
{
    public partial class SeperateTimePicker : UserControl
    {
        #region Constructor
        public SeperateTimePicker()
        {
            InitializeComponent();

            // 初始化日期
            for (int i = 1; i <= 31; i++)
            {
                var menuItem = new MenuItem() { Header = i };
                menuItem.Click += OnDayItemSelected;
                _dayDictionary[i] = menuItem;
            }
        }
        #endregion

        #region Fields
        private object _logicAttachedLock = new object();
        private bool _yearGet = false;
        private bool _monthGet = false;
        private bool _dayGet = false;

        private TextBox? _yearBox;
        private TextBox? _monthBox;
        private TextBox? _dayBox;
        private readonly DateTime _current = DateTime.Now;

        private readonly Dictionary<int, MenuItem> _dayDictionary = new Dictionary<int, MenuItem>();

        IBrush _selected = new SolidColorBrush(Color.Parse("#ea5e27"));
        IBrush _released = new SolidColorBrush(Color.Parse("#000000"));
        #endregion

        #region Properties
        #endregion

        #region Styled Properties
        public double YearWidth
        {
            get { return GetValue(YearWidthProperty); }
            set { SetValue(YearWidthProperty, value); }
        }

        public static readonly StyledProperty<double> YearWidthProperty
            = AvaloniaProperty.Register<SeperateTimePicker, double>(nameof(YearWidth), 40);

        public double MonthWidth
        {
            get { return GetValue(MonthWidthProperty); }
            set { SetValue(MonthWidthProperty, value); }
        }

        public static readonly StyledProperty<double> MonthWidthProperty
            = AvaloniaProperty.Register<SeperateTimePicker, double>(nameof(MonthWidth), 20);

        public double DayWidth
        {
            get { return GetValue(DayWidthProperty); }
            set { SetValue(DayWidthProperty, value); }
        }

        public static readonly StyledProperty<double> DayWidthProperty
            = AvaloniaProperty.Register<SeperateTimePicker, double>(nameof(DayWidth), 20);

        public DateTime DateTime
        {
            get { return GetValue(DateTimeProperty); }
            set
            {
                SetValue(DateTimeProperty, value);
            }
        }

        public static readonly StyledProperty<DateTime> DateTimeProperty
            = AvaloniaProperty.Register<SeperateTimePicker, DateTime>(nameof(DateTime), DateTime.Now, defaultBindingMode: Avalonia.Data.BindingMode.TwoWay);

        public int MaxTimePeriod
        {
            get { return GetValue(MaxTimePeriodProperty); }
            set { SetValue(MaxTimePeriodProperty, value); }
        }

        public static readonly StyledProperty<int> MaxTimePeriodProperty
            = AvaloniaProperty.Register<SeperateTimePicker, int>(nameof(MaxTimePeriod), 5);
        #endregion

        #region Event Callback
        public void OnTextBoxAttachedToLogicTree(object sender, LogicalTreeAttachmentEventArgs args)
        {
            lock (_logicAttachedLock)
            {
                if (sender is TextBox textBox)
                {
                    if (textBox.Name == "YearTextBox" && !_yearGet)
                    {
                        _yearBox = textBox;
                        _yearBox.AddHandler(PointerPressedEvent, OnTextBoxClick, handledEventsToo: true);
                        _yearGet = true;
                    }
                    else if (textBox.Name == "MonthTextBox" && !_monthGet)
                    {
                        _monthBox = textBox;
                        _monthBox.AddHandler(PointerPressedEvent, OnTextBoxClick, handledEventsToo: true);
                        _monthGet = true;
                    }
                    else if (textBox.Name == "DayTextBox" && !_dayGet)
                    {
                        _dayBox = textBox;
                        _dayBox.AddHandler(PointerPressedEvent, OnTextBoxClick, handledEventsToo: true);
                        _dayGet = true;
                    }
                }
            }
        }

        public void OnTextBoxClick(object sender, PointerPressedEventArgs args)
        {
            if (sender is TextBox textBox)
            {
                var flyout = FlyoutBase.GetAttachedFlyout(textBox) as MenuFlyout;
                if (textBox.Name == "YearTextBox")
                {
                    _yearBox.BorderBrush = _selected;
                    _monthBox.BorderBrush = _released;
                    _dayBox.BorderBrush = _released;
                    flyout.Items.Clear();
                    var maxTimePeriod = GetValue(MaxTimePeriodProperty);
                    var minYear = _current.Year - 1;
                    var maxYear = _current.Year + maxTimePeriod;
                    for (var i = minYear; i <= maxYear; i++)
                    {
                        var item = new MenuItem() { Header = i };
                        item.Click += OnYearItemSelected;
                        flyout.Items.Add(item);
                    }
                }
                else if (textBox.Name == "MonthTextBox")
                {
                    _yearBox.BorderBrush = _released;
                    _monthBox.BorderBrush = _selected;
                    _dayBox.BorderBrush = _released;
                    flyout.Items.Clear();
                    for (var i = 1; i <= 12; i++)
                    {
                        var item = new MenuItem() { Header = i };
                        item.Click += OnMonthItemSelected;
                        flyout.Items.Add(item);
                    }
                }
                else if (textBox.Name == "DayTextBox")
                {
                    _yearBox.BorderBrush = _released;
                    _monthBox.BorderBrush = _released;
                    _dayBox.BorderBrush = _selected;
                    flyout.Items.Clear();
                    var dayCount = DateTime.DaysInMonth(DateTime.Year, DateTime.Month);
                    for (var i = 1; i <= dayCount; i++)
                    {
                        flyout.Items.Add(_dayDictionary[i]);
                    }
                }
                FlyoutBase.ShowAttachedFlyout(textBox);
            }
        }
        #endregion

        #region Private
        private void OnYearItemSelected(object sender, RoutedEventArgs args)
        {
            var menuItem = sender as MenuItem;
            var year = Int32.Parse(menuItem.Header.ToString());
            DateTime = DateTime.Parse($"{year}/1/1");
        }

        private void OnMonthItemSelected(object sender, RoutedEventArgs args)
        {
            var menuItem = sender as MenuItem;
            var month = Int32.Parse(menuItem.Header.ToString());
            DateTime = DateTime.Parse($"{DateTime.Year}/{month}/1");
        }

        private void OnDayItemSelected(object sender, RoutedEventArgs args)
        {
            var menuItem = sender as MenuItem;
            var day = Int32.Parse(menuItem.Header.ToString());
            DateTime = DateTime.Parse($"{DateTime.Year}/{DateTime.Month}/{day}");
        }
        #endregion
    }
}