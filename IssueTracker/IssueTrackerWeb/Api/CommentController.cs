using IssueTrackerDomain.Services;
using IssueTrackerModels;
using IssueTrackerWeb.Infrastructure;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace IssueTrackerWeb.Api
{
    public class CommentController : BaseApiController
    {
        [AuthApi]
        public IEnumerable<CommentDetailsModel> Get(CommentSearchCriteriaModel criteria = null,
            bool includeUser = false, bool includeIssue = false)
        {
            if (criteria == null)
                criteria = new CommentSearchCriteriaModel { UserId = CurrentUserId };

            return Service<CommentService>().GetCommentDetails(
                criteria, includeUser, includeIssue);
        }

        [AuthApi]
        public CommentDetailsModel Get(
            int id, bool includeUser = false, bool includeIssue = false)
        {
            return Service<CommentService>().GetCommentDetails(id, includeUser, includeIssue);
        }

        [AuthApi, HttpPost]
        public int Create(int issueId, string text)
        {
            return Service<CommentService>().CreateComment(CurrentUserId, issueId, text);
        }

        [AuthApi, HttpPost]
        public void Update(int commentId, string text)
        {
            if (!Service<AuthService>().CheckCanUpdateComment(commentId, CurrentUserId))
                throw new HttpResponseException(HttpStatusCode.Unauthorized);

            Service<CommentService>().UpdateComment(commentId, text);
        }

        [AuthApi(UserRoleModel.Manager)]
        public void Delete(int id)
        {
            Service<CommentService>().DeleteComment(id);
        }
    }
}
