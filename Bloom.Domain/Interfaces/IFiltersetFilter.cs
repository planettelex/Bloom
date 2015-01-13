using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Models;

namespace Bloom.Domain.Interfaces
{
    public interface IFiltersetFilter
    {
        Guid Id { get; set; }

        string Name { get; set; }

        List<Song> Apply(FiltersetItemScope scope, List<Song> songs, FilterComparison comparison);
    }
}
