using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using Microsoft.Practices.Prism.Mvvm;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a recording label.
    /// </summary>
    [Table(Name = "label")]
    public class Label : BindableBase
    {
        /// <summary>
        /// Creates a new label instance.
        /// </summary>
        /// <param name="name">The label name.</param>
        public static Label Create(string name)
        {
            return new Label
            {
                Id = Guid.NewGuid(),
                Name = name
            };
        }

        /// <summary>
        /// Gets or sets the label identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the label name.
        /// </summary>
        [Column(Name = "name")]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        private string _name;

        /// <summary>
        /// Gets or sets the label's bio.
        /// </summary>
        [Column(Name = "bio")]
        public string Bio
        {
            get { return _bio; }
            set { SetProperty(ref _bio, value); }
        }
        private string _bio;

        /// <summary>
        /// Gets or sets the label logo file path.
        /// </summary>
        [Column(Name = "logo_file_path")]
        public string LogoFilePath { get; set; }

        /// <summary>
        /// Gets or sets the date on which the label was founded.
        /// </summary>
        [Column(Name = "founded_on")]
        public DateTime? FoundedOn { get; set; }

        /// <summary>
        /// Gets or sets the date the label was closed.
        /// </summary>
        [Column(Name = "closed_on")]
        public DateTime? ClosedOn { get; set; }

        /// <summary>
        /// Gets or sets the parent label identifier.
        /// </summary>
        [Column(Name = "parent_label_id")]
        public Guid? ParentLabelId { get; set; }

        /// <summary>
        /// Gets or sets the label personnel.
        /// </summary>
        public List<LabelPersonnel> Personnel { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            return Name;
        }
    }
}
