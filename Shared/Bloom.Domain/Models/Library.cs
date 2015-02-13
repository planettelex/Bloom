using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a music library.
    /// </summary>
    [Table(Name = "library")]
    public class Library
    {
        /// <summary>
        /// Creates a new library instance.
        /// </summary>
        /// <param name="owner">The library owner.</param>
        /// <param name="name">The library name.</param>
        /// <param name="folderPath">The library folder path.</param>
        public static Library Create(Person owner, string name, string folderPath)
        {
            var library = new Library
            {
                Id = Guid.NewGuid(),
                Name = name,
                FolderPath = folderPath,
                FileName = name + ".blm",
                OwnerId = owner.Id,
                Owner = owner
            };
            return library;
        }

        /// <summary>
        /// Gets or sets the library identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the library name.
        /// </summary>
        [Column(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the folder path.
        /// </summary>
        [Column(Name = "folder_path")]
        public string FolderPath { get; set; }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        [Column(Name = "file_name")]
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the owner identifier.
        /// </summary>
        [Column(Name = "owner_id")]
        public Guid OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the identifer owner.
        /// </summary>
        public Person Owner { get; set; }

        /// <summary>
        /// Gets the file path.
        /// </summary>
        public string FilePath
        {
            get
            {
                var filePath = FolderPath.TrimEnd('\\') + "\\" + FileName;
                return filePath.ToLower();
            }
        }
    }
}
