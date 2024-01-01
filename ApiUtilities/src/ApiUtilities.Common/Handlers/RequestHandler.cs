using ApiUtilities.Common.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApiUtilities.Common.Handlers
{
	public class RequestHandler : IRequestHandler
	{
		private readonly IExceptionHandler _exceptionHandler;
		private HttpClient _httpClient;
		private Dictionary<string,string> _requestHeaders;
        public RequestHandler(IExceptionHandler exceptionHandler)
        {
			_exceptionHandler = exceptionHandler;
			_requestHeaders = new Dictionary<string, string>();
		}

        public void AddHeader(string key, string value)
		{
			_requestHeaders.TryAdd(key, value);
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

		public void RefreshHandler()
		{
			_httpClient = new HttpClient();
			foreach (var item in _requestHeaders)
			{
				_httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
			}
		}
	}
}
