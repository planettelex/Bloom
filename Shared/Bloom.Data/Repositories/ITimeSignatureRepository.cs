using System;
using Bloom.Data.Interfaces;
using Bloom.Domain.Enums;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for time signature data.
    /// </summary>
    public interface ITimeSignatureRepository
    {
        /// <summary>
        /// Gets a time signature.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="timeSignatureId">The time signature identifier.</param>
        TimeSignature GetTimeSignature(IDataSource dataSource, Guid timeSignatureId);

        /// <summary>
        /// Gets a time signature.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="beatsPerMeasure">The beats per measure.</param>
        /// <param name="noteLength">The length of the note.</param>
        TimeSignature GetTimeSignature(IDataSource dataSource, int beatsPerMeasure, BeatLength noteLength);

        /// <summary>
        /// Adds a time signature.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="timeSignature">The time signature.</param>
        void AddTimeSignature(IDataSource dataSource, TimeSignature timeSignature);

        /// <summary>
        /// Deletes a time signature.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="timeSignature">The time signature.</param>
        void DeleteTimeSignature(IDataSource dataSource, TimeSignature timeSignature);
    }
}
