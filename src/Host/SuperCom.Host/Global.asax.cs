using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using IoC.CastleWindsor;

namespace SuperCom.Host
{
    public class Global : HttpApplication
    {
        private IDependencyInjectionResolver _resolver;

        void Application_Start(object sender, EventArgs e)
        {
            var assemblise = new string[] 
            {
                "Sql.Infrastructure.IoC"
            };

            DependencyResolverFactory.Initialize(assemblise);

            _resolver = DependencyResolverFactory.GetResolver();


            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes); 
        }
    }
}