using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Q1.Startup))]
namespace Q1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
