using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using IoC.CastleWindsor;
using System.Web.Http.Dispatcher;

namespace SuperCom.Host
{
    public class Global : HttpApplication
    {
        private IDependencyInjectionResolver _resolver;

        void Application_Start(object sender, EventArgs e)
        {
            var assemblise = new string[]
            {
                "SuperCom.Host",
                "Sql.Infrastructure.IoC"
            };

            DependencyResolverFactory.Initialize(assemblise);

            _resolver = DependencyResolverFactory.GetResolver();

            GlobalConfiguration.Configuration.Services.Replace(
                typeof(IHttpControllerActivator),
                new WindsorCompositionRoot(_resolver.GetResolver));

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}