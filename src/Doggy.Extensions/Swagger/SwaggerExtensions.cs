using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Doggy.Extensions.Configuration.AppInfo;
using Doggy.Extensions.Configuration.Authentication;
using Doggy.Extensions.Configuration.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Doggy.Extensions.Swagger
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            var appDisplayName = configuration.TryGetAppInfoDisplayName();
            var appVersion = configuration.TryGetVersion();
            var authEnabled = configuration.TryGetAuthenticationEnabled();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(appVersion, new OpenApiInfo
                {
                    Title = appDisplayName,
                    Version = appVersion
                });
                
                c.EnableAnnotations();

                var xmlFile = $"{Assembly.GetEntryAssembly()?.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                if (authEnabled)
                {
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
                }
            });

            return services;
        }

        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            var configuration = app.ApplicationServices.GetService<IConfiguration>();
            var appDisplayName = configuration.GetAppInfoDisplayName();
            var appVersion = configuration.TryGetVersion();
            var virtualDirectory = configuration.TryGetVirtualDirectory();

            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                    swagger.Servers = new List<OpenApiServer>
                    {
                        new OpenApiServer {Url = "/"}
                    };
                });
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"{virtualDirectory}swagger/v1/swagger.json", $"{appDisplayName} {appVersion}");
                c.RoutePrefix = "docs";
            });

            return app;
        }
    }
}