using Cardapio.DB.Entiites;
using Cardapio.DB.Enums;
using Cardapio.DB.Notifiers.Interface;
using System.Linq.Expressions;

namespace Cardapio.DB.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        public IResponseMessage responseMessage { get;}
        Task<T> GetAsync(Expression<Func<T,bool>> predicate);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllAsync();
        Task<int> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        bool Validate(T entity, OperationType operationType);

    }
}
