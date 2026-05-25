using AndidaTour.API.Models;

namespace AndidaTour.API.Services;

public class AlertService
{
    private readonly List<PriceAlert> _alerts = new()
    {
        new() { Id="a1", From="GRU", To="LIS", PeriodStart=new DateTime(2026,6,1), PeriodEnd=new DateTime(2026,6,30), MaxPrice=3500, MilesProgram="Smiles", Channels=new(){"email","panel"}, Active=true },
        new() { Id="a2", From="GRU", To="MIA", PeriodStart=new DateTime(2026,7,1), PeriodEnd=new DateTime(2026,7,15), MaxPrice=4000, Channels=new(){"whatsapp"}, Active=true },
    };

    public List<PriceAlert> GetAll() => _alerts;
    public void Create(PriceAlert alert) { alert.Id = Guid.NewGuid().ToString()[..8]; _alerts.Add(alert); }
    public void Toggle(string id) { var a = _alerts.FirstOrDefault(x => x.Id == id); if (a != null) a.Active = !a.Active; }
}