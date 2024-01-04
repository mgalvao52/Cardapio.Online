using AutoMapper;
using Cardapio.Application.DTOs;
using Cardapio.Application.Services.Interface;
using Cardapio.DB.Entiites;
using Cardapio.DB.Notifiers;
using Cardapio.DB.Notifiers.Interface;
using Cardapio.DB.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Cardapio.Application.Services
{
    public abstract class BaseService<Add,Read,U> : IBaseService<Add,Read,U> where Add : BaseModelDTO where U : BaseEntity where Read : BaseModelDTO
    {
        private readonly IBaseRepository<U> _repository;
        private readonly IMapper _mapper;

        public BaseService(IBaseRepository<U> repository, IMapper mapper)
        {
            _repository = repository;  
            _mapper = mapper;
            responseMessage = new ResponseMessage();
        }

        public IResponseMessage responseMessage { get; }

        public virtual async Task<int> CreateAsync(Add entity)
        {
            int result = 0;
            if (!Validate(entity)) 
            {
                return result;
            }
            var temp = _mapper.Map<U>(entity);
            result = await _repository.CreateAsync(temp);
            if (!_repository.responseMessage.IsValid) 
            {
                foreach (var item in _repository.responseMessage.Erros)
                {
                    responseMessage.AddErros(item);
                }
            }
            return result;
        }


        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public virtual async Task<IEnumerable<Read>> GetAllAsync(Expression<Func<U, bool>> predicate)
        {
            var temp = await _repository.GetAllAsync(predicate);
            return _mapper.Map<IEnumerable<Read>>(temp);
        }

        public async Task<IEnumerable<Read>> GetAllAsync()
        {
            var temp = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<Read>>(temp);
        }

        public virtual async Task<Read> GetAsync(Expression<Func<U, bool>> predicate)
        {
            var temp = await _repository.GetAsync(predicate);
            if (temp != null)
            {
                return _mapper.Map<Read>(temp);  

            }
            return null;
        }

        public virtual async Task UpdateAsync(Add entity)
        {
            if (!Validate(entity))
            {
                return;
            }
            var temp = _mapper.Map<U>(entity);
            await _repository.UpdateAsync(temp);
            if (!_repository.responseMessage.IsValid)
            {
                foreach (var item in _repository.responseMessage.Erros)
                {
                    responseMessage.AddErros(item);
                }
            }
        }
        public abstract bool Validate(Add entity);
    }
}
