namespace SASA.Api.DTOs
{
    public record struct StockCreateDto
        (
            DateTime Date,
            long Price,
            long TransportFee,
            List<StockProductCreateDto> Products
        );

}
