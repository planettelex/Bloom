using System;
using System.Collections.Generic;

namespace Bloom.Domain.Models
{
    public class Label
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Bio { get; set; }

        public string LogoUrl { get; set; }

        public DateTime Founded { get; set; }

        public DateTime? Closed { get; set; }

        public List<LabelPersonel> Personel { get; set; }

        public List<AlbumRelease> Releases { get; set; } 
    }
}
