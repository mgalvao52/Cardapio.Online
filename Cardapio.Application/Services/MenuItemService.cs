using AutoMapper;
using Cardapio.Application.DTOs;
using Cardapio.Application.Services.Interface;
using Cardapio.Application.Validators;
using Cardapio.DB.Entiites;
using Cardapio.DB.Repositories.Interfaces;

namespace Cardapio.Application.Services
{
    public class MenuItemService : BaseService<AddMenuItemDTO,ReadMenuItemDTO, MenuItem>,IMenuItemService
    {
        public MenuItemService(IMenuItemRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public override bool Validate(AddMenuItemDTO entity)
        {
            var result = new MenuItemValidator().Validate(entity);
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
