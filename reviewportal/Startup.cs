using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(reviewportal.Startup))]
namespace reviewportal
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
