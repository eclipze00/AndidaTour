using System.ComponentModel.DataAnnotations;

namespace AndidaTour.API.Entities;

public class PriceAlertEntity
{
    public int Id { get; set; }

    [Required, MaxLength(10)]
    public string FromCode { get; set; } = string.Empty;

    [Required, MaxLength(10)]
    public string ToCode { get; set; } = string.Empty;

    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
    public decimal MaxPrice { get; set; }

    [MaxLength(100)]
    public string? MilesProgram { get; set; }

    public string Channels { get; set; } = "email";
    public bool Active { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int UserId { get; set; }
    public UserEntity User { get; set; } = null!;
}