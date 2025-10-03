using SenkaSticker.Controls.Common.EventAggregator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenkaSticker.Controls.Events
{
    public class MessageNotifyEvent : IEvent
    {
        #region Constructors
        public MessageNotifyEvent(string title, string description)
        {
            Title = title;
            Description = description;
            CreateTime = DateTime.Now;
        }
        #endregion

        #region Properties
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreateTime { get; set; }
        #endregion
    }
}
