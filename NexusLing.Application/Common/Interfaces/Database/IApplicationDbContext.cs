using Microsoft.EntityFrameworkCore;
using NexusLing.Domain.Entities;

namespace NexusLing.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
    }
}
