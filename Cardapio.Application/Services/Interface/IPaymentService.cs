using Cardapio.Application.DTOs;
using Cardapio.DB.Entiites;
using Cardapio.DB.Notifiers.Interface;
using System.Linq.Expressions;

namespace Cardapio.Application.Services.Interface
{
    public interface IPaymentService
    {
        public IResponseMessage responseMessage { get; }
        Task CreateAsync(AddPaymentDTO entity);
        Task<IEnumerable<ReadPaymentDTO>> GetAllAsync(Expression<Func<Payment, bool>> predicate);
        Task<ReadPaymentDTO> GetAsync(Expression<Func<Payment, bool>> predicate);
        bool Validate(AddPaymentDTO entity);
    }
}
