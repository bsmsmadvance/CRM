using System;
using System.IO;
using Auth;
using Common;
using Database.Models;
using DateTimeExtensions;
using ErrorHandling;
using Commission.API.Extensions;
using Commission.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using RequestLogging;

namespace Commission.API
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
            services.AddScoped<IIncreaseMoneyService, IncreaseMoneyService>();
            services.AddScoped<IDeductMoneyService, DeductMoneyService>();
            services.AddScoped<ICalculatePerMonthHighRiseSaleService, CalculatePerMonthHighRiseSaleService>();
            services.AddScoped<ICalculatePerMonthHighRiseTransferService, CalculatePerMonthHighRiseTransferService>();
            services.AddScoped<ICalculatePerMonthLowRiseService, CalculatePerMonthLowRiseService>();
            services.AddScoped<IChangeLCSaleService, ChangeLCSaleService>();
            services.AddScoped<IChangeLCTransferService, ChangeLCTransferService>();
            services.AddScoped<ICommissionHighRiseSaleVeiwService, CommissionHighRiseSaleVeiwService>();
            services.AddScoped<ICommissionHighRiseTransferVeiwService, CommissionHighRiseTransferVeiwService>();
            services.AddScoped<ICommissionLowRiseVeiwService, CommissionLowRiseVeiwService>();
            services.AddScoped<ICommissionSettingService, CommissionSettingService>();
            services.AddScoped<IGeneralSettingService, GeneralSettingService>();
            services.AddScoped<IRateSettingAgentService, RateSettingAgentService>();
            services.AddScoped<IRateSettingFixSaleModelService, RateSettingFixSaleModelService>();
            services.AddScoped<IRateSettingFixSaleService, RateSettingFixSaleService>();
            services.AddScoped<IRateSettingFixTransferModelService, RateSettingFixTransferModelService>();
            services.AddScoped<IRateSettingFixTransferService, RateSettingFixTransferService>();
            services.AddScoped<IRateSettingSaleService, RateSettingSaleService>();
            services.AddScoped<IRateSettingTransferService, RateSettingTransferService>();
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
