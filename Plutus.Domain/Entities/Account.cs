using System.Collections.Generic;
using Plutus.Domain.Common;

namespace Plutus.Domain.Entities
{
    public class Account : BaseAuditableEntity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal Balance { get; private set; }
        private readonly List<Transaction> _transactions = new();
        public IEnumerable<Transaction> Transactions => _transactions.AsReadOnly();
        
        public Account(string title, string description)
        {
            Title = title;
            Description = description;
            Balance = decimal.Zero;
        }

        public void AddTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
            Balance += transaction.Amount;
        }

        public void UpdateTitle(string title)
        {
            Title = title;
        }

        public void UpdateDescription(string description)
        {
            Description = description;
        }
    }
}