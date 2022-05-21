using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Users.WebApi
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        public void Configure(SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "User API",
                Version = "v1",
            });

            options.AddSecurityDefinition("AuthToken", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer",
                Name = "Authorization",
                Description = "Authorization token"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "AuthToken"
                        }
                    },
                new string[] {}
                }
            });

            options.CustomOperationIds(apiDescription =>
            apiDescription.TryGetMethodInfo(out MethodInfo methodInfo)
            ? methodInfo.Name
            : null);
        }
    }
}
