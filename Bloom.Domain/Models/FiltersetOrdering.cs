using System;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models
{
    public class FiltersetOrdering
    {
        public Guid FiltersetId { get; set; }

        public int Priority { get; set; }

        public IFiltersetOrder Order { get; set; }

        public FiltersetItemScope Scope { get; set; }

        public OrderDirection Direction { get; set; }
    }
}
