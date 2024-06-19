using Microsoft.EntityFrameworkCore;
using SocialMediaApi.Entities;
using SocialMediaApi.Migrations;
using SocialMediaApi.Repositories.Interfaces;

namespace SocialMediaApi.Repositories
{
    public class LikeRepository : GenericRepository<LikeEntity>, ILikeRepository
    {
        public LikeRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        public async Task<bool> CheckExitsAsync(int postId, string userId)
        {
            var result = await DbEntitySet.AnyAsync(i => i.PostId == postId && i.UserId == userId);
            return result;
        }
    }
}
