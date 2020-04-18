using System;
using System.Collections.Generic;
using System.Text;

namespace Pacific.ORM.HelpModels
{
	public class ReportOptions
	{
		public string CustomerId { get; set; }
		public DateTime? DateFrom { get; set; }
		public DateTime? DateTo { get; set; }
		public int? Take { get; set; }
		public int? Skip { get; set; }
	}
}
