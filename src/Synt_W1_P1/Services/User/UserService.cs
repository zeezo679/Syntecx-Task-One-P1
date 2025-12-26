using Microsoft.EntityFrameworkCore;
using Synt_W1_P1.Infrastructure;
using System.Collections;
using Synt_W1_P1.Models;
using Microsoft.Identity.Client;

namespace Synt_W1_P1.Services.UserService
{
    public class UserService
    {
        private readonly AppDbContext _context;
        public UserService(AppDbContext context) 
        { 
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<User?> GetUserAsync(int id)  
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User?> AddUser(User user)
        {
            if (user is null)
                return null;

            //checking for existing user first 
            var existingUser = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == user.Email);

            if (existingUser is not null)
                return null;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> UpdateUser(int id, User user)
        {
            var userToUpdate = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if(userToUpdate is null)
                return null;

            if (userToUpdate.Id != id)
                return null;

            var emailExists = await _context.Users
                .AnyAsync(u => u.Email == user.Email && u.Id != id);

            if (emailExists)
                return null;

            //update user
            userToUpdate.Email = user.Email;
            userToUpdate.Name = user.Name;
            userToUpdate.Address = user.Address;

            await _context.SaveChangesAsync();

            return userToUpdate;    
        }

        public async Task<bool> DeleteUser(int id)
        {
            var userToDelete = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (userToDelete is null)
                return false;

            _context.Users.Remove(userToDelete);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
