using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DUT4UStudentApp.Startup))]
namespace DUT4UStudentApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
