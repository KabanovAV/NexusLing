using Microsoft.EntityFrameworkCore;
using NexusLing.Domain.Entities;

namespace NexusLing.Domain.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
    }
}
