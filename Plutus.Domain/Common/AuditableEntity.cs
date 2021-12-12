using System;
using System.ComponentModel.DataAnnotations;

namespace Plutus.Domain.Common;

public abstract class AuditableEntity
{
    public DateTime Created { get; private set; }

    [MaxLength(length: 100)] public string CreatedBy { get; private set; } = null!;

    public DateTime LastModified { get; private set; }

    [MaxLength(length: 100)] public string LastModifiedBy { get; private set; } = null!;

    public void LogAuditValues(
        bool created,
        DateTime timeNow,
        string userId)
    {
        if (created)
        {
            Created = timeNow;
            CreatedBy = userId;
        }

        LastModified = timeNow;
        LastModifiedBy = userId;
    }
}