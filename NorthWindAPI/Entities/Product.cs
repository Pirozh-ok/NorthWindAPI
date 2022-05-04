namespace NorthWindAPI.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal? UnitPrice { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
