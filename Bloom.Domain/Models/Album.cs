using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;

namespace Bloom.Domain.Models
{
    public class Album
    {
        public Guid Id { get; set; }

        public Guid ArtistId { get; set; }

        public Artist Artist { get; set; }

        public string Name { get; set; }

        public string Edition { get; set; }

        public LengthType LengthType { get; set; }

        public int Length { get; set; }

        public string Description { get; set; }

        public string LinerNotes { get; set; }

        public bool IsLive { get; set; }

        public bool IsRemix { get; set; }

        public bool IsTribute { get; set; }

        public Guid TributeArtistId { get; set; }

        public Artist TributeArtist { get; set; }

        public bool IsSoundtrack { get; set; }

        public bool IsHoliday { get; set; }

        public Guid HolidayId { get; set; }

        public Holiday Holiday { get; set; }

        public bool IsBootleg { get; set; }

        public bool IsPromotional { get; set; }

        public bool IsCompilation { get; set; }

        public bool IsMixedArtist { get; set; }

        public bool IsSingleTrack { get; set; }

        public List<AlbumTrack> Tracks { get; set; } 

        public List<AlbumArtwork> Artwork { get; set; }

        public List<AlbumCredit> Credits { get; set; }

        public List<AlbumRelease> Releases { get; set; }

        public List<AlbumCollaborator> Collaborators { get; set; }

        public List<AlbumReference> References { get; set; } 

    }
}
