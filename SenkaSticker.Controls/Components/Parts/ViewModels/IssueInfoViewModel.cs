using Avalonia;
using Avalonia.Interactivity;
using Avalonia.Styling;
using SenkaSticker.Controls.Base;
using SenkaSticker.Common.TypeDef.Enum;
using SenkaSticker.Controls.Model;
using SenkaSticker.Build.Attributes;

namespace SenkaSticker.Controls.Components.Parts.ViewModels
{
    // Issue Statics
    [IncludeSelfNotifyProperty(typeof(string), "IssueName")]
    [IncludeSelfNotifyProperty(typeof(string), "Description")]
    [IncludeSelfNotifyProperty(typeof(string), "RootCause")]
    [IncludeSelfNotifyProperty(typeof(string), "Solution")]
    [IncludeSelfNotifyProperty(typeof(DateTime), "PlannedStartTime")]
    [IncludeSelfNotifyProperty(typeof(DateTime), "PlannedFinishTime")]
    // Performance
    [IncludeSelfNotifyProperty(typeof(bool), "IsExpand")]
    [IncludeSelfNotifyProperty(typeof(bool), "IsEditing")]
    public partial class IssueInfoViewModel : ViewModelBase
    {
        #region Constructor
        public IssueInfoViewModel(IssueType issueType)
        {
            IssueModel = new IssueModel(issueType);
        }

        public IssueInfoViewModel(IssueModel issueModel)
        {
            IssueModel = issueModel;
            SyncModelStaticsToGui();
        }
        #endregion

        #region Events
        public event EventHandler IssueSaved;
        public event EventHandler<bool> IssueInfoExpand;
        public event EventHandler<bool> IssueInfoStared;
        public event EventHandler<IssueState> IssueStateChanged;
        #endregion

        #region Fields
        #endregion

        #region Properties
        public IssueModel IssueModel { get; set; }
        #endregion

        #region Methods
        public void Expand()
        {
            IsExpand = !_isExpand;
            IssueInfoExpand?.Invoke(this, !_isExpand);
        }

        public void DoStar()
        {
            IssueModel.IsStared = true;
            IssueInfoStared?.Invoke(this, true);
        }

        public void DoUnStar()
        {
            IssueModel.IsStared = false;
            IssueInfoStared?.Invoke(this, false);
        }

        public void TryStar()
        {
            if (IssueModel.IsStared)
            {
                DoUnStar();
            }
            else
            {
                DoStar();
            }
        }

        public void Edit()
        {
            IsEditing = true;
        }

        public void Save()
        {
            SyncGuiStaticsToModel();
            IssueSaved?.Invoke(this, EventArgs.Empty);
            IsEditing = false;
        }

        public void Cancel()
        {
            SyncModelStaticsToGui();
            IsEditing = false;
        }

        public void EnterNextStage()
        {
            switch (IssueModel.State)
            {
                case IssueState.Backlog:
                    IssueModel.State = IssueState.Processing;
                    IssueModel.ActualStartTime = DateTime.Now;
                    break;
                case IssueState.Processing:
                    IssueModel.State = IssueState.WaitingForVerification;
                    break;
                case IssueState.WaitingForVerification:
                    IssueModel.State = IssueState.Verifying;
                    break;
                case IssueState.Verifying:
                    IssueModel.State = IssueState.Closed;
                    IssueModel.ActualFinishTime = DateTime.Now;
                    break;
                case IssueState.Closed:
                    IssueModel.State = IssueState.Backlog;
                    break;
            }
            IssueStateChanged?.Invoke(this, IssueModel.State);
        }
        #endregion

        #region Private Methods
        private void SyncGuiStaticsToModel()
        {
            if (!string.IsNullOrEmpty(IssueName))
            {
                IssueModel.IssueName = IssueName;
            }
            IssueModel.Description = Description;
            IssueModel.RootCause = RootCause;
            IssueModel.Solution = Solution;
            IssueModel.PlannedStartTime = PlannedStartTime;
            IssueModel.PlannedFinishTime = PlannedFinishTime;
        }

        private void SyncModelStaticsToGui()
        {
            IssueName = IssueModel.IssueName;
            Description = IssueModel.Description;
            RootCause = IssueModel.RootCause;
            Solution = IssueModel.Solution;
            PlannedStartTime = IssueModel.PlannedStartTime;
            PlannedFinishTime = IssueModel.PlannedFinishTime;
        }
        #endregion
    }
}
