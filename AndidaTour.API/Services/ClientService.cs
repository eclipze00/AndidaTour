using AndidaTour.API.Models;

namespace AndidaTour.API.Services;

public class ClientService
{
    private readonly List<Client> _clients = new()
    {
        new() { Id="c1", Name="Mariana Costa", Email="mariana@exemplo.com", Phone="+55 11 99999-1234", Document="123.456.789-00", Preferences="Janela, voos diretos", Quotes=7 },
        new() { Id="c2", Name="Rafael Almeida", Email="rafael@exemplo.com", Phone="+55 21 98877-2211", Document="987.654.321-00", Preferences="Executiva, LATAM", Quotes=3 },
        new() { Id="c3", Name="Juliana Pires", Email="ju@exemplo.com", Phone="+55 31 97766-5544", Document="456.123.789-00", Preferences="Smiles, datas flexíveis", Quotes=12 },
        new() { Id="c4", Name="Eduardo Lima", Email="edu@exemplo.com", Phone="+55 41 96655-3322", Document="321.654.987-00", Quotes=1 },
    };

    public List<Client> GetAll() => _clients;
    public void Create(Client client) { client.Id = Guid.NewGuid().ToString()[..8]; _clients.Add(client); }
}