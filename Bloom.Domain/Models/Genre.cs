using System;

namespace Bloom.Domain.Models
{
    public class Genre
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid ParentGenreId { get; set; }

        public Genre ParentGenre { get; set; }
    }
}
