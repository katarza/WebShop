using Microsoft.EntityFrameworkCore;
using WebShop.Application.Contracts.Persistence;

namespace WebShop.Persistence.Repository
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly WebShopDbContext _dbContext;

        public BaseRepository(WebShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            T? t = await _dbContext.Set<T>().FindAsync(id);
            return t;
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async virtual Task<IReadOnlyList<T>> GetPagedReponseAsync(int page, int size)
        {
            return await _dbContext.Set<T>().Skip((page - 1) * size).Take(size).AsNoTracking().ToListAsync();
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);

            return entity;
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return;
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);

            return;
             
        }
    }
}
