using CQ.Utility;
using Microsoft.Extensions.Configuration;

namespace CQ.Extensions.Configuration;
public static class ConfigurationExtensions
{
    public static TSectionSchema GetSection<TSectionSchema>(
    this IConfiguration configuration,
    string sectionName)
    where TSectionSchema : class
    {
        var sectionSchema = configuration
            .GetSection(sectionName)
            .Get<TSectionSchema>();

        Guard.ThrowIsNull(sectionSchema, sectionName);

        return sectionSchema;
    }
}
