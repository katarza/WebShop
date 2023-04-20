using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Contracts.Persistence;

namespace WebShop.Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly WebShopDbContext _dbContext;

        public UnitOfWork(WebShopDbContext dbContext) => _dbContext = dbContext;

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        { 
        
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
