using System;

namespace Bloom.Domain.Models
{
    public class PersonReference
    {
        public Guid PersonId { get; set; }

        public Guid ReferenceId { get; set; }

        public Reference Reference { get; set; }
    }
}
