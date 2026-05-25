using AndidaTour.API.Entities;

namespace AndidaTour.API.Repositories;

public interface IUserRepository
{
    Task<UserEntity?> GetByEmailAsync(string email);
    Task<UserEntity> CreateAsync(UserEntity user);
    Task<bool> EmailExistsAsync(string email);
}