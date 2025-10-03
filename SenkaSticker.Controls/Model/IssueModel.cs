using Caliburn.Micro;
using SenkaSticker.Build.Attributes;
using SenkaSticker.Common.Utilities;
using SenkaSticker.Common.TypeDef.Enum;
using System;
using System.Collections;
using System.Linq;
using System.Threading;

namespace SenkaSticker.Controls.Model
{
    [IncludeSelfNotifyProperty(typeof(string), "IssueName")]
    [IncludeSelfNotifyProperty(typeof(IssueType), "Type")]
    [IncludeSelfNotifyProperty(typeof(IssueState), "State")]
    [IncludeSelfNotifyProperty(typeof(bool), "IsStared")]
    [IncludeSelfNotifyProperty(typeof(string), "Description")]
    [IncludeSelfNotifyProperty(typeof(string), "RootCause")]
    [IncludeSelfNotifyProperty(typeof(string), "Solution")]
    [IncludeSelfNotifyProperty(typeof(DateTime), "CreatedTime")]
    [IncludeSelfNotifyProperty(typeof(DateTime), "PlannedStartTime")]
    [IncludeSelfNotifyProperty(typeof(DateTime), "ActualStartTime")]
    [IncludeSelfNotifyProperty(typeof(DateTime), "PlannedFinishTime")]
    [IncludeSelfNotifyProperty(typeof(DateTime), "ActualFinishTime")]
    public partial class IssueModel : PropertyChangedBase
    {
        #region Constructor
        public IssueModel()
        {

        }

        public IssueModel(IssueType issueType)
        {
            Type = issueType;
            State = IssueState.Backlog;
            PlannedStartTime = DateTime.Now;
            IssueName = string.Empty;
            Description = string.Empty;
            RootCause = string.Empty;
            Solution = string.Empty;
            CreatedTime = DateTime.Now;
            PlannedStartTime = DateTime.Now;
            PlannedFinishTime = DateTime.Now;
            ActualStartTime = DateTime.Now;
            ActualFinishTime = DateTime.Now;
        }
        #endregion

        #region Events
        #endregion

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Methods
        #endregion
    }
}
