using AndidaTour.API.Entities;

namespace AndidaTour.API.Repositories;

public interface IQuoteRequestRepository
{
    // Para o cliente — vê só as próprias solicitações
    Task<List<QuoteRequestEntity>> GetByClientAsync(int clientUserId);
    Task<QuoteRequestEntity?> GetByIdAndClientAsync(int id, int clientUserId);
    Task<QuoteRequestEntity> CreateAsync(QuoteRequestEntity request);
    Task<bool> CancelAsync(int id, int clientUserId);

    // Para o admin — vê todas
    Task<List<QuoteRequestEntity>> GetAllAsync();
    Task<QuoteRequestEntity?> GetByIdAsync(int id);
    Task<bool> UpdateAsync(QuoteRequestEntity request);
}