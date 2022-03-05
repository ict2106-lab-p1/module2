namespace LivingLab.Core.Interfaces.Repositories;

public interface IAccessoryRepository : IRepository<Accessory>
{
    Task<List<Accessory>> GetAccessoryWithAccessoryType();
}
