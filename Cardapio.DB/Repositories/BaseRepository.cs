using Cardapio.DB.Entiites;
using Cardapio.DB.Enums;
using Cardapio.DB.Notifiers;
using Cardapio.DB.Notifiers.Interface;
using Cardapio.DB.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cardapio.DB.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly CardapioContext _context;
        public IResponseMessage responseMessage { get; }
        public BaseRepository(CardapioContext context)
        {
            _context = context;
            responseMessage = new ResponseMessage();
        }
        public virtual async Task<int> CreateAsync(T entity)
        {
            try
            {
                if (Validate(entity, OperationType.Create))
                {
                    await _context.Set<T>().AddAsync(entity);
                    _context.SaveChanges();
                    return entity.Id;
                }
                return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public virtual async Task UpdateAsync(T entity)
        {
            try
            {
                if (Validate(entity, OperationType.Update))
                {
                    _context.Set<T>().Update(entity);
                    await _context.SaveChangesAsync();
                }
               
            }
            catch (Exception)
            {

                throw;
            }
        }

        public abstract bool Validate(T entity, OperationType operationType);

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public  virtual async Task DeleteAsync(int id)
        {
            try
            {
               var temp = await _context.Set<T>().FindAsync(id);
                if (temp != null)
                {
                    _context.Set<T>().Remove(temp);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
