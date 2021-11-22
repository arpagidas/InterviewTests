using BlazorApp.Data;
using BlazorApp.Services.Customers;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace BlazorApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BlazorAppContext>(opts => 
                opts.UseSqlServer(Configuration.GetConnectionString("AppConnection")));

            services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.User.RequireUniqueEmail = false;
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddEntityFrameworkStores<BlazorAppContext>();

            services.AddAuthentication()
                .AddJwtBearer(IdentityServerAuthenticationDefaults.AuthenticationScheme, opts =>
                {
                    opts.Authority = "https://demo.identityserver.io";
                    opts.RequireHttpsMetadata = false;
                
                    opts.Audience = "api";
                });

            services.AddScoped<ICustomerService, CustomerService>();

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddHttpClient();
            services.AddHttpContextAccessor();
        }

        public void Configure(
            IApplicationBuilder app, 
            IWebHostEnvironment env,
            BlazorAppContext context,
            UserManager<IdentityUser> userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            context.Database.Migrate();

            var seedTask = Initializer.SeedAsync(context, userManager);
            Task.WaitAll(seedTask);

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
