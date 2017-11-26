using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(testMvc.Startup))]
namespace testMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
