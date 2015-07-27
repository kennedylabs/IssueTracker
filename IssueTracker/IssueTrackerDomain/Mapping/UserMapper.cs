using IssueTrackerCommon.Mapping;
using IssueTrackerEntities;
using IssueTrackerModels;

namespace IssueTrackerDomain.Mapping
{
    class UserMapper : MapperBase
    {
        internal void Map(User user, AuthResponseModel authResponse)
        {
            authResponse.UserId = user.Id;
            authResponse.UserName = user.UserName;
            authResponse.AuthToken = user.AuthToken;
            authResponse.UserRoleId = user.UserRoleId;
            authResponse.UserRoleName = UserRoleModel.GetName(user.UserRoleId);
        }

        internal void Map(User user, UserTicketModel userTicketModel)
        {
            userTicketModel.Status = "Succeeded";
            userTicketModel.UserId = user.Id;
            userTicketModel.UserName = user.UserName;
            userTicketModel.UserRoleId = user.UserRoleId;
            userTicketModel.UserRoleName = UserRoleModel.GetName(user.UserRoleId);
        }

        internal void Map(User user, UserSummaryModel userSummaryModel)
        {
            userSummaryModel.UserId = user.Id;
            userSummaryModel.UserName = user.UserName;
        }

        internal void Map(User user, UserDetailsModel userDetailsModel)
        {
            userDetailsModel.UserId = user.Id;
            userDetailsModel.UserRoleId = user.UserRoleId;
            userDetailsModel.UserRoleName = UserRoleModel.GetName(user.UserRoleId);
            userDetailsModel.UserName = user.UserName;
            userDetailsModel.First = user.First;
            userDetailsModel.Last = user.Last;
            userDetailsModel.Email = user.Email;
        }

        internal void Map(UserDetailsModel userDetailsModel, User user)
        {
            user.UserRoleId = userDetailsModel.UserRoleId;
            user.UserName = userDetailsModel.UserName;
            user.First = userDetailsModel.First;
            user.Last = userDetailsModel.Last;
            user.Email = userDetailsModel.Email;
        }
    }
}
