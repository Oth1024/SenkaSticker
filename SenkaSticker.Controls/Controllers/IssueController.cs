using SenkaSticker.Common.Logger;
using SenkaSticker.Common.Utilities;
using SenkaSticker.Controls.Model;
using SenkaSticker.Database.IssueRepo;
using System;
using System.Collections;
using System.Linq;
using System.Threading;

namespace SenkaSticker.Controls.Controllers
{
    public class IssueController
    {
        #region Constructor
        private IssueController()
        {

        }
        #endregion

        #region Events
        #endregion

        #region Singleton
        public static IssueController Instance => Singleton<IssueController>.Instance;
        #endregion

        #region Fields
        private ILogger _logger = LoggerFactory.GetLogger(nameof(IssueController));
        #endregion

        #region Properties
        private IssueRepository _db => IssueRepository.Instance;
        #endregion

        #region Methods
        public List<IssueModel> GetAllIssues()
        {
            _logger.DebugIn();

            var result = new List<IssueModel>();
            var response = _db.GetAllIssues();
            foreach (var issue in response)
            {
                var issueModel = new IssueModel()
                {
                    IssueName = issue.IssueName,
                    Type = issue.IssueType,
                    State = issue.IssueState,
                    IsStared = issue.IsStared == 1 ? true : false,
                    Description = issue.Description,
                    RootCause = issue.RootCause,
                    Solution = issue.Solution,
                    CreatedTime = issue.CreateTime,
                    PlannedStartTime = issue.ExpectedStartTime,
                    ActualStartTime = issue.ActualStartTime,
                    PlannedFinishTime = issue.ExpectedFinishTime,
                    ActualFinishTime = issue.ActualFinishTime,
                };
                result.Add(issueModel);
            }

            _logger.DebugOut();
            return result;
        }

        public void AddOrUpdate(IssueModel issueModel)
        {
            _logger.DebugIn();

            _db.InsertOrUpdateIssue(
                issueModel.IssueName,
                issueModel.Type,
                issueModel.State,
                issueModel.IsStared,
                issueModel.Description,
                issueModel.RootCause,
                issueModel.Solution,
                issueModel.CreatedTime,
                issueModel.PlannedFinishTime,
                issueModel.ActualStartTime,
                issueModel.PlannedFinishTime,
                issueModel.ActualFinishTime);

            _logger.DebugOut();
        }
        #endregion
    }
}
