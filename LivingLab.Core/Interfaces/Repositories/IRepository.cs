namespace LivingLab.Core.Interfaces.Repositories;

/// <remarks>
/// Author: Team P1-1
/// </remarks>
public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(int id);
}
