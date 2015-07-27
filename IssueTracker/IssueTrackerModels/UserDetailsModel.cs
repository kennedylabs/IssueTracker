using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IssueTrackerModels
{
    public class UserDetailsModel
    {
        public int UserId { get; set; }

        [Required, MinLength(6), MaxLength(16)]
        public string UserName { get; set; }

        [Required, MinLength(6), MaxLength(16)]
        public string Password { get; set; }

        [Required, Compare("Passowrd")]
        public string ConfirmPassword { get; set; }

        [Required, MinLength(2), MaxLength(64)]
        public string First { get; set; }

        [Required, MinLength(2), MaxLength(64)]
        public string Last { get; set; }

        [Required, MinLength(6), MaxLength(64)]
        public string Email { get; set; }

        public int UserRoleId { get; set; }

        public string UserRoleName { get; set; }

        public IList<IssueDetailsModel> CreatedIssues { get; set; }

        public IList<IssueDetailsModel> AssignedIssues { get; set; }
    }
}
