using Avalonia;
using Avalonia.Interactivity;
using Avalonia.Styling;
using SenkaSticker.Build.Attributes;
using SenkaSticker.Common.TypeDef.Enum;
using SenkaSticker.Controls.Base;
using SenkaSticker.Controls.Controllers;
using SenkaSticker.Controls.Model;
using System.Collections.ObjectModel;

namespace SenkaSticker.Controls.Components.Parts.ViewModels
{
    [IncludeSelfNotifyProperty(typeof(bool), "ShowSearchContent")]
    public partial class IssuePopViewModel : ViewModelBase
    {
        #region Constructor
        public IssuePopViewModel()
        {
            CreateIssue = new();
            var allIssueModels = _issueController.GetAllIssues();
            CreateIssue.IssueCreated += OnIssueCreated;
            foreach (var issueModel in allIssueModels)
            {
                var issueInfo = new IssueInfoViewModel(issueModel);
                _allIssues.Add(issueInfo);
                issueInfo.IssueSaved += OnIssueSaved;
                issueInfo.IssueStateChanged += OnIssueStateChanged;
                issueInfo.IssueInfoStared += OnIssueStarStateChanged;
            }
            DisplayIssues = new(_allIssues);
        }
        #endregion

        #region Events
        #endregion

        #region Fields
        private readonly IssueController _issueController = IssueController.Instance;
        private List<IssueInfoViewModel> _allIssues = new();
        private string _searchedContent = string.Empty;
        #endregion

        #region Properties
        public CreateNewIssueViewModel CreateIssue { get; set; }

        public ObservableCollection<IssueInfoViewModel> DisplayIssues { get; set; } = new();

        public ObservableCollection<IssueInfoViewModel> FinishedIssues { get; set; } = new();

        public ObservableCollection<IssueInfoViewModel> SearchedIssues { get; set; } = new();

        public string SearchedContent
        {
            get => _searchedContent;
            set
            {
                if (_searchedContent != value)
                {
                    _searchedContent= value;
                    NotifyOfPropertyChange(nameof(SearchedContent));
                    Search();
                }
            }
        }
        #endregion

        #region Methods
        public void OrderByDefault()
        {
            var isStaredIssues = _allIssues.Where(x => x.IssueModel.IsStared);
            var notStaredIssues = _allIssues
                .Where(x => !x.IssueModel.IsStared)
                .OrderBy(x => x.IssueModel.IssueName);
            var orderedIssues = isStaredIssues.ToList();
            orderedIssues.AddRange(notStaredIssues);
            DisplayIssues = new(orderedIssues);
            NotifyOfPropertyChange(nameof(DisplayIssues));
        }

        public void OrderByName()
        {
            DisplayIssues = new(_allIssues.OrderByDescending(x => x.IssueModel.IssueName));
            NotifyOfPropertyChange(nameof(DisplayIssues));
        }

        public void OrderByPlannedStartTimeAscending()
        {
            DisplayIssues = new(_allIssues.OrderBy(x => x.IssueModel.PlannedStartTime));
            NotifyOfPropertyChange(nameof(DisplayIssues));
        }

        public void OrderByPlannedStartTimeDescending()
        {
            DisplayIssues = new(_allIssues.OrderByDescending(x => x.IssueModel.PlannedStartTime));
            NotifyOfPropertyChange(nameof(DisplayIssues));
        }

        public void OrderByPlannedFinishTimeAscending()
        {
            DisplayIssues = new(_allIssues.OrderBy(x => x.IssueModel.PlannedFinishTime));
            NotifyOfPropertyChange(nameof(DisplayIssues));
        }

        public void OrderByPlannedFinishTimeDescending()
        {
            DisplayIssues = new(_allIssues.OrderByDescending(x => x.IssueModel.PlannedFinishTime));
            NotifyOfPropertyChange(nameof(DisplayIssues));
        }

        private void Search()
        {
            if (string.IsNullOrEmpty(_searchedContent))
            {
                SearchedIssues.Clear();
            }
            else
            {
                var searched = _allIssues.Where(x => x.IssueName.ToLower().Contains(_searchedContent.ToLower()));
                SearchedIssues = new(searched);
            }
            NotifyOfPropertyChange(nameof(SearchedIssues));
            RefreshSearchState();
        }

        private void RefreshSearchState()
        {
            if (string.IsNullOrEmpty(_searchedContent))
            {
                ShowSearchContent = false;
            }
            else
            {
                ShowSearchContent = true;
            }
        }

        private void OnIssueCreated(object? sender, IssueModel issueModel)
        {
            OnIssueUpdated(issueModel);
            var issueInfo = new IssueInfoViewModel(issueModel);
            issueInfo.IssueSaved += OnIssueSaved;
            issueInfo.IssueStateChanged += OnIssueStateChanged;
            issueInfo.IssueInfoStared += OnIssueStarStateChanged;
            _allIssues.Add(issueInfo);
            DisplayIssues.Add(issueInfo);
            NotifyOfPropertyChange(nameof(DisplayIssues));
        }

        private void OnIssueStateChanged(object sender, IssueState _)
        {
            var issueInfo = sender as IssueInfoViewModel;
            OnIssueUpdated(issueInfo.IssueModel);
        }

        private void OnIssueStarStateChanged(object sender, bool _)
        {
            var issueInfo = sender as IssueInfoViewModel;
            OnIssueUpdated(issueInfo.IssueModel);
        }

        private void OnIssueSaved(object sender, EventArgs _)
        {
            var issueInfo = sender as IssueInfoViewModel;
            OnIssueUpdated(issueInfo.IssueModel);
        }

        private void OnIssueUpdated(IssueModel issueModel)
        {
            _issueController.AddOrUpdate(issueModel);
        }
        #endregion
    }
}
