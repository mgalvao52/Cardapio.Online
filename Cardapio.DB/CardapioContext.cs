using Cardapio.DB.Entiites;
using Microsoft.EntityFrameworkCore;

namespace Cardapio.DB
{
    public class CardapioContext : DbContext
    {
        public CardapioContext(DbContextOptions<CardapioContext> options):base(options)
        {
        }
        public DbSet<MenuItem>  MenuItem { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Payment> Payment { get; set; }
    }
}
