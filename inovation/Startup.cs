using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(inovation.Startup))]
namespace inovation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
