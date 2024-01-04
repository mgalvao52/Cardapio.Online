using System.ComponentModel;

namespace Cardapio.Application.DTOs
{
    public class AddOrderDTO:BaseModelDTO
    {
        public AddOrderDTO()
        {
            OrderItems = new List<AddOrderItemDTO>();
        }
        [DisplayName("Numero da Mesa")]
        public int Number { get; set; }
        [DisplayName("Item do pedido")]
        public IEnumerable<AddOrderItemDTO>? OrderItems { get; set; }
    }
}
