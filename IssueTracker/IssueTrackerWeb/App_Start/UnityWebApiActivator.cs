using Microsoft.Practices.Unity.WebApi;
using System.Web.Http;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(IssueTrackerWeb.UnityWebApiActivator), "Start")]

namespace IssueTrackerWeb
{
    public static class UnityWebApiActivator
    {
        public static void Start()
        {
            var resolver = new UnityDependencyResolver(UnityConfig.GetConfiguredContainer());

            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
    }
}