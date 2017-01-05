using Microsoft.Owin;
using Owin;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AppStart = Dootrix.Planets.Api.App_Start;

namespace Dootrix.Planets.Api
{
    /// <summary>
    /// OWIN Startup class (replaces Global.asax)
    /// </summary>
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AppStart.UnityWebApiActivator.Start();
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            ConfigureWebApi(app);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ConfigureAuth(app);
        }
    }
}
