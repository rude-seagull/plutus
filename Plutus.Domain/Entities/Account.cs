using System;
using System.Collections.Generic;
using Plutus.Domain.Common;

namespace Plutus.Domain.Entities
{
    public class Account : AuditableEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Balance { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}