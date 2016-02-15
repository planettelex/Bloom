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
        /// Creates a new filterset ordering instance.
        /// </summary>
        /// <param name="filterset">The filterset.</param>
        /// <param name="order">The order.</param>
        /// <param name="priority">The ordering priority.</param>
        public static FiltersetOrder Create(Filterset filterset, IOrder order, int priority)
        {
            return new FiltersetOrder
            {
                FiltersetId = filterset.Id,
                OrderId = order.Id,
                Order = order,
                Priority = priority
            };
        }

        /// <summary>
        /// Creates a new filterset ordering instance.
        /// </summary>
        /// <param name="filterset">The filterset.</param>
        /// <param name="order">The order.</param>
        /// <param name="priority">The ordering priority.</param>
        /// <param name="direction">The ordering direction.</param>
        public static FiltersetOrder Create(Filterset filterset, IOrder order, int priority, OrderDirection direction)
        {
            return new FiltersetOrder
            {
                FiltersetId = filterset.Id,
                OrderId = order.Id,
                Order = order,
                Priority = priority,
                Direction = direction
            };
        }

        /// <summary>
        /// Gets or sets the filterset ordering identifier.
        /// </summary>
        [Column(Name = "filterset_id", IsPrimaryKey = true)]
        public Guid FiltersetId { get; set; }

        /// <summary>
        /// Gets or sets the filterset order number.
        /// </summary>
        [Column(Name = "order_number", IsPrimaryKey = true)]
        public int Priority { get; set; }

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
        public IOrder Order { get; set; }
    }
}
