using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace IssueTrackerEntities
{
    [Table("User")]
    public partial class User
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            CreatedIssues = new HashSet<Issue>();
            AssignedIssues = new HashSet<Issue>();
            Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }

        public int UserRoleId { get; set; }

        [Required, StringLength(128)]
        public string UserName { get; set; }

        [StringLength(128)]
        public string First { get; set; }

        [StringLength(128)]
        public string Last { get; set; }

        [StringLength(128)]
        public string Email { get; set; }

        [StringLength(128)]
        public string PasswordHash { get; set; }

        [StringLength(128)]
        public string PasswordSalt { get; set; }

        public int PasswordLength { get; set; }

        [StringLength(128)]
        public string AuthToken { get; set; }

        public DateTimeOffset? AuthExpiration { get; set; }

        public bool? IsDeleted { get; set; }

        public virtual UserRole UserRole { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Issue> CreatedIssues { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Issue> AssignedIssues { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
