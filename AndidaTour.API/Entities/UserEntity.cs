using System.ComponentModel.DataAnnotations;

namespace AndidaTour.API.Entities;

public class UserEntity
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required, MaxLength(200)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navegação
    public ICollection<ClientEntity> Clients { get; set; } = new List<ClientEntity>();
    public ICollection<FlightQuoteEntity> Quotes { get; set; } = new List<FlightQuoteEntity>();
    public ICollection<PriceAlertEntity> Alerts { get; set; } = new List<PriceAlertEntity>();
}