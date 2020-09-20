using ElibForumMVC.Data;
using ElibForumMVC.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SqlServer.Management.Smo;

[assembly: HostingStartup(typeof(ElibForumMVC.Areas.Identity.IdentityHostingStartup))]
namespace ElibForumMVC.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {


                services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
               .AddRoles<ApplicationRole>()
               .AddRoleManager<RoleManager<ApplicationRole>>()
               .AddEntityFrameworkStores<ApplicationDbContext>();

            });
        }
    }
}