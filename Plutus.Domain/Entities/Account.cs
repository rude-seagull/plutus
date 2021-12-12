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

    // Domain behavior
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
            Id = Guid.NewGuid(),
            Title = title,
            Description = description,
            Balance = initialBalance,
            UserId = userId,
            _transactions = new HashSet<Transaction>()
        };
    }

    // Aggregate Behavior
    public void AddTransaction(
        decimal amount)
    {
        if (_transactions is null)
            throw new InvalidOperationException("The transactions collection must be loaded before calling this method");

        _transactions.Add(Transaction.CreateInstance(amount, Id));
        
        // TODO Update balance, maybe introduce events here ? 
    }
}