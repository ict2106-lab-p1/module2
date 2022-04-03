using LivingLab.Core.Entities.Identity;
using LivingLab.Core.Enums;
using LivingLab.Core.Interfaces.Services;

using Microsoft.Extensions.Logging;

namespace LivingLab.Core.DomainServices;

/// <remarks>
/// Author: Team P1-1
/// </remarks>
public class NotificationDomainService : INotificationDomainService
{
    private readonly IAccountDomainService _accountDomainService;
    private readonly ILogger<NotificationDomainService> _logger;
    public NotificationDomainService(IAccountDomainService accountDomainService, ILogger<NotificationDomainService> logger)
    {
        _accountDomainService = accountDomainService;
        _logger = logger;
    }
    public Task SetNotificationPref(ApplicationUser currentUser, NotificationType preference)
    {
        currentUser.PreferredNotification = preference;
        return _accountDomainService.UpdateUser(currentUser);
    }

    // method stub to make compilation work. See interface for explanation.
    public Task SetNotificationPref()
    {
        throw new NotImplementedException("This method is deprecated. Use the parameterized version instead.");
    }
}
