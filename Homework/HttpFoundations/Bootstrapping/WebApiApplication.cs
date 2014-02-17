using System.Web;
using System.Web.Http;

namespace HttpFoundations.Bootstrapping
{
    public class WebApiApplication : HttpApplication
    {
        public HttpConfiguration Configure(HttpConfiguration httpConfig)
        {
            httpConfig.MapHttpAttributeRoutes();

            httpConfig.DependencyResolver = new CompositionRoot().Register();

            return httpConfig;
        }

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(httpConfig => Configure(httpConfig));
        }
    }
}
