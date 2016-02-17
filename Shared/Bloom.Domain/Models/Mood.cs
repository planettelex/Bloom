﻿using System;
using System.Data.Linq.Mapping;
using Microsoft.Practices.Prism.Mvvm;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a mood (e.g. Happy, Excited, Brooding)
    /// </summary>
    [Table(Name = "mood")]
    public class Mood : BindableBase
    {
        /// <summary>
        /// Creates a new mood instance.
        /// </summary>
        /// <param name="name">The mood name.</param>
        public static Mood Create(string name)
        {
            return new Mood
            {
                Id = Guid.NewGuid(),
                Name = name
            };
        }

        /// <summary>
        /// Gets or sets the mood identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the mood name.
        /// </summary>
        [Column(Name = "name")]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        private string _name;
    }
}
