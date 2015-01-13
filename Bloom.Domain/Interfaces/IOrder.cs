using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Models;

namespace Bloom.Domain.Interfaces
{
    public interface IOrder
    {
        Guid Id { get; set; }

        string Name { get; set; }

        OrderDirection Direction { get; set; }

        List<Song> Apply(List<Song> songs);

        List<Album> Apply(List<Album> albums);
    }
}
