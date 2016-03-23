using System.Collections.Generic;
using System.Data.Linq;
using Bloom.Data.Interfaces;
using Bloom.Data.Tables;

namespace Bloom.Data.Services
{
    /// <summary>
    /// Service for managing library SQL tables.
    /// </summary>
    public class LibraryTableService : ITableService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryTableService"/> class.
        /// </summary>
        public LibraryTableService()
        {
            _tables = new List<ISqlTable>
            {
                new ActivityTable(),
                new ArtistTable(),
                new FiltersetTable(),
                new GenreTable(),
                new HolidayTable(),
                new LabelTable(),
                new MoodTable(),
                new PersonTable(),
                new PhotoTable(),
                new SourceTable(),
                new ReferenceTable(),
                new RoleTable(),
                new TagTable(),
                new TimeSignatureTable(),
                new PersonPhotoTable(),
                new PersonReferenceTable(),
                new LabelPersonnelTable(),
                new LabelPersonnelRoleTable(),
                new ArtistMemberTable(),
                new ArtistMemberRoleTable(),
                new ArtistPhotoTable(),
                new ArtistReferenceTable(),
                new ReviewTable(),
                new SongTable(),
                new SongActivityTable(),
                new SongCollaboratorTable(),
                new SongCreditTable(),
                new SongCreditRoleTable(),
                new SongMoodTable(),
                new SongReferenceTable(),
                new SongReviewTable(),
                new SongSegmentTable(),
                new SongTagTable(),
                new SongMediaTable(),
                new RecordingSessionTable(),
                new AlbumTable(),
                new AlbumActivityTable(),
                new AlbumArtworkTable(),
                new AlbumCollaboratorTable(),
                new AlbumCreditTable(),
                new AlbumCreditRoleTable(),
                new AlbumMoodTable(),
                new AlbumReferenceTable(),
                new AlbumReleaseTable(),
                new AlbumReviewTable(),
                new AlbumTagTable(),
                new AlbumTrackTable(),
                new AlbumMediaTable(),
                new PlaylistTable(),
                new PlaylistActivityTable(),
                new PlaylistArtworkTable(),
                new PlaylistMoodTable(),
                new PlaylistReferenceTable(),
                new PlaylistTagTable(),
                new PlaylistTrackTable(),
                new FiltersetElementTable(),
                new FiltersetOrderTable(),
                new LibraryTable()
            };
        }
        private readonly List<ISqlTable> _tables;

        /// <summary>
        /// Creates the SQL tables.
        /// </summary>
        /// <param name="dataContext">A data context.</param>
        public void CreateTables(DataContext dataContext)
        {
            if (dataContext == null)
                return;

            foreach (var table in _tables)
                dataContext.ExecuteCommand(table.CreateSql);
        }
    }
}
