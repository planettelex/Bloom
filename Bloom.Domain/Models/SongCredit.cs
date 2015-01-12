using System;
using System.Collections.Generic;

namespace Bloom.Domain.Models
{
    public class SongCredit
    {
        public Guid Id { get; set; }

        public Guid SongId { get; set; }

        public Guid PersonId { get; set; }

        public Person Person { get; set; }

        public bool IsFeatured { get; set; }

        public List<SongCreditRole> Roles { get; set; } 
    }
}
