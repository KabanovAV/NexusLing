using NexusLing.Domain.Interfaces;
using NexusLing.Infrastructure.Database;

namespace NexusLing.Infrastructure.Repositories
{
    public class Repository : IRepository
    {
        /// <summary>
        /// Пользователи
        /// </summary>
        public IUserRepository UserRepository { get; set; }

        public Repository(ApplicationDbContext db)
        {
            UserRepository = new UserRepository(db);
        }
    }
}
