using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MMYBWebService.Web.Miscellaneous;
using MMYBWebService.Web.Util;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMYBWebService.Web
{
    public class Startup
    {
        private IConfiguration _configuration { get; }

        private readonly IWebHostEnvironment _env;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;

            InitInterfaceHNSetting(configuration);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(
               options =>
               {
                   options.EnableEndpointRouting = false;
                   options.Filters.Add<GlobalExceptionFilter>();
               }
               ).AddNewtonsoftJson(opt =>
               {
                   opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                   opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
                   opt.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
               });

            services.AddHttpContextAccessor();
            services.AddSingleton(typeof(Logger<ApiActionFilter>));
            services.AddSingleton(typeof(Logger<GlobalExceptionFilter>));

            services.Configure<KestrelServerOptions>(x => x.AllowSynchronousIO = true)
                    .Configure<IISServerOptions>(x => x.AllowSynchronousIO = true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.Use(next => context =>
            {
                context.Request.EnableBuffering();
                return next(context);
            });

            app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void InitInterfaceHNSetting(IConfiguration configuration)
        {
            var section = configuration.GetSection("InterfaceHNSetting");

            InterfaceHNSetting config = new InterfaceHNSetting()
            {

                CenterId = section.GetSection("center_id").Value.ToString(),
                CenterName = section.GetSection("center_name").Value.ToString(),
                HospitalId = section.GetSection("hosp_id").Value.ToString(),
                HospitalName = section.GetSection("hosp_name").Value.ToString(),
                HospitalId_pwd = section.GetSection("hosp_pwd").Value.ToString(),
                Server = section.GetSection("server").Value.ToString(),
                Port = Convert.ToInt64(section.GetSection("port").Value.ToString()),
                Servle = section.GetSection("servlet").Value.ToString(),
                StaffId = section.GetSection("staff_id").Value.ToString(),
                StaffId_pwd = section.GetSection("staff_pwd").Value.ToString(),


            };

            InterfaceHNUtil.config = config;
        }
    }
}
