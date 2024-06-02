using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialMediaApi.Entities;
using SocialMediaApi.Repositories.Interfaces;

// Use default userStore Identity Framework
namespace SocialMediaApi.Repositories
{
    public class UserRepository : UserStore<UserEntity>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
            AutoSaveChanges = false;
        }

        public async Task<UserEntity?> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public async Task Create(UserEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Update(UserEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(UserEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
