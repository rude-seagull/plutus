using System;
using Plutus.Domain.Common;

namespace Plutus.Domain.Entities;

public class Transaction : AuditableEntity
{
    private Transaction()
    {
    }

    public Guid Id { get; private set; }
    public decimal Amount { get; private set; }
    public Guid AccountId { get; private set; }

    internal static Transaction CreateInstance(
        decimal amount,
        Guid accountId)
    {
        return new Transaction
        {
            Id = Guid.NewGuid(),
            Amount = amount,
            AccountId = accountId
        };
    }
}