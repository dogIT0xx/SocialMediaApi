using System.ComponentModel.DataAnnotations;

namespace SocialMediaApi.Share.RequestModels
{
    public sealed record LikeReq
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public int PostId { get; set; }
    }
}
