using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusLing.Domain.Entities;
using NexusLing.Domain.ValueObjects;

namespace NexusLing.Infrastructure.Database.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.ComplexProperty(x => x.Login, b =>
            {
                b.Property(l => l.Value).HasColumnName("Login")
                    .IsRequired()
                    .HasMaxLength(Login.MaxLength);
            });

            builder.ComplexProperty(x => x.PasswordHash, b =>
            {
                b.Property(p => p.Value).HasColumnName("PasswordHash")
                    .IsRequired()
                    .HasMaxLength(PasswordHash.MaxLength);
            });
        }
    }
}
