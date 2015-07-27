using IssueTrackerDomain.Services;
using IssueTrackerModels;
using System;
using System.Net;
using System.Security.Principal;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace IssueTrackerWeb.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthApiAttribute : AuthorizeAttribute
    {
        int _roleValue;

        public AuthApiAttribute(int roleValue = UserRoleModel.Developer)
        {
            _roleValue = roleValue;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var controller = actionContext.ControllerContext.Controller as BaseApiController;
            if (controller == null)
                throw new Exception("Controller does not inherit from BaseController.");

            var token = BaseApiController.GetHeaderString(actionContext.Request, "auth-token");

            var ticket = controller.Service<AuthService>().CheckAuthToken(token, _roleValue);

            if (ticket.Status == "Succeeded")
                actionContext.ControllerContext.RequestContext.Principal =
                    new GenericPrincipal(new SimpleIdentity(ticket), null);
            else if (ticket.Status == "Expired")
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            else if (ticket.Status == "Unauthorized")
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }
    }
}
