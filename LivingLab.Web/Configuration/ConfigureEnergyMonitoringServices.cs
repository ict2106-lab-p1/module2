using LivingLab.Web.UIServices.Accessory;
using LivingLab.Web.UIServices.Device;
using LivingLab.Web.UIServices.EnergyUsage;

namespace LivingLab.Web.Configuration;

public static class ConfigureEnergyMonitoringServices
{
    public static IServiceCollection AddEnergyMonitoringServices(this IServiceCollection services)
    {
        AddWebTransientServices(services);
        AddWebScopedServices(services);
        AddWebSingletonServices(services);
        return services;
    }

    private static IServiceCollection AddWebTransientServices(this IServiceCollection services)
    {
        services.AddTransient<IEnergyUsageAnalysisUIService, EnergyUsageAnalysisUIService>();
        services.AddTransient<IEnergyUsageComparisonService, EnergyUsageComparisonService>();
        services.AddTransient<IEnergyUsageService, EnergyUsageService>();

        return services;
    }

    private static IServiceCollection AddWebScopedServices(this IServiceCollection services)
    {
        return services;
    }
    private static IServiceCollection AddWebSingletonServices(this IServiceCollection services)
    {
        return services;
    }
}
