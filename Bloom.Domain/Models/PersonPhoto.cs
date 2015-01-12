using System;

namespace Bloom.Domain.Models
{
    public class PersonPhoto
    {
        public Guid PersonId { get; set; }

        public Guid PhotoId { get; set; }

        public int Order { get; set; }
    }
}
