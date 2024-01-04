using System.ComponentModel;

namespace Cardapio.Application.DTOs
{
    public class AddPaymentDTO:BaseModelDTO
    {
        [DisplayName("Numero do pedido")]
        public int OrderId { get; set; }
        [DisplayName("Valor total")]
        public decimal Total { get; set; }
    }
}
