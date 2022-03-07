using LivingLab.Core.Entities.Identity;

namespace LivingLab.Core.Entities;

public class EmailLog : BaseEntity
{
    public string Message { get; set; }
    public string Subject { get; set; }
    public string Status { get; set; }
    public DateTime LoggedDate { get; set; }
    public List<ApplicationUser> Users { get; set; }
}
