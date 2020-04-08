using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KomodoBackend.Startup))]
namespace KomodoBackend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
