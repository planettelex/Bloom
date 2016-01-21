﻿using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class PlaylistTable : ISqlTable
    {
        /// <summary>
        /// Gets the create playlist table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE playlist (" +
                       "id VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "name VARCHAR NOT NULL , " +
                       "description VARCHAR , " +
                       "length INTEGER NOT NULL DEFAULT 0 , " +
                       "created_on DATETIME NOT NULL , " +
                       "created_by_id VARCHAR(36) NOT NULL , " +
                       "FOREIGN KEY (created_by_id) REFERENCES person(id) )";
            }
        }
    }
}