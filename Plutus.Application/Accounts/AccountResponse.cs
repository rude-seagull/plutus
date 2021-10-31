using System;
using AutoMapper;
using Plutus.Domain.Entities;

namespace Plutus.Application.Accounts
{
    public record AccountResponse(
        Guid Id,
        string Title,
        string Description,
        decimal Balance);

    public class AccountResponseProfile : Profile
    {
        public AccountResponseProfile()
        {
            CreateMap<Account, AccountResponse>();
        }
    }
}