using AndidaTour.API.Entities;

namespace AndidaTour.API.Repositories;

public interface IQuoteRepository
{
    Task<List<FlightQuoteEntity>> GetAllByUserAsync(int userId);
    Task<FlightQuoteEntity?> GetByIdAsync(int id, int userId);
    Task<FlightQuoteEntity> CreateAsync(FlightQuoteEntity quote);
    Task<bool> DeleteAsync(int id, int userId);
    Task<bool> UpdateStatusAsync(int id, int userId, QuoteStatusEntity status);
}