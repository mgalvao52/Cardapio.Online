using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cardapio.DB.Entiites
{
    public class MenuItem:BaseEntity
    {
        
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(300)]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public  string Image { get; set; }
        public int Time { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}
