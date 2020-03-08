using Pacific.Core.EventData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Pacific.Core.Services
{
    public class FileSystemVisitor
    {
        public event EventHandler<StartExploreEventArgs> OnStartExplore = delegate { };
        public event EventHandler<FinishExploreEventArgs> OnFinishExplore = delegate { };
        public event EventHandler<FileFindedEventArgs> OnFileFinded = delegate { };
        public event EventHandler<DirectoryFindedEventArgs> OnDirectoryFinded = delegate { };

        private Predicate<FileInfo> _fileFileter = (fi) => true;
        private Predicate<DirectoryInfo> _directoryFilter = (di) => true;

        private FileSystemEnumerator _fileSystemEnumerator;
        private int _filesCount;
        private int _directoryCount;
        private string _startPosition;

        public FileSystemVisitor() : this(@"D:\Cash")
        {
        }

        public FileSystemVisitor(string startPosition)
        {
            this._fileSystemEnumerator = new FileSystemEnumerator(startPosition);
            this.OnFileFinded += (sender, e) =>
            {
                this._filesCount++;
            };

            this.OnDirectoryFinded += (sender, e) =>
            {
                this._directoryCount++;
            };
        }

        public IEnumerable<FileSystemInfo> Explore()
        {
            this._fileSystemEnumerator.Reset();
            this.OnStartExplore(this, new StartExploreEventArgs { StartPosition = this._startPosition });

            while (this._fileSystemEnumerator.MoveNext())
            {
                this.IncrementFilesDirectoriesCount(this._fileSystemEnumerator.Current);
                yield return this._fileSystemEnumerator.Current;
            }

            this.OnFinishExplore(this, new FinishExploreEventArgs 
            { 
                StartPosition = this._startPosition,
                DirectoriesCount = this._directoryCount,
                FilesCount = this._filesCount
            });
        }

        public void Reset()
        {
            this._filesCount = 0;
            this._directoryCount = 0;
        }

        private void IncrementFilesDirectoriesCount(dynamic currentInfo)
        {
            this.IncrementFilesDirectoriesCount(currentInfo);
        }

        private void IncrementFilesDirectoriesCount(FileInfo fileInfo)
        {
            this.OnFileFinded(this, new FileFindedEventArgs { FileInfo = fileInfo });
        }

        private void IncrementFilesDirectoriesCount(DirectoryInfo directoryInfo)
        {
            this.OnDirectoryFinded(this, new DirectoryFindedEventArgs { DirectoryInfo = directoryInfo });
        }

        private class FileSystemEnumerator : IEnumerator<FileSystemInfo>
        {
            private string _startPosition;
            private IEnumerator<FileSystemInfo> _dataEnumerator;

            public FileSystemEnumerator(string startPosition)
            {
                this._startPosition = startPosition;

                this.Reset();
            }

            public FileSystemInfo Current => this._dataEnumerator.Current;

            object IEnumerator.Current => this.Current;

            public bool MoveNext()
            {
                return this._dataEnumerator.MoveNext();
            }

            public void Reset()
            {
                this._dataEnumerator = this.InitializeFilesDirectoriesInfo(this._startPosition).GetEnumerator();
            }

            public void Dispose()
            {
            }

            private IEnumerable<FileSystemInfo> InitializeFilesDirectoriesInfo(string path)
            {
                var directoryInfo = new DirectoryInfo(path);

                return directoryInfo.EnumerateFileSystemInfos("*.*", SearchOption.AllDirectories);
            }
        }
    }
}