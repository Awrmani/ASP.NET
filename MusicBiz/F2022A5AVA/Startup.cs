using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(F2022A5AVA.Startup))]

namespace F2022A5AVA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
