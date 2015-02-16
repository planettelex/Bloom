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
                new ArtistTable(),
                new ArtistMemberTable(),
                new ArtistMemberRoleTable(),
                new ArtistReferenceTable(),
                new FiltersetTable(),
                new FiltersetElementTable(),
                new FiltersetStatementTable(),
                new FiltersetOrderingTable(),
                new GenreTable(),
                new HolidayTable(),
                new LabelTable(),
                new LabelPersonelTable(),
                new LabelPersonelRoleTable(),
                new LibraryTable(),
                new LibraryAlbumTable(),
                new LibraryAlbumMediaTable(),
                new LibraryArtistTable(),
                new LibraryPersonTable(),
                new LibraryPlaylistTable(),
                new LibrarySongTable(),
                new LibrarySongMediaTable(),
                new MoodTable(),
                new PersonTable(),
                new PersonPhotoTable(),
                new PersonReferenceTable(),
                new PhotoTable(),
                new PlaylistTable(),
                new PlaylistActivityTable(),
                new PlaylistArtworkTable(),
                new PlaylistMoodTable(),
                new PlaylistReferenceTable(),
                new PlaylistTagTable(),
                new PlaylistTrackTable(),
                new PublicationTable(),
                new RecordingSessionTable(),
                new ReferenceTable(),
                new ReviewTable(),
                new RoleTable(),
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
                new TagTable(),
                new TimeSignatureTable()
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
