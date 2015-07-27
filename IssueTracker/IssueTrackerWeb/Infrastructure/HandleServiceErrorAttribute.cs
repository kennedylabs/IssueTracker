using IssueTrackerCommon.Services;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace IssueTrackerWeb.Infrastructure
{
    public class HandleServiceErrorAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var serviceException = actionExecutedContext.Exception as ServiceException;

            if (serviceException != null)
            {
                var statusCode = serviceException.Type == ServiceExceptionType.NotFound ?
                    HttpStatusCode.NotFound : serviceException.Type ==
                    ServiceExceptionType.BadRequest ? HttpStatusCode.BadRequest :
                    HttpStatusCode.InternalServerError;

                actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                    statusCode, serviceException);
            }
        }
    }
}
