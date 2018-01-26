using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for holiday data.
    /// </summary>
    public class HolidayRepository : IHolidayRepository
    {
        /// <summary>
        /// Gets the holiday.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="holidayId">The holiday identifier.</param>
        public Holiday GetHoliday(IDataSource dataSource, Guid holidayId)
        {
            if (!dataSource.IsConnected())
                return null;

            var holidayTable = HolidayTable(dataSource);
            if (holidayTable == null)
                return null;

            var holidayQuery =
                from h in holidayTable
                where h.Id == holidayId
                select h;

            return holidayQuery.SingleOrDefault();
        }

        /// <summary>
        /// Lists the holidays.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        public List<Holiday> ListHolidays(IDataSource dataSource)
        {
            if (!dataSource.IsConnected())
                return null;

            var holidayTable = HolidayTable(dataSource);
            if (holidayTable == null)
                return null;

            var holidaysQuery =
                from h in holidayTable
                orderby h.Name
                select h;

            return holidaysQuery.ToList();
        }

        /// <summary>
        /// Adds a holiday.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="holiday">The holiday.</param>
        public void AddHoliday(IDataSource dataSource, Holiday holiday)
        {
            if (!dataSource.IsConnected())
                return;

            var holidayTable = HolidayTable(dataSource);
            if (holidayTable == null)
                return;

            holidayTable.InsertOnSubmit(holiday);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes a holiday.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="holiday">The holiday.</param>
        public void DeleteHoliday(IDataSource dataSource, Holiday holiday)
        {
            if (!dataSource.IsConnected())
                return;

            var holidayTable = HolidayTable(dataSource);
            if (holidayTable == null)
                return;

            holidayTable.DeleteOnSubmit(holiday);
            dataSource.Save();
        }

        #region Tables

        private static Table<Holiday> HolidayTable(IDataSource dataSource)
        {
            return dataSource?.Context.GetTable<Holiday>();
        }

        #endregion
    }
}
