using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Bloom.Data;
using Microsoft.Practices.Prism.Mvvm;

namespace Bloom.State.Domain.Models
{
    /// <summary>
    /// Application state.
    /// </summary>
    public class ApplicationState : BindableBase
    {
        /// <summary>
        /// Gets or sets the name of the process.
        /// </summary>
        public string ProcessName { get; set; }

        /// <summary>
        /// Gets or sets the library connections.
        /// </summary>
        public List<LibraryConnection> Connections { get; set; }

        /// <summary>
        /// Gets or sets the current user.
        /// </summary>
        public User CurrentUser { get; set; }

        /// <summary>
        /// Gets or sets the name of the skin.
        /// </summary>
        public string SkinName { get; set; }

        /// <summary>
        /// Gets or sets the state of the window.
        /// </summary>
        public WindowState WindowState { get; set; }

        /// <summary>
        /// Gets the library data source.
        /// </summary>
        /// <param name="libraryId">The library identifier.</param>
        public LibraryDataSource GetLibraryDataSource(Guid libraryId)
        {
            if (Connections == null || Connections.Count == 0)
                return null;

            var connection = Connections.SingleOrDefault(c => c.LibraryId == libraryId);

            return connection == null ? null : connection.DataSource;
        }
    }
}
