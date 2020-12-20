using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(PetGuide.Web.Areas.Identity.IdentityHostingStartup))]

namespace PetGuide.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}
