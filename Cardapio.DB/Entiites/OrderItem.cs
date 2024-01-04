using Cardapio.DB.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cardapio.DB.Entiites
{
    public class OrderItem:BaseEntity
    {
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Amount { get; set; }
        public int OrderId { get; set; }
        public int MenuItemId { get; set; }
        [EnumDataType(typeof(OrderItemStatus)), DefaultValue(OrderItemStatus.Pending)]
        public OrderItemStatus Status { get; set; } = OrderItemStatus.Pending;
        public virtual Order Order { get; set; }
        public virtual MenuItem MenuItem { get; set; }
      
    }
}
