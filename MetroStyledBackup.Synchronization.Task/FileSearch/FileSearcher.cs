﻿using System;
using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using MetroStyledBackup.Synchronization.Contracts.IO;

namespace MetroStyledBackup.Synchronization.Task.FileSearch
{
    /// <summary>
    /// Searches for files and directories that match a pattern.
    /// </summary>
    [Export(typeof(FileSearcher))]
    public class FileSearcher
    {
        /// <summary>
        /// The file access.
        /// </summary>
        private readonly IFileAccess _fileAccess;
        /// <summary>
        /// Determines whether the search has been canceled.
        /// </summary>
        private bool _canceled;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSearcher" /> class.
        /// </summary>
        /// <param name="fileAccess">The file access.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        [ImportingConstructor]
        public FileSearcher(IFileAccess fileAccess)
        {
            if(fileAccess == null) throw  new ArgumentNullException(nameof(fileAccess));
            _fileAccess = fileAccess;
        }

        /// <summary>
        /// Raised when a file is found.
        /// </summary>
        public event Action<string> FileFound;

        /// <summary>
        /// Raised when a directory is found.
        /// </summary>
        public event Action<string> DirectoryFound;

        /// <summary>
        /// Raised when search changes the current directory.
        /// </summary>
        public event Action<string> DirectoryChanged;

        public bool Search(string path, string filePattern, string directoryPattern, bool recursive)
        {
            _canceled = false;
            return this.SearchDirectory(path, filePattern, directoryPattern, recursive);
        }

        /// <summary>
        /// Stops the search.
        /// </summary>
        public void StopSearch()
        {
            this._canceled = true;
        }

        /// <summary>
        /// Executed when a file is found.
        /// </summary>
        /// <param name="file">The found file.</param>
        protected virtual void OnFileFound(string file)
        {
            FileFound?.Invoke(file);
        }

        /// <summary>
        /// Executed when a directory is found.
        /// </summary>
        /// <param name="directory">The found directory.</param>
        protected virtual void OnDirectoryFound(string directory)
        {
            DirectoryFound?.Invoke(directory);
        }

        /// <summary>
        /// Executed when search changes the current directory.
        /// </summary>
        /// <param name="directory">The directory.</param>
        protected virtual void OnDirectoryChanged(string directory)
        {
            DirectoryChanged?.Invoke(directory);
        }

        /// <summary>
        /// Searches in a directory for files and subdirectors that match a pattern if search has not been canceled.
        /// </summary>
        /// <param name="path">The path to search in.</param>
        /// <param name="filepattern">The pattern that the filename has to match.</param>
        /// <param name="dirpattern">The pattern that the name of the directory has to match.</param>
        /// <param name="recurse">Specifies whether the search should work recursivly.</param>
        /// <returns><c>true</c> if the search has not been canceled.</returns>
        private bool SearchDirectory(string path, string filePattern, string directoryPattern, bool recursive)
        {
            string[] files;
            string[] directories;

            if (_canceled) return false;

            files = _fileAccess.GetFilesInDirectory(path);
            directories = _fileAccess.GetSubdirectoriesInDirectory(path);

            foreach (string file in files)
            {
                if (_canceled) return false;
                if (Regex.IsMatch(file, filePattern, RegexOptions.IgnoreCase)) this.OnFileFound(file);
            }

            foreach (string directory in directories)
            {
                if (_canceled) return false;
                if (recursive)
                {
                    this.OnDirectoryChanged(directory);
                    this.SearchDirectory(directory, filePattern, directoryPattern, true);
                }
                if (Regex.IsMatch(directory, directoryPattern, RegexOptions.IgnoreCase) && recursive) this.OnDirectoryFound(directory);
            }
            return true;
        }

    }
}