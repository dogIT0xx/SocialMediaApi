using Microsoft.Build.Framework;

namespace SocialMediaApi.Share.RequestModels
{
    public sealed record CreateCommentReq
    {
        [Required]
        public string UserId { get; init; }

        [Required]
        public int PostId { get; init; }

        [Required]
        public string Content { get; init; }

        public int? ParentCommentId { get; init; }
    }
}
