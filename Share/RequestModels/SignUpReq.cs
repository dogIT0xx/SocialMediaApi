using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Share.RequestModels
{
    public sealed record SignUpReq
    {
        [Required]
        [EmailAddress]
        public string Email { get; init; }

        [Required]
        public string UserName { get; init; }

        [Required]
        public string Password { get; init; }
    }
}