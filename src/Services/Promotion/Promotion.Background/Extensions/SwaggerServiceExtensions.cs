using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PagingExtensions;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Promotion.Background.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new Info { Title = "CRM Promotion Background API", Version = "v1.0" });
                var filePath = Path.Combine(System.AppContext.BaseDirectory, "Base.DTOs.xml");
                c.IncludeXmlComments(filePath);
                filePath = Path.Combine(System.AppContext.BaseDirectory, "Promotion.Background.xml");
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
                c.OperationFilter<SecurityOperationFilter>();
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
                c.SwaggerEndpoint($"{config["Http"]}{config["Host"]}/swagger/v1.0/swagger.json", "CRM Promotion Background API");
                c.DocumentTitle = "CRM Promotion Background API";
                c.DocExpansion(DocExpansion.None);
            });

            return app;
        }
    }

    public class SecurityOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            //var authorizeAttribute = context.MethodInfo.GetCustomAttributes(true)
            //    .OfType<AuthorizeAttribute>()
            //    .Distinct();

            //if (authorizeAttribute.Any())
            //{
            //    operation.Responses.Add("401", new Response { Description = "Unauthorized" });

            //    operation.Security = new List<IDictionary<string, IEnumerable<string>>>();
            //    operation.Security.Add(new Dictionary<string, IEnumerable<string>>
            //    {
            //        { "Bearer", new string[]{ } },
            //    });
            //}

            operation.Responses.Add("401", new Response { Description = "Unauthorized" });

            operation.Security = new List<IDictionary<string, IEnumerable<string>>>();
            operation.Security.Add(new Dictionary<string, IEnumerable<string>>
            {
                { "Bearer", new string[]{ } },
            });

        }
    }
}
