namespace SASA.Api.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public string Unit { get; set; }
        public long Price { get; set; }
    }
}
