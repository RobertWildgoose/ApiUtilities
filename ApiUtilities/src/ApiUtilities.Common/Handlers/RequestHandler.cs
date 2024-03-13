using ApiUtilities.Common.Interfaces;
using Newtonsoft.Json;
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
		private HttpClient _httpClient;
		private Dictionary<string,string> _requestHeaders;
        public RequestHandler()
        {
			_requestHeaders = new Dictionary<string, string>();
		}
		//TODO: Add In Attributed Headers 
		//Headers that are gained from api calls that need to be appended to the request through use.
        public void AddHeader(string key, string value)
		{
			_requestHeaders.TryAdd(key, value);
		}

		public async Task<string> GetAsync(string url, bool requireSuccess = true)
		{
			using (HttpClient client = _httpClient)
			{
				try
				{
					HttpResponseMessage response = await client.GetAsync(url);
					if (requireSuccess) { response.EnsureSuccessStatusCode(); }
					return await response.Content.ReadAsStringAsync();
				}
				catch (HttpRequestException ex)
				{
					throw ex;
				}
			}
		}

		public async Task<string> PostAsync(string url, string data, bool requireSuccess = true)
		{
			using (HttpClient client = _httpClient)
			{
				try
				{
					var content = new StringContent(data, Encoding.UTF8, "application/json");
					HttpResponseMessage response = await client.PostAsync(url,content);
					if (requireSuccess) { response.EnsureSuccessStatusCode(); }
					return await response.Content.ReadAsStringAsync();
				}
				catch (HttpRequestException ex)
				{
					throw ex;
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
