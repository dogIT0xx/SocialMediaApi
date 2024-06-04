using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialMediaApi.Entities;
using SocialMediaApi.Migrations;

namespace SocialMediaApi.Repositories
{
    public class UserRepository : UserStore<UserEntity>
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
            AutoSaveChanges = false;
        }

        public async Task<bool> CheckExistById(string id)
        {
            var result = await Users.AnyAsync(i => i.Id == id);
            return result;
        }
    }
}
