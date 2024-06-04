using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialMediaApi.Entities;
using SocialMediaApi.Migrations;
using SocialMediaApi.Repositories.Interfaces;

namespace SocialMediaApi.Repositories
{
    public class PostRepository : GenericRepository<PostEntity>, IPostRepository
    {
        public PostRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
