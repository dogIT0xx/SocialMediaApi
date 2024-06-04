using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Share.RequestModels
{
    public sealed class SignUpReq
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}