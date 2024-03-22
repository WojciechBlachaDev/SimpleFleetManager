namespace SimpleFleetManager.Services.Interfaces
{
    public interface ILogsServices<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetByForkliftId(int forkliftId);
        Task<T> Get(int id);
        Task<T> Create(T entity);
        Task<T> Update(int id, T entity);
        Task<bool> Delete(int id);
    }
}
