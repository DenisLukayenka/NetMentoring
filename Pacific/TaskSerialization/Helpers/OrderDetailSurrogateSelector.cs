using System;
using System.Runtime.Serialization;
using TaskSerialization.DB;

namespace TaskSerialization.Helpers
{
	public class OrderDetailSurrogateSelector : ISurrogateSelector
	{
		private ISurrogateSelector _next;

		public void ChainSelector(ISurrogateSelector selector)
		{
			this._next = selector;
		}

		public ISurrogateSelector GetNextSelector()
		{
			return this._next;
		}

		public ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector selector)
		{
			selector = this;

			if(typeof(Order_Detail) == type)
			{
				return new OrderDetailSurrogate();
			}

			return null;
		}
	}
}
