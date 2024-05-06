namespace TaskManager.WebAPI.Models;

public sealed class Personel
{
    public Personel()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    // Action Property. Code first de get set tanımlanmayan özellik dbye tanımlanmaz.
    public string FullName => string.Join(" ", FirstName, LastName);
    public string AvatarUrl { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
