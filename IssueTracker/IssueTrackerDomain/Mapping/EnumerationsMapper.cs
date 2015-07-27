using IssueTrackerCommon.Mapping;
using IssueTrackerEntities;
using IssueTrackerModels;

namespace IssueTrackerDomain.Mapping
{
    class EnumerationsMapper : MapperBase
    {
        internal void Map(UserRole userRole, UserRoleModel userRoleModel)
        {
            userRoleModel.Name = userRole.Name;
            userRoleModel.Value = userRole.Id;
        }

        internal void Map(UserRoleModel userRoleModel, UserRole userRole)
        {
            userRole.Name = userRoleModel.Name;
            userRole.Id = userRoleModel.Value;
        }

        internal void Map(IssueType issueType, IssueTypeModel issueTypeModel)
        {
            issueTypeModel.Name = issueType.Name;
            issueTypeModel.Value = issueType.Id;
        }

        internal void Map(IssueTypeModel issueTypeModel, IssueType issueType)
        {
            issueType.Name = issueTypeModel.Name;
            issueType.Id = issueTypeModel.Value;
        }

        internal void Map(IssueStatus issueStatus, IssueStatusModel issueStatusModel)
        {
            issueStatusModel.Name = issueStatus.Name;
            issueStatusModel.Value = issueStatus.Id;
        }

        internal void Map(IssueStatusModel issueStatusModel, IssueStatus issueStatus)
        {
            issueStatus.Name = issueStatusModel.Name;
            issueStatus.Id = issueStatusModel.Value;
        }

        internal void Map(IssuePriority issuePriority, IssuePriorityModel issuePriorityModel)
        {
            issuePriorityModel.Name = issuePriority.Name;
            issuePriorityModel.Value = issuePriority.Id;
        }

        internal void Map(IssuePriorityModel issuePriorityModel, IssuePriority issuePriority)
        {
            issuePriority.Name = issuePriorityModel.Name;
            issuePriority.Id = issuePriorityModel.Value;
        }
    }
}
