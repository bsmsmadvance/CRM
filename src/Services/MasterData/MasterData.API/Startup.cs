using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Auth;
using Common;
using Database.Models;
using DateTimeExtensions;
using ErrorHandling;
using MasterData.API.Extensions;
using MasterData.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog;
using NLog.Extensions.Logging;
using RequestLogging;

namespace MasterData.API
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
                    builder => builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin());
                    //.AllowCredentials());
                    //.WithExposedHeaders("authorization", "accept", "content-type", "origin", "X-Paging-PageNo, X-Paging-PageSize, X-Paging-PageCount, X-Paging-TotalRecordCount")
                    //.WithHeaders("authorization", "accept", "content-type", "origin", "X-Paging-PageNo, X-Paging-PageSize, X-Paging-PageCount, X-Paging-TotalRecordCount"));
            });

            //JWT
            services.AddJwt(Configuration);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

            //Swagger
            services.AddSwaggerDocumentation();

            //Database
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Configuration["DBConnectionString"], b => b.MigrationsAssembly("Database.Models")));
            services.AddDbContext<DbQueryContext>(options => options.UseSqlServer(Configuration["DBConnectionString"], b => b.MigrationsAssembly("Database.Models")));

            //Services
            services.AddScoped<IProvinceService, ProvinceService>();
            services.AddScoped<IBankBranchService, BankBranchService>();
            services.AddScoped<IBankService, BankService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IDistrictService, DistrictService>();
            services.AddScoped<ILandOfficeService, LandOfficeService>();
            services.AddScoped<IMasterCenterService, MasterCenterService>();
            services.AddScoped<IMasterCenterGroupService, MasterCenterGroupService>();
            services.AddScoped<ISubDistrictService, SubDistrictService>();
            services.AddScoped<IBOConfigurationService, BOConfigurationService>();
            services.AddScoped<IBGService, BGService>();
            services.AddScoped<ISubBGService, SubBGService>();
            services.AddScoped<ILegalEntityService, LegalEntityService>();
            services.AddScoped<ITypeOfRealEstateService, TypeOfRealEstateService>();
            services.AddScoped<IAgentsService, AgentsService>();
            services.AddScoped<IBankAccountService, BankAccountService>();
            services.AddScoped<IEDCService, EDCService>();
            services.AddScoped<IAgentEmployeeService, AgentEmployeeService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICancelReasonService, CancelReasonService>();
            services.AddScoped<ICancelReturnSettingService, CancelReturnSettingService>();
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
            if (env.IsDevelopment() || env.EnvironmentName == "Local")
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<RequestLoggingMiddleware>();
            app.UseSwaggerDocumentation(Configuration);
            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseMvc();
        }
    }
}
