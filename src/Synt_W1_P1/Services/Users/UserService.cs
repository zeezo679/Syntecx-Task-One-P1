using Microsoft.EntityFrameworkCore;
using Synt_W1_P1.Infrastructure;
using System.Collections;
using Synt_W1_P1.Models;
using Microsoft.Identity.Client;
using Synt_W1_P1.Repository;
using Synt_W1_P1.Interfaces;

namespace Synt_W1_P1.Services.UserService
{
    public class UserService
    {
        //private readonly AppDbContext _context;
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User?> GetUserAsync(int id)  
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User?> AddUser(User user)
        {
            if (user is null)
                return null;

            //checking for existing user first 
            var existingUser = await _userRepository.ExistsAsync(user.Email);

            if (existingUser is not null)
                return null;

            await _userRepository.AddAsync(user);
            await _userRepository.SaveAsync();

            return user;
        }

        public async Task<User?> UpdateUser(int id, User user)
        {
            var userToUpdate = await _userRepository.GetByIdAsync(id);

            if(userToUpdate is null)
                return null;

            if (userToUpdate.Id != id)
                return null;

            var emailExists = await _userRepository.ExistsWithAnotherUser(user.Email, id);

            if (emailExists)
                return null;

            //update user
            userToUpdate.Email = user.Email;
            userToUpdate.Name = user.Name;
            userToUpdate.Address = user.Address;

            //_userRepository.Update(userToUpdate); redundant because GetByIdAsync is being tracked
            await _userRepository.SaveAsync();

            return userToUpdate;    
        }

        public async Task<bool> DeleteUser(int id)
        {
            var userToDelete = await _userRepository.GetByIdAsync(id);

            if (userToDelete is null)
                return false;

            _userRepository.Delete(userToDelete);
            await _userRepository.SaveAsync();

            return true;
        }
    }
}
