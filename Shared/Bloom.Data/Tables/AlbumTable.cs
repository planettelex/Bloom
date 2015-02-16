﻿using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class AlbumTable : ISqlTable
    {
        /// <summary>
        /// Gets the create album table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE album (" +
                       "id VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "artist_id VARCHAR(36) , " +
                       "name VARCHAR NOT NULL , " +
                       "edition VARCHAR , " +
                       "length_type INTEGER NOT NULL , " +
                       "length INTEGER NOT NULL , " +
                       "description VARCHAR , " +
                       "liner_notes VARCHAR , " +
                       "is_live BOOL NOT NULL DEFAULT FALSE , " +
                       "is_remix BOOL NOT NULL DEFAULT FALSE , " +
                       "is_tribute BOOL NOT NULL DEFAULT FALSE , " +
                       "tribute_artist_id VARCHAR(36) , " +
                       "is_soundtrack BOOL NOT NULL DEFAULT FALSE , " +
                       "is_holiday BOOL NOT NULL DEFAULT FALSE , " +
                       "holiday_id VARCHAR(36) , " +
                       "is_bootleg BOOL NOT NULL DEFAULT FALSE , " +
                       "is_promotional BOOL NOT NULL DEFAULT FALSE , " +
                       "is_compilation BOOL NOT NULL DEFAULT FALSE , " +
                       "is_mixed_artist BOOL NOT NULL DEFAULT FALSE , " +
                       "is_single_track BOOL NOT NULL DEFAULT FALSE , " +
                       "FOREIGN KEY (artist_id) REFERENCES artist(id) , " +
                       "FOREIGN KEY (tribute_artist_id) REFERENCES artist(id) , " +
                       "FOREIGN KEY (holiday_id) REFERENCES holiday(id) )";
            }
        }
    }
}