using System.Collections.Generic;

namespace IssueTrackerModels
{
    public class IssueTypeModel : EnumerationBaseModel
    {
        public const int Unknown = 0;
        public const int Bug = 1;
        public const int Task = 2;
        public const int Feature = 3;

        public static string GetName(int typeValue)
        {
            return typeValue == Bug ? "Bug" : typeValue == Task ? "Task" :
                typeValue == Feature ? "Feature" : "Unknown";
        }

        public static IssueTypeModel Create(int typeValue)
        {
            return new IssueTypeModel { Value = typeValue, Name = GetName(typeValue) };
        }

        public static IList<IssueTypeModel> CreateAll()
        {
            return new List<IssueTypeModel>
            {
                Create(Bug),
                Create(Task),
                Create(Feature)
            };
        }
    }
}
