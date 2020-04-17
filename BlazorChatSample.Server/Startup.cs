using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Linq;

namespace BlazorChatSample.Server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(); 

            // I think this was pre-Core3.x
            //services.AddMvc();
            services.AddSignalR();

            // not sure this is required any more
            //services.AddResponseCompression(opts =>
            //{
            //    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
            //        new[] { "application/octet-stream" });
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }

            app.UseStaticFiles();
            app.UseBlazorFrameworkFiles();  // preview2 change

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                // SignalR endpoint routing setup
                endpoints.MapHub<Hubs.ChatHub>(Shared.ChatClient.HUBURL);

                endpoints.MapFallbackToFile("index.html");  // preview2 change
            });
        }

    }
}
