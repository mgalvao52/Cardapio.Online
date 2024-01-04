using Microsoft.AspNetCore.Identity;

namespace Cardapio.DB.Entiites
{
    public class UserEntity:IdentityUser<string>
    {
        public int TableNumber { get; set; }
    }
}
