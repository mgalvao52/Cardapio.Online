namespace Cardapio.DB.Entiites
{
    public class Payment:BaseEntity
    {
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public virtual Order Order { get; set; }
        public int OrderId { get; set; }
        public decimal Total { get; set; }

    }
}
