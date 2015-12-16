using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloom.Common;
using Bloom.Data.Interfaces;
using Bloom.State.Data.Tables;
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

        public Tab GetTab(Guid tabId)
        {
            var tabsQuery =
                from tabs in TabTable
                where tabs.Id == tabId
                select tabs;

            return tabsQuery.SingleOrDefault();
        }

        public List<Tab> ListTabs(ProcessType process, Guid userId)
        {
            var tabsQuery =
                from tabs in TabTable
                where tabs.Process == process && tabs.UserId == userId
                orderby tabs.Order
                select tabs;

            return tabsQuery.ToList();
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

            if (existingTab == null)
                TabTable.InsertOnSubmit(tab);
        }

        public void RemoveTab(Tab tab)
        {
            if (!_dataSource.IsConnected())
                return;

            TabTable.DeleteOnSubmit(tab);
        }
    }
}
