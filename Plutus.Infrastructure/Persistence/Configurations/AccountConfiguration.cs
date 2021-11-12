using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plutus.Domain.Entities;

namespace Plutus.Infrastructure.Persistence.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> accountEntity)
        {
            accountEntity.HasKey(a => a.Id);
            accountEntity.Property(a => a.Balance).HasColumnType("decimal(18,4)");
            accountEntity.Property(a => a.Title).HasMaxLength(maxLength: 140);
            accountEntity.Property(a => a.Description).HasMaxLength(maxLength: 500);
        }
    }
}