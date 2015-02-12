﻿using System;
using System.Data.Linq.Mapping;
using Bloom.Domain.Enums;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a segment of a song.
    /// </summary>
    [Table(Name = "song_segment")]
    public class SongSegment
    {
        /// <summary>
        /// Gets or sets the song segment identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the song identifier.
        /// </summary>
        [Column(Name = "song_id")]
        public Guid SongId { get; set; }

        /// <summary>
        /// Gets or sets the song segment start time in miliseconds.
        /// </summary>
        [Column(Name = "start")]
        public int Start { get; set; }

        /// <summary>
        /// Gets or sets the song segment stop time in milliseconds.
        /// </summary>
        [Column(Name = "stop")]
        public int Stop { get; set; }

        /// <summary>
        /// Gets or sets the song segment musical key.
        /// </summary>
        [Column(Name = "key")]
        public MusicalKeys Key { get; set; }

        /// <summary>
        /// Gets or sets the time signature identifier.
        /// </summary>
        [Column(Name = "time_signature_id")]
        public Guid TimeSignatureId { get; set; }

        /// <summary>
        /// Gets or sets the song segment time signature.
        /// </summary>
        public TimeSignature TimeSignature { get; set; }
    }
}