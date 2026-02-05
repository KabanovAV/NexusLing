using Microsoft.EntityFrameworkCore;
using NexusLing.Domain.Entities;

namespace NexusLing.Application.Common.Interfaces.Database
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
    }
}
