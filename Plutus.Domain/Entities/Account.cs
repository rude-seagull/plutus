using System;
using Plutus.Domain.Common;

namespace Plutus.Domain.Entities
{
    public class Account : AuditableEntity
    {
        private Account() {}
        
        public static Account CreateInstance(
            string title, 
            string description, 
            decimal initialBalance)
        {
            return new Account
            {
                Id = default,
                Title = title,
                Description = description,
                Balance = initialBalance
            };
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public decimal Balance { get; private set; }
    }
}