using System;
using System.ComponentModel.DataAnnotations;

namespace IssueTrackerModels
{
    public class CommentDetailsModel
    {
        public int CommentId { get; set; }

        [Required, MinLength(2), MaxLength(2048)]
        public string Text { get; set; }

        public string TextPreview { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public DateTimeOffset ModifiedOn { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public UserDetailsModel UserDetails { get; set; }

        public int IssueId{ get; set; }

        public string IssueName { get; set; }

        public IssueDetailsModel IssueDetails { get; set; }
    }
}
