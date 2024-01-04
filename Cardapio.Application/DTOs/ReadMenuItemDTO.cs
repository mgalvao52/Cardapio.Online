using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Cardapio.Application.DTOs
{
    public class ReadMenuItemDTO:BaseModelDTO
    {
        [DisplayName("Nome")]
        public string Name { get; set; }
        [DisplayName("Descrição")]
        public string Description { get; set; }
        [DisplayName("Preço")]
        public decimal Price { get; set; }
        public string Image { get; set; } = "-";
        [DisplayName("Tempo de preparo(min)")]
        public int Time { get; set; }

        [DisplayName("Imagem")]
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
