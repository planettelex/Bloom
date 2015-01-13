using System;
using Bloom.Domain.Enums;

namespace Bloom.Domain.Models
{
    public class FiltersetElement
    {
        public Guid FiltersetId { get; set; }

        public int Order { get; set; }

        public FiltersetElementType ElementType { get; set; }

        public Guid StatementId { get; set; }

        public FiltersetStatement Statement { get; set; }
    }
}
