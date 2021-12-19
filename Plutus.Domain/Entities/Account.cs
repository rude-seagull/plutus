using System;
using System.Collections.Generic;
using System.Linq;
using Plutus.Domain.Common;
using Plutus.Domain.Interfaces;

// ReSharper disable ReturnTypeCanBeEnumerable.Global

#pragma warning disable 8618

namespace Plutus.Domain.Entities;

public class Account : AuditableEntity, IAggregateRoot
{
    // Navigation & Relationships
    private HashSet<Transaction> _transactions;

    private Account()
    {
    }

    // Scalar properties
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public decimal Balance { get; private set; }
    public string UserId { get; private set; }
    public IReadOnlyCollection<Transaction> Transactions => _transactions.ToList();

    public static Account CreateInstance(
        string title,
        string description,
        decimal initialBalance,
        string userId)
    {
        return new Account
        {
            Title = title,
            Description = description,
            Balance = initialBalance,
            UserId = userId,
            _transactions = new HashSet<Transaction>()
        };
    }

    public Transaction AddTransaction(
        decimal amount)
    {
        if (_transactions is null)
            throw new InvalidOperationException("The transactions collection must be loaded before calling this method");

        var newTransaction = Transaction.CreateInstance(amount, Id);
        _transactions.Add(newTransaction);

        Balance += amount;

        return newTransaction;
    }
}