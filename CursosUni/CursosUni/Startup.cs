using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CursosUni.Startup))]
namespace CursosUni
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
