using IssueTrackerDomain.Services;
using IssueTrackerModels;
using IssueTrackerWeb.Infrastructure;
using System.Collections.Generic;
using System.Web.Http;

namespace IssueTrackerWeb.Api
{
    public class UserController : BaseApiController
    {
        public AuthResponseModel Login(AuthRequestModel authRequest)
        {
            return Service<AuthService>().Login(authRequest);
        }

        [AuthApi]
        public void Logout()
        {
            Service<AuthService>().Logout(CurrentUserTicket.AuthToken);
        }

        [AuthApi]
        public IEnumerable<UserSummaryModel> Summary(UserSearchCriteriaModel criteria = null)
        {
            return Service<UserService>().GetUserSummaries(criteria);
        }

        [AuthApi]
        public UserSummaryModel Summary(int id)
        {
            return Service<UserService>().GetUserSummary(id);
        }

        [AuthApi]
        public IEnumerable<UserDetailsModel> Get(
            UserSearchCriteriaModel criteria = null, bool includeIssues = false)
        {
            return Service<UserService>().GetUserDetails(criteria, includeIssues);
        }

        [AuthApi]
        public UserDetailsModel Get(int id)
        {
            return Service<UserService>().GetUserDetails(id);
        }

        [AuthApi(UserRoleModel.Manager)]
        public int Post([FromBody]UserDetailsModel userDetails)
        {
            return Service<UserService>().CreateUser(userDetails);
        }

        [AuthApi(UserRoleModel.Manager)]
        public void Put([FromBody]UserDetailsModel userDetails)
        {
            Service<UserService>().UpdateUser(userDetails);
        }

        [AuthApi(UserRoleModel.Manager)]
        public void Delete(int id)
        {
            Service<UserService>().DeleteUser(id);
        }

        [AuthApi]
        public bool CheckUserNameExists(string userName)
        {
            return Service<UserService>().CheckUserNameExists(userName);
        }
    }
}
