
namespace IssueTrackerModels
{
    public class CommentSearchCriteriaModel : SearchCriteriaModel
    {
        public int? UserId { get; set; }

        public int? IssueId { get; set; }

        public string Text { get; set; }
    }
}
