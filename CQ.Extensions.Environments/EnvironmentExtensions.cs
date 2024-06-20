using CQ.Utility;
using Microsoft.Extensions.Hosting;

namespace CQ.Extensions.Environments;
public static class EnvironmentExtensions
{
    public static string Get()
    {
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        return Guard.IsNotNullOrEmpty(environmentName)
            ? environmentName
            : "Local";
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
