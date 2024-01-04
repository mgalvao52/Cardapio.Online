using System.ComponentModel;

namespace Cardapio.Application.DTOs
{
    public class ReadOrderDTO:BaseModelDTO
    {
        [DisplayName("Numero da Mesa")]
        public int Number { get; set; }
        [DisplayName("Itens do pedido")]
        public IEnumerable<ReadOrderItemDTO> OrderItems { get; set; }
        [DisplayName("Total do pedido")]
        public decimal? Total { get { return OrderItems?.Sum(s => s.Amount * s.Price); } }
    }
}
