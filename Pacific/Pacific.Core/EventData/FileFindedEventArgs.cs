using System;
using System.IO;

namespace Pacific.Core.EventData
{
	public class FileFindedEventArgs: EventArgs
	{
		public FileInfo FileInfo { get; set; }
	}
}
