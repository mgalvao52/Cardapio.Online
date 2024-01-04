using Cardapio.DB.Entiites;
using Cardapio.DB.Notifiers.Interface;
using System.Linq.Expressions;

namespace Cardapio.DB.Repositories.Interfaces
{
    public interface IPaymentRepository
    {
        public IResponseMessage responseMessage { get; }
        Task CreateAsync(Payment payment);
        Task<IEnumerable<Payment>> GetAllAsync(Expression<Func<Payment, bool>> predicate);
        Task<Payment> GetAsync(Expression<Func<Payment, bool>> predicate);
        bool Validate(Payment entity);
    }
}
