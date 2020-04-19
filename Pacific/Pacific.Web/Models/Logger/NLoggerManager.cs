using NLog;
using System;
namespace Pacific.Web.Models.Logger
{
	public class NLoggerManager : ILoggerManager
	{
		private static ILogger logger = LogManager.GetCurrentClassLogger();

		public void Error(Exception ex, string message)
		{
			logger.Error(ex, message);
		}

		public void Fatal(Exception ex, string message)
		{
			logger.Fatal(ex, message);
		}

		public void Info(string message)
		{
			logger.Info(message);
		}

		public void Trace(string message)
		{
			logger.Trace(message);
		}

		public void Warn(string message)
		{
			logger.Warn(message);
		}
	}
}
