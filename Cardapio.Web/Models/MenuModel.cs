using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cardapio.Web.Models
{
    public class MenuModel
    {
        public int? Id { get; set; }
        [DisplayName("Nome")]
        public string Name { get; set; }
        [DisplayName("Descrição")]
        public string Description { get; set; }
        [DisplayName("Preço")]
        public decimal Price { get; set; }
        [NotMapped]
        public string Image { get; set; }
        [DisplayName("Imagem")]
        public IFormFile ImageFile { get; set; }
        [DisplayName("Tempo de preparo")]
        public int Time { get; set; }
    }
}
