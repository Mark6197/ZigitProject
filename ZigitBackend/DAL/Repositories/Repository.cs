using DAL.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly DbContext _context;
        private DbSet<TEntity> _entities;

        public Repository(DbContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }
        public async Task<TEntity> GetAsync(long? id)
        {
            return await _entities.FindAsync(id);
        }

        public bool IsExist(long? id)
        {
            return _entities.Any(e => e.Id == id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }
        public async Task AddAsync(TEntity entity)
        {
            await _entities.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _entities.AddRangeAsync(entities);
        }

        public void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter)
        {
            return _entities.Where(filter);
        }

        public void Update(TEntity entity)
        {
            _entities.Update(entity);
        }

        public async Task<IEnumerable<TEntity>> GetEntityWithEagerLoadAsync(Expression<Func<TEntity, bool>> filter, string[] children)
        {
            try
            {
                IQueryable<TEntity> query = _entities;
                foreach (string entity in children)
                {
                    query = query.Include(entity);

                }
                return await query.Where(filter).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}