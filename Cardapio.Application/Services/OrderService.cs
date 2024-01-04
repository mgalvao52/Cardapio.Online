using AutoMapper;
using Cardapio.Application.DTOs;
using Cardapio.Application.Services.Interface;
using Cardapio.Application.Validators;
using Cardapio.DB.Entiites;
using Cardapio.DB.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Cardapio.Application.Services
{
    public class OrderService : BaseService<AddOrderDTO,ReadOrderDTO, Order>,IOrderService
    {
        private readonly IOrderRepository repository;
        private readonly IMapper mapper;

        public OrderService(IOrderRepository repository, IMapper mapper) : base(repository, mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ReadOrderDTO> GetWithItemsAsync(Expression<Func<Order, bool>> expression)
        {
            var result = await repository.GetWithItemsAsync(expression);
            return mapper.Map<ReadOrderDTO>(result);
        }

        public override bool Validate(AddOrderDTO entity)
        {
            var result = new OrderValidator().Validate(entity);
            if (!result.IsValid)
            {
                foreach (var item in result.Errors)
                {
                    responseMessage.AddErros(item.ErrorMessage);
                }
                return false;
            }
            return true;
        }
    }
}
