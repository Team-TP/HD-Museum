using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Globalization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HD.Station.Startup
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        public IConfigurationRoot Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddLogging();
            if (Configuration["AppName"] == null || Configuration["AppName"].ToString() == "")
            {
                throw new System.Exception("Config AppName first");
            }

            services.AddDataProtection().PersistKeysToDistributedCache();

            services.AddDistributedCache(Configuration);

            services
                .AddMuseumFeature(Configuration.GetSection("HD:Station:Museum"))
                .AddManagers()
                .UseSqlServer(Configuration);


            services.AddSession(s =>
            {
                s.Cookie.Name = Configuration["AppName"] ?? Assembly.GetAssembly(typeof(Startup)).FullName;
                Configuration.GetSection("SessionOptions").Bind(s);
            });

            var sqlLocalizerConnectionString = Configuration.GetConnectionString("Localizer");
            services.AddSqlLocalization(opt =>
            {
                opt.ConnectionString = sqlLocalizerConnectionString;
                opt.SquareLocalizedString = true;
                opt.SaveLocalizableString = true;
            });

            services.AddHdMvc(Configuration);
            services.AddKendo();
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSession();
            // app.UseAuthentication();

            var cultures = new CultureInfo[]
            {
                            new CultureInfo("de-DE"),
            new CultureInfo("en-US"),

            new CultureInfo("vi-VN")
            };
            var opt = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("vi-VN"),
                FallBackToParentCultures = true,
                FallBackToParentUICultures = true,
                SupportedCultures = cultures,
                SupportedUICultures = cultures,

            };
            app.UseRequestLocalization(opt);
            app.UseStatusCodePagesWithReExecute("/error/{0}");
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "fullArea",
                    template: "{area:exists}/{controller=Home}/{id:guid}/{action=Index}"
                );
                routes.MapRoute(
                    name: "auth",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id:guid?}"
                );
                routes.MapRoute(
                    name: "home",
                    template: "{controller=Home}/{action=Index}/{id?}"
                );
            });

            app.UseKendo(env);
        }
    }
}
