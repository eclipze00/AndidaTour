using AndidaTour.API.Data;
using AndidaTour.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace AndidaTour.API.Repositories;

public class QuoteRequestRepository : IQuoteRequestRepository
{
    private readonly AppDbContext _db;
    public QuoteRequestRepository(AppDbContext db) => _db = db;

    public async Task<List<QuoteRequestEntity>> GetByClientAsync(int clientUserId) =>
        await _db.QuoteRequests
            .Where(q => q.ClientUserId == clientUserId)
            .OrderByDescending(q => q.CreatedAt)
            .ToListAsync();

    public async Task<QuoteRequestEntity?> GetByIdAndClientAsync(int id, int clientUserId) =>
        await _db.QuoteRequests
            .FirstOrDefaultAsync(q => q.Id == id && q.ClientUserId == clientUserId);

    public async Task<QuoteRequestEntity> CreateAsync(QuoteRequestEntity request)
    {
        _db.QuoteRequests.Add(request);
        await _db.SaveChangesAsync();
        return request;
    }

    public async Task<bool> CancelAsync(int id, int clientUserId)
    {
        var req = await GetByIdAndClientAsync(id, clientUserId);
        if (req == null || req.Status == QuoteRequestStatus.Completed) return false;
        req.Status = QuoteRequestStatus.Cancelled;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<List<QuoteRequestEntity>> GetAllAsync() =>
        await _db.QuoteRequests
            .Include(q => q.ClientUser)
            .OrderByDescending(q => q.CreatedAt)
            .ToListAsync();

    public async Task<QuoteRequestEntity?> GetByIdAsync(int id) =>
        await _db.QuoteRequests
            .Include(q => q.ClientUser)
            .FirstOrDefaultAsync(q => q.Id == id);

    public async Task<bool> UpdateAsync(QuoteRequestEntity request)
    {
        request.UpdatedAt = DateTime.UtcNow;
        _db.QuoteRequests.Update(request);
        await _db.SaveChangesAsync();
        return true;
    }
}