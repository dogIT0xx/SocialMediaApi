using Microsoft.AspNetCore.Identity;

namespace SocialMediaApi.Entities
{
    public class UserEntity : IdentityUser
    {
        public string Bio { get; set; }
        public string Avatar { get; set; }
    }
}
