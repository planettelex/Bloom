using System;
using System.Collections.Generic;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for holiday data.
    /// </summary>
    public interface IHolidayRepository
    {
        /// <summary>
        /// Gets the holiday.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="holidayId">The holiday identifier.</param>
        Holiday GetHoliday(IDataSource dataSource, Guid holidayId);

        /// <summary>
        /// Lists the holidays.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        List<Holiday> ListHolidays(IDataSource dataSource);

        /// <summary>
        /// Adds a holiday.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="holiday">The holiday.</param>
        void AddHoliday(IDataSource dataSource, Holiday holiday);

        /// <summary>
        /// Deletes a holiday.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="holiday">The holiday.</param>
        void DeleteHoliday(IDataSource dataSource, Holiday holiday);
    }
}
