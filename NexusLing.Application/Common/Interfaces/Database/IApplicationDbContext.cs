using Microsoft.EntityFrameworkCore;
using NexusLing.Domain;

namespace NexusLing.Application
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
    }
}
