using Cardapio.Application.DTOs;
using Cardapio.DB.Entiites;

namespace Cardapio.Application.Services.Interface
{
    public interface IMenuItemService:IBaseService<AddMenuItemDTO,ReadMenuItemDTO,MenuItem>
    {
    }
}
