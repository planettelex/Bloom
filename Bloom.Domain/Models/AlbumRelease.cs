using System;
using Bloom.Domain.Enums;

namespace Bloom.Domain.Models
{
    public class AlbumRelease
    {
        public Guid Id { get; set; }

        public Guid AlbumId { get; set; }

        public Album Album { get; set; }

        public MediaTypes MediaTypes { get; set; }

        public DigitalFormat? DigitalFormat { get; set; }

        public DateTime ReleaseDate { get; set; }

        public Guid LabelId { get; set; }

        public Label Label { get; set; }

        public string CatalogNumber { get; set; }

    }
}
