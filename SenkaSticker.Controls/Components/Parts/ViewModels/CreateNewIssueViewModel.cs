using Avalonia;
using Avalonia.Interactivity;
using Avalonia.Styling;
using SenkaSticker.Build.Attributes;
using SenkaSticker.Common.TypeDef.Enum;
using SenkaSticker.Controls.Base;
using SenkaSticker.Controls.Model;
using System.Collections.ObjectModel;

namespace SenkaSticker.Controls.Components.Parts.ViewModels
{
    [IncludeSelfNotifyProperty(typeof(string), "IssueName")]
    [IncludeSelfNotifyProperty(typeof(IssueType), "IssueType")]
    [IncludeSelfNotifyProperty(typeof(DateTime), "PlannedStartTime")]
    [IncludeSelfNotifyProperty(typeof(DateTime), "PlannedFinishTime")]
    [IncludeSelfNotifyProperty(typeof(string), "Description")]
    [IncludeSelfNotifyProperty(typeof(bool), "IsCreating")]
    [IncludeSelfNotifyProperty(typeof(string), "ErrorMessage")]
    public partial class CreateNewIssueViewModel : ViewModelBase
    {
        #region Constructor
        public CreateNewIssueViewModel()
        {
            IsCreating = false;
            IssueTypes = new(Enum.GetValues<IssueType>());
            IssueType = IssueTypes.First();
            PlannedStartTime = DateTime.Now;
            PlannedFinishTime = DateTime.Now;
        }
        #endregion

        #region Events
        public event EventHandler<IssueModel>? IssueCreated;
        #endregion

        #region Fields
        #endregion

        #region Properties
        public ObservableCollection<IssueType> IssueTypes { get; private set; }
        #endregion

        #region Methods
        public void Create()
        {
            Reset();
            IsCreating = true;
        }

        public void Save()
        {
            if (AssertParams(out var errorMessage))
            {
                IsCreating = false;
                var issueModel = new IssueModel(IssueType)
                {
                    IssueName = IssueName,
                    PlannedStartTime = PlannedStartTime,
                    PlannedFinishTime = PlannedFinishTime,
                    Description = Description,
                };
                IssueCreated?.Invoke(this, issueModel);
            }
            ErrorMessage = errorMessage;
        }

        public void Cancel()
        {
            IsCreating = false;
            ErrorMessage = string.Empty;
        }

        private void Reset()
        {
            IssueName = string.Empty;
            PlannedStartTime = DateTime.Now;
            PlannedFinishTime = DateTime.Now;
            Description = string.Empty;
        }

        private bool AssertParams(out string errorMessage)
        {
            if (string.IsNullOrEmpty(IssueName))
            {
                errorMessage = "Issue Name is null";
                return false;
            }
            else
            {
                errorMessage = string.Empty;
                return true;
            }
        }
        #endregion
    }
}
