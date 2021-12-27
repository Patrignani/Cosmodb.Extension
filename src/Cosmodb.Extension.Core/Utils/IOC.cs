using Cosmodb.Extension.Core.Enums;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Cosmodb.Extension.Core.Utils
{
    public static class IOC
    {
        public static IServiceCollection AddIoc(this IServiceCollection services, RegisterServiceType registerServiceType, Type serviceType, Type implemetationType)
        {
            switch (registerServiceType)
            {
                case RegisterServiceType.Scoped:
                    services.AddScoped(serviceType, implemetationType);
                    break;
                case RegisterServiceType.Singleton:
                    services.AddSingleton(serviceType, implemetationType);
                    break;
                case RegisterServiceType.Transient:
                    services.AddTransient(serviceType, implemetationType);
                    break;
            }

            return services;
        }

        public static IServiceCollection AddIoc<Service>(this IServiceCollection services, RegisterServiceType registerServiceType, Func<IServiceProvider, Service> implementationFactory) where Service : class
        {
            switch (registerServiceType)
            {
                case RegisterServiceType.Scoped:
                    services.AddScoped(implementationFactory);
                    break;
                case RegisterServiceType.Singleton:
                    services.AddSingleton(implementationFactory);
                    break;
                case RegisterServiceType.Transient:
                    services.AddTransient(implementationFactory);
                    break;
            }

            return services;
        }
    }
}
