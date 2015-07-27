using IssueTrackerCommon.Mapping;
using IssueTrackerEntities;
using IssueTrackerModels;

namespace IssueTrackerDomain.Mapping
{
    class CommentMapper : MapperBase
    {
        internal void Map(Comment comment, CommentDetailsModel commentDetailsModel)
        {
            commentDetailsModel.CommentId = comment.Id;
            commentDetailsModel.CreatedOn = comment.CreatedOn;
            commentDetailsModel.ModifiedOn = comment.ModifiedOn;
            commentDetailsModel.UserId = comment.UserId;
            commentDetailsModel.UserName = comment.User.UserName;
            commentDetailsModel.IssueId = comment.IssueId;
            commentDetailsModel.UserName = comment.Issue.Name;
        }
    }
}
