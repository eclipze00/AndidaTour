using AndidaTour.API.Data;
using AndidaTour.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace AndidaTour.API.Repositories;

public class AlertRepository : IAlertRepository
{
    private readonly AppDbContext _db;
    public AlertRepository(AppDbContext db) => _db = db;

    public async Task<List<PriceAlertEntity>> GetAllByUserAsync(int userId) =>
        await _db.Alerts
            .Where(a => a.UserId == userId)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync();

    public async Task<PriceAlertEntity> CreateAsync(PriceAlertEntity alert)
    {
        _db.Alerts.Add(alert);
        await _db.SaveChangesAsync();
        return alert;
    }

    public async Task<bool> ToggleAsync(int id, int userId)
    {
        var alert = await _db.Alerts
            .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
        if (alert == null) return false;
        alert.Active = !alert.Active;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id, int userId)
    {
        var alert = await _db.Alerts
            .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
        if (alert == null) return false;
        _db.Alerts.Remove(alert);
        await _db.SaveChangesAsync();
        return true;
    }
}