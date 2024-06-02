using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SocialMediaApi.Entities
{
    [Table("Comments")]
    [PrimaryKey(nameof(SenderId), nameof(ReceiverId))]
    public class CommentEntity
    {
        [ForeignKey(nameof(SenderUser))]
        public string SenderId { get; set; }

        [ForeignKey(nameof(ReceiverUser))]
        public string ReceiverId { get; set; }

        public string Content { get; set; }


        [DeleteBehavior(DeleteBehavior.NoAction)]
        public UserEntity SenderUser { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public UserEntity ReceiverUser { get; set; }
    }
}
