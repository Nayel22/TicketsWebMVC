namespace TicketsWebMVC.Services
{
    // Services/IApiService.cs
    public interface IApiService<T>
    {
        Task<IEnumerable<T>> GetAllAsync(string endpoint);
        Task<T> GetByIdAsync(string endpoint, int id);
        Task<T> CreateAsync(string endpoint, T item);
        Task UpdateAsync(string endpoint, int id, T item);
        Task DeleteAsync(string endpoint, int id);
    }
}
