using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.IO;
using System.Windows;
using Bloom.Controls;
using Bloom.Data;
using Bloom.Data.Interfaces;
using Microsoft.Practices.Prism.Mvvm;

namespace Bloom.State.Domain.Models
{
    /// <summary>
    /// The state of the browser application.
    /// </summary>
    [Table(Name = "browser_state")]
    public class BrowserState : BindableBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BrowserState"/> class.
        /// </summary>
        public BrowserState()
        {
            ProcessName = "Bloom.Browser";
            Connections = new Connections();
            LibraryDataSources = new Dictionary<Guid, IDataSource>();
            SkinName = Properties.Settings.Default.SkinName;
            WindowState = Properties.Settings.Default.WindowState;
            SidebarWidth = Properties.Settings.Default.SidebarWidth;
            Tabs = new List<Tab>();
            SelectedTabId = Guid.Empty;
        }

        /// <summary>
        /// Gets or sets the name of the process.
        /// </summary>
        [Column(Name = "process_name", IsPrimaryKey = true)]
        public string ProcessName { get; set; }

        /// <summary>
        /// Gets or sets the connections.
        /// </summary>
        public Connections Connections { get; set; }

        /// <summary>
        /// Gets or sets the library data sources.
        /// </summary>
        public Dictionary<Guid, IDataSource> LibraryDataSources { get; set; }

            /// <summary>
        /// Gets or sets the name of the skin.
        /// </summary>
        [Column(Name = "skin_name")]
        public string SkinName { get; set; }

        /// <summary>
        /// Gets or sets the state of the window.
        /// </summary>
        [Column(Name = "window_state")]
        public WindowState WindowState { get; set; }

        /// <summary>
        /// Gets or sets the width of the sidebar column.
        /// </summary>
        [Column(Name = "sidebar_width")]
        public int SidebarWidth
        {
            get { return _sidebarWidth; }
            set { SetProperty(ref _sidebarWidth, value); }
        }
        private int _sidebarWidth;

        /// <summary>
        /// Gets or sets the selected tab identifier.
        /// </summary>
        [Column(Name = "selected_tab_id")]
        public Guid SelectedTabId { get; set; }

        /// <summary>
        /// Gets or sets the tabs.
        /// </summary>
        public List<Tab> Tabs { get; set; }

        /// <summary>
        /// Resets the width of the sidebar to the default value.
        /// </summary>
        public void ResetSidebarWidth()
        {
            SidebarWidth = Properties.Settings.Default.SidebarWidth;
        }

        /// <summary>
        /// Connects the library data sources.
        /// </summary>
        public void ConnectLibraryDataSources()
        {
            if (Connections == null || Connections.LibraryConnections == null || Connections.LibraryConnections.Count == 0)
                return;

            LibraryDataSources.Clear();
            foreach (var libraryConnection in Connections.LibraryConnections)
            {
                if (File.Exists(libraryConnection.FilePath) && libraryConnection.IsConnected)
                    AddLibraryDataSource(libraryConnection);
            }
        }

        public void AddLibraryConnection(LibraryConnection libraryConnection)
        {
            if (Connections == null || Connections.LibraryConnections == null)
                return;

            if (!Connections.LibraryConnections.Contains(libraryConnection))
                Connections.LibraryConnections.Add(libraryConnection);

            if (File.Exists(libraryConnection.FilePath) && libraryConnection.IsConnected)
                AddLibraryDataSource(libraryConnection);
        }

        public void ConnectLibraryDataSource(LibraryConnection libraryConnection)
        {
            if (Connections == null || Connections.LibraryConnections == null)
                return;

            libraryConnection.IsConnected = true;
            if (!Connections.LibraryConnections.Contains(libraryConnection))
                Connections.LibraryConnections.Add(libraryConnection);

            if (File.Exists(libraryConnection.FilePath))
                AddLibraryDataSource(libraryConnection);
        }

        private void AddLibraryDataSource(LibraryConnection libraryConnection)
        {
            var libraryDataSource = new LibraryDataSource();
            libraryDataSource.Connect(libraryConnection.FilePath);
            libraryDataSource.RegisterRepositories();

            LibraryDataSources.Add(libraryConnection.LibraryId, libraryDataSource);
        }
    }
}
