using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaApi.Entities
{
    [Table("Posts")]
    public class PostEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string MediaURL { get; set; }
        public int CreateAt { get; set; }
        public int UpdateAt { get; set; }
        public int CommentCount { get; set; }
        public int LikeCount { get; set; }
        public bool IsDeleted { get; set; }
        public int? DeleteAt { get; set; }

        [ForeignKey(nameof(User))]
        public string AuthorId { get; set; }

        public UserEntity User { get; set; }
    }
}
