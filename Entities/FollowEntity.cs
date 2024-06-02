using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SocialMediaApi.Entities
{
    [Table("Follows")]
    [PrimaryKey(nameof(FollowingUserId), nameof(FollowedUserId))]
    public class FollowEntity
    {
        [ForeignKey(nameof(FollowingUser))]
        public string FollowingUserId { get; set; }

        [ForeignKey(nameof(FollowedUser))]
        public string FollowedUserId { get; set; }

        [DeleteBehavior(DeleteBehavior.NoAction)]
        public UserEntity FollowingUser { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public UserEntity FollowedUser { get; set; }
    }
}
