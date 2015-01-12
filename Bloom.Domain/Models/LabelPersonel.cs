using System;
using System.Collections.Generic;

namespace Bloom.Domain.Models
{
    public class LabelPersonel
    {
        public Guid Id { get; set; }

        public Guid LabelId { get; set; }

        public Guid PersonId { get; set; }

        public Person Person { get; set; }

        public List<LabelPersonelRole> Roles { get; set; } 
    }
}
