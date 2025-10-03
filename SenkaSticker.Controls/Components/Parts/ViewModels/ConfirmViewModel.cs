using Avalonia;
using Avalonia.Interactivity;
using Avalonia.Styling;
using SenkaSticker.Controls.Base;
using SenkaSticker.Controls.Model;

namespace SenkaSticker.Controls.Components.Parts.ViewModels
{
    public class ConfirmViewModel : ViewModelBase
    {
        #region Constructor
        public ConfirmViewModel(MessageModel messageModel)
        {
            MessageContent = messageModel;
        }
        #endregion

        #region Events
        public event EventHandler MessageConfirmedEvent;
        public event EventHandler MessageIgnoredEvent;
        #endregion

        #region Fields
        #endregion

        #region Properties
        public MessageModel MessageContent { get; private set; }
        #endregion

        #region Methods
        public void Confirm()
        {
            MessageContent.Confirmed = true;
            MessageConfirmedEvent?.Invoke(this, EventArgs.Empty);
        }

        public void Ignore()
        {
            MessageIgnoredEvent?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region Private Methods
        #endregion
    }
}
