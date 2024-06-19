using System.ComponentModel.DataAnnotations;

namespace SocialMediaApi.Share.RequestModels
{
    public sealed record UpdateCommentReq
    {
        [Required]
        public int CommentId { get; init; }

        [Required]
        public string Content { get; init; }
    }
}
