using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chi_ExpenseTracker_Repesitory.Infrastructure.Cors
{
    public static class CorsModuleRegister
    {
        private const string _developOrigins = "_DevelopOrigins";

        /// <summary>
        /// 加入Cors服務
        /// </summary>
        /// <param name="services"></param>
        public static void AddCorsPolicy(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options => 
            {
                options.AddPolicy(name: _developOrigins,
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });
        }

        /// <summary>
        /// 使用Cors
        /// </summary>
        /// <param name="webApplication"></param>
        public static void AddCorsConfigure(this WebApplication webApplication)
        {
            //看是否有需要限制環境變數
            //if (webApplication.Environment.IsDevelopment())
            //{
            //    webApplication.UseCors(_developOrigins);
            //}

            webApplication.UseCors(_developOrigins);
        }
    }
}
