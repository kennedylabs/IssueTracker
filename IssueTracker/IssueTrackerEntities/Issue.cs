using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace IssueTrackerEntities
{
    [Table("Issue")]
    public partial class Issue
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Issue()
        {
            Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }

        public int IssueTypeId { get; set; }

        public int IssueStatusId { get; set; }

        public int IssuePriorityId { get; set; }

        public int CreatedByUserId { get; set; }

        public int AssignedToUserId { get; set; }

        [Required, StringLength(128)]
        public string Name { get; set; }

        [Required]
        public string Summary { get; set; }

        [Required, StringLength(128)]
        public string SubSystem { get; set; }

        [StringLength(128)]
        public string Customer { get; set; }

        public int? Estimate { get; set; }

        public DateTimeOffset ReportedOn { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public DateTimeOffset ModifiedOn { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }

        public virtual User CreatedByUser { get; set; }

        public virtual User AssignedToUser { get; set; }

        public virtual IssueType IssueType { get; set; }

        public virtual IssueStatus IssueStatus { get; set; }

        public virtual IssuePriority IssuePriority { get; set; }
    }
}
