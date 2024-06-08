using SocialMediaApi.Entities;

namespace SocialMediaApi.Repositories.Interfaces;

public interface IUserRepository : IGenericRepository<UserEntity>
{
    Task<bool> CheckExitsAsync(string userId);
}