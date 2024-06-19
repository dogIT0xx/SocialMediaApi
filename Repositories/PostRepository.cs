using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialMediaApi.Entities;
using SocialMediaApi.Migrations;
using SocialMediaApi.Repositories.Interfaces;

namespace SocialMediaApi.Repositories
{
    public class PostRepository : GenericRepository<PostEntity>, IPostRepository
    {
        public PostRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<bool> CheckExitsAsync(int postId)
        {
            var result = await DbEntitySet.AnyAsync(i => i.Id == postId);
            return result;
        }

        public async Task<IEnumerable<PostEntity>> GetPostsAsync(int pageSize, int pageIndex)
        {
            var result = await DbEntitySet
                .OrderBy(p => p.Id)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
            return result;
        }
    }
}
