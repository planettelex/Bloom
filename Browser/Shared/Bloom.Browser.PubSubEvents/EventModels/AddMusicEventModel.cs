using System;
using System.Collections.Generic;

namespace Bloom.Browser.PubSubEvents.EventModels
{
    public class AddMusicEventModel
    {        
        public string Source { get; set; }

        public string FromPath { get; set; }

        public bool CopyFiles { get; set; }

        public List<Guid> LibraryIds { get; set; } 
    }
}
