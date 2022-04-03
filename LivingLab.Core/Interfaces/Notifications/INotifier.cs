using LivingLab.Core.Entities.Identity;

namespace LivingLab.Core.Interfaces.Notifications;

/// <remarks>
/// Author: Team P1-1
/// </remarks>
public interface INotifier
{
    Task Notify(string message);
}
