using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Chi_ExpenseTracker_Repesitory.Infrastructure.Swagger
{
    public static class SwaggerModuleRegister
    {
        /// <summary>
        /// 加入Swagger服務
        /// </summary>
        /// <param name="builder"></param>
        public static void AddSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(options => 
            {
                options.AddSecurityDefinition("Bearer",
                        new OpenApiSecurityScheme
                        {
                            Name = "Authorization",
                            Type = SecuritySchemeType.ApiKey,
                            Scheme = "Bearer",
                            BearerFormat = "JWT",
                            In = ParameterLocation.Header,
                            Description = "JWT Authorization"
                        });

                options.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            Array.Empty<string>()
                        }
                    });
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "WeatherForecast API", Version = "v1" });
            });
        }
        /// <summary>
        /// 使用Swagger服務
        /// </summary>
        /// <param name="webApplication"></param>
        public static void AddSwaggerConfigure(this WebApplication webApplication)
        {
            webApplication.UseSwagger();
            webApplication.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });
        }
    }
}
