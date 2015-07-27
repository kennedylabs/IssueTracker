using System.Collections.Generic;

namespace IssueTrackerModels
{
    public class IssueStatusModel : EnumerationBaseModel
    {
        public const int Unknown = 0;
        public const int Submitted = 1;
        public const int Open = 2;
        public const int Closed = 3;
        public const int ReOpened = 4;

        public static string GetName(int statusValue)
        {
            return statusValue == Submitted ? "Submitted" : statusValue == Open ? "Open" :
                statusValue == Closed ? "Closed" : statusValue == ReOpened ? "ReOpened" :
                "Unknown";
        }

        public static IssueStatusModel Create(int statusValue)
        {
            return new IssueStatusModel { Value = statusValue, Name = GetName(statusValue) };
        }

        public static IList<IssueStatusModel> CreateAll()
        {
            return new List<IssueStatusModel>
            {
                Create(Submitted),
                Create(Open),
                Create(Closed),
                Create(ReOpened)
            };
        }
    }
}
