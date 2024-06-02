using SocialMediaApi.Repositories.Interfaces;

namespace SocialMediaAPI.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }

        Task<int> CompleteAsync();
    }
}