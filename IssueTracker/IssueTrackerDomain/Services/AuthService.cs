using IssueTrackerCommon.Infrastructure;
using IssueTrackerCommon.Services;
using IssueTrackerEntities;
using IssueTrackerModels;
using System;

namespace IssueTrackerDomain.Services
{
    public class AuthService : ServiceBase
    {
        public AuthResponseModel Login(AuthRequestModel authRequest)
        {
            var user = FindSingleOrNull<User>(u => u.UserName == authRequest.UserName);

            if (user != null && Cryptography.HashPassword(
                authRequest.Password, user.PasswordSalt) == user.PasswordHash)
            {
                user.AuthToken = Guid.NewGuid().ToString("N");
                user.AuthExpiration = authRequest.Remember ? DateTime.UtcNow.AddDays(16) :
                    DateTime.UtcNow.AddHours(4);
                UpdateAndSave(user);

                var authResponse = Map<AuthResponseModel>(user);

                authResponse.Succeeded = true;
                authResponse.DaysUntilExpiration = authRequest.Remember ? 16 : (int?)null;

                return authResponse;
            }
            else
            {
                return new AuthResponseModel { Succeeded = false };
            }
        }

        public void Logout(string authToken)
        {
            var user = FindSingleOrNull<User>(u => u.AuthToken == authToken);

            if (user != null)
            {
                user.AuthToken = null;
                user.AuthToken = null;
                user.AuthExpiration = null;
                UpdateAndSave(user);
            }
        }

        public bool CheckCanUpdateComment(int commentId, int currentUserId)
        {
            return FindSingleValue<Comment, int>(c => c.Id == commentId, c => c.UserId) ==
                currentUserId || FindSingleValue<User, int>(
                    u => u.Id == currentUserId, u => u.UserRoleId) == UserRoleModel.Admin;
        }

        public UserTicketModel CheckAuthToken(string authToken, int roleValue)
        {
            var user = FindSingleOrNull<User>(u => u.AuthToken == authToken);

            if (user == null)
            {
                if (roleValue == UserRoleModel.Unknown)
                    return new UserTicketModel
                    {
                        Status = "Succeeded",
                        UserName = String.Empty,
                        UserRoleId = roleValue,
                        UserRoleName = UserRoleModel.GetName(roleValue),
                        AuthToken = authToken
                    };
                else
                    return new UserTicketModel { Status = "Unauthorized" };
            }
            else if (user.AuthExpiration.HasValue &&
                user.AuthExpiration.Value < DateTimeOffset.UtcNow)
            {
                user.AuthToken = null;
                user.AuthExpiration = null;
                UpdateAndSave(user);

                return new UserTicketModel { Status = "Expired" };
            }
            else if (user.UserRoleId < roleValue)
            {
                return new UserTicketModel { Status = "Unauthorized" };
            }
            else
            {
                if (user.AuthExpiration.HasValue &&
                    (user.AuthExpiration.Value - DateTime.UtcNow).TotalHours < 2)
                {
                    user.AuthExpiration = DateTime.UtcNow.AddHours(4);
                    UpdateAndSave(user);
                }

                return Map<UserTicketModel>(user);
            }
        }
    }
}
