using Cardapio.DB.Entiites;
using Cardapio.DB.Enums;

namespace Cardapio.DB.Repositories.Interfaces
{
    public interface IOrderItemRepository:IBaseRepository<OrderItem>
    {
        Task UpdateStatusAsync(int id, OrderItemStatus orderItemStatus);
        Task<OrderItem> GetByIdAsync(int id);
    }
}
