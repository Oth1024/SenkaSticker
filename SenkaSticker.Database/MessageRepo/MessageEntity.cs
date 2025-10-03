using SqlSugar;

namespace SenkaSticker.Database.MessageRepo
{
    [SugarTable("Messages")]
    public class MessageEntity : EntityBase
    {
        [SugarColumn(IsNullable = false)]
        public string Title { get; set; }

        [SugarColumn(IsNullable = false)]
        public DateTime CreateTime { get; set; }

        [SugarColumn(IsNullable = false, ColumnDataType = "LONGTEXT")]
        public string Description { get; set; }

        [SugarColumn(IsNullable = false)]
        public int Confirmed { get; set; }
    }
}
