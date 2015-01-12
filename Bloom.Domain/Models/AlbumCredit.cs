using System;
using System.Collections.Generic;

namespace Bloom.Domain.Models
{
    public class AlbumCredit
    {
        public Guid Id { get; set; }

        public Guid AlbumId { get; set; }

        public Guid PersonId { get; set; }

        public Person Person { get; set; }

        public List<AlbumCreditRole> Roles { get; set; } 
    }
}
