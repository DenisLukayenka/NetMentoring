using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using Pacific.Core.EventData;

namespace Pacific.Core.Services
{
    public class FileSystemVisitor
    {
        public event EventHandler<StartExploreEventArgs> OnStartExplore = delegate { };
        public event EventHandler<FinishExploreEventArgs> OnFinishExplore = delegate { };
        public event EventHandler<FileFindedEventArgs> OnFileFinded = delegate { };
        public event EventHandler<DirectoryFindedEventArgs> OnDirectoryFinded = delegate { };
        public event EventHandler<FileFindedEventArgs> OnFilteredFileFinded = delegate { };
        public event EventHandler<DirectoryFindedEventArgs> OnFilteredDirectoryFinded = delegate { };

        private Predicate<FileInfo> _fileFileter = (fi) => true;
        private Predicate<DirectoryInfo> _directoryFilter = (di) => true;

        private FileSystemEnumerator _fileSystemEnumerator;
        private int _filesCount;
        private int _directoryCount;
        private string _startPosition;
        private bool _enabled;

        public FileSystemVisitor() : this(@"D:\Cash")
        {
        }

        public FileSystemVisitor(string startPosition)
        {
            this._startPosition = startPosition;
            this._fileSystemEnumerator = new FileSystemEnumerator(startPosition);
            this.OnFileFinded += (sender, e) =>
            {
                this._filesCount++;
            };

            this.OnDirectoryFinded += (sender, e) =>
            {
                this._directoryCount++;
            };

            this._enabled = true;
        }

        public IEnumerable<FileSystemInfo> Explore()
        {
            this._fileSystemEnumerator.Reset();
            this.OnStartExplore(this, new StartExploreEventArgs { StartPosition = this._startPosition });

            while (this._enabled && this._fileSystemEnumerator.MoveNext())
            {
                if(this.ProcessFilesDirectoriesInfo(this._fileSystemEnumerator.Current))
                {
                    yield return this._fileSystemEnumerator.Current;
                }
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
            this._enabled = true;
        }

        public void Stop()
        {
            this._enabled = false;
        }

        private bool ProcessFilesDirectoriesInfo(dynamic currentInfo)
        {
            return this.ProcessFilesDirectoriesInfo(currentInfo);
        }

        private bool ProcessFilesDirectoriesInfo(FileInfo fileInfo)
        {
            this.OnFileFinded(this, new FileFindedEventArgs { FileInfo = fileInfo });
            if(this._fileFileter(fileInfo))
            {
                this.OnFilteredFileFinded(this, new FileFindedEventArgs { FileInfo = fileInfo });
                return true;
            }

            return false;
        }

        private bool ProcessFilesDirectoriesInfo(DirectoryInfo directoryInfo)
        {
            this.OnDirectoryFinded(this, new DirectoryFindedEventArgs { DirectoryInfo = directoryInfo });

            if(this._directoryFilter(directoryInfo))
            {
                this.OnFilteredDirectoryFinded(this, new DirectoryFindedEventArgs { DirectoryInfo = directoryInfo });
                return true;
            }

            return false;
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