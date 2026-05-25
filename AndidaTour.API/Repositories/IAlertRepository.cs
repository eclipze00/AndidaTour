using AndidaTour.API.Entities;

namespace AndidaTour.API.Repositories;

public interface IAlertRepository
{
    Task<List<PriceAlertEntity>> GetAllByUserAsync(int userId);
    Task<PriceAlertEntity> CreateAsync(PriceAlertEntity alert);
    Task<bool> ToggleAsync(int id, int userId);
    Task<bool> DeleteAsync(int id, int userId);
}