using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DOTNET.Startup))]
namespace DOTNET
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
