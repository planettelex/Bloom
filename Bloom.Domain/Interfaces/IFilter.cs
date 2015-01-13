using System;
using System.Collections.Generic;
using Bloom.Domain.Models;

namespace Bloom.Domain.Interfaces
{
    public interface IFilter
    {
        Guid Id { get; set; }

        string Name { get; set; }

        List<Song> Apply(List<Song> songs);

        List<Album> Apply(List<Album> albums);
    }
}
