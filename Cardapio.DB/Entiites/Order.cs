using Cardapio.DB.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cardapio.DB.Entiites
{
    public class Order:BaseEntity
    {
        public int Number { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }

        [EnumDataType(typeof(OrderStatus)), DefaultValue(OrderStatus.Openned)]
        public OrderStatus Status { get; set; } = OrderStatus.Openned;
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}
