using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Doggy.Learning.Infrastructure.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Doggy.Learning.Infrastructure.Helpers
{
    public static class SwaggerHelper
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            var appName = configuration.GetValue<string>(AppSettingsConstants.AppInfoName);
            var appVersion = configuration.GetValue<string>(AppSettingsConstants.AppInfoVersion);
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Learn .Net Core web api",
                    Version = "v1"
                });

                var xmlFile = $"{Assembly.GetEntryAssembly()?.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Please enter into field the word 'Bearer' followed by a space and the JWT value",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            return services;
        }

        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                    swagger.Servers = new List<OpenApiServer>
                        {new OpenApiServer {Url = $"{httpReq.Scheme}://{httpReq.Host.Value}"}};
                });
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = "docs";
            });

            return app;
        }
    }
}