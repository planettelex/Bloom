﻿using System;
using System.Collections.Generic;

namespace Bloom.Browser.LibraryModule.Services
{
    /// <summary>
    /// Service interface for library import operations.
    /// </summary>
    public interface IImportService
    {
        /// <summary>
        /// Imports music files at the provided folder to the specified libraries.
        /// </summary>
        /// <param name="folderPath">The folder path.</param>
        /// <param name="libraryIds">The library identifiers to import to.</param>
        /// <param name="copyFiles">If set to <c>true</c> copy media files.</param>
        void ImportFiles(string folderPath, List<Guid> libraryIds, bool copyFiles);

        /// <summary>
        /// Determines whether this instance is currently running an import.
        /// </summary>
        bool IsRunning();
    }
}
