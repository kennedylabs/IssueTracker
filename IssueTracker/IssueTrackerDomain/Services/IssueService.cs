using IssueTrackerCommon.Services;
using IssueTrackerEntities;
using IssueTrackerModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IssueTrackerDomain.Services
{
    public class IssueService : ServiceBase
    {
        public IssueSummaryModel GetIssueSummary(int issueId)
        {
            return Map<IssueSummaryModel>(EnsureFindSingle<Issue>(issueId));
        }

        public IList<IssueSummaryModel> GetIssueSummaries(IssueSearchCriteriaModel criteria = null)
        {
            return Search(criteria).Select(Map<IssueSummaryModel>).ToList();
        }

        public IList<IssueDetailsModel> GetIssueDetails(IssueSearchCriteriaModel criteria = null,
            bool includeSummary = false, bool includeUser = false, bool includeIssue = false)
        {
            return Search(criteria).Select(
                c => BuildIssueDetails(c, includeSummary, includeUser, includeIssue)).ToList();
        }

        public IssueDetailsModel GetIssueDetails(int issueId,
            bool includeSummary = false, bool includeUsers = false, bool includeComments = false)
        {
            var issue = EnsureFindSingle<Issue>(c => c.Id == issueId);
            return BuildIssueDetails(issue, includeSummary, includeUsers, includeComments);
        }

        public int CreateIssue(IssueDetailsModel issueDetailsModel, int currentUserId)
        {
            var issue = Map<Issue>(issueDetailsModel);

            var currentTime = DateTimeOffset.Now;

            issue.CreatedByUserId = currentUserId;
            issue.ReportedOn = currentTime;
            issue.CreatedOn = currentTime;
            issue.ModifiedOn = currentTime;

            return InsertAndSave(issue).Id;
        }

        public void UpdateIssue(IssueDetailsModel issueDetailsModel)
        {
            var issue = EnsureFindSingle<Issue>(issueDetailsModel.IssueId);

            Map(issueDetailsModel, issue);

            issue.ModifiedOn = DateTimeOffset.Now;

            UpdateAndSave(issue);
        }

        public void DeleteIssue(int issueId)
        {
            var issue = EnsureFindSingle<Issue>(issueId);

            var commentIds = issue.Comments.Select(c => c.Id).ToList();

            foreach (var commentId in commentIds)
                Repo<Comment>().Delete(commentId);

            DeleteAndSave<Issue>(issue);
        }

        internal IssueDetailsModel BuildIssueDetails(Issue issue,
            bool includeSummary = false, bool includeUsers = false, bool includeComments = false)
        {
            var issueDetails = Map<IssueDetailsModel>(issue);

            issueDetails.SummaryPreview = issue.Summary.Length <= 10 ? issue.Summary :
                issue.Summary.Substring(0, 10) + "...";

            if (includeSummary)
            {
                issueDetails.Summary = issue.Summary;
            }
            if (includeUsers)
            {
                issueDetails.CreatedByUser =
                    Service<UserService>().BuildUserDetails(issue.CreatedByUser);
                issueDetails.AssignedToUser =
                    Service<UserService>().BuildUserDetails(issue.AssignedToUser);
            }
            if (includeComments)
            {
                issueDetails.Comments = issue.Comments.Select(
                    c => Service<CommentService>().BuildCommentDetails(c)).ToList();
            }

            return issueDetails;
        }

        IList<Issue> Search(IssueSearchCriteriaModel criteria)
        {
            if (criteria == null) criteria = new IssueSearchCriteriaModel();

            var issues = Repo<Issue>().Find();

            if (criteria.CreatedByUserId.HasValue)
                issues = issues.Where(i => i.CreatedByUserId == criteria.CreatedByUserId.Value);
            if (criteria.AssignedToUserId.HasValue)
                issues = issues.Where(i => i.AssignedToUserId == criteria.AssignedToUserId.Value);
            if (criteria.IssueTypeId.HasValue)
                issues = issues.Where(i => i.IssueTypeId == criteria.IssueTypeId.Value);
            if (criteria.IssueStatusId.HasValue)
                issues = issues.Where(i => i.IssueStatusId == criteria.IssueStatusId.Value);
            if (criteria.IssuePriorityId.HasValue)
                issues = issues.Where(i => i.IssuePriorityId == criteria.IssuePriorityId.Value);
            if (criteria.Summary != null)
                issues = issues.Where(i => i.Summary.Contains(criteria.Summary));

            if (criteria.SortField == "ModifiedOn") issues = issues.OrderBy(c => c.ModifiedOn);
            else issues = issues.OrderBy(u => u.CreatedOn);

            return issues.Skip(criteria.Skip ?? 0).Take(criteria.Take ?? 10).ToList();
        }
    }
}
