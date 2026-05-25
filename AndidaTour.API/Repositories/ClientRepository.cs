using AndidaTour.API.Data;
using AndidaTour.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace AndidaTour.API.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly AppDbContext _db;
    public ClientRepository(AppDbContext db) => _db = db;

    public async Task<List<ClientEntity>> GetAllByUserAsync(int userId) =>
        await _db.Clients
            .Where(c => c.UserId == userId)
            .OrderBy(c => c.Name)
            .ToListAsync();

    public async Task<ClientEntity?> GetByIdAsync(int id, int userId) =>
        await _db.Clients.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

    public async Task<ClientEntity> CreateAsync(ClientEntity client)
    {
        _db.Clients.Add(client);
        await _db.SaveChangesAsync();
        return client;
    }

    public async Task<bool> DeleteAsync(int id, int userId)
    {
        var client = await GetByIdAsync(id, userId);
        if (client == null) return false;
        _db.Clients.Remove(client);
        await _db.SaveChangesAsync();
        return true;
    }
}