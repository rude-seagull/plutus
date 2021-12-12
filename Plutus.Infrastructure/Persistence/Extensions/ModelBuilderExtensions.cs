using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Plutus.Infrastructure.Persistence.Extensions;

internal static class ModelBuilderExtensions
{
    internal static void AddDateTimeUtcConverter(
        this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        foreach (var entityProperty in entityType.GetProperties())
            if (entityProperty.ClrType == typeof(DateTime))
                entityProperty.SetValueConverter(new ValueConverter<DateTime, DateTime>(toDatabase => toDatabase,
                    fromDatabase => DateTime.SpecifyKind(fromDatabase, DateTimeKind.Utc)));
    }
}