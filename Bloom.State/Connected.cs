using System.Collections.Generic;
using Bloom.Domain.Models;

namespace Bloom.State
{
    public class Connected
    {
        public string StateFilePath { get; set; }

        public List<Library> Libraries { get; set; }
    }
}
