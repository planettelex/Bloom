using System;
using System.Collections.Generic;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for label data.
    /// </summary>
    public interface ILabelRepository
    {
        /// <summary>
        /// Gets the label.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="labelId">The label identifier.</param>
        Label GetLabel(IDataSource dataSource, Guid labelId);

        /// <summary>
        /// Lists the labels.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        List<Label> ListLabels(IDataSource dataSource);

        /// <summary>
        /// Adds a label.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="label">The label.</param>
        void AddLabel(IDataSource dataSource, Label label);

        /// <summary>
        /// Adds the label personnel.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="personnel">The label personnel.</param>
        void AddLabelPersonnel(IDataSource dataSource, LabelPersonnel personnel);

        /// <summary>
        /// Deletes the label personnel.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="personnel">The label personnel.</param>
        void DeleteLabelPersonnel(IDataSource dataSource, LabelPersonnel personnel);

        /// <summary>
        /// Adds the label personnel role.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="personnel">The label personnel.</param>
        /// <param name="role">The role.</param>
        void AddLabelPersonnelRole(IDataSource dataSource, LabelPersonnel personnel, Role role);

        /// <summary>
        /// Deletes the label personnel role.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="personnel">The label personnel.</param>
        /// <param name="role">The role.</param>
        void DeleteLabelPersonnelRole(IDataSource dataSource, LabelPersonnel personnel, Role role);

        /// <summary>
        /// Deletes a label.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="label">The label.</param>
        void DeleteLabel(IDataSource dataSource, Label label);
    }
}
