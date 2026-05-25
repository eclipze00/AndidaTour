namespace AndidaTour.API.Models;

public enum CabinClass { Economica, Executiva, Primeira }

public class FlightSegment
{
    public string From { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public string Departure { get; set; } = string.Empty;
    public string Arrival { get; set; } = string.Empty;
    public string Airline { get; set; } = string.Empty;
    public string FlightNumber { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
}

public class FlightOption
{
    public string Id { get; set; } = string.Empty;
    public Airline Airline { get; set; } = new Airline();
    public Airport From { get; set; } = new Airport();
    public Airport To { get; set; } = new Airport();
    public string Departure { get; set; } = string.Empty;
    public string Arrival { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
    public int Stops { get; set; }
    public decimal Price { get; set; }
    public decimal Taxes { get; set; }
    public int? Miles { get; set; }
    public string? MilesProgram { get; set; }
    public string Cabin { get; set; } = string.Empty;
    public string Baggage { get; set; } = string.Empty;
    public List<FlightSegment> Segments { get; set; } = new();
}

    