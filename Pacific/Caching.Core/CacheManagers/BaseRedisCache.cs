using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace Caching.Core.CacheManagers
{
	public abstract class BaseRedisCache<T> : IItemCache<T>, IDisposable
	{
		private ConnectionMultiplexer _redisConnection;
		private readonly DataContractSerializer _serializer;

		public BaseRedisCache(string hostName)
		{
			this._redisConnection = ConnectionMultiplexer.Connect(hostName);
			this._serializer = new DataContractSerializer(typeof(IEnumerable<T>));
		}

		protected abstract string KeyPrefix { get; }

		public virtual IEnumerable<T> Get(string key)
		{
			var db = this._redisConnection.GetDatabase();
			byte[] value = db.StringGet(this.KeyPrefix + key);

			if (value is null)
			{
				return null;
			}

			return (IEnumerable<T>)this._serializer.ReadObject(new MemoryStream(value));
		}

		public virtual void Set(string key, IEnumerable<T> items)
		{
			var db = this._redisConnection.GetDatabase();
			var redisKey = this.KeyPrefix + key;

			if (items == null)
			{
				db.StringSet(redisKey, RedisValue.Null);
			}
			else
			{
				var memoryStream = new MemoryStream();
				this._serializer.WriteObject(memoryStream, items);
				db.StringSet(redisKey, memoryStream.ToArray(), this.GetCacheExpireTime());
			}
		}

		protected virtual TimeSpan GetCacheExpireTime()
		{
			return new TimeSpan().Add(TimeSpan.FromSeconds(10));
		}

		public void Dispose()
		{
			this._redisConnection.Dispose();
		}

		public bool Contains(string key)
		{
			throw new NotImplementedException();
		}
	}
}
