namespace ECommerce.WebAPI.Dtos
{
    public record ProductUpdateDto(Guid Id, string Name, string Description, decimal Price)
    {
    }
}
