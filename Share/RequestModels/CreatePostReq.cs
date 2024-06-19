using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Share.RequestModels
{
    public sealed record CreatePostReq
    {
        [Required]
        public string Title { get; init; }

        [Required]
        public string Content { get; init; }

        [Required]
        public string AuthorId { get; init; }
    }
}
