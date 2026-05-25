using System.ComponentModel.DataAnnotations;

namespace AndidaTour.API.Entities;

public enum QuoteStatusEntity
{
    Rascunho,
    Enviada,
    Aprovada,
    Expirada,
    Cancelada
}

public class FlightQuoteEntity
{
    public int Id { get; set; }

    [Required, MaxLength(10)]
    public string FromCode { get; set; } = string.Empty;

    [Required, MaxLength(10)]
    public string ToCode { get; set; } = string.Empty;

    public DateTime TravelDate { get; set; }
    public decimal BestPrice { get; set; }
    public QuoteStatusEntity Status { get; set; } = QuoteStatusEntity.Rascunho;
    public string? FlightDataJson { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int ClientId { get; set; }
    public ClientEntity Client { get; set; } = null!;

    public int UserId { get; set; }
    public UserEntity User { get; set; } = null!;
}