using RefactorThis.Domain.Common;
using System;

namespace RefactorThis.Persistence
{
    public class Payment : AuditableEntity
    {
        public decimal Amount { get; set; }
        public string Reference { get; set; }
    }
}