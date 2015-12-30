using Metro.Backup.Synchronization.Contracts.IO;

namespace Metro.Backup.Synchronization.Contracts.SyncModes
{
    public interface ISyncMode
    {
        /// <summary>
        /// Gets the name of the sync mode.
        /// </summary>
        /// <value>The name of the sync mode.</value>
        string Name { get; }

        /// <summary>
        /// Gets a value indicating whether the reference directory should be created.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if the reference directory should be created; otherwise, <c>false</c>.
        /// </value>
        bool CreateReferenceDirectory { get; }

        /// <summary>
        /// Gets a value indicating whether the target directory should be created.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if the target directory should be created; otherwise, <c>false</c>.
        /// </value>
        bool CreateTargetDirectory { get; }

        /// <summary>
        /// Gets the reference directory file access.
        /// </summary>
        /// <value>
        /// The reference directory file access.
        /// </value>
        IFileAccess ReferenceDirectoryFileAccess { get; }

        /// <summary>
        /// Gets the target directory file access.
        /// </summary>
        /// <value>
        /// The target directory file access.
        /// </value>
        ICachedFileAccess TargetDirectoryFileAccess { get; }


    }
}