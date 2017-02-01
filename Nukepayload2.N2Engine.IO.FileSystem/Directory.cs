using Nukepayload2.N2Engine.Platform;
using Nukepayload2.N2Engine.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DirectoryIO = System.IO.Directory;

namespace Nukepayload2.N2Engine.IO.FileSystem
{
    /// <summary>
    /// N2 引擎的目录实现。用于解决文件系统 API 不可移植的问题。
    /// </summary>
    [PlatformImpl(typeof(IDirectory))]
    internal class Directory : IDirectory
    {
        public string DirectoryName { get; set; }
        public Directory(string dirName)
        {
            DirectoryName = dirName;
        }
        public bool Exists()
        {
            return DirectoryIO.Exists(DirectoryName);
        }
        public async Task DeleteAsync()
        {
            await Task.Run(() => DirectoryIO.Delete(DirectoryName));
        }
        public async Task CreateAsync()
        {
            await Task.Run(() => DirectoryIO.CreateDirectory(DirectoryName));
        }
        public async Task MoveAsync(string dest)
        {
            await Task.Run(() => DirectoryIO.Move(DirectoryName, dest));
        }
        public async Task<string[]> EnumerateDirectoriesAsync()
        {
            return await Task.Run(() => DirectoryIO.EnumerateDirectories(DirectoryName).ToArray());
        }
        public async Task<string[]> EnumerateDirectoriesAsync(string searchPattern)
        {
            return await Task.Run(() => DirectoryIO.EnumerateDirectories(DirectoryName, searchPattern).ToArray());
        }
        public async Task<string[]> EnumerateFilesAsync()
        {
            return await Task.Run(() => DirectoryIO.EnumerateFiles(DirectoryName).ToArray());
        }
        public async Task<string[]> EnumerateFilesAsync(string searchPattern)
        {
            return await Task.Run(() => DirectoryIO.EnumerateFiles(DirectoryName, searchPattern).ToArray());
        }

        public async Task<System.IO.Stream> OpenStreamForReadAsync(string fileName)
        {
            return await new File(System.IO.Path.Combine(DirectoryName, fileName)).OpenForReadAsync();
        }

        public async Task<System.IO.Stream> OpenStreamForWriteAsync(string fileName)
        {
            return await new File(System.IO.Path.Combine(DirectoryName, fileName)).OpenForWriteAsync();
        }

        public DateTime CreationTime
        {
            get { return DirectoryIO.GetCreationTime(DirectoryName); }
            set { DirectoryIO.SetCreationTime(DirectoryName, value); }
        }
        public DateTime CreationTimeUtc
        {
            get { return DirectoryIO.GetCreationTimeUtc(DirectoryName); }
            set { DirectoryIO.SetCreationTimeUtc(DirectoryName, value); }
        }
        public DateTime LastAccessTime
        {
            get { return DirectoryIO.GetLastAccessTime(DirectoryName); }
            set { DirectoryIO.SetLastAccessTime(DirectoryName, value); }
        }
        public DateTime LastAccessTimeUtc
        {
            get { return DirectoryIO.GetLastAccessTimeUtc(DirectoryName); }
            set { DirectoryIO.SetLastAccessTimeUtc(DirectoryName, value); }
        }
        public DateTime LastWriteTime
        {
            get { return DirectoryIO.GetLastWriteTime(DirectoryName); }
            set { DirectoryIO.SetLastWriteTime(DirectoryName, value); }
        }
        public DateTime LastWriteTimeUtc
        {
            get { return DirectoryIO.GetLastWriteTimeUtc(DirectoryName); }
            set { DirectoryIO.SetLastWriteTimeUtc(DirectoryName, value); }
        }
    }
}
