using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataStore.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebApi.Auth;

namespace PlatformDemo
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public Startup(IWebHostEnvironment env)
        {
            this._env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ICustomTokenManager, JwtTokenManager>();
            services.AddSingleton<ICustomUserManager, CustomUserManager>();

            if (_env.IsDevelopment())
            {
                services.AddDbContext<BugsContext>(options =>
                {
                    options.UseInMemoryDatabase("Bugs");
                });
            }

            services.AddControllers();

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://localhost:5001";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("WebApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "webapi");
                });

                options.AddPolicy("WebApiWriteScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "write");
                });
            });

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                //options.ApiVersionReader = new HeaderApiVersionReader("X-API-Version");
            });

            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");

            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "My Web API v1", Version = "version 1" });
                options.SwaggerDoc("v2", new OpenApiInfo { Title = "My Web API v2", Version = "version 2" });
            });

            services.AddCors(options => {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("https://localhost:44393")
                    .AllowAnyHeader()
                    .AllowAnyMethod();                    
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, BugsContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                //Create the in-memory database for dev environment
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                //Configure OpenAPI
                app.UseSwagger();
                app.UseSwaggerUI(
                        options => {
                            options.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1");
                            options.SwaggerEndpoint("/swagger/v2/swagger.json", "WebAPI v2");
                        });
            }

            app.UseRouting();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
