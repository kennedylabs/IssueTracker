using IssueTrackerWeb.Infrastructure;
using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace IssueTrackerWeb
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "ActionApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Filters.Add(new HandleServiceErrorAttribute());

            config.Filters.Add(new ValidateModelAttribute());

            config.Formatters
                .OfType<JsonMediaTypeFormatter>().First()
                .SerializerSettings.ContractResolver =
                    new CamelCasePropertyNamesContractResolver();
        }
    }
}
