using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Bloom.Common;
using Bloom.Data.Interfaces;
using Bloom.State.Domain.Models;

namespace Bloom.State.Data.Respositories
{
    public class TabRepository : ITabRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TabRepository"/> class.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        public TabRepository(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        private readonly IDataSource _dataSource;
        private Table<Tab> TabTable { get { return _dataSource.Context.GetTable<Tab>(); } }
        private Table<TabLibrary> TabLibraryTable { get { return _dataSource.Context.GetTable<TabLibrary>(); } } 

        /// <summary>
        /// Gets the tab.
        /// </summary>
        /// <param name="tabId">The tab identifier.</param>
        public Tab GetTab(Guid tabId)
        {
            if (!_dataSource.IsConnected())
                return null;

            var tabsQuery =
                from tabs in TabTable
                where tabs.Id == tabId
                select tabs;

            var result = tabsQuery.SingleOrDefault();

            if (result == null)
                return null;

            var tabLibrariesQuery =
                from tabLibraries in TabLibraryTable
                where tabLibraries.TabId == result.Id
                select tabLibraries;

            result.Libraries = tabLibrariesQuery.ToList();

            return result;
        }

        public List<Tab> ListTabs(ProcessType process, Guid userId)
        {
            if (!_dataSource.IsConnected())
                return null;

            var tabsQuery =
                from tabs in TabTable
                where tabs.Process == process && tabs.UserId == userId
                orderby tabs.Order
                select tabs;

            var results = tabsQuery.ToList();
            if (!results.Any())
                return null;

            foreach (var result in results)
            {
                var tab = result;
                var tabLibrariesQuery =
                    from tabLibraries in TabLibraryTable
                    where tabLibraries.TabId == tab.Id
                    select tabLibraries;

                result.Libraries = tabLibrariesQuery.ToList();
            }

            return results;
        }

        public void AddTab(Tab tab)
        {
            if (!_dataSource.IsConnected())
                return;

            var existingTabQuery =
                from tabs in TabTable
                where tabs.Id == tab.Id
                select tabs;

            var existingTab = existingTabQuery.SingleOrDefault();

            if (existingTab != null) 
                return;
            
            TabTable.InsertOnSubmit(tab);
            foreach (var tabLibrary in tab.Libraries)
                TabLibraryTable.InsertOnSubmit(tabLibrary);
        }

        public void DeleteTab(Tab tab)
        {
            if (!_dataSource.IsConnected())
                return;

            foreach (var tabLibrary in tab.Libraries)
                TabLibraryTable.DeleteOnSubmit(tabLibrary);

            TabTable.DeleteOnSubmit(tab);
        }
    }
}
