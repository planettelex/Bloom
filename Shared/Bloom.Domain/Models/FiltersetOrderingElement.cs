using System;
using System.Data.Linq.Mapping;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a filterset order.
    /// </summary>
    [Table(Name = "filterset_order")] //TODO
    public class FiltersetOrderingElement
    {
        /// <summary>
        /// Creates a new filterset order instance.
        /// </summary>
        /// <param name="filterset">A filterset.</param>
        /// <param name="order">The order.</param>
        /// <param name="orderNumber">The order number.</param>
        public static FiltersetOrderingElement Create(Filterset filterset, IOrdering order, int orderNumber)
        {
            return new FiltersetOrderingElement
            {
                FiltersetId = filterset.Id,
                Order = order,
                OrderNumber = orderNumber
            };
        }

        /// <summary>
        /// Creates a new filterset order instance.
        /// </summary>
        /// <param name="filterset">A filterset.</param>
        /// <param name="order">The order.</param>
        /// <param name="orderNumber">The order number.</param>
        /// <param name="direction">The ordering direction.</param>
        public static FiltersetOrderingElement Create(Filterset filterset, IOrdering order, int orderNumber, OrderingDirection direction)
        {
            return new FiltersetOrderingElement
            {
                FiltersetId = filterset.Id,
                Order = order,
                OrderNumber = orderNumber,
                Direction = direction
            };
        }

        /// <summary>
        /// Gets or sets the filterset order identifier.
        /// </summary>
        [Column(Name = "filterset_id", IsPrimaryKey = true)]
        public Guid FiltersetId { get; set; }

        /// <summary>
        /// Gets or sets the filterset order number.
        /// </summary>
        [Column(Name = "order_number", IsPrimaryKey = true)]
        public int OrderNumber { get; set; }

        /// <summary>
        /// Gets or sets the order identifier.
        /// </summary>
        [Column(Name = "order_id")]
        public Guid OrderId { get; set; }

        /// <summary>
        /// Gets or sets the order direction.
        /// </summary>
        [Column(Name = "order_direction")]
        public OrderingDirection Direction { get; set; }

        /// <summary>
        /// Gets or sets the filterset order.
        /// </summary>
        public IOrdering Order
        {
            get { return _order; }
            set
            {
                _order = value;
                OrderId = _order?.Id ?? Guid.Empty;
            }
        }
        private IOrdering _order;

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            return Order != null ? $"{Order.Label} {Direction}" : Direction.ToString();
        }
    }
}
