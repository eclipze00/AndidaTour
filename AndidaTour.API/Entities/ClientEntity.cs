using System.ComponentModel.DataAnnotations;

namespace AndidaTour.API.Entities;

public class ClientEntity
{
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required, MaxLength(200)]
    public string Email { get; set; } = string.Empty;

    [MaxLength(30)]
    public string Phone { get; set; } = string.Empty;

    [MaxLength(20)]
    public string Document { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Preferences { get; set; }

    [MaxLength(1000)]
    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int UserId { get; set; }
    public UserEntity User { get; set; } = null!;

    public ICollection<FlightQuoteEntity> Quotes { get; set; } = new List<FlightQuoteEntity>();
}