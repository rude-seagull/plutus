using System;

namespace Plutus.Domain.Common
{
    public abstract class AuditableEntity
    {
        public DateTime Created { get; private set; }
        public string CreatedBy { get; private set; } = null!;
        public DateTime LastModified { get; private set; }
        public string LastModifiedBy { get; private set; } = null!;

        public void SetAuditValues(bool added, DateTime timeNow, string userId)
        {
            if (added)
            {
                Created = timeNow;
                CreatedBy = userId;
            }
            
            LastModified = timeNow;
            LastModifiedBy = userId;
        }
    }
}