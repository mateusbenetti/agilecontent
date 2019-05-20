using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AgileContent.CrossCutting.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;

namespace AgileContent.WebApi
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen();
            //services.ConfigureSwaggerGen(p =>
            //{
                
            //});
            //services.ConfigureSwaggerGen(options =>
            //{
            //    //options.SingleApiVersion(new Info
            //    //{
            //    //        Title = "Agile Content Tests",
            //    //        Version = "v1",
            //    //        Description = ".Test from Net Developer Sr. C# job in Agile Content.",
            //    //        Contact = new Contact
            //    //        {
            //    //            Name = "Mateus Benetti",
            //    //            Url = "https://github.com/mateusbenetti/agilecontent"
            //    //        }
            //    //    });
            //    var applicationPath =
            //        PlatformServices.Default.Application.ApplicationBasePath;
            //    var applicationName =
            //        PlatformServices.Default.Application.ApplicationName;
            //    var xmlDocPath =
            //        Path.Combine(applicationPath, $"{applicationName}.xml");

            //    c.IncludeXmlComments(xmlDocPath);
            //});

            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            NativeInjector.RegisterServices(services);
        }
    }
}
