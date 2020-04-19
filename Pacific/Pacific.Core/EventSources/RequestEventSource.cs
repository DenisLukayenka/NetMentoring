using System;
using System.Diagnostics.Tracing;
using System.Threading;

namespace Pacific.Core.EventSources
{
	[EventSource(Name = "RequestsStatistic")]
	public sealed class RequestEventSource: EventSource
	{
		private static RequestEventSource instance;

		private long _requestCountValue;
		private PollingCounter _requestCount;
		private IncrementingPollingCounter _requestCountDelta;

		private long _requestForReportCountValue;
		private PollingCounter _requestForReportCount;
		private EventCounter _reportGenerationDuration;

		private long _requestToDbCountValue;
		private PollingCounter _requestToDbCount;
		private EventCounter _requestToDbDuration;

		public static RequestEventSource Instance
		{
			get
			{
				if(instance == null)
				{
					instance = new RequestEventSource();
				}

				return instance;
			}
		}

		private RequestEventSource()
			: base(EventSourceSettings.EtwSelfDescribingEventFormat)
		{
			this._requestCount = new PollingCounter("request-count", this, () => this._requestCountValue) 
			{ 
				DisplayName = "All Requests count" 
			};
			this._requestCountDelta = new IncrementingPollingCounter("request-count-delta", this, () => this._requestCountValue)
			{
				DisplayName = "New requests",
				DisplayRateTimeScale = new TimeSpan(0, 0, 1)
			};

			this._requestForReportCount = new PollingCounter("report-request-count", this, () => this._requestForReportCountValue)
			{
				DisplayName = "Request for report count"
			};
			this._reportGenerationDuration = new EventCounter("report-generation-duration", this)
			{
				DisplayName = "Duration to generate new order report"
			};

			this._requestToDbCount = new PollingCounter("report-db-Count", this, () => this._requestToDbCountValue)
			{
				DisplayName = "Request to database count"
			};
			this._requestToDbDuration = new EventCounter("report-db-duration", this)
			{
				DisplayName = "Duration to perform request to db"
			};
		}

		public void AddReportGenerationDuration(long elapsedMilliseconds)
		{
			this.IncrementCounter(ref this._requestCountValue);
			this.IncrementCounter(ref this._requestForReportCountValue);

			this._reportGenerationDuration.WriteMetric(elapsedMilliseconds);
			WriteEvent(1, elapsedMilliseconds);
		}

		public void AddRequestToDb(long elapsedMilliseconds)
		{
			this.IncrementCounter(ref this._requestToDbCountValue);

			this._requestToDbDuration.WriteMetric(elapsedMilliseconds);
			WriteEvent(2, elapsedMilliseconds);
		}

		public void AddRequest()
		{
			this.IncrementCounter(ref this._requestCountValue);
			WriteEvent(3);
		}

		private void IncrementCounter(ref long countValue)
		{
			Interlocked.Increment(ref countValue);
		}
	}
}
