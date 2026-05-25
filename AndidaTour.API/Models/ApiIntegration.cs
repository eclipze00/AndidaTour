namespace AndidaTour.API.Models;

public class ApiIntegration
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty; // airline, search, miles, payment, email, whatsapp
    public string Status { get; set; } = string.Empty;   // connected, disconnected, error
    public string? ApiKeyMask { get; set; }
}