using Cardapio.DB.Entiites;
using System.Linq.Expressions;

namespace Cardapio.DB.Repositories.Interfaces
{
    public interface IOrderRepository:IBaseRepository<Order>
    {
        Task<Order> GetWithItemsAsync(Expression<Func<Order, bool>> expression);    
    }
}
