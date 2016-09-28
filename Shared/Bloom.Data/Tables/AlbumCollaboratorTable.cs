﻿using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the album_collaborator table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class AlbumCollaboratorTable : ISqlTable
    {
        /// <summary>
        /// Gets the create album_collaborator table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE album_collaborator (" +
                       "album_id VARCHAR(36) NOT NULL , " +
                       "artist_id VARCHAR(36) NOT NULL , " +
                       "is_featured BOOL NOT NULL DEFAULT FALSE ," +
                       "PRIMARY KEY (album_id, artist_id) , " +
                       "FOREIGN KEY (album_id) REFERENCES album(id) , " +
                       "FOREIGN KEY (artist_id) REFERENCES artist(id) )";
            }
        }
    }
}