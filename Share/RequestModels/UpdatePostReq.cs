using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Share.RequestModels
{
    public sealed record UpdatePostReq
    {
        [Required]
        public int PostId { get; init; }

        [Required]
        public string Title { get; init; }

        [Required]
        public string Content { get; init; }
    }
}
