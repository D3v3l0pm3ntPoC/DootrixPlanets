using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Dootrix.UI.Startup))]
namespace Dootrix.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
