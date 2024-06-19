using Microsoft.EntityFrameworkCore;
using SocialMediaApi.Entities;
using SocialMediaApi.Migrations;
using SocialMediaApi.Repositories.Interfaces;

namespace SocialMediaApi.Repositories
{
    public class CommentRepository : GenericRepository<CommentEntity>, ICommentRepository
    {
        public CommentRepository(ApplicationDbContext context) : base(context)
        {

        }

        public void DeleteAllReply(int commentId)
        {
            var replyComments = DbEntitySet.Where(c => c.ParentCommentId == commentId);
            DbEntitySet.RemoveRange(replyComments);
        }

        public async Task<IEnumerable<CommentEntity>> GetAllByPostIdAsync(int postId)
        {
            var comments = await DbEntitySet
                .Where(c => c.PostId == postId)
                .AsNoTracking()
                .ToListAsync();
            return comments;
        }
    }
}
