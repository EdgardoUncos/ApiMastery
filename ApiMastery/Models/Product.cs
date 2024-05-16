namespace ApiMastery.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int ProductCategoryId { get; set; } // establecemos la relacion entre productos y categorias

        public List<OrderDetail>? OrderDetails { get; set; }
    }
}
