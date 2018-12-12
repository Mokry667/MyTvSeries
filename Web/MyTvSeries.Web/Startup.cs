using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyTvSeries.Domain.Ef;
using MyTvSeries.Domain.Identity;

namespace MyTvSeries.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var tvDbConnection = Configuration["tvDbConnection"];
            services.AddDbContext<TvSeriesContext>(options => options.UseSqlServer(tvDbConnection, 
                x => x.MigrationsAssembly("MyTvSeries.Domain.Migrations")));

            /*            var identityDbConnection = Configuration["identityDbConnection"];
                        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(identityDbConnection,
                            x => x.MigrationsAssembly("MyTvSeries.Identity.Migrations")));*/

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<TvSeriesContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
