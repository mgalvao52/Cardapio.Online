using Cardapio.Application.DTOs;
using Cardapio.DB.Entiites;
using System.Linq.Expressions;

namespace Cardapio.Application.Services.Interface
{
    public interface IOrderService:IBaseService<AddOrderDTO,ReadOrderDTO,Order>
    {
        Task<ReadOrderDTO> GetWithItemsAsync(Expression<Func<Order, bool>> expression);
    }
}
