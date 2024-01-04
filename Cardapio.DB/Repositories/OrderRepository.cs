using Cardapio.DB.Entiites;
using Cardapio.DB.Enums;
using Cardapio.DB.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cardapio.DB.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly CardapioContext context;

        public OrderRepository(CardapioContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<bool> CloseOrderAsync(int number)
        {
            
            var tempOrder = await GetAsync(s => s.Number == number && s.Status == OrderStatus.Openned);
            if (tempOrder != null)
            {
                tempOrder.Status = OrderStatus.Closed;
                await UpdateAsync(tempOrder);
            }
            else
            {
                responseMessage.AddErros($"Pedido da mesa {number} não encontrado");
            }
            return responseMessage.IsValid;
        }

        public async Task<Order> GetWithItemsAsync(Expression<Func<Order, bool>> expression)
        {
           return await context.Order.Include(s=>s.OrderItems).ThenInclude(s=>s.MenuItem).FirstOrDefaultAsync(expression);
        }

        public override bool Validate(Order entity, OperationType operationType)
        {
            var temp = context.Order.FirstOrDefault(s => s.Number == entity.Number && s.Status == OrderStatus.Openned);
            if (operationType == OperationType.Create)
            {
                if (temp != null) { responseMessage.AddErros("Ja existe uma ordem aberta para essa mesa"); }                
            }
            return responseMessage.IsValid;
        }

    }
}
