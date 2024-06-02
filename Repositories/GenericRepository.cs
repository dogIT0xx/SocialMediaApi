using Microsoft.EntityFrameworkCore;
using SocialMediaApi.Migrations;
using SocialMediaApi.Repositories.Interfaces;

namespace SocialMediaApi.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext Context;
        protected DbSet<TEntity> DbEntitySet { get; }

        public GenericRepository(ApplicationDbContext context)
        {
            Context = context;
            DbEntitySet = context.Set<TEntity>();
        }

        public async Task Create(TEntity entity)
        {
            await DbEntitySet.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            DbEntitySet.Update(entity);
        }


        public async Task<TEntity?> GetByIdAsync(object id)
        {
            return await DbEntitySet.FindAsync(id);
        }

        public void Delete(TEntity entity)
        {
            DbEntitySet.Remove(entity);
        }
    }
}
