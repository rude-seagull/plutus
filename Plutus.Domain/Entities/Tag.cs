using System;
using System.Collections.Generic;

namespace Plutus.Domain.Entities
{
    public class Tag
    {
        public Guid Id { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}