using Avalonia;
using Avalonia.Interactivity;
using Avalonia.Styling;
using SenkaSticker.Controls.Base;
using SenkaSticker.Controls.Common.EventAggregator;
using SenkaSticker.Controls.Controllers;
using SenkaSticker.Controls.Events;
using SenkaSticker.Controls.Model;
using System.Collections.ObjectModel;

namespace SenkaSticker.Controls.Components.Parts.ViewModels
{
    public class MessagePopViewModel : ViewModelBase
    {
        #region Constructor
        public MessagePopViewModel()
        {
            var allMessageModels = _messageController.GetAllMessages();
            EventAggregator.Subscribe<MessageNotifyEvent>(OnMessageNotified);
            foreach (var messageModel in allMessageModels)
            {
                var messageConfirm = new ConfirmViewModel(messageModel);
                messageConfirm.MessageConfirmedEvent += OnMessageConfirmed;
                messageConfirm.MessageIgnoredEvent += OnMessageIgnored;
                _allMessages.Add(messageConfirm);
            }
            ToDisplayMessages = new(_allMessages);
        }
        #endregion

        #region Events
        #endregion

        #region Fields
        private readonly MessageController _messageController = MessageController.Instance;
        private List<ConfirmViewModel> _allMessages = new List<ConfirmViewModel>();
        #endregion

        #region Properties
        public ObservableCollection<ConfirmViewModel> ToDisplayMessages { get; set; } = new ObservableCollection<ConfirmViewModel>();
        #endregion

        #region Methods
        public void ConfirmAll()
        {
            Task.Run(() =>
            {
                foreach (var message in ToDisplayMessages)
                {
                    message.Confirm();
                }
            });
        }

        public void IgnoreAll()
        {
            Task.Run(() =>
            {
                foreach (var message in ToDisplayMessages)
                {
                    message.Ignore();
                }
            });
        }
        #endregion

        #region Private Methods
        private void OnMessageNotified(MessageNotifyEvent @event)
        {
            var messageModel = new MessageModel(@event.Title, @event.Description, @event.CreateTime);
            _messageController.AddOrUpdate(messageModel);
            var messageConfirm = new ConfirmViewModel(messageModel);
            _allMessages.Add(messageConfirm);
            ToDisplayMessages.Add(messageConfirm);
            NotifyOfPropertyChange(nameof(ToDisplayMessages));
            messageConfirm.MessageConfirmedEvent += OnMessageConfirmed;
            messageConfirm.MessageIgnoredEvent += OnMessageIgnored;
        }

        private void OnMessageConfirmed(object sender, EventArgs _)
        {
            var message = (ConfirmViewModel)sender;
            _messageController.AddOrUpdate(message.MessageContent);
            OnMessageProcessed(message);
        }

        private void OnMessageIgnored(object sender, EventArgs _)
        {
            var message = (ConfirmViewModel)sender;
            OnMessageProcessed(message);
        }

        private void OnMessageProcessed(ConfirmViewModel message)
        {
            _allMessages.Remove(message);
            ToDisplayMessages.Remove(message);
            NotifyOfPropertyChange(nameof(ToDisplayMessages));
        }
        #endregion
    }
}
