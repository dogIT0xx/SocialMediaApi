using SocialMediaApi.Entities;

namespace SocialMediaApi.Repositories.Interfaces;

public interface IPostRepository : IGenericRepository<PostEntity>
{
    Task<bool> CheckExitsAsync(int postId);
}