using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AccountStore.API.Contracts
{
    public class CustomHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters =
            [
                new OpenApiParameter
            {
                Name = "X-Device",
                In = ParameterLocation.Header,
                Description = "Custom header for authentication",
                Required = false
            },
             new OpenApiParameter
            {
                Name = "Id",
                In = ParameterLocation.Query,
                Description = "Id",
                Required = false
            },

        ];
        }
    }
}
