using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
