using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BandAide.Web.Startup))]
namespace BandAide.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
