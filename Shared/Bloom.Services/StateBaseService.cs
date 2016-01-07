using System;
using System.IO;
using Bloom.Data.Interfaces;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.Services
{
    public class StateBaseService
    {
        protected IDataSource StateDataSource { get; set; }

        protected IEventAggregator EventAggregator { get; set; }

        protected ApplicationState State { get; set; }

        /// <summary>
        /// Connects the data source.
        /// </summary>
        public void ConnectDataSource()
        {
            if (StateDataSource == null)
                throw new InvalidOperationException("Cannot connect to a null state data source.");

            if (string.IsNullOrEmpty(StateDataSource.FilePath))
                throw new InvalidOperationException("The file path to the state database file has not been specified.");

            if (File.Exists(StateDataSource.FilePath))
                StateDataSource.Connect();
            else
                StateDataSource.Create();
        }

        /// <summary>
        /// Saves the application state.
        /// </summary>
        public virtual void SaveState()
        {
            if (State.User != null)
                StateDataSource.Save();
        }
    }
}
