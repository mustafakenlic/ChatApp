using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Docq_ChatApp.Startup))]

namespace Docq_ChatApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
