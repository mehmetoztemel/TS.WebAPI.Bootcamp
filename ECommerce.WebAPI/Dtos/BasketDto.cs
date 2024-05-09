namespace ECommerce.WebAPI.Dtos
{
    public record BasketDto(Guid ProductId, int Quantity, decimal Price)
    {
    }
    public record BasketResult(List<BasketDto> Products, decimal totalPrice)
    {

    }
}
