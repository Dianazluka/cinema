using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(cinema_i_s.Startup))]
namespace cinema_i_s
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
