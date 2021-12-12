using System;
using System.Linq;
using System.Linq.Expressions;
using Plutus.Domain.Entities;

namespace Plutus.Application.Handlers.Accounts;

public record AccountResponse(
    Guid Id,
    string Title,
    string Description,
    decimal Balance);

internal static class AccountResponseMapper
{
    internal static IQueryable<AccountResponse> ProjectToAccountResponse(
        this IQueryable<Account> accounts)
    {
        return accounts.Select(MapToAccountResponse());
    }

    internal static AccountResponse MapToAccountResponse(
        this Account account)
    {
        return MapToAccountResponse().Compile().Invoke(account);
    }
    
    private static Expression<Func<Account, AccountResponse>> MapToAccountResponse()
    {
        return a => new AccountResponse(a.Id, a.Title, a.Description, a.Balance);
    }
}