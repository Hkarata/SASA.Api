namespace SASA.Api.DTOs
{
    public record struct StockProductCreateDto
        (
            string Name,
            string Description,
            int Amount,
            string unit,
            long Price
        );
    
}
