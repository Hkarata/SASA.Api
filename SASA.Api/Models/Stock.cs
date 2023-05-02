namespace SASA.Api.Models
{
    public class Stock
    {
        public int Id { get; set; }    
        public DateTime Date { get; set; }
        public List<StockProduct> StockProducts { get; set; }
        public long Price { get; set; }
        public long TransportFee { get; set; }
    }
}
