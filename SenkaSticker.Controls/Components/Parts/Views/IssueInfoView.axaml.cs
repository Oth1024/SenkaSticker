using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace SenkaSticker.Controls.Components.Parts.Views
{
    public partial class IssueInfoView : UserControl
    {
        #region Constructor
        public IssueInfoView()
        {
            InitializeComponent();
        }
        #endregion

        #region Fields
        private FlyoutBase? _processStateflyout;
        #endregion

        #region Methods
        public void OnProcessStateFlyoutOpen(object sender, EventArgs _)
        {
            if (_processStateflyout == null)
            {
                _processStateflyout = sender as FlyoutBase;
            }
        }

        public void OnButtonClicked(object sender, RoutedEventArgs args)
        {
            var button = sender as Button;
            button?.Command?.Execute(null);
            _processStateflyout?.Hide();
        }
        #endregion
    }
}