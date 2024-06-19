using SocialMediaApi.Entities;

namespace SocialMediaApi.Repositories.Interfaces
{
    public interface ILikeRepository : IGenericRepository<LikeEntity>
    {
        Task<bool> CheckExitsAsync(int postId, string userId);
    }
}
