
namespace IssueTrackerModels
{
    public abstract class SearchCriteriaModel
    {
        public int? Skip { get; set; }

        public int? Take { get; set; }

        public string SortField { get; set; }
    }
}
