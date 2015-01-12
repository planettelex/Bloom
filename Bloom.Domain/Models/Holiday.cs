using System;
using Bloom.Domain.Enums;

namespace Bloom.Domain.Models
{
    public class Holiday
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Month StartMonth { get; set; }

        public int StartDay { get; set; }

        public Month EndMonth { get; set; }

        public int EndDay { get; set; }
    }
}
