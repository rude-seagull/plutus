using System;

namespace Plutus.Domain.Entities
{
    public class Transaction
    {
        private Transaction() {}

        internal static Transaction CreateInstance(
            decimal amount,
            Guid accountId
        )
        {
            return new Transaction
            {
                Id = Guid.NewGuid(),
                Amount = amount,
                AccountId = accountId
            };
        }
        
        public Guid Id { get; private set; }
        public decimal Amount { get; private set; }
        public Guid AccountId { get; private set; }
    }
}