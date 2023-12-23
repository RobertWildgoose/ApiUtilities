using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUtilities.Common.Interfaces
{
	public interface IRequestHandler
	{
		public void AddHeader(string key, string value);
		public Task<string> GetAsync(string url);

	}
}
