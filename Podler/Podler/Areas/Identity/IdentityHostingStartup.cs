using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Podler.Data;
using Podler.Models;

[assembly: HostingStartup(typeof(Podler.Areas.Identity.IdentityHostingStartup))]
namespace Podler.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {

                var identityConnectionString = context.Configuration.GetConnectionString("IdentityConnection");

                services.AddDbContext<ApplicationIdentityContext>(options =>
                    options.UseSqlite(identityConnectionString));

                services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<ApplicationIdentityContext>();

                services.AddIdentityServer()
                    .AddApiAuthorization<ApplicationUser, ApplicationIdentityContext>();

                services.AddAuthentication()
                    .AddIdentityServerJwt();
            });
        }
    }
}