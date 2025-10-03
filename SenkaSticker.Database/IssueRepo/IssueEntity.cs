using SenkaSticker.Common.TypeDef.Enum;
using SqlSugar;

namespace SenkaSticker.Database.IssueRepo;

[SugarTable("Issues")]
public class IssueEntity : EntityBase
{
    [SugarColumn(IsNullable = false)]
    public string IssueName { get; set; }

    [SugarColumn(IsNullable = false)]
    public IssueType IssueType { get; set; }

    [SugarColumn(IsNullable = false)]
    public IssueState IssueState { get; set; }

    [SugarColumn(IsNullable = false)]
    public int IsStared { get; set; }

    [SugarColumn(IsNullable = true, ColumnDataType ="LONGTEXT")]
    public string Description { get; set; }

    [SugarColumn(IsNullable = true, ColumnDataType ="LONGTEXT")]
    public string RootCause { get; set; }

    [SugarColumn(IsNullable = true, ColumnDataType ="LONGTEXT")]
    public string Solution { get; set; }

    [SugarColumn(IsNullable = false)]
    public DateTime CreateTime { get; set; }

    [SugarColumn(IsNullable = false)]
    public DateTime ExpectedStartTime { get; set; }

    [SugarColumn(IsNullable = false)]
    public DateTime ExpectedFinishTime { get; set; }

    [SugarColumn(IsNullable = false)]
    public DateTime ActualStartTime { get; set; }

    [SugarColumn(IsNullable = false)]
    public DateTime ActualFinishTime { get; set; }
}