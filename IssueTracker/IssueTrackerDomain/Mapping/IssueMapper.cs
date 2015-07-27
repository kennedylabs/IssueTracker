using IssueTrackerCommon.Mapping;
using IssueTrackerEntities;
using IssueTrackerModels;

namespace IssueTrackerDomain.Mapping
{
    class IssueMapper : MapperBase
    {
        internal void Map(Issue issue, IssueSummaryModel issueSummaryModel)
        {
            issueSummaryModel.IssueId = issue.Id;
            issueSummaryModel.Name = issue.Name;
        }

        internal void Map(Issue issue, IssueDetailsModel issueDetailsModel)
        {
            issueDetailsModel.IssueId = issue.Id;
            issueDetailsModel.Name = issue.Name;
            issueDetailsModel.SubSystem = issue.SubSystem;
            issueDetailsModel.Customer = issue.Customer;
            issueDetailsModel.ReportedOn = issue.ReportedOn;
            issueDetailsModel.CreatedOn = issue.CreatedOn;
            issueDetailsModel.ModifiedOn = issue.ModifiedOn;
            issueDetailsModel.CreatedByUserId = issue.CreatedByUserId;
            issueDetailsModel.AssignedToUserId = issue.AssignedToUserId;
            issueDetailsModel.IssueTypeId = issue.IssueTypeId;
            issueDetailsModel.IssueTypeName = IssueTypeModel.GetName(issue.IssueTypeId);
            issueDetailsModel.IssueStatusId = issue.IssueStatusId;
            issueDetailsModel.IssueStatusName = IssueStatusModel.GetName(issue.IssueStatusId);
            issueDetailsModel.IssuePriorityId = issue.IssuePriorityId;
            issueDetailsModel.IssuePriorityName =
                IssuePriorityModel.GetName(issue.IssuePriorityId);
        }

        internal void Map(IssueDetailsModel issueDetailsModel, Issue issue)
        {
            issue.SubSystem = issueDetailsModel.SubSystem;
            issue.Customer = issueDetailsModel.Customer;
            issue.CreatedByUserId = issueDetailsModel.CreatedByUserId;
            issue.AssignedToUserId = issueDetailsModel.AssignedToUserId;
            issue.IssueTypeId = issueDetailsModel.IssueTypeId;
            issue.IssueStatusId = issueDetailsModel.IssueStatusId;
            issue.IssuePriorityId = issueDetailsModel.IssuePriorityId;
        }
    }
}
