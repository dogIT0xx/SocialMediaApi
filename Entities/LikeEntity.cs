using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SocialMediaApi.Entities
{
    [Table("Likes")]
    [PrimaryKey(nameof(SenderId), nameof(ReceiverId))]
    public class LikeEntity
    {
        [ForeignKey(nameof(SenderUser))]
        public string SenderId { get; set; }

        [ForeignKey(nameof(ReceiverUser))]
        public string ReceiverId { get; set; }


        [DeleteBehavior(DeleteBehavior.NoAction)]
        public UserEntity SenderUser { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public UserEntity ReceiverUser { get; set; }
    }
}
