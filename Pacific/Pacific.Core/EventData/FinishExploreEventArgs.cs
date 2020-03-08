using System;

namespace Pacific.Core.EventData
{
	public class FinishExploreEventArgs: EventArgs
	{
		public string StartPosition { get; set; }

		public int DirectoriesCount { get; set; }
		public int FilesCount { get; set; }
	}
}
