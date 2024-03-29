﻿using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the song_collaborator table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class SongCollaboratorTable : ISqlTable
    {
        /// <summary>
        /// Gets the create song_collaborator table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE song_collaborator (" +
                                   "song_id BLOB NOT NULL , " +
                                   "artist_id BLOB NOT NULL , " +
                                   "is_featured BOOL NOT NULL DEFAULT FALSE ," +
                                   "PRIMARY KEY (song_id, artist_id) , " +
                                   "FOREIGN KEY (song_id) REFERENCES song(id) , " +
                                   "FOREIGN KEY (artist_id) REFERENCES artist(id) )";
    }
}