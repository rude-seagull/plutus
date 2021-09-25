using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Plutus.Application.Common.Interfaces;
using Plutus.Domain.Common;
using Plutus.Domain.Entities;
using Plutus.Infrastructure.Persistence.Extensions;

namespace Plutus.Infrastructure.Persistence
{
    public class PlutusDbContext : DbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;

        public PlutusDbContext(
            DbContextOptions<PlutusDbContext> options,
            ICurrentUserService currentUserService,
            IDateTime dateTime) : base(options)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }

        public DbSet<Account> Accounts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddDateTimeUtcConverter();
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>()
                .Where(e => e.State is EntityState.Added or EntityState.Modified))
            {
                entry.Entity.LogAuditValues(
                    entry.State == EntityState.Added, 
                    _dateTime.Now,
                    _currentUserService.UserId);
            }
            
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}