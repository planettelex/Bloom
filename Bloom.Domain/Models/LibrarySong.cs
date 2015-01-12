using System;
using System.Collections.Generic;

namespace Bloom.Domain.Models
{
    public class LibrarySong
    {
        public Guid SongId { get; set; }

        public Song Song { get; set; }

        public int Rating { get; set; }

        public string Notes { get; set; }

        public int PlayCount { get; set; }

        public int SkipCount { get; set; }

        public int RemoveCount { get; set; }

        public DateTime AddedOn { get; set; }

        public DateTime RatedOn { get; set; }

        public DateTime LastPlayed { get; set; }

        public Guid ReceivedFromId { get; set; }

        public Person ReceivedFrom { get; set; }

        public List<LibrarySongMedia> Media { get; set; } 
    }
}
