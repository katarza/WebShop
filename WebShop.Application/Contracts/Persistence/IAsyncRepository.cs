namespace WebShop.Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<IReadOnlyList<T>> GetPagedReponseAsync(int page, int size);
    }

}
