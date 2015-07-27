using System.Collections.Generic;

namespace IssueTrackerModels
{
    public class UserRoleModel : EnumerationBaseModel
    {
        public const int Unknown = 0;
        public const int Developer = 1;
        public const int QA = 2;
        public const int Manager = 3;
        public static int Admin = 3;

        public static string GetName(int roleValue)
        {
            return roleValue == Developer ? "Developer" : roleValue == QA ? "QA" :
                roleValue == Manager ? "Manager" : roleValue == Admin ? "Admin" : "Unknown";
        }

        public static UserRoleModel Create(int roleValue)
        {
            return new UserRoleModel { Value = roleValue, Name = GetName(roleValue) };
        }

        public static IList<UserRoleModel> CreateAll()
        {
            return new List<UserRoleModel>
            {
                Create(Developer),
                Create(QA),
                Create(Manager),
                Create(Admin)
            };
        }
    }
}
