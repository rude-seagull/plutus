using System;
using System.Collections.Generic;
using System.Linq;
using Plutus.Domain.Common;
#pragma warning disable 8618

namespace Plutus.Domain.Entities
{
    public class Account : AuditableEntity
    {
        // Domain behavior
        private Account() {}
        
        public static Account CreateInstance(
            string title, 
            string description, 
            decimal initialBalance,
            string userId)
        {
            return new Account
            {
                Id = Guid.NewGuid(),
                Title = title,
                Description = description,
                Balance = initialBalance,
                UserId = userId,
                _transactions = new HashSet<Transaction>()
            };
        }
        
        // Scalar properties
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal Balance { get; private set; }
        public string UserId { get; private set; }
        
        // Navigation & Relationships
        private HashSet<Transaction> _transactions;
        public IReadOnlyCollection<Transaction> Transactions => _transactions.ToList();
    }
}