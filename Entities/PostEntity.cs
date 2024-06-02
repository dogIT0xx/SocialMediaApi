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

        [ForeignKey(nameof(User))]
        public string AuthorId { get; set; }

        public UserEntity User { get; set; }
    }
}
