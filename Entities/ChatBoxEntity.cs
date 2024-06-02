using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SocialMediaApi.Entities
{
    [Table("ChatBoxes")]
    [PrimaryKey(nameof(SenderId), nameof(ReceiverId))]
    public class ChatBoxEntity
    {
        [ForeignKey(nameof(SenderUser))]
        public string SenderId { get; set; }

        [ForeignKey(nameof(ReceiverUser))]
        public string ReceiverId { get; set; }

        public string Message { get; set; }
        public int Timestamp { get; set; }


        [DeleteBehavior(DeleteBehavior.NoAction)]
        public UserEntity SenderUser { get; set; }

        [DeleteBehavior(DeleteBehavior.NoAction)]
        public UserEntity ReceiverUser { get; set; }

    }
}
