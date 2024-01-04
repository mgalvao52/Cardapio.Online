using Cardapio.DB.Entiites;
using Cardapio.DB.Notifiers;
using Cardapio.DB.Notifiers.Interface;
using Cardapio.DB.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cardapio.DB.Repositories
{
    public class PaymentRepository:IPaymentRepository
    {
        private readonly CardapioContext _cardapioContext;
        public IResponseMessage responseMessage { get;}

        public PaymentRepository(CardapioContext cardapioContext)
        {
            this._cardapioContext = cardapioContext;
            responseMessage = new ResponseMessage();
        }

        public bool Validate(Payment payment)
        {
            var order = _cardapioContext.Order.FirstOrDefault(s => s.Id == payment.OrderId && s.Status == Enums.OrderStatus.Openned);
            if (order == null) { responseMessage.AddErros("Pedido não encontrado"); }
            else
            {
                var totalItems = _cardapioContext.OrderItem.Where(s => s.OrderId == payment.OrderId).ToList();
                if (totalItems.Count < 0) { responseMessage.AddErros("Nenhum item encontrado para o pedido"); }
                else
                {
                    if (totalItems.Any(s=>s.Status != Enums.OrderItemStatus.Ready))
                    {
                        responseMessage.AddErros("Existe item de pedido pendente");
                    }
                    decimal total = totalItems.Sum(s => s.Amount * s.Price);
                    if (payment.Total != total)
                    {
                        responseMessage.AddErros("Valor total incorreto");
                    }
                }
            }
            return responseMessage.IsValid;
        }
        public async Task CreateAsync(Payment payment)
        {
            try
            {
                if (Validate(payment))
                {
                    var order = await _cardapioContext.Order.FirstOrDefaultAsync(s => s.Id == payment.OrderId);
                    order.Status = Enums.OrderStatus.Closed;

                    await _cardapioContext.Payment.AddAsync(payment);
                    _cardapioContext.Order.Update(order);
                    _cardapioContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Payment>> GetAllAsync(Expression<Func<Payment, bool>> predicate)
        {
            return await _cardapioContext.Payment.Include(s=>s.Order)
                .ThenInclude(s=>s.OrderItems).Where(predicate).ToListAsync();
        }

        public async Task<Payment> GetAsync(Expression<Func<Payment, bool>> predicate)
        {
            return await _cardapioContext.Payment.Include(s => s.Order)
                .ThenInclude(s => s.OrderItems).FirstOrDefaultAsync(predicate);
        }

    }
}
