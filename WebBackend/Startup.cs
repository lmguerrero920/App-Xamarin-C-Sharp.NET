using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebBackend.Startup))]
namespace WebBackend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
