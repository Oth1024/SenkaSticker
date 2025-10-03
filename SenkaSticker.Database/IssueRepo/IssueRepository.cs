using SenkaSticker.Common.TypeDef.Enum;
using SenkaSticker.Common.Utilities;
using System;
using System.Collections;
using System.Linq;
using System.Threading;

namespace SenkaSticker.Database.IssueRepo
{
    public class IssueRepository : RepositoryBase<IssueEntity>
    {
        #region Constructor
        private IssueRepository()
        {

        }
        #endregion

        #region Events
        #endregion

        #region Singleton
        public static IssueRepository Instance => Singleton<IssueRepository>.Instance;
        #endregion

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Methods
        public List<IssueEntity> GetAllIssues()
        {
            return _db.Queryable<IssueEntity>()
                .Where(x => x.IssueState != IssueState.Closed)
                .ToList();
        }

        public void InsertOrUpdateIssue(
            string issueName,
            IssueType issueType,
            IssueState issueState,
            bool isStared,
            string description,
            string rootCause,
            string solution,
            DateTime createTime,
            DateTime expectedStartTime,
            DateTime actualStartTime,
            DateTime expectedFinishTime,
            DateTime actualFinishTime)
        {
            if (!Exist(createTime, out var issueEntity))
            {
                _db.Insertable(new IssueEntity
                {
                    IssueName = issueName,
                    IssueType = issueType,
                    IssueState = issueState,
                    IsStared = isStared ? 1 : 0,
                    Description = description,
                    RootCause = rootCause,
                    Solution = solution,
                    CreateTime = createTime,
                    ExpectedStartTime = expectedStartTime,
                    ActualStartTime = actualStartTime,
                    ExpectedFinishTime = expectedFinishTime,
                    ActualFinishTime = actualFinishTime
                }).ExecuteCommand();
            }
            else
            {
                issueEntity.IssueName = issueName;
                issueEntity.IssueType = issueType;
                issueEntity.IssueState = issueState;
                issueEntity.IsStared = isStared ? 1 : 0;
                issueEntity.Description = description;
                issueEntity.RootCause = rootCause;
                issueEntity.Solution = solution;
                issueEntity.CreateTime = createTime;
                issueEntity.ExpectedStartTime = expectedStartTime;
                issueEntity.ActualStartTime = actualStartTime;
                issueEntity.ExpectedFinishTime = expectedFinishTime;
                issueEntity.ActualFinishTime = actualFinishTime;
                _db.Updateable(issueEntity).ExecuteCommand();
            }
        }

        public void Delete(
            string issueName,
            IssueType issueType,
            IssueState issueState,
            DateTime createTime)
        {
            if (Exist(createTime, out _))
            {
                _db.Deleteable<IssueEntity>()
                    .Where(x =>
                    x.IssueName == issueName
                    && x.IssueType == issueType
                    && x.IssueState == issueState
                    && x.CreateTime == createTime).ExecuteCommand();
            }
        }

        public bool Exist(
            DateTime createTime,
            out IssueEntity issueEntity)
        {
            issueEntity = _db.Queryable<IssueEntity>()
                .Where(x =>
                x.CreateTime == createTime)
                .ToList()
                .FirstOrDefault();
            return issueEntity != null;
        }
        #endregion
    }
}
