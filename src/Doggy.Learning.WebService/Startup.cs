using System.Text;
using AutoMapper;
using Doggy.Learning.Auth.Business.Services;
using Doggy.Learning.Auth.Data.Repositories;
using Doggy.Learning.Auth.Domain.Entities;
using Doggy.Learning.Auth.Domain.Interfaces;
using Doggy.Learning.Infrastructure.Filters;
using Doggy.Learning.Infrastructure.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Doggy.Learning.WebService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region injection settings

            services.Configure<AppSettings>(Configuration);
            var appSettings = services.BuildServiceProvider().GetService<IOptions<AppSettings>>().Value;

            #endregion

            #region swagger settings

            services.AddCustomSwagger();

            #endregion

            services.AddCors();
            services.AddControllers(options => { options.Filters.Add<HeaderFilter>(); })
                .AddJsonOptions(options => options.JsonSerializerOptions.IgnoreNullValues = true);
            services.AddAutoMapper(typeof(Startup));

            #region auth jwt

            if (appSettings.Authentication.Enabled)
            {
                var key = Encoding.ASCII.GetBytes(appSettings.Authentication.Secret);
                services.AddAuthentication(x =>
                    {
                        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer(x =>
                    {
                        x.RequireHttpsMetadata = false;
                        x.SaveToken = true;
                        x.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(key),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    });
            }

            #endregion

            #region injection helper

            services.AddSingleton(typeof(JwtHelper));

            #endregion

            #region injection db context

            services.AddDbContextPool<AuthContext>(options =>
                options.UseLazyLoadingProxies().UseMySql(appSettings.ConnectionString));

            #endregion

            #region injection repositories

            services.AddScoped<GroupRepositoryBase, GroupRepository>();
            services.AddScoped<RoleRepositoryBase, RoleRepository>();
            services.AddScoped<ModuleRepositoryBase, ModuleRepository>();
            services.AddScoped<ServiceRepositoryBase, ServiceRepository>();

            #endregion

            #region ingjection services

            services.AddScoped<IUserService, UserService>();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IOptions<AppSettings> appSettingsOptions)
        {
            var appSettings = appSettingsOptions.Value;

            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseStaticFiles();
            app.UseRouting();

            app.UseCustomSwagger();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            if (appSettings.Authentication.Enabled)
            {
                app.UseAuthentication();
                app.UseAuthorization();
            }

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}