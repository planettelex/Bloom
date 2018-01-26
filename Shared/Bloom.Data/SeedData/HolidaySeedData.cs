using System;
using System.Collections.Generic;
using Bloom.Data.Interfaces;
using Bloom.Domain.Enums;
using Bloom.Domain.Models;

namespace Bloom.Data.SeedData
{
    /// <summary>
    /// Seed data for holidays. While most holidays occur on a single day, we set a range of dates as it is common to play holiday music in advance of the holiday itself.
    /// The holidays included here are ones that have major significant musical associations.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISeedData{Holiday}" />
    public class HolidaySeedData : ISeedData<Holiday>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HolidaySeedData"/> class.
        /// </summary>
        public HolidaySeedData()
        {
            _holidaySeedData = new List<Holiday>
            {
                Christmas,
                Hanukkah,
                NewYears,
                Halloween,
                IndependenceDay,
                StPatricksDay,
                ValentinesDay
            };
        }
        private readonly List<Holiday> _holidaySeedData;

        /// <summary>
        /// Lists the seed data for holidays.
        /// </summary>
        public List<Holiday> SeedData()
        {
            return _holidaySeedData;
        }

        #region Holidays

        /// <summary>
        /// The Christmas holiday.
        /// </summary>
        public Holiday Christmas => new Holiday
        {
            Id = Guid.Parse("e0aaf912-f1f2-4f46-ba46-ae758ba17cef"),
            Name = "Christmas",
            StartMonth = Month.December,
            StartDay = 10,
            EndMonth = Month.December,
            EndDay = 26,
            Description = "An annual festival commemorating the birth of Jesus, observed most commonly on December 25."
        };

        /// <summary>
        /// The Hanukkah holiday. The dates for this holiday vary on the Gregorian calendar, we'll have it last the month of December up until the 25th.
        /// </summary>
        public Holiday Hanukkah => new Holiday
        {
            Id = Guid.Parse("e23fed62-781f-4a24-9ea8-c2c05620b26d"),
            Name = "Hanukkah",
            StartMonth = Month.November,
            StartDay = 30,
            EndMonth = Month.December,
            EndDay = 25,
            Description = "A Jewish holiday commemorating the rededication of the Holy Temple (the Second Temple) in Jerusalem at the time of the Maccabean Revolt against the Seleucid Empire."
        };

        /// <summary>
        /// The New Years holiday.
        /// </summary>
        public Holiday NewYears => new Holiday
        {
            Id = Guid.Parse("abccf531-cd2b-485a-a1c2-9b09dc0ad503"),
            Name = "New Years",
            StartMonth = Month.December,
            StartDay = 31,
            EndMonth = Month.January,
            EndDay = 2,
            Description = "A holiday celebrating the year change of the Gregorian calendar"
        };

        /// <summary>
        /// The Halloween holiday.
        /// </summary>
        public Holiday Halloween => new Holiday
        {
            Id = Guid.Parse("d0ee7200-0c91-4313-a21a-4298bc4c720a"),
            Name = "Halloween",
            StartMonth = Month.October,
            StartDay = 20,
            EndMonth = Month.November,
            EndDay = 1,
            Description = "A celebration observed in a number of countries on 31 October dedicated to remembering the dead, including saints (hallows), martyrs, and all the faithful departed."
        };

        /// <summary>
        /// The Independence Day holiday.
        /// </summary>
        public Holiday IndependenceDay => new Holiday
        {
            Id = Guid.Parse("a5c46618-a627-4358-806e-7465829b8db8"),
            Name = "Independence Day",
            StartMonth = Month.July,
            StartDay = 1,
            EndMonth = Month.July,
            EndDay = 5,
            Description = "Also referred to as the Fourth of July, this is the US federal holiday commemorating the adoption of the Declaration of Independence 240 years ago in 1776 on July 4 by the Continental Congress."
        };

        /// <summary>
        /// The St. Patrick's Day holiday.
        /// </summary>
        public Holiday StPatricksDay => new Holiday
        {
            Id = Guid.Parse("085e701e-e021-4338-838e-7da4efacb3b7"),
            Name = "St. Patrick's Day",
            StartMonth = Month.March,
            StartDay = 12,
            EndMonth = Month.March,
            EndDay = 18,
            Description = "A cultural and religious celebration held on 17 March, the traditional death date of Saint Patrick, the foremost patron saint of Ireland."
        };

        /// <summary>
        /// The Valentine's Day holiday.
        /// </summary>
        public Holiday ValentinesDay => new Holiday
        {
            Id = Guid.Parse("ff9f739b-e315-4ac6-a8ca-5294f1e3758a"),
            Name = "Valentine's Day",
            StartMonth = Month.February,
            StartDay = 12,
            EndMonth = Month.February,
            EndDay = 16,
            Description = "An annual holiday celebrated on February 14 honoring one or more early saints named Valentinus. The day first became associated with romantic love within the circle of Geoffrey Chaucer in the 14th century, when the tradition of courtly love flourished."
        };

        #endregion
    }
}
