using IssueTrackerCommon.Infrastructure;
using IssueTrackerCommon.Services;
using IssueTrackerEntities;
using IssueTrackerModels;
using System.Collections.Generic;
using System.Linq;

namespace IssueTrackerDomain.Services
{
    public class UserService : ServiceBase
    {
        public UserSummaryModel GetUserSummary(int userId)
        {
            return Map<UserSummaryModel>(EnsureFindSingle<User>(
                u => u.Id == userId && u.IsDeleted != true));
        }

        public IList<UserSummaryModel> GetUserSummaries(UserSearchCriteriaModel criteria = null)
        {
            return Search(criteria).Select(Map<UserSummaryModel>).ToList();
        }

        public UserDetailsModel GetUserDetails(int userId, bool includeIssues = false)
        {
            var user = EnsureFindSingle<User>(u => u.Id == userId && u.IsDeleted != true);
            return BuildUserDetails(user, includeIssues);
        }

        public IList<UserDetailsModel> GetUserDetails(
            UserSearchCriteriaModel criteria = null, bool includeIssues = false)
        {
            return Search(criteria).Select(u => BuildUserDetails(u, includeIssues)).ToList();
        }

        public int CreateUser(UserDetailsModel userDetails)
        {
            ThrowBadDataIf(CheckUserNameExists(userDetails.UserName), "UserName exists");

            var user = Map<User>(userDetails);

            user.PasswordSalt = Cryptography.CreateSalt();
            user.PasswordHash = Cryptography.HashPassword(userDetails.Password, user.PasswordSalt);
            user.PasswordLength = userDetails.Password.Length;

            return userDetails.UserId = InsertAndSave(Map<User>(userDetails)).Id;
        }

        public void UpdateUser(UserDetailsModel userDetails)
        {
            var user = EnsureFindSingle<User>(userDetails.UserId);

            ThrowBadDataIf(userDetails.UserName != user.UserName &&
                CheckUserNameExists(userDetails.UserName), "UserName exists");

            Map(userDetails, user);

            if (userDetails.Password != string.Concat(Enumerable.Repeat("*", user.PasswordLength)))
            {
                user.PasswordHash = Cryptography.HashPassword(
                    userDetails.Password, user.PasswordSalt);
                user.PasswordLength = userDetails.Password.Length;
            }

            UpdateAndSave(user);
        }

        public void DeleteUser(int userId)
        {
            var user = EnsureFindSingle<User>(userId);
            user.IsDeleted = true;

            UpdateAndSave(user);
        }

        public bool CheckUserNameExists(string userName)
        {
            var idForUserName = FindSingleValue<User, int>(
                u => u.UserName == userName, u => u.Id);

            return idForUserName != 0;
        }
        
        internal UserDetailsModel BuildUserDetails(User user, bool includeIssues = false)
        {
            var userDetails = Map<UserDetailsModel>(user);

            userDetails.Password = string.Concat(Enumerable.Repeat("*", user.PasswordLength));
            userDetails.ConfirmPassword = userDetails.Password;

            if (includeIssues)
            {
                userDetails.CreatedIssues = user.CreatedIssues
                    .Select(i => Service<IssueService>().BuildIssueDetails(i)).ToList();
                userDetails.AssignedIssues = user.AssignedIssues
                    .Select(i => Service<IssueService>().BuildIssueDetails(i)).ToList();
            }

            return userDetails;
        }

        IList<User> Search(UserSearchCriteriaModel criteria)
        {
            if (criteria == null) criteria = new UserSearchCriteriaModel();

            var users = Repo<User>().Find(u => u.IsDeleted != true);

            if (criteria.UserRoleId.HasValue)
                users = users.Where(u => u.UserRoleId == criteria.UserRoleId.Value);

            if (criteria.MultiColumnSearch == null)
            {
                if (criteria.UserNameSearch != null)
                    users = users.Where(u => u.UserName.Contains(criteria.UserNameSearch));
                if (criteria.FirstSearch != null)
                    users = users.Where(u => u.First.Contains(criteria.FirstSearch));
                if (criteria.LastSearch != null)
                    users = users.Where(u => u.Last.Contains(criteria.LastSearch));
                if (criteria.EmailSearch != null)
                    users = users.Where(u => u.Email.Contains(criteria.EmailSearch));
            }
            else
            {
                users = users.Where(u => u.UserName.Contains(criteria.MultiColumnSearch) ||
                    u.First.Contains(criteria.MultiColumnSearch) ||
                    u.Last.Contains(criteria.MultiColumnSearch) ||
                    u.Email.Contains(criteria.MultiColumnSearch));
            }

            if (criteria.SortField == "First") users = users.OrderBy(u => u.First);
            else if (criteria.SortField == "Last") users = users.OrderBy(u => u.Last);
            else if (criteria.SortField == "Email") users = users.OrderBy(u => u.Email);
            else users = users.OrderBy(u => u.UserName);

            return users.Skip(criteria.Skip ?? 0).Take(criteria.Take ?? 10).ToList();
        }
    }
}
