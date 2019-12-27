using System;
using System.Collections.Generic;
using System.Reflection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PagingExtensions
{
    public class PagingOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            MethodInfo methodInfo;
            context.ApiDescription.TryGetMethodInfo(out methodInfo);
            var paging = methodInfo.GetCustomAttribute<PagingResponseHeadersAttribute>();

            if (paging != null)
            {
                foreach (var responseCode in operation.Responses.Keys)
                {
                    if (responseCode == "200")
                    {
                        var response = operation.Responses[responseCode];
                        if (response.Headers == null)
                            response.Headers = new Dictionary<string, Header>();

                        response.Headers["X-Paging-PageNo"] = new Header
                        {
                            Type = "integer",
                            Description = "Page No"
                        };
                        response.Headers["X-Paging-PageSize"] = new Header
                        {
                            Type = "integer",
                            Description = "Page Size"
                        };
                        response.Headers["X-Paging-PageCount"] = new Header
                        {
                            Type = "integer",
                            Description = "Paging Page Count"
                        };
                        response.Headers["X-Paging-TotalRecordCount"] = new Header
                        {
                            Type = "integer",
                            Description = "Total Record Count"
                        };
                    }
                }
            }
        }
    }
}
