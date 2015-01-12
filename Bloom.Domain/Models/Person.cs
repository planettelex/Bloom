using System;
using System.Collections.Generic;

namespace Bloom.Domain.Models
{
    public class Person
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime Born { get; set; }

        public DateTime? Died { get; set; }

        public string Bio { get; set; }

        public string Twitter { get; set; }

        public List<PersonPhoto> Photos { get; set; }

        public List<ArtistMember> MemberOf { get; set; }

        public List<PersonReference> References { get; set; }
    }
}
