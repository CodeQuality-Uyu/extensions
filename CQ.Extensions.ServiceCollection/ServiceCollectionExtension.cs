using CQ.Utility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Principal;

namespace CQ.Extensions.ServiceCollection;
public static class ServiceCollectionExtension
{
    public static IServiceCollection AddService<TService, TImplementation>(
        this IServiceCollection services,
        LifeTime lifeTime)
        where TService : class
        where TImplementation : class, TService
    {
        switch (lifeTime)
        {
            case LifeTime.Scoped:
                {
                    services.AddScoped<TService, TImplementation>();
                    break;
                }

            case LifeTime.Transient:
                {
                    services.AddTransient<TService, TImplementation>();
                    break;
                }
            case LifeTime.Singleton:
                {
                    services.AddSingleton<TService, TImplementation>();
                    break;
                }
        }

        return services;
    }

    public static IServiceCollection AddService(
        this IServiceCollection services,
        Type service,
        Type implementation,
        LifeTime lifeTime)
    {
        switch (lifeTime)
        {
            case LifeTime.Scoped:
                {
                    services.AddScoped(service, implementation);
                    break;
                }

            case LifeTime.Transient:
                {
                    services.AddTransient(service, implementation);
                    break;
                }
            case LifeTime.Singleton:
                {
                    services.AddSingleton(service, implementation);
                    break;
                }
        }

        return services;
    }

    public static IServiceCollection AddService<TService>(
        this IServiceCollection services,
        LifeTime lifeTime)
        where TService : class
    {
        switch (lifeTime)
        {
            case LifeTime.Scoped:
                {
                    services.AddScoped<TService>();
                    break;
                }

            case LifeTime.Transient:
                {
                    services.AddTransient<TService>();
                    break;
                }
            case LifeTime.Singleton:
                {
                    services.AddSingleton<TService>();
                    break;
                }
        }

        return services;
    }

    public static IServiceCollection AddService<TService, TImplementation>(
        this IServiceCollection services,
        Func<IServiceProvider, TImplementation> implementationFactory,
        LifeTime lifeTime)
        where TService : class
        where TImplementation : class, TService
    {
        switch (lifeTime)
        {
            case LifeTime.Scoped:
                {
                    services.AddScoped<TService, TImplementation>(implementationFactory);
                    break;
                }

            case LifeTime.Transient:
                {
                    services.AddTransient<TService, TImplementation>(implementationFactory);
                    break;
                }
            case LifeTime.Singleton:
                {
                    services.AddSingleton<TService, TImplementation>(implementationFactory);
                    break;
                }
        }

        return services;
    }

    public static IServiceCollection AddService<TService, TImplementation>(
        this IServiceCollection services,
        TImplementation implementation,
        LifeTime lifeTime)
        where TService : class
        where TImplementation : class, TService
    {
        switch (lifeTime)
        {
            case LifeTime.Scoped:
                {
                    services.AddScoped<TService, TImplementation>((serviceProvider) => implementation);
                    break;
                }

            case LifeTime.Transient:
                {
                    services.AddTransient<TService, TImplementation>((serviceProvider) => implementation);
                    break;
                }
            case LifeTime.Singleton:
                {
                    services.AddSingleton<TService, TImplementation>((serviceProvider) => implementation);
                    break;
                }
        }

        return services;
    }

    public static IServiceCollection AddService<TService>(
        this IServiceCollection services,
        Func<IServiceProvider, TService> implementationFactory,
        LifeTime lifeTime)
        where TService : class
    {
        switch (lifeTime)
        {
            case LifeTime.Scoped:
                {
                    services.AddScoped(implementationFactory);
                    break;
                }

            case LifeTime.Transient:
                {
                    services.AddTransient(implementationFactory);
                    break;
                }
            case LifeTime.Singleton:
                {
                    services.AddSingleton(implementationFactory);
                    break;
                }
        }

        return services;
    }

    public static IServiceCollection AddService<TService>(
        this IServiceCollection services,
        TService service,
        LifeTime lifeTime)
        where TService : class
    {
        switch (lifeTime)
        {
            case LifeTime.Scoped:
                {
                    services.AddScoped((serviceProvider) => service);
                    break;
                }

            case LifeTime.Transient:
                {
                    services.AddTransient((serviceProvider) => service);
                    break;
                }
            case LifeTime.Singleton:
                {
                    services.AddSingleton((serviceProvider) => service);
                    break;
                }
        }

        return services;
    }

    public static IServiceCollection AddFakeAuthentication<TPrincipal>(
        this IServiceCollection services,
        IConfiguration configuration,
        string fakeAuthenticationIsActiveKey = "Authentication:Fake:IsActive",
        string fakeAuthenticationKey = "Authentication:Fake",
        LifeTime fakeAuthenticationLifeTime = LifeTime.Scoped)
        where TPrincipal : IPrincipal
    {
        var isFakeAccountActive = Convert.ToBoolean(configuration[fakeAuthenticationIsActiveKey]);

        if (!isFakeAccountActive)
        {
            return services;
        }

        var fakeAuthentication = configuration
            .GetSection(fakeAuthenticationKey)
            .Get<TPrincipal>();

        services.AddService<IPrincipal>(fakeAuthentication, fakeAuthenticationLifeTime);

        return services;
    }
}
