using System.Diagnostics.CodeAnalysis;

namespace WebApplication2Test.Models
{
    public class Product
    {
        [NotNull]
        public int Id { get; set; }

        [NotNull]
        public string Name { get; set; }

        public string Description { get; set; }

        [NotNull]
        public decimal Price { get; set; }

        public int Stock { get; set; }
    }
}
