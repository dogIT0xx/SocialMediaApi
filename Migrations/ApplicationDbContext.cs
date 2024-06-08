using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialMediaApi.Entities;

namespace SocialMediaApi.Migrations
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<ChatBoxEntity> ChatBoxEntity { get; set; }
        public DbSet<CommentEntity> CommentEntity { get; set; }
        public DbSet<FollowEntity> FollowEntity { get; set; }
        public DbSet<GroupEntity> GroupEntity { get; set; }
        //public DbSet<LikeEntity> LikeEntity { get; set; }
        public DbSet<PostEntity> PostEntity { get; set; }
        public DbSet<UserEntity> UserEntity { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}




