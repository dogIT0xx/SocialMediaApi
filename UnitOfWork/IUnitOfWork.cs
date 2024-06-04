using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SocialMediaApi.Entities;
using SocialMediaApi.Repositories;

namespace SocialMediaApi.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        UserRepository UserRepository { get; }
        PostRepository PostRepository { get; }
        Task<int> CompleteAsync();
    }
}