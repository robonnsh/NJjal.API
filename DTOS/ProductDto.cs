namespace Njal_back.DTOS
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string DesignerName { get; set; }

        public string ProductName { get; set; }

        public int Price { get; set; }
    }
}
