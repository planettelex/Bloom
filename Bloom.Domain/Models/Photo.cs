﻿using System;
using System.Collections.Generic;

namespace Bloom.Domain.Models
{
    public class Photo
    {
        public Guid Id { get; set; }

        public string Url { get; set; }

        public string Caption { get; set; }

        public List<PersonPhoto> People { get; set; }

        public List<ArtistPhoto> Artists { get; set; } 

        public DateTime Taken { get; set; }
    }
}
