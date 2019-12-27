﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PagingExtensions;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Background.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new Info { Title = "CRM Identity Background API", Version = "v1.0" });
                var filePath = Path.Combine(System.AppContext.BaseDirectory, "Base.DTOs.xml");
                c.IncludeXmlComments(filePath);
                filePath = Path.Combine(System.AppContext.BaseDirectory, "Identity.Background.xml");
                c.IncludeXmlComments(filePath);
                c.DescribeAllEnumsAsStrings();
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.DescribeAllParametersInCamelCase();
                c.OperationFilter<PagingOperationFilter>();
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app, IConfiguration config)
        {
            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) => swaggerDoc.Host = config["Host"]);
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"{config["Http"]}{config["Host"]}/swagger/v1.0/swagger.json", "CRM Identity Background API");
                c.DocumentTitle = "CRM Identity Background API";
                c.DocExpansion(DocExpansion.None);
            });

            return app;
        }
    }
}
