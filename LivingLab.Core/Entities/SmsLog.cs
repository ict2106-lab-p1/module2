using System.ComponentModel.DataAnnotations;

using LivingLab.Core.Entities.Identity;
using LivingLab.Core.Enums;

namespace LivingLab.Core.Entities;
public class SmsLog: BaseEntity
{
    public string? Message { get; set; }
    public MessageStatus Status { get; set; }
    public DateTime LoggedDate { get; set; }
    public List<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
}