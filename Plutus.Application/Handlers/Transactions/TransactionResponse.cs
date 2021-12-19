using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Plutus.Domain.Entities;

namespace Plutus.Application.Handlers.Transactions;

public record TransactionResponse(Guid Id, decimal Amount);

internal static class TransactionResponseMapper
{
    internal static IEnumerable<TransactionResponse> ProjectToTransactionResponse(
        this IEnumerable<Transaction> transactions)
    {
        return transactions.Select(MapToTransactionResponse().Compile());
    }

    internal static TransactionResponse MapToTransactionResponse(
        this Transaction transaction)
    {
        return MapToTransactionResponse().Compile().Invoke(transaction);
    }
    
    private static Expression<Func<Transaction, TransactionResponse>> MapToTransactionResponse()
    {
        return t => new TransactionResponse(t.Id, t.Amount);
    }
}