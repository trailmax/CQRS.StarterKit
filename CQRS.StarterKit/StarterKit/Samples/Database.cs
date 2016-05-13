using System;
using System.Collections.Generic;

namespace StarterKit.Samples
{
    public class Product
    {
        public Product()
        {
            ProductId = Guid.NewGuid();
            IsActive = true;
        }
        public Guid ProductId { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public bool IsActive { get; set; }
    }

    public class Order
    {
        public Order()
        {
            OrderId = Guid.NewGuid();
        }
        public Guid OrderId { get; set; }
        public String OrderNumber { get; set; }
        public List<Product> Products { get; set; }
    }


    /// <summary>
    /// Stub of DbContext - for example only
    /// </summary>
    public class Database
    {
        public Database()
        {
            Products = new List<Product>()
            {
                new Product() { Name = "Chocolate cake", Description = "Cake full of chocolate!" },
                new Product() { Name = "Lemon Cake", Description = "Unknown", IsActive = false, },
            };
            Orders = new List<Order>();
        }

        public List<Product> Products { get; }

        public List<Order> Orders { get;  }
    }
}
