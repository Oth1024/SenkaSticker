using SenkaSticker.Common.Logger;
using SenkaSticker.Common.Utilities;
using SenkaSticker.Controls.Model;
using SenkaSticker.Database.MessageRepo;
using System;
using System.Collections;
using System.Linq;
using System.Security.AccessControl;
using System.Threading;

namespace SenkaSticker.Controls.Controllers
{
    public class MessageController
    {
        #region Constructor
        private MessageController()
        {

        }
        #endregion

        #region Events
        #endregion

        #region Singleton
        public static MessageController Instance => Singleton<MessageController>.Instance;
        #endregion

        #region Fields
        private ILogger _logger = LoggerFactory.GetLogger(nameof(MessageController));
        private MessageRepository _db => MessageRepository.Instance;
        #endregion

        #region Properties
        #endregion

        #region Methods
        public List<MessageModel> GetAllMessages()
        {
            _logger.DebugIn();
            var result = new List<MessageModel>();

            var response = _db.GetAllMessages();
            foreach (var message in response)
            {
                var messageModel = new MessageModel()
                {
                    Title = message.Title,
                    Description = message.Description,
                    Confirmed = message.Confirmed == 1 ? true : false,
                    CreateTime = message.CreateTime,
                };
                result.Add(messageModel);
            }

            _logger.DebugOut();
            return result;
        }

        public void AddOrUpdate(MessageModel messageModel)
        {
            _logger.DebugIn();

            _db.InsertOrUpdateMessage(
                messageModel.Title,
                messageModel.Description,
                messageModel.CreateTime,
                messageModel.Confirmed);

            _logger.DebugOut();
        }
        #endregion
    }
}
