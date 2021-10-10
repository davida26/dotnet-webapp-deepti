using Dot.Services;
using Dot.Services.Mappings;
using Dot.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using DOt.Helpers;

namespace Dot
{
    public partial class Startup
    {
        private const string gitHubUsersApi = "https://api.github.com/users";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddAutoMapper(typeof(Startup));

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<IUoW, DotUoW>();

            services.AddScoped<IDotRepository, DotRepository>();

            services.AddScoped<IDotService, DotService>();

            services.AddDbContext<DotContext>(options =>
            {
                options.UseInMemoryDatabase(databaseName : "DotDatabase");
            }, ServiceLifetime.Scoped);

            services.AddScoped<ILogger>(svc => svc.GetRequiredService<ILogger<DotService>>());

            var isLive = Configuration.GetValue(typeof(bool), "IsLive");

            var sampleUsers = DemoData.GetAllSampleUsers();

            _ = InitializeAppDataAsync(Configuration.GetValue(typeof(string), "GitApiUsersUrl").ToString(), false, (bool)isLive, sampleUsers).Result;

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
