namespace AndidaTour.API.Models
{
    public class PriceAlert
    {
        public string Id { get; set; } = string.Empty;
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public DateTime PeriodStart { get; set; } = DateTime.MinValue;
        public DateTime PeriodEnd { get; set; } = DateTime.MinValue;
        public decimal MaxPrice { get; set; }
        public string? MilesProgram { get; set; }
        public List<string> Channels { get; set; } = new();
        public bool Active { get; set; }
    }
}