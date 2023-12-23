using ApiUtilities.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUtilities.Common.Handlers
{
	public class RequestHandler : IRequestHandler
	{
		private readonly IExceptionHandler _exceptionHandler;
		private readonly HttpClient _httpClient;
        public RequestHandler(IExceptionHandler exceptionHandler)
        {
			_exceptionHandler = exceptionHandler;
			_httpClient = new HttpClient();
		}

        public void AddHeader(string key, string value)
		{
			_httpClient.DefaultRequestHeaders.Add(key, value);
		}

		public async Task<string> GetAsync(string url)
		{
			using (HttpClient client = _httpClient)
			{
				try
				{
					HttpResponseMessage response = await client.GetAsync(url);
					response.EnsureSuccessStatusCode();
					return await response.Content.ReadAsStringAsync();
				}
				catch (HttpRequestException ex)
				{
					if (_exceptionHandler.CanHandle(ex))
					{
						_exceptionHandler.HandleException(ex);
						return null;
					}
                    else
                    {
						throw ex;
                    }
				}
			}
		}
	}
}
