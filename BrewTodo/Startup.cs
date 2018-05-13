using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BrewTodo.Startup))]
namespace BrewTodo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
