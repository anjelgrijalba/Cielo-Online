using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CIELO_TM.Startup))]
namespace CIELO_TM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
