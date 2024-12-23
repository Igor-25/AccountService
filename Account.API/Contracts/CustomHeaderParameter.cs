using AccountStore.API.Controllers;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AccountStore.API.Contracts
{
    public class CustomHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {

            //var descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;
            var descriptor = context.ApiDescription.ActionDescriptor;

            if (descriptor.DisplayName.Contains("CreateAccount"))
            {
                operation.Parameters =
                [
                    new OpenApiParameter
                    {
                        Name = "X-Device",
                        In = ParameterLocation.Header,
                        Description = "Обязательный заголовок для создания пользователя:" +
                " mail – обязательны только имя и электронная почта," +
                " mobile – обязательный только номер телефона," +
                " web – обязательно ввести все поля, но необязательно электронную почту и адрес.",

                        Required = false
                    },

                //    new OpenApiParameter
                //{
                //Name = "Id",
                //In = ParameterLocation.Query,
                //Description = "Id",
                //Required = false
                //},

                ];
            }

        }
    }
}
