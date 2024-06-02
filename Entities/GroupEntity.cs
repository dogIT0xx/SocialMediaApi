using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaApi.Entities
{
    [Table("Groups")]
    public class GroupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Description { get; set; }
        public int CreateAt { get; set; }

        [ForeignKey(nameof(User))]
        public string AdminId { get; set; }

        public UserEntity User { get; set; }
    }
}

