using Cardapio.Application.DTOs;
using Cardapio.DB.Entiites;

namespace Cardapio.Application.Profiles
{
    public class OrderItemProfile:BaseProfile<AddOrderItemDTO,ReadOrderItemDTO,OrderItem>
    {
        public OrderItemProfile()
        {
            CreateMap<OrderItem,ReadOrderItemDTO>()
                .ForMember(s=>s.MenuName,s=>s.MapFrom(s=>s.MenuItem.Name))
                .ForMember(s=>s.MenuDescription,s=>s.MapFrom(s=>s.MenuItem.Description))
                .ForMember(s=>s.Time,s=>s.MapFrom(s=>s.MenuItem.Time));
        }
    }
}
