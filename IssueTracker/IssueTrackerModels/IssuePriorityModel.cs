using System.Collections.Generic;

namespace IssueTrackerModels
{
    public class IssuePriorityModel : EnumerationBaseModel
    {
        public const int Unknown = 0;
        public const int Low = 1;
        public const int Medium = 2;
        public const int High = 3;
        public const int Critical = 4;

        public static string GetName(int priorityValue)
        {
            return priorityValue == Low ? "Low" : priorityValue == Medium ? "Medium" :
                priorityValue == High ? "High" : priorityValue == Critical ? "Critical" :
                "Unknown";
        }

        public static IssuePriorityModel Create(int priorityValue)
        {
            return new IssuePriorityModel { Value = priorityValue, Name = GetName(priorityValue) };
        }

        public static IList<IssuePriorityModel> CreateAll()
        {
            return new List<IssuePriorityModel>
            {
                Create(Low),
                Create(Medium),
                Create(High),
                Create(Critical)
            };
        }
    }
}
