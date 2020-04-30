using Core.Interfaces;
using Infrastructure;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Web
{
    public class Startup
    {
        // Now we create configuration throw dependency InJection to intialize appseetings.json in startup file
        private readonly IConfiguration configration;

        public Startup(IConfiguration Configration)
        {
            this.configration = Configration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Now add MVC Web View 
            services.AddControllersWithViews();
            // Now add DbContext Configuration
            services.AddDbContext<DataContext>(options =>
            {

                options.UseSqlServer(configration.GetConnectionString("MyPortifolioDb"));
            
            });
            // to add interface and its implementation in startup file
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();  // to allow you use files of your page like js css pics fonts

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "defaultRoute","{controller=Home}/{action=Index}/{id?}"
                    );
                endpoints.MapControllerRoute(
                    "portifolioitems", "{controller=portifolioitems}/{action=Index}/{id?}"
                    );

            });
        }
    }
}
