using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;

namespace Chi_ExpenseTracker_Repesitory.Configuration
{
    public static class ConfigurationModuleRegister
    {
        /// <summary>
        /// 加入Configuration設定
        /// </summary>
        /// <param name="builder"></param>
        public static void AddConfiguration(this WebApplicationBuilder builder)
        {
            var configRoot = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddJsonFile($"/dotnet/appsettings/wmsapi-5001/appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            builder.Configuration.AddConfiguration(configRoot);

            new AppSettings(builder.Configuration);

        }
    }
}
