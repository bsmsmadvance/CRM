﻿using System;
using System.IO;
using Database.Models;
using DateTimeExtensions;
using ErrorHandling;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using RequestLogging;
using Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Accounting.Services.IService;
using Accounting.Services.Service;
using Accounting.API.Extensions;
using Microsoft.AspNetCore.Http;
using Common;

namespace Accounting.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), $"/nlog.{env.EnvironmentName}.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //CORS
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            //JWT
            services.AddJwt(Configuration);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

            //Swagger
            services.AddSwaggerDocumentation();

            //Database
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Configuration["DBConnectionString"], b => b.MigrationsAssembly("Database.Models")));

            //Services
            services.AddScoped<ICalendarLockService, CalendarLockService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMvc()
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.Converters.Add(new JsonDateTimeConverter());
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                options.SerializerSettings.Converters.Add(new TrimmingConverter());
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment() && env.EnvironmentName == "Local")
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("CorsPolicy");
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<RequestLoggingMiddleware>();
            app.UseSwaggerDocumentation(Configuration);
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}