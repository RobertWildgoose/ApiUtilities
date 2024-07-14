using ApiUtilities.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUtilities.Tests.Configurations
{
	public class MockBaseConfiguration : IBaseConfiguration
	{
		public string BaseUrl { get; set; }
		public string AuthToken { get; set; }
		public IDictionary<string, string> Headers { get; set; }
		public int TimeoutSeconds { get; set; } = 30; // Default timeout of 30 seconds
	}
}
