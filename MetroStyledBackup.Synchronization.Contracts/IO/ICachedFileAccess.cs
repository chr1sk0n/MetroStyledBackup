using System.Collections.Generic;

namespace MetroStyledBackup.Synchronization.Contracts.IO
{
    public interface ICachedFileAccess : IFileAccess
    {
        /// <summary>
        /// Gets a value indicating whether caching is available or not.
        /// </summary>
        /// <value>
        ///   <c>true</c> if caching is available; otherwise, <c>false</c>.
        /// </value>
        bool CacheAvailable { get; }

        /// <summary>
        /// Gets or sets a value indicating whether caching is currently active or not.
        /// </summary>
        /// <value>
        ///   <c>true</c> if caching is currently active; otherwise, <c>false</c>.
        /// </value>
        bool CacheActive { get; }

        /// <summary>
        /// Gets the cached directories.
        /// </summary>
        /// <value>
        /// The cached directories.
        /// </value>
        IEnumerable<ICachedDirectory> Directories { get; }

        /// <summary>
        /// Adds the given directory to the cache.
        /// </summary>
        /// <param name="directory">The directory.</param>
        void AddDirectory(ICachedDirectory directory);
    }
}