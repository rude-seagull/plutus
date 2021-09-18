using System;
using Plutus.Application.Common.Interfaces;

namespace Plutus.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.UtcNow;
    }
}