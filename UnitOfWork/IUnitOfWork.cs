using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SocialMediaApi.Entities;
using SocialMediaApi.Repositories;
using SocialMediaApi.Repositories.Interfaces;

namespace SocialMediaApi.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepository PostRepository { get; }
        IUserRepository UserRepository { get; }
        Task<int> CompleteAsync();
    }
}