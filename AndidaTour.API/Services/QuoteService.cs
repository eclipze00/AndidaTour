using AndidaTour.API.Models;

namespace AndidaTour.API.Services;

public class QuoteService
{
    private readonly List<FlightQuote> _quotes = new()
    {
        new() { Id="q1", ClientName="Mariana Costa", From="GRU", To="LIS",
                TravelDate=new DateTime(2026,6,12), BestPrice=3890, Status=QuoteStatus.Enviada, CreatedAt=new DateTime(2026,5,15) },
        new() { Id="q2", ClientName="Rafael Almeida", From="GRU", To="MIA",
                TravelDate=new DateTime(2026,7,4), BestPrice=4250, Status=QuoteStatus.Aprovada, CreatedAt=new DateTime(2026,5,12) },
        new() { Id="q3", ClientName="Juliana Pires", From="CGH", To="SDU",
                TravelDate=new DateTime(2026,5,28), BestPrice=489, Status=QuoteStatus.Rascunho, CreatedAt=new DateTime(2026,5,17) },
        new() { Id="q4", ClientName="Eduardo Lima", From="BSB", To="POA",
                TravelDate=new DateTime(2026,6,2), BestPrice=712, Status=QuoteStatus.Expirada, CreatedAt=new DateTime(2026,4,29) },
    };

    public List<FlightQuote> GetAll() => _quotes;
    public FlightQuote? GetById(string id) => _quotes.FirstOrDefault(q => q.Id == id);
    public void Delete(string id) => _quotes.RemoveAll(q => q.Id == id);
}