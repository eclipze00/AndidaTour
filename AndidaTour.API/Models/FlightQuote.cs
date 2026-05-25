namespace AndidaTour.API.Models;

public enum QuoteStatus { Rascunho, Enviada, Aprovada, Expirada, Cancelada}



public class FlightQuote
{
    public string Id { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public string From { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public DateTime TravelDate { get; set; } = DateTime.MinValue;
    public decimal BestPrice { get; set; }
    public QuoteStatus Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
