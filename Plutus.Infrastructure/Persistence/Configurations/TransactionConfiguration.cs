using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plutus.Domain.Entities;

namespace Plutus.Infrastructure.Persistence.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> transactionEntity)
        {
            transactionEntity.HasKey(a => a.Id);
            transactionEntity.Property(a => a.Amount).HasColumnType("decimal(18,4)");
        }
    }
}