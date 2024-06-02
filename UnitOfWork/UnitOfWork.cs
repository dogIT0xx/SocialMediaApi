using SocialMediaApi.Migrations;
using SocialMediaAPI.UnitOfWork;
using SocialMediaApi.Repositories;
using SocialMediaApi.Repositories.Interfaces;

namespace SocialMediaApi.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IUserRepository UserRepository { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            UserRepository = new UserRepository(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}