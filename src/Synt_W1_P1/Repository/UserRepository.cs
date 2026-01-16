using Microsoft.EntityFrameworkCore;
using Synt_W1_P1.Infrastructure;
using Synt_W1_P1.Interfaces;
using Synt_W1_P1.Models;

namespace Synt_W1_P1.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
            :base(context) 
        {
            _context = context;
        }
        public async Task<User?> ExistsAsync(string email)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> ExistsWithAnotherUser(string email, int id)
        {
            return await _context.Users
                .AnyAsync(u => u.Email == email && u.Id != id);
        }
    }
}
