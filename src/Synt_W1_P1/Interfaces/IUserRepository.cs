using Synt_W1_P1.Models;

namespace Synt_W1_P1.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> ExistsAsync(string email);
        Task<bool> ExistsWithAnotherUser(string email, int id);
    }
}
