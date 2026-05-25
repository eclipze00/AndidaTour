using AndidaTour.API.Data;
using AndidaTour.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace AndidaTour.API.Repositories;

public class QuoteRepository : IQuoteRepository
{
    private readonly AppDbContext _db;
    public QuoteRepository(AppDbContext db) => _db = db;

    public async Task<List<FlightQuoteEntity>> GetAllByUserAsync(int userId) =>
        await _db.Quotes
            .Where(q => q.UserId == userId)
            .Include(q => q.Client)
            .OrderByDescending(q => q.CreatedAt)
            .ToListAsync();

    public async Task<FlightQuoteEntity?> GetByIdAsync(int id, int userId) =>
        await _db.Quotes
            .Include(q => q.Client)
            .FirstOrDefaultAsync(q => q.Id == id && q.UserId == userId);

    public async Task<FlightQuoteEntity> CreateAsync(FlightQuoteEntity quote)
    {
        _db.Quotes.Add(quote);
        await _db.SaveChangesAsync();
        return quote;
    }

    public async Task<bool> DeleteAsync(int id, int userId)
    {
        var quote = await GetByIdAsync(id, userId);
        if (quote == null) return false;
        _db.Quotes.Remove(quote);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateStatusAsync(int id, int userId, QuoteStatusEntity status)
    {
        var quote = await GetByIdAsync(id, userId);
        if (quote == null) return false;
        quote.Status = status;
        await _db.SaveChangesAsync();
        return true;
    }
}