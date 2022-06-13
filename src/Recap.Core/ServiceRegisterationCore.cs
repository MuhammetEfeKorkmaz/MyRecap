
using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Recap.Core.Aspects.Base;
using Recap.Core.Aspects.Caching;
using Recap.Core.Aspects.Caching.Factories;
using Recap.Core.Extensions.PacketCustomException;
using Recap.Core.Utilities.Email;
using Recap.Core.Utilities.Email.Factories;
using Recap.Core.Utilities.ErrorLogger;
using Recap.Core.Utilities.ErrorLogger.Factories;
using System.Diagnostics;

namespace Recap.Core
{
    public class ServiceRegisterationCore : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new InterceptorCollection()
                }).SingleInstance();

        }

    }





    public static class CustomExceptionInit
    {
        public static void UseCustomExceptionExtension(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionExtension>();
        }
    }






    public interface IServiceRegisterationMicrosoftDEPI
    {
        void Load(IServiceCollection collection, string configurationEmail, string configurationErrorLogger);
    }
    public class ServiceRegisterationMicrosoftDEPI : IServiceRegisterationMicrosoftDEPI
    {
        public void Load(IServiceCollection services, string configurationEmail, string configurationErrorLogger)
        {
            services.AddMemoryCache(); // Cacheleme mekanizması olarak Microsoft ayarlandı.
            services.AddSingleton<ICacheManager, MicrosoftMemoryCacheFactory>(); // Cacheleme mekanizması olarak Microsoft ayarlandı.

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IEmailManager>(provider => new SmptClientFactory(configurationEmail));

            services.AddSingleton<IErrorLoggerDBManager>(provider => new MsSqlServerFactory(configurationErrorLogger));
            services.AddSingleton<IErrorLoggerLocalManager, NoteFactory>();

            services.AddSingleton<Stopwatch>();
        }

    }
    public static class ServiceRegisterationMicrosoftDEPIServiceTool
    {
        public static IServiceCollection AddDependencyResolversWithEmailErrorLogger(this IServiceCollection serviceCollection, IServiceRegisterationMicrosoftDEPI[] modules, string configurationEmail, string configurationErrorLogger)
        {
            foreach (var module in modules)
            {
                module.Load(serviceCollection, configurationEmail, configurationErrorLogger);
            }

            return ServiceRegisterationMicrosoftDEPIServiceTool.Create(serviceCollection);
        }




        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceCollection Create(IServiceCollection services)
        {
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }

}
