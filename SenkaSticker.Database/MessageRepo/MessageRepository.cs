using SenkaSticker.Common.Utilities;
using SqlSugar;
using System;
using System.Collections;
using System.Linq;
using System.Threading;

namespace SenkaSticker.Database.MessageRepo
{
    public class MessageRepository : RepositoryBase<MessageEntity>
    {
        #region Constructor
        private MessageRepository()
        {

        }
        #endregion

        #region Singleton
        public static MessageRepository Instance => Singleton<MessageRepository>.Instance;
        #endregion

        #region Events
        #endregion

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Methods
        public List<MessageEntity> GetAllMessages()
        {
            return _db.Queryable<MessageEntity>().Where(x => x.Confirmed == 0).ToList();
        }

        public void InsertOrUpdateMessage(
            string title,
            string description,
            DateTime createTime,
            bool confirmed)
        {
            if (!Exist(createTime, out var messageEntity))
            {
                _db.Insertable(new MessageEntity()
                {
                    Title = title,
                    CreateTime = createTime,
                    Description = description,
                    Confirmed = 0
                }).ExecuteCommand();
            }
            else
            {
                messageEntity.Title = title;
                messageEntity.CreateTime = createTime;
                messageEntity.Description = description;
                messageEntity.Confirmed = confirmed ? 1 : 0;
                _db.Updateable(messageEntity).ExecuteCommand();
            }
        }

        public void DeleteMessage(
            string title,
            string description,
            DateTime createTime)
        {
            if (Exist(createTime, out _))
            {
                _db.Deleteable<MessageEntity>()
                    .Where(x =>
                    x.Title == title
                    && x.Description == description
                    && x.CreateTime == createTime
                    && x.Confirmed == 0).ExecuteCommand();
            }
        }

        public bool Exist(
            DateTime createTime,
            out MessageEntity? messageEntity)
        {
            messageEntity = _db.Queryable<MessageEntity>()
                .Where(x =>
                x.CreateTime == createTime)
                .ToList()
                .FirstOrDefault();
            return messageEntity != null;
        }
        #endregion
    }
}
