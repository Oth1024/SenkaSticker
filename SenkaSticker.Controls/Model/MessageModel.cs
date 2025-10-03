using Caliburn.Micro;
using SenkaSticker.Build.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenkaSticker.Controls.Model
{
    [IncludeSelfNotifyProperty(typeof(string), "Title")]
    [IncludeSelfNotifyProperty(typeof(string), "Description")]
    [IncludeSelfNotifyProperty(typeof(bool), "Confirmed")]
    public partial class MessageModel : PropertyChangedBase
    {
        public MessageModel()
        {

        }

        public MessageModel(string title, string description, DateTime createTime)
        {
            Title = title;
            Description = description;
            Confirmed = false;
            CreateTime = createTime;
        }

        public DateTime CreateTime { get; set; }
    }
}
