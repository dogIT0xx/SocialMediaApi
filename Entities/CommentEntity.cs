using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SocialMediaApi.Entities
{
    [Table("Comments")]
    public class CommentEntity
    {
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        [ForeignKey(nameof(ParentComment))]
        public int? ParentCommentId { get; set; }

        [ForeignKey(nameof(Post))]
        public int PostId { get; set; }

        public string Content { get; set; }
        public int CreatedAt  { get; set; }
        public int? UpdateAt  { get; set; }


        [DeleteBehavior(DeleteBehavior.NoAction)]
        public UserEntity User { get; set; }
        
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public PostEntity Post { get; set; }

        [DeleteBehavior(DeleteBehavior.NoAction)]
        public CommentEntity ParentComment { get; set; }
    }
}
