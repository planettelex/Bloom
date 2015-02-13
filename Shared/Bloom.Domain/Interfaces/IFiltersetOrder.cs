using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Models;

namespace Bloom.Domain.Interfaces
{
    public interface IFiltersetOrder
    {
        Guid Id { get; set; }

        string Name { get; set; }

        string Label { get; set; }

        void Apply(FiltersetItemScope scope, ref List<Song> songs, OrderDirection direction);
    }
}
