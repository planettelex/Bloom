using System;
using System.Data.Linq.Mapping;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a filterset order.
    /// </summary>
    [Table(Name = "filterset_order")]
    public class FiltersetOrder
    {
        /// <summary>
        /// Creates a new filterset order instance.
        /// </summary>
        /// <param name="filterset">A filterset.</param>
        /// <param name="order">The order.</param>
        /// <param name="orderNumber">The order number.</param>
        public static FiltersetOrder Create(Filterset filterset, IOrder order, int orderNumber)
        {
            return new FiltersetOrder
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
        public static FiltersetOrder Create(Filterset filterset, IOrder order, int orderNumber, OrderDirection direction)
        {
            return new FiltersetOrder
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
        public OrderDirection Direction { get; set; }

        /// <summary>
        /// Gets or sets the filterset order.
        /// </summary>
        public IOrder Order
        {
            get { return _order; }
            set
            {
                _order = value;
                OrderId = _order != null ? _order.Id : Guid.Empty;
            }
        }
        private IOrder _order;

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            return Order != null ? string.Format("{0} {1}", Order.ToString(), Direction) : Direction.ToString();
        }
    }
}
