using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Zinder.Startup))]
namespace Zinder
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
