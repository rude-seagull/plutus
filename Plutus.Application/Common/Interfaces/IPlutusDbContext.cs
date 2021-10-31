using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Plutus.Domain.Entities;

namespace Plutus.Application.Common.Interfaces
{
    public interface IPlutusDbContext
    {
        DbSet<Account> Accounts { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}