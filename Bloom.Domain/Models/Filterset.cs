using System;
using System.Collections.Generic;

namespace Bloom.Domain.Models
{
    public class Filterset
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<FiltersetElement> Elements { get; set; }

        public List<FiltersetOrdering> Ordering { get; set; } 
    }
}
