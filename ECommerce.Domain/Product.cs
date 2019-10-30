using System;

namespace ECommerce.Domain
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int Availbility { get; set; }

    }
}
