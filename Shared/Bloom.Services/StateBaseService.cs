using System;
using System.IO;
using Bloom.Common;
using Bloom.Data.Interfaces;
using Bloom.State.Data.Respositories;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.Services
{
    public class StateBaseService
    {
        protected IDataSource StateDataSource { get; set; }

        protected ISuiteStateRepository SuiteStateRepository { get; set; }

        protected ILibraryConnectionRepository LibraryConnectionRepository { get; set; }

        protected IEventAggregator EventAggregator { get; set; }

        protected SuiteState SuiteState { get; set; }

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

        public void RefreshStateOf(object toRefresh)
        {
            StateDataSource.Refresh(toRefresh);
        }

        public ProcessType LastProcessToAccessState()
        {
            var suiteState = SuiteStateRepository.GetSuiteState();
            if (suiteState == null || string.IsNullOrEmpty(suiteState.LastProcessAccess))
                return ProcessType.None;

            var process = new BloomProcess(suiteState.LastProcessAccess);

            return process.Type;
        }

        public void ChangeStateProcess(ProcessType processType)
        {
            var suiteState = SuiteStateRepository.GetSuiteState();
            var process = new BloomProcess(processType);
            suiteState.LastProcessAccess = process.Name;
            SaveState(); 
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
