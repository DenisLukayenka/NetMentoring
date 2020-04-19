using System;

namespace Pacific.Web.Models.Logger
{
	public interface ILoggerManager
	{
		void Trace(string message);
		void Info(string message);
		void Warn(string message);
		void Error(Exception ex, string message);
		void Fatal(Exception ex, string message);
	}
}
