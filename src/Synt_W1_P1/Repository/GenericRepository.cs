using Microsoft.EntityFrameworkCore;
using Synt_W1_P1.Infrastructure;
using Synt_W1_P1.Interfaces;
using System.Threading.Tasks;

namespace Synt_W1_P1.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet; //private field to set DBSet for specific entity

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();  
        }
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            return entity != null;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity); 
        }
    }
}
