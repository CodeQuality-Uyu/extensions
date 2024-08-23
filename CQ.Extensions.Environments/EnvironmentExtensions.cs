using CQ.Utility;
using Microsoft.Extensions.Hosting;

namespace CQ.Extensions.Environments;

public static class EnvironmentExtensions
{
    private static string Get()
    {
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        return Guard.IsNotNullOrEmpty(environmentName)
            ? environmentName!
            : "Local";
    }

    public static bool IsProd()
    {
        var environmentName = Get();

        return Guard.Is(environmentName, "Production");
    }

    public static bool IsLocal()
    {
        var environmentName = Get();

        return Guard.Is(environmentName, "Local");
    }

    public static bool IsTesting()
    {
        var environmentName = Get();

        return Guard.Is(environmentName, "Testing");
    }

    public static bool IsDevelopment()
    {
        var environmentName = Get();

        return Guard.Is(environmentName, "Development");
    }

    public static bool IsLocal(this IHostEnvironment environment)
        => Guard.Is(environment.EnvironmentName, "Local");

    public static bool IsTesting(this IHostEnvironment environment)
        => Guard.Is(environment.EnvironmentName, "Testing");

    public static bool IsDev(this IHostEnvironment environment)
        => Guard.Is(environment.EnvironmentName, "Development");

    public static bool IsProd(this IHostEnvironment environment)
        => Guard.Is(environment.EnvironmentName, "Production");
}
