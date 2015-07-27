using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IssueTrackerEntities
{
    [Table("Comment")]
    public partial class Comment
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int IssueId { get; set; }

        [Required]
        public string Text { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public DateTimeOffset ModifiedOn { get; set; }

        public virtual User User { get; set; }

        public virtual Issue Issue { get; set; }
    }
}
