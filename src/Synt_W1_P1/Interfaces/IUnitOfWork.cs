using Synt_W1_P1.Models;

namespace Synt_W1_P1.Interfaces
{
    public interface IUnitOfWork : IDisposable 
    {
        IRepository<User> Users { get; }
        IRepository<Author> Authors { get; }
        IRepository<Book> Books { get; }

        int Complete();
    }
}
