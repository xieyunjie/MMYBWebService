using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMYBWebService.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(
       //options =>
       //{
       //    options.EnableEndpointRouting = false;
       //    options.Filters.Add<GlobalExceptionFilter>();
       //}
       ).AddNewtonsoftJson(opt =>
       {
           opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
           opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
           opt.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
       });

            services.AddHttpContextAccessor();
            // 这里是多租户的关键！！
            //services.AddAutofacMultitenantRequestServices();

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

            //app.Use(next => context =>
            //{
            //    context.Request.EnableBuffering();
            //    return next(context);
            //});

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
    }
}
