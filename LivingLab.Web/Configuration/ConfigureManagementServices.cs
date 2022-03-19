using LivingLab.Web.UIServices.Accessory;
using LivingLab.Web.UIServices.Device;
using LivingLab.Web.UIServices.ManualLogs;

namespace LivingLab.Web.Configuration;

/// <summary>
/// Team P1-3 & P1-5 to add dependency injections for mod 1 here.
/// </summary>
public static class ConfigureManagementServices
{
    public static IServiceCollection AddManagementServices(this IServiceCollection services)
    {
        AddWebTransientServices(services);
        AddWebScopedServices(services);
        AddWebSingletonServices(services);
        return services;
    }

    private static IServiceCollection AddWebTransientServices(this IServiceCollection services)
    {
        services.AddTransient<IManualLogService, ManualLogService>();
        services.AddTransient<IDeviceService, DeviceService>();
        services.AddTransient<IAccessoryService, AccessoryServices>();

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
