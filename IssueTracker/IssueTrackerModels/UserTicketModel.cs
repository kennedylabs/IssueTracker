namespace IssueTrackerModels
{
    public class UserTicketModel
    {
        public string Status { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int UserRoleId { get; set; }
        public string UserRoleName { get; set; }
        public string AuthToken { get; set; }
    }
}
