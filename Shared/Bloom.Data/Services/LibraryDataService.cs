using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.Data.Repositories;
using Bloom.Data.SeedData;
using Bloom.Domain.Models;

namespace Bloom.Data.Services
{
    /// <summary>
    /// Service for managing library SQL data.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.IDataService" />
    public class LibraryDataService : IDataService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryDataService" /> class.
        /// </summary>
        /// <param name="holidayRepository">The holiday repository.</param>
        /// <param name="timeSignatureRepository">The time signature repository.</param>
        public LibraryDataService(IHolidayRepository holidayRepository, ITimeSignatureRepository timeSignatureRepository)
        {
            _holidayRepository = holidayRepository;
            _holidayData = new HolidaySeedData();
            _timeSignatureRepository = timeSignatureRepository;
            _timeSignatureData = new TimeSignatureSeedData();
        }
        private readonly ISeedData<Holiday> _holidayData;
        private readonly IHolidayRepository _holidayRepository;
        private readonly ISeedData<TimeSignature> _timeSignatureData;
        private readonly ITimeSignatureRepository _timeSignatureRepository;

        /// <summary>
        /// Seeds the library SQL tables.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        public void SeedTables(IDataSource dataSource)
        {
            SeedHolidayTable(dataSource);
            SeedTimeSignatureTable(dataSource);
        }

        /// <summary>
        /// Seeds the holiday table.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        private void SeedHolidayTable(IDataSource dataSource)
        {
            var seedData = _holidayData.SeedData();
            if (seedData == null || !seedData.Any())
                return;

            foreach (var holiday in seedData)
                _holidayRepository.AddHoliday(dataSource, holiday);
        }

        /// <summary>
        /// Seeds the time signature table.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        private void SeedTimeSignatureTable(IDataSource dataSource)
        {
            var seedData = _timeSignatureData.SeedData();
            if (seedData == null || !seedData.Any())
                return;

            foreach (var timeSignature in seedData)
                _timeSignatureRepository.AddTimeSignature(dataSource, timeSignature);
        }
    }
}
