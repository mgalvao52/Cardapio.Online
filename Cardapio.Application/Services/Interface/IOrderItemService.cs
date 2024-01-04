using Cardapio.Application.DTOs;
using Cardapio.DB.Entiites;
using Cardapio.DB.Enums;

namespace Cardapio.Application.Services.Interface
{
    public interface IOrderItemService:IBaseService<AddOrderItemDTO,ReadOrderItemDTO,OrderItem>
    {
        Task UpdateStatusAsync(int id, OrderItemStatus orderItemStatus);
        Task<ReadOrderItemDTO> GetByIdAsync(int id);
    }
}
