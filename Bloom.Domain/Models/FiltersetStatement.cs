using System;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models
{
    public class FiltersetStatement
    {
        public Guid Id { get; set; }

        public Guid FilterId { get; set; }

        public IFilter Filter { get; set; }

        public FiltersetItemScope Scope { get; set; }

        public Comparison Comparison { get; set; }
    }
}
