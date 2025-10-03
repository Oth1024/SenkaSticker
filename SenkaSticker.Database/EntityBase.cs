using SqlSugar;

namespace SenkaSticker.Database
{
    public class EntityBase
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
    }
}
