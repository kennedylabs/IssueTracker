
namespace IssueTrackerModels
{
    public class AuthResponseModel
    {
        public bool Succeeded { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string AuthToken { get; set; }
        public int UserRoleId { get; set; }
        public string UserRoleName { get; set; }
        public int? DaysUntilExpiration { get; set; }
    }
}
