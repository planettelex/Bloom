using System;
using System.Data.Linq.Mapping;
using Bloom.Domain.Enums;
using Microsoft.Practices.Prism.Mvvm;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a holiday.
    /// </summary>
    [Table(Name = "holiday")]
    public class Holiday : BindableBase
    {
        /// <summary>
        /// Creates a new holiday instance.
        /// </summary>
        /// <param name="name">The holiday name.</param>
        public static Holiday Create(string name)
        {
            return new Holiday
            {
                Id = Guid.NewGuid(),
                Name = name
            };
        }

        /// <summary>
        /// Gets or sets the holiday identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the holiday name.
        /// </summary>
        [Column(Name = "name")]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        private string _name;

        /// <summary>
        /// Gets or sets the holiday description.
        /// </summary>
        [Column(Name = "description")]
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }
        private string _description;

        /// <summary>
        /// Gets or sets the month the holiday starts.
        /// </summary>
        [Column(Name = "start_month")]
        public Month StartMonth
        {
            get { return _startMonth; }
            set { SetProperty(ref _startMonth, value); }
        }
        private Month _startMonth;

        /// <summary>
        /// Gets or sets the day the holiday starts.
        /// </summary>
        [Column(Name = "start_day")]
        public int StartDay
        {
            get { return _startDay; }
            set { SetProperty(ref _startDay, value); }
        }
        private int _startDay;

        /// <summary>
        /// Gets or sets the month the holiday ends.
        /// </summary>
        [Column(Name = "end_month")]
        public Month EndMonth
        {
            get { return _endMonth; }
            set { SetProperty(ref _endMonth, value); }
        }
        private Month _endMonth;

        /// <summary>
        /// Gets or sets the day the holiday ends.
        /// </summary>
        [Column(Name = "end_day")]
        public int EndDay
        {
            get { return _endDay; }
            set { SetProperty(ref _endDay, value); }
        }
        private int _endDay;
    }
}
