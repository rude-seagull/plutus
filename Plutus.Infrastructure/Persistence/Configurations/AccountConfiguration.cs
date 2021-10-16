﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plutus.Domain.Entities;

namespace Plutus.Infrastructure.Persistence.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Balance).HasColumnType("decimal(18,4)");
            builder.Property(a => a.Title).HasMaxLength(maxLength: 140);
            builder.Property(a => a.Description).HasMaxLength(maxLength: 500);
        }
    }
}