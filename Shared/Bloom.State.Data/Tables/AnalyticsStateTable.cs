﻿using Bloom.Data.Interfaces;

namespace Bloom.State.Data.Tables
{
    /// <summary>
    /// Represents the analytics_state table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class AnalyticsStateTable : ISqlTable
    {
        /// <summary>
        /// Gets the create analytics_state table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE analytics_state (" +
                                   "process_name VARCHAR NOT NULL , " +
                                   "person_id VARCHAR(36) NOT NULL , " +
                                   "skin_name VARCHAR NOT NULL , " +
                                   "window_state INTEGER NOT NULL , " +
                                   "sidebar_visible BOOL NOT NULL DEFAULT FALSE , " +
                                   "sidebar_width INTEGER NOT NULL , " +
                                   "selected_tab_id VARCHAR(36) , " +
                                   "PRIMARY KEY (process_name, person_id) , " +
                                   "FOREIGN KEY (selected_tab_id) REFERENCES tab(id) )";
    }
}
