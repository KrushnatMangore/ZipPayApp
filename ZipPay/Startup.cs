using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ZipPay.Middleware;
using ZipPay.Context;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ZipPay
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
            //services.AddScoped()
            services.AddDbContext<ZipPayContext>(opts =>
        opts.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));
            //services.AddScoped<IZipcodeRepository, ZipcodeRepository>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    version = "v1",
                    title = "MyAPI",
                    description = "Testing"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<CustomExceptionMiddleware>();
            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ZipPayAPI");
            });
        }
    }

    internal class Info : OpenApiInfo
    {
        public string version { get; set; }
        public string title { get; set; }
        public string description { get; set; }
    }
}
