
namespace IssueTrackerModels
{
    public class UserSearchCriteriaModel : SearchCriteriaModel
    {
        public int? UserRoleId { get; set; }

        public string UserNameSearch { get; set; }

        public string FirstSearch { get; set; }

        public string LastSearch { get; set; }

        public string EmailSearch { get; set; }

        public string MultiColumnSearch { get; set; }
    }
}
