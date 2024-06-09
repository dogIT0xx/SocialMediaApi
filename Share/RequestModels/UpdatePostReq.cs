using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Share.RequestModels
{
    public sealed class UpdatePostReq
    {
        [Required]
        public int PostId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
