using System;
using System.Data.Linq;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.Domain.Enums;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for time signature data.
    /// </summary>
    public class TimeSignatureRepository : ITimeSignatureRepository
    {
        /// <summary>
        /// Gets a time signature.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="timeSignatureId">The time signature identifier.</param>
        public TimeSignature GetTimeSignature(IDataSource dataSource, Guid timeSignatureId)
        {
            if (!dataSource.IsConnected())
                return null;

            var timeSignatureTable = TimeSignatureTable(dataSource);
            if (timeSignatureTable == null)
                return null;

            var timeSignatureQuery =
                from t in timeSignatureTable
                where t.Id == timeSignatureId
                select t;

            return timeSignatureQuery.SingleOrDefault();
        }

        /// <summary>
        /// Gets a time signature.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="beatsPerMeasure">The beats per measure.</param>
        /// <param name="noteLength">The length of the note.</param>
        public TimeSignature GetTimeSignature(IDataSource dataSource, int beatsPerMeasure, BeatLength noteLength)
        {
            if (!dataSource.IsConnected())
                return null;

            var timeSignatureTable = TimeSignatureTable(dataSource);
            if (timeSignatureTable == null)
                return null;

            var timeSignatureQuery =
                from t in timeSignatureTable
                where t.BeatsPerMeasure == beatsPerMeasure && t.BeatLength == noteLength
                select t;

            return timeSignatureQuery.SingleOrDefault();
        }

        /// <summary>
        /// Adds a time signature.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="timeSignature">The time signature.</param>
        public void AddTimeSignature(IDataSource dataSource, TimeSignature timeSignature)
        {
            if (!dataSource.IsConnected())
                return;

            var timeSignatureTable = TimeSignatureTable(dataSource);
            if (timeSignatureTable == null)
                return;

            timeSignatureTable.InsertOnSubmit(timeSignature);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes a time signature.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="timeSignature">The time signature.</param>
        public void DeleteTimeSignature(IDataSource dataSource, TimeSignature timeSignature)
        {
            if (!dataSource.IsConnected())
                return;

            var timeSignatureTable = TimeSignatureTable(dataSource);
            if (timeSignatureTable == null)
                return;

            timeSignatureTable.DeleteOnSubmit(timeSignature);
            dataSource.Save();
        }

        #region Tables

        private static Table<TimeSignature> TimeSignatureTable(IDataSource dataSource)
        {
            return dataSource?.Context.GetTable<TimeSignature>();
        }

        #endregion
    }
}
