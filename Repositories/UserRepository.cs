using Microsoft.EntityFrameworkCore;
using SocialMediaApi.Entities;
using SocialMediaApi.Migrations;
using SocialMediaApi.Repositories.Interfaces;

namespace SocialMediaApi.Repositories
{
    public class UserRepository : GenericRepository<UserEntity>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> CheckExitsAsync(string userId)
        {
           var result =  await DbEntitySet.AnyAsync(i => i.Id.Equals(userId));
           return result;
        }
    }
}
