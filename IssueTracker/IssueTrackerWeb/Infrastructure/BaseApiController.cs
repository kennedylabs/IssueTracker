using IssueTrackerCommon.Services;
using IssueTrackerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace IssueTrackerWeb.Infrastructure
{
    public abstract class BaseApiController : ApiController
    {
        //Lazy<IServiceFactory> _serviceFactory =
        //    new Lazy<IServiceFactory>(UnityConfig.CreateServiceFactory);

        Lazy<IServiceFactory> _serviceFactory =
            new Lazy<IServiceFactory>(() => null);

        public T Service<T>() where T : IService
        {
            return _serviceFactory.Value.CreateService<T>();
        }

        public static string GetHeaderString(HttpRequestMessage request, string headerName)
        {
            IEnumerable<string> headerValues;
            request.Headers.TryGetValues(headerName, out headerValues);
            return headerValues != null ? headerValues.First() : String.Empty;
        }

        protected int CurrentUserId
        {
            get
            {
                var simpleIdentity = User.Identity as SimpleIdentity;
                return simpleIdentity != null ? simpleIdentity.Ticket.UserId : 0;
            }
        }

        protected UserTicketModel CurrentUserTicket
        {
            get
            {
                var simpleIdentity = User.Identity as SimpleIdentity;
                return simpleIdentity != null ? simpleIdentity.Ticket : null;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (_serviceFactory.IsValueCreated)
                _serviceFactory.Value.Dispose();
        }
    }
}
