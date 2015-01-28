using System;

namespace Bloom.Domain.Models
{
    public class Library
    {
        public Guid Id { get; set; }

        public string FilePath { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid OwnerId { get; set; }

        public Person Owner { get; set; }
    }
}
