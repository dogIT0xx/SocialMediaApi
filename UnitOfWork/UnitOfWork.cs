using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SocialMediaApi.Entities;
using SocialMediaApi.Migrations;
using SocialMediaApi.UnitOfWork;
using SocialMediaApi.Repositories;
using SocialMediaApi.Repositories.Interfaces;

namespace SocialMediaApi.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IPostRepository PostRepository { get; }
        public IUserRepository UserRepository { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            PostRepository = new PostRepository(context);
            UserRepository = new UserRepository(context);
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