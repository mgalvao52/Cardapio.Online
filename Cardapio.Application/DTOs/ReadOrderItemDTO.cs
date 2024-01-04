using Cardapio.DB.Enums;
using System.ComponentModel;

namespace Cardapio.Application.DTOs
{
    public class ReadOrderItemDTO:BaseModelDTO
    {
        [DisplayName("Quantidade")]
        public int Amount { get; set; }
        [DisplayName("Preço")]
        public decimal Price { get; set; }
        [DisplayName("Numero do pedido")]
        public int OrderId { get; set; }
        [DisplayName("Item do menu")]
        public int MenuItemId { get; set; }
        [DisplayName("Nome do item")]
        public string MenuName { get; set; }
        [DisplayName("Descrição do item")]
        public string MenuDescription { get; set; }
        [DisplayName("Tempo de preparo")]
        public int Time { get; set; }
        public OrderItemStatus Status { get; set; }
    }
}
