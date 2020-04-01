using System;

namespace Pacific.Core.EventData
{
	public class StartExploreEventArgs: EventArgs
	{
		public string StartPosition { get; set; }
	}
}
