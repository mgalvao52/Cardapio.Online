using System.ComponentModel;

namespace Cardapio.Application.DTOs
{
    public class AddOrderItemDTO:BaseModelDTO
    {
        [DisplayName("Quantidade")]
        public int Amount { get; set; }
        [DisplayName("Numero do pedido")]
        public int OrderId { get; set; }
        [DisplayName("Itens do Menu")]
        public int MenuItemId { get; set; }
    }
}
