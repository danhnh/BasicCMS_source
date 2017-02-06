using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BasicCMS.Startup))]
namespace BasicCMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
