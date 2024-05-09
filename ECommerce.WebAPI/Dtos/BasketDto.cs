namespace ECommerce.WebAPI.Dtos
{
    public record BasketDto(Guid UserId, Guid ProductId,int Quantity, decimal Price)
    {
    }
}
