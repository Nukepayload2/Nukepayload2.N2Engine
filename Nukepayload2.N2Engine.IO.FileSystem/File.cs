using Nukepayload2.N2Engine.Platform;
using Nukepayload2.N2Engine.Storage;
using System;
using System.IO;
using System.Threading.Tasks;
using FileIO = System.IO.File;

namespace Nukepayload2.N2Engine.IO.FileSystem
{
    /// <summary>
    /// N2 引擎的文件实现。用于解决文件系统 API 不可移植的问题。
    /// </summary>
    [PlatformImpl(typeof(IFile))]
    internal class File : IFile
    {
        public File(string fileName)
        {
            FileName = fileName;
        }
        public string FileName { get; set; }

        public bool Exists()
        {
            return FileIO.Exists(FileName);
        }
        public async Task<Stream> CreateAsync()
        {
            return await Task.Run(() => FileIO.Create(FileName));
        }
        public async Task DeleteAsync()
        {
            await Task.Run(() => FileIO.Delete(FileName));
        }
        public async Task<Stream> OpenForReadAsync()
        {
            return await Task.Run(() => FileIO.OpenRead(FileName));
        }
        public async Task<Stream> OpenForWriteAsync()
        {
            return await Task.Run(() => FileIO.OpenWrite(FileName));
        }
        public async Task<byte[]> ReadAllBytesAsync()
        {
            return await Task.Run(() => FileIO.ReadAllBytes(FileName));
        }
        public async Task WriteAllBytesAsync(byte[] data)
        {
            await Task.Run(() => FileIO.WriteAllBytes(FileName, data));
        }
        public async Task<string> ReadAllTextAsync()
        {
            return await Task.Run(() => FileIO.ReadAllText(FileName));
        }
        public async Task WriteAllTextAsync(string data)
        {
            await Task.Run(() => FileIO.WriteAllText(FileName, data));
        }
        public async Task<string> ReadAllTextAsync(System.Text.Encoding encoding)
        {
            return await Task.Run(() => FileIO.ReadAllText(FileName, encoding));
        }
        public async Task WriteAllTextAsync(string data, System.Text.Encoding encoding)
        {
            await Task.Run(() => FileIO.WriteAllText(FileName, data, encoding));
        }
        public async Task MoveAsync(string target)
        {
            await Task.Run(() => FileIO.Move(FileName, target));
        }
        public async Task CopyAsync(string target)
        {
            await Task.Run(() => FileIO.Copy(FileName, target));
        }
        public async Task CopyAndOverwriteAsync(string target)
        {
            await Task.Run(() => FileIO.Copy(FileName, target, true));
        }
        public DateTime CreationTime
        {
            get { return FileIO.GetCreationTime(FileName); }
            set { FileIO.SetCreationTime(FileName, value); }
        }
        public DateTime CreationTimeUtc
        {
            get { return FileIO.GetCreationTimeUtc(FileName); }
            set { FileIO.SetCreationTimeUtc(FileName, value); }
        }
        public DateTime LastAccessTime
        {
            get { return FileIO.GetLastAccessTime(FileName); }
            set { FileIO.SetLastAccessTime(FileName, value); }
        }
        public DateTime LastAccessTimeUtc
        {
            get { return FileIO.GetLastAccessTimeUtc(FileName); }
            set { FileIO.SetLastAccessTimeUtc(FileName, value); }
        }
        public DateTime LastWriteTime
        {
            get { return FileIO.GetLastWriteTime(FileName); }
            set { FileIO.SetLastWriteTime(FileName, value); }
        }
        public DateTime LastWriteTimeUtc
        {
            get { return FileIO.GetLastWriteTimeUtc(FileName); }
            set { FileIO.SetLastWriteTimeUtc(FileName, value); }
        }
    }
}
