using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IssueTrackerModels
{
    public class IssueDetailsModel
    {
        public int IssueId { get; set; }

        [Required, MinLength(2), MaxLength(64)]
        public string Name { get; set; }

        public string SummaryPreview { get; set; }

        [Required]
        public string Summary { get; set; }

        [Required, MinLength(2), MaxLength(64)]
        public string SubSystem { get; set; }

        [MinLength(2), MaxLength(64)]
        public string Customer { get; set; }

        public int? Estimate { get; set; }

        public DateTimeOffset ReportedOn { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public DateTimeOffset ModifiedOn { get; set; }

        public int CreatedByUserId { get; set; }

        public string CreatedByUserName { get; set; }

        public UserDetailsModel CreatedByUser { get; set; }

        public int AssignedToUserId { get; set; }

        public string AssignedToUserName { get; set; }

        public UserDetailsModel AssignedToUser { get; set; }

        public int IssueTypeId { get; set; }

        public string IssueTypeName { get; set; }

        public int IssueStatusId { get; set; }

        public string IssueStatusName { get; set; }

        public int IssuePriorityId { get; set; }

        public string IssuePriorityName { get; set; }

        public virtual IList<CommentDetailsModel> Comments { get; set; }
    }
}
