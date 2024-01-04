using System.ComponentModel;

namespace Cardapio.Application.DTOs
{
    public class ReadPaymentDTO:BaseModelDTO
    {
        [DisplayName("Data do registro")]
        public DateTime RegistrationDate { get; set; }
        [DisplayName("Pedido")]
        public ReadOrderDTO Order { get; set; }
        [DisplayName("Valor total")]
        public decimal? Total { get { return Order?.OrderItems?.Sum(s => s.Amount * s.Price); } }
    }
}
