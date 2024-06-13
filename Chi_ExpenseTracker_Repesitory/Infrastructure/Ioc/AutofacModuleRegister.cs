using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Chi_ExpenseTracker_Repesitory.Infrastructure.Ioc
{
    public static class AutofacModuleRegister
    {
        /// <summary>
        /// 加入Autofac服務
        /// </summary>
        /// <param name="builder"></param>
        public static void AddAutofac(this WebApplicationBuilder builder)
        {
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(containerBuilder => 
            {
                // 批量註冊某個命名空間中的所有服務
                var serviceAssembly = new Assembly[] { Assembly.Load("Chi_ExpenseTracker_Service") };
                var repoAssembly = new Assembly[] { Assembly.Load("Chi_ExpenseTracker_Repesitory") };
                // 註冊Service
                containerBuilder.RegisterAssemblyTypes(serviceAssembly)
                    .Where(t => t.Name.EndsWith("Service"))
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();
                // 註冊Repository
                containerBuilder.RegisterAssemblyTypes(repoAssembly)
                    .Where(t => t.Name.EndsWith("Repository"))
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();
            });


        }
    }
}
