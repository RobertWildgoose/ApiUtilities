using System;
using System.Collections.Generic;
using System.Text;

namespace ApiUtilities.Common.Interfaces
{
	public interface IBaseConfiguration
	{
		string BaseUrl { get; }
		string AuthToken { get; }
		IDictionary<string,string> Headers { get; }
		int TimeoutSeconds { get; }
	}
}
