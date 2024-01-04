using AutoMapper;
using Cardapio.Application.DTOs;
using Cardapio.Application.Services.Interface;
using Cardapio.Application.Validators;
using Cardapio.DB.Entiites;
using Cardapio.DB.Enums;
using Cardapio.DB.Repositories.Interfaces;

namespace Cardapio.Application.Services
{
    public class OrderItemService : BaseService<AddOrderItemDTO,ReadOrderItemDTO, OrderItem>,IOrderItemService
    {
        private readonly IOrderItemRepository repository;
        private readonly IMapper mapper;

        public OrderItemService(IOrderItemRepository repository, IMapper mapper) : base(repository, mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public override bool Validate(AddOrderItemDTO entity)
        {
           var result = new OrderItemValidator().Validate(entity);
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

        public async Task UpdateStatusAsync(int id, OrderItemStatus orderItemStatus)
        {
           await repository.UpdateStatusAsync(id,orderItemStatus);
        }

        public async Task<ReadOrderItemDTO> GetByIdAsync(int id)
        {
            var result = await repository.GetByIdAsync(id);
            if (result != null)
            {
                return mapper.Map<ReadOrderItemDTO>(result);
            }
            return null;
        }
    }
}
