using System.ComponentModel.DataAnnotations;

namespace AndidaTour.API.Entities;

public enum QuoteRequestStatus
{
    Pending,      // aguardando análise do admin
    InProgress,   // admin está trabalhando
    Completed,    // admin respondeu
    Cancelled     // cancelada
}

public class QuoteRequestEntity
{
    public int Id { get; set; }

    [Required, MaxLength(10)]
    public string FromCode { get; set; } = string.Empty;

    [Required, MaxLength(10)]
    public string ToCode { get; set; } = string.Empty;

    public DateTime DepartureDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    public int Passengers { get; set; } = 1;

    [MaxLength(20)]
    public string TripType { get; set; } = "roundtrip"; // oneway | roundtrip

    [MaxLength(20)]
    public string CabinClass { get; set; } = "economy";

    [MaxLength(1000)]
    public string? Notes { get; set; } // observações do cliente

    public QuoteRequestStatus Status { get; set; } = QuoteRequestStatus.Pending;

    // Resposta do admin
    public decimal? AdminPrice { get; set; }

    [MaxLength(2000)]
    public string? AdminNotes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // FK para o cliente (usuário com role Client)
    public int ClientUserId { get; set; }
    public UserEntity ClientUser { get; set; } = null!;
}