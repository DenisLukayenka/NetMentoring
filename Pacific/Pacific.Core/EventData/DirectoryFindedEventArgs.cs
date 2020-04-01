using System;
using System.IO;

namespace Pacific.Core.EventData
{
	public class DirectoryFindedEventArgs: EventArgs
	{
		public DirectoryInfo DirectoryInfo { get; set; }
	}
}
