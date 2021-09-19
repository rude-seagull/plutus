using System;
using Plutus.Domain.Common;

namespace Plutus.Domain.Entities
{
    public class Transaction : BaseAuditableEntity
    {
        public decimal Amount { get; private set; }
        public string Comment { get; private set; }
        public Guid AccountId { get; private set; }

        public Transaction(decimal amount, string comment, Guid accountId)
        {
            Amount = amount;
            Comment = comment;
            AccountId = accountId;
        }
    }
}