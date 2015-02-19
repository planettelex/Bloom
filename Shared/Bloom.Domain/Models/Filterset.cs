using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a set of filters and orders on a library.
    /// </summary>
    [Table(Name = "filterset")]
    public class Filterset
    {
        /// <summary>
        /// Creates a new filterset instance.
        /// </summary>
        /// <returns></returns>
        public static Filterset Create()
        {
            return new Filterset
            {
                Id = Guid.NewGuid()
            };
        }
        
        /// <summary>
        /// Gets or sets the filterset identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the filterset name.
        /// </summary>
        [Column(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the elements of the filterset.
        /// </summary>
        public List<FiltersetElement> Elements { get; set; }

        #region AddElement

        /// <summary>
        /// Creates and adds an element to this filterset.
        /// </summary>
        /// <param name="elementType">Type of the element.</param>
        /// <param name="elementNumber">The element number.</param>
        /// <returns>A new filterset element.</returns>
        public FiltersetElement AddElement(FiltersetElementType elementType, int elementNumber)
        {
            if (Elements == null)
                Elements = new List<FiltersetElement>();

            var filtersetElement = FiltersetElement.Create(this, elementType, elementNumber);
            Elements.Add(filtersetElement);

            return filtersetElement;
        }

        /// <summary>
        /// Creates and adds an element to this filterset.
        /// </summary>
        /// <param name="statement">A filterset statement.</param>
        /// <param name="elementNumber">The element number.</param>
        /// <returns>A new filterset element.</returns>
        /// <exception cref="System.ArgumentNullException">statement</exception>
        public FiltersetElement AddElement(FiltersetStatement statement, int elementNumber)
        {
            if (statement == null)
                throw new ArgumentNullException("statement");

            if (Elements == null)
                Elements = new List<FiltersetElement>();

            var filtersetElement = FiltersetElement.Create(this, statement, elementNumber);
            Elements.Add(filtersetElement);

            return filtersetElement;
        }

        #endregion

        /// <summary>
        /// Gets or sets the ordering of the filterset.
        /// </summary>
        public List<FiltersetOrdering> Ordering { get; set; }

        #region AddOrdering

        /// <summary>
        /// Creates and adds an ordering to this filterset.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <param name="priority">The ordering priority.</param>
        /// <param name="scope">The ordering scope.</param>
        /// <returns>A new filterset ordering.</returns>
        /// <exception cref="System.ArgumentNullException">order</exception>
        public FiltersetOrdering AddOrdering(IFiltersetOrder order, int priority, FiltersetItemScope scope)
        {
            if (order == null)
                throw new ArgumentNullException("order");

            if (Ordering == null)
                Ordering = new List<FiltersetOrdering>();

            var filtersetOrdering = FiltersetOrdering.Create(this, order, priority, scope);
            Ordering.Add(filtersetOrdering);

            return filtersetOrdering;
        }

        /// <summary>
        /// Creates and adds an ordering to this filterset.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <param name="priority">The ordering priority.</param>
        /// <param name="scope">The ordering scope.</param>
        /// <param name="direction">The ordering direction.</param>
        /// <returns>A new filterset ordering.</returns>
        /// <exception cref="System.ArgumentNullException">order</exception>
        public FiltersetOrdering AddOrdering(IFiltersetOrder order, int priority, FiltersetItemScope scope, OrderDirection direction)
        {
            if (order == null)
                throw new ArgumentNullException("order");

            if (Ordering == null)
                Ordering = new List<FiltersetOrdering>();

            var filtersetOrdering = FiltersetOrdering.Create(this, order, priority, scope, direction);
            Ordering.Add(filtersetOrdering);

            return filtersetOrdering;
        }

        #endregion
    }
}
