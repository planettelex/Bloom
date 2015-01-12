using System;

namespace Bloom.Domain.Models
{
    public class Publication
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string WebsiteUrl { get; set; }
    }
}
