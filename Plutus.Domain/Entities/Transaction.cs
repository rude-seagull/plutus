using System;
using System.Collections.Generic;
using Plutus.Domain.Enums;

namespace Plutus.Domain.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
        public TransactionType Type { get; set; }
        public Guid AccountId { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}