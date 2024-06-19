using SocialMediaApi.Entities;

namespace SocialMediaApi.Repositories.Interfaces
{
    public interface ICommentRepository : IGenericRepository<CommentEntity>
    {
        void DeleteAllReply(int commentId);
        Task<IEnumerable<CommentEntity>> GetAllByPostIdAsync(int postId);
    }
}
