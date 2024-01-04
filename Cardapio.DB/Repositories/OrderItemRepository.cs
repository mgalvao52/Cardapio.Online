using Cardapio.DB.Entiites;
using Cardapio.DB.Enums;
using Cardapio.DB.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cardapio.DB.Repositories
{
    public class OrderItemRepository : BaseRepository<OrderItem>, IOrderItemRepository
    {
        private readonly CardapioContext context;

        public OrderItemRepository(CardapioContext context) : base(context)
        {
            this.context = context;
        }

        public override bool Validate(OrderItem entity, OperationType operationType)
        {
            var orderTemp = context.Order.FirstOrDefault(s => s.Id == entity.OrderId);
            var menuTemp = context.MenuItem.FirstOrDefault(s => s.Id == entity.MenuItemId);

            if (orderTemp == null)
            {
                responseMessage.AddErros("Pedido não encontrado");
            }
            if (menuTemp == null)
            {
                responseMessage.AddErros("Item de menu não encontrado");
            }
            return responseMessage.IsValid;
        }

        public override async Task<int> CreateAsync(OrderItem entity)
        {
            var menuItem = await context.MenuItem.FirstOrDefaultAsync(s=>s.Id == entity.MenuItemId);
            entity.Price = menuItem.Price;
            return await base.CreateAsync(entity);
        }

        public async Task UpdateStatusAsync(int id, OrderItemStatus orderItemStatus)
        {
            var item = await context.OrderItem.FirstOrDefaultAsync(s => s.Id == id);
            if (item != null)
            {
                item.Status = orderItemStatus;
                await UpdateAsync(item);
            }
            responseMessage.AddErros("Item do pedido não encontrado");
        }

        public async Task<OrderItem> GetByIdAsync(int id)
        {
            return await context.OrderItem.Include(s => s.MenuItem).FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
