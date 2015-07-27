using System.Data.Entity;

namespace IssueTrackerEntities
{
    public partial class IssueTrackerContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Issue> Issues { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }

        public virtual DbSet<UserRole> UserRoles { get; set; }

        public virtual DbSet<IssueType> IssueTypes { get; set; }

        public virtual DbSet<IssueStatus> IssueStatuses { get; set; }

        public virtual DbSet<IssuePriority> IssuePriorties { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.CreatedIssues)
                .WithRequired(e => e.CreatedByUser)
                .HasForeignKey(e => e.CreatedByUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.AssignedIssues)
                .WithRequired(e => e.AssignedToUser)
                .HasForeignKey(e => e.AssignedToUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasRequired(e => e.UserRole)
                .WithMany()
                .HasForeignKey(e => e.UserRoleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Issue>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.Issue)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<UserRole>()
            //    .HasMany(e => e.User)
            //    .WithRequired(e => e.UserRole)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<IssueType>()
                .HasMany(e => e.Issue)
                .WithRequired(e => e.IssueType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IssueStatus>()
                .HasMany(e => e.Issue)
                .WithRequired(e => e.IssueStatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IssuePriority>()
                .HasMany(e => e.Issue)
                .WithRequired(e => e.IssuePriority)
                .WillCascadeOnDelete(false);
        }
    }
}
