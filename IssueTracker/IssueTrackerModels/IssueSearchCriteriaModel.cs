
namespace IssueTrackerModels
{
    public class IssueSearchCriteriaModel : SearchCriteriaModel
    {
        public int? CreatedByUserId { get; set; }

        public int? AssignedToUserId { get; set; }

        public int? IssueTypeId { get; set; }

        public int? IssueStatusId { get; set; }

        public int? IssuePriorityId { get; set; }

        public string Summary { get; set; }
    }
}
