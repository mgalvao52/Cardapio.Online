using Cardapio.Application.DTOs;
using Cardapio.DB.Notifiers.Interface;
using System.Linq.Expressions;

namespace Cardapio.Application.Services.Interface
{
    public interface IBaseService<Add,Read,U> where Add : BaseModelDTO where U : class where Read : BaseModelDTO
    {
        public IResponseMessage responseMessage { get;}
        Task<int> CreateAsync(Add entity);
        Task UpdateAsync(Add entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<Read>> GetAllAsync(Expression<Func<U,bool>> predicate);
        Task<Read> GetAsync(Expression<Func<U,bool>> predicate);
        Task<IEnumerable<Read>> GetAllAsync();
        bool Validate(Add entity);
    }
}
