using AndidaTour.API.Models;

namespace AndidaTour.API.Services;

public class FlightService
{
    private readonly List<Airport> _airports = new()
    {
        new() { Code="GRU", City="São Paulo", Name="Guarulhos Intl.", Country="Brasil" },
        new() { Code="CGH", City="São Paulo", Name="Congonhas", Country="Brasil" },
        new() { Code="GIG", City="Rio de Janeiro", Name="Galeão Intl.", Country="Brasil" },
        new() { Code="BSB", City="Brasília", Name="Pres. Juscelino K.", Country="Brasil" },
        new() { Code="MIA", City="Miami", Name="Miami Intl.", Country="EUA" },
        new() { Code="LIS", City="Lisboa", Name="Humberto Delgado", Country="Portugal" },
        new() { Code="CDG", City="Paris", Name="Charles de Gaulle", Country="França" },
    };

    private readonly List<Airline> _airlines = new()
    {
        new() { Code="LA", Name="LATAM Airlines", Color="oklch(0.55 0.18 15)", Initials="LA" },
        new() { Code="G3", Name="GOL Linhas Aéreas", Color="oklch(0.62 0.18 50)", Initials="G3" },
        new() { Code="AD", Name="Azul Linhas Aéreas", Color="oklch(0.48 0.17 255)", Initials="AD" },
        new() { Code="AA", Name="American Airlines", Color="oklch(0.55 0.05 250)", Initials="AA" },
    };

    public List<Airport> GetAirports() => _airports;
    public List<Airline> GetAirlines() => _airlines;

    public List<FlightOption> GenerateMockFlights(string fromCode, string toCode, string cabin)
    {
        var from = _airports.FirstOrDefault(a => a.Code == fromCode) ?? _airports[0];
        var to = _airports.FirstOrDefault(a => a.Code == toCode) ?? _airports[4];
        var cabinMult = cabin == "primeira" ? 4m : cabin == "executiva" ? 2.6m : 1m;
        var results = new List<FlightOption>();
        var baseTime = DateTime.Today.AddHours(6);

        for (int i = 0; i < _airlines.Count; i++)
        {
            var airline = _airlines[i];
            var depOffset = i * 95 + 30;
            var dur = 180 + (i % 3) * 75 + (to.Country != "Brasil" ? 360 : 0);
            var dep = baseTime.AddMinutes(depOffset);
            var arr = dep.AddMinutes(dur);
            var basePrice = (to.Country != "Brasil" ? 3200 : 480) + i * 180;
            var price = Math.Round(basePrice * cabinMult);

            results.Add(new FlightOption
            {
                Id = $"f-{i}-{fromCode}-{toCode}",
                Airline = airline,
                From = from,
                To = to,
                Departure = dep.ToString("HH:mm"),
                Arrival = arr.ToString("HH:mm"),
                Duration = $"{dur / 60}h {dur % 60:00}m",
                Stops = i % 3 == 0 ? 0 : i % 3 == 1 ? 1 : 2,
                Price = price,
                Taxes = Math.Round(price * 0.18m),
                Miles = 15000 + i * 4500,
                MilesProgram = new[] { "Smiles", "LATAM Pass", "TudoAzul", "AAdvantage" }[i % 4],
                Cabin = cabin,
                Baggage = i % 2 == 0 ? "1 bagagem 23kg incluída" : "Somente bagagem de mão",
                Segments = new List<FlightSegment>
                {
                    new() { From = from.Code, To = to.Code, Departure = dep.ToString("HH:mm"),
                            Arrival = arr.ToString("HH:mm"), Airline = airline.Name,
                            FlightNumber = $"{airline.Code}{1200 + i * 7}", Duration = $"{dur/60}h {dur%60:00}m" }
                }
            });
        }
        return results;
    }
}