using System;

namespace MetroStyledBackup.Synchronization.Contracts.IO
{
    public interface IFileAccess
    {
        bool DoesFileExists(string path);

        bool DoesDirectoryExits(string path);

        DateTime GetLastWriteTime(string path);

        void CopyFile(string source, string target);

        void DeleteFile(string path);

        void CreateDirectory(string path);

        void DeleteDirectory(string path);

        string[] GetFilesInDirectory(string path);

        string[] GetSubdirectoriesInDirectory(string path);
    }
}