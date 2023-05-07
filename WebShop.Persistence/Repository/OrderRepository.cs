using WebShop.Application.Contracts.Persistence;
using WebShop.Domain.Entities;

namespace WebShop.Persistence.Repository
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(WebShopDbContext dbContext) : base(dbContext)
        {
        }
    }
}
