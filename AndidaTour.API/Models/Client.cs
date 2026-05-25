namespace AndidaTour.API.Models
{
    public class Client
    {
        public string Id { get; set; } = string.Empty;    
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Document { get; set; } = string.Empty;
        public string? Preferences { get; set; }
        public string? Notes { get; set; }
        public int Quotes { get; set; }
    }
}