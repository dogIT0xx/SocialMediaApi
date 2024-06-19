using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SocialMediaApi.Entities
{
    [Table("Likes")]
    [PrimaryKey(nameof(UserId), nameof(PostId))]
    public class LikeEntity
    {
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        [ForeignKey(nameof(Post))]
        public int PostId { get; set; }


        [DeleteBehavior(DeleteBehavior.NoAction)]
        public UserEntity User { get; set; }

        [DeleteBehavior(DeleteBehavior.NoAction)]
        public PostEntity Post { get; set; }
    }
}
