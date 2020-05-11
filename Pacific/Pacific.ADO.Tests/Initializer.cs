using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Pacific.ADO.Tests
{
	public class Initializer
	{
		static Initializer()
		{
			DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);
		}
	}
}
