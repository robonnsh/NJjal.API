namespace Njal_back.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string DesignerName { get; set; }

        public string ProductName { get; set; }

        public int Price { get; set; }
    }
}
