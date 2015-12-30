using System;
using System.Collections.Generic;

namespace MetroStyledBackup.Synchronization.Contracts.IO
{
    public interface ICachedDirectory
    {
        /// <summary>
        /// Gets the path of the directory.
        /// </summary>
        /// <value>
        /// The path of the directory.
        /// </value>
        string Path { get; }

        /// <summary>
        /// Gets the path of the parent directory.
        /// </summary>
        /// <value>
        /// The path of the parent directory.
        /// </value>
        string ParentPath { get; }

        /// <summary>
        /// Gets the last write times of files by path.
        /// </summary>
        /// <value>
        /// The last write times of files by path.
        /// </value>
        IDictionary<string, DateTime> LastWriteTimeOfFilesByPath { get; } 
    }
}