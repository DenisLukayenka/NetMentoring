using System;

namespace Pacific.ADO.Extensions
{
	public static class DbValueExtensions
	{
		public static T ConvertFromDbValue<T>(this object obj)
		{
			if(obj == null || obj == DBNull.Value)
			{
				return default(T);
			}

			return (T)obj;
		}
	}
}
