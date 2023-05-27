using LaNacion.Entities.Api.Version1.Contacts;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json;

namespace LaNacion.Entities
{
    public class CustomOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {

            if (operation.RequestBody != null && operation.RequestBody.Content.TryGetValue("multipart/form-data", out var openApiMediaType))
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                var array = new OpenApiArray
             {
            new OpenApiString(JsonSerializer.Serialize(new BasePhoneNumber {Type=0 ,Number=""}, options)),
             };

                openApiMediaType.Schema.Properties["PhoneNumbers"].Example = array;
            }
        }
    }
}
//https://stackoverflow.com/questions/70579248/swagger-ui-array-of-objects-in-multipart-form-data