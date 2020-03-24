using System.Collections.Generic;

namespace StoreApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        // public List<Product> Products { get; } = new List<Product>();
    }
}