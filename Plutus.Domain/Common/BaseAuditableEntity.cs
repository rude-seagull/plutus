﻿using System;

namespace Plutus.Domain.Common
{
    public abstract class BaseAuditableEntity
    {
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}