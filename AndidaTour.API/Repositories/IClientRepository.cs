using AndidaTour.API.Entities;

namespace AndidaTour.API.Repositories;

public interface IClientRepository
{
    Task<List<ClientEntity>> GetAllByUserAsync(int userId);
    Task<ClientEntity?> GetByIdAsync(int id, int userId);
    Task<ClientEntity> CreateAsync(ClientEntity client);
    Task<bool> DeleteAsync(int id, int userId);
}