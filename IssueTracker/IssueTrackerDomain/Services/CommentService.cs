using IssueTrackerCommon.Services;
using IssueTrackerEntities;
using IssueTrackerModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IssueTrackerDomain.Services
{
    public class CommentService : ServiceBase
    {
        public CommentDetailsModel GetCommentDetails(int commentId,
            bool includeText = false, bool includeUser = false, bool includeIssue = false)
        {
            var comment = EnsureFindSingle<Comment>(c => c.Id == commentId);
            return BuildCommentDetails(comment, includeUser, includeIssue);
        }

        public IList<CommentDetailsModel> GetCommentDetails(
            CommentSearchCriteriaModel criteria = null,
            bool includeText = false, bool includeUser = false, bool includeIssue = false)
        {
            return Search(criteria).Select(
                c => BuildCommentDetails(c, includeUser, includeIssue)).ToList();
        }

        public int CreateComment(int userId, int issueId, string text)
        {
            var currentTime = DateTimeOffset.Now;

            var comment = new Comment
            {
                UserId = userId,
                IssueId = issueId,
                Text = text,
                CreatedOn = currentTime,
                ModifiedOn = currentTime
            };

            return InsertAndSave(comment).Id;
        }

        public void UpdateComment(int commentId, string text)
        {
            var comment = EnsureFindSingle<Comment>(commentId);

            comment.Text = text;

            UpdateAndSave(comment);
        }

        public void DeleteComment(int commentId)
        {
            DeleteAndSave<Comment>(commentId);
        }

        internal CommentDetailsModel BuildCommentDetails(Comment comment,
            bool includeText = false, bool includeUser = false, bool includeIssue = false)
        {
            var commentDetails = Map<CommentDetailsModel>(comment);

            commentDetails.TextPreview = comment.Text.Length <= 10 ? comment.Text :
                comment.Text.Substring(0, 10) + "...";

            if (includeText) commentDetails.Text = comment.Text;
            if (includeUser) commentDetails.UserDetails = Service<UserService>().BuildUserDetails(comment.User);
            if (includeIssue) commentDetails.IssueDetails = Service<IssueService>().BuildIssueDetails(comment.Issue);

            return commentDetails;
        }

        IList<Comment> Search(CommentSearchCriteriaModel criteria)
        {
            if (criteria == null) criteria = new CommentSearchCriteriaModel();

            var comments = Repo<Comment>().Find();

            if (criteria.UserId.HasValue)
                comments = comments.Where(c => c.UserId == criteria.UserId.Value);
            if (criteria.IssueId.HasValue)
                comments = comments.Where(c => c.IssueId == criteria.IssueId.Value);
            if (criteria.Text != null)
                comments = comments.Where(c => c.Text.Contains(criteria.Text));

            if (criteria.SortField == "ModifiedOn") comments = comments.OrderBy(c => c.ModifiedOn);
            else comments = comments.OrderBy(u => u.CreatedOn);

            return comments.Skip(criteria.Skip ?? 0).Take(criteria.Take ?? 10).ToList();
        }
    }
}
