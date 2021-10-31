using System;

namespace Plutus.Application.Accounts
{
    public record AccountResponse(
        Guid AccountId,
        string Title,
        string Description,
        decimal Balance);
}