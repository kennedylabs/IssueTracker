using System;

namespace IssueTrackerCommon.Services
{
    [Serializable]
    public class ServiceException : Exception
    {
        [NonSerialized]
        ServiceExceptionType _type;

        public ServiceExceptionType Type { get { return _type; } }

        public ServiceException(ServiceExceptionType type, string message = null,
            Exception innerException = null)
            : base(message, innerException)
        {
            _type = type;
        }
    }
}
