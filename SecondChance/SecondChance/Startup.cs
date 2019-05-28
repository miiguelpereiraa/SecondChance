using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SecondChance.Startup))]
namespace SecondChance
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
