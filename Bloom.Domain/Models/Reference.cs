using System;
using System.Collections.Generic;

namespace Bloom.Domain.Models
{
    public class Reference
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string IconUrl { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public List<PersonReference> People { get; set; }

        public List<ArtistReference> Artists { get; set; } 
    }
}
