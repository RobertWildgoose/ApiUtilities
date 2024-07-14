using ApiUtilities.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApiUtilities.Common.Services
{
	public class HttpClientService : IHttpClientService
	{
		private readonly IBaseConfiguration _config;
		private readonly HttpClient _httpClient;
		public HttpClientService(IBaseConfiguration config)
        {
			_config = config;
			_httpClient = new HttpClient();

		}

		public void ConfigureHttpClient(IBaseConfiguration config)
		{
			if (!string.IsNullOrEmpty(_config.AuthToken))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _config.AuthToken);
			}

			if (_config.Headers != null)
			{
				foreach (var header in _config.Headers)
				{
					_httpClient.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
				}
			}
		}

		public Task<HttpResponseMessage> DeleteAsync(string requestUri, CancellationToken cancellationToken)
		{
			return _httpClient.DeleteAsync(requestUri, cancellationToken);
		}

		public Task<HttpResponseMessage> GetAsync(string requestUri, CancellationToken cancellationToken)
		{
			return _httpClient.GetAsync(requestUri, cancellationToken);
		}

		public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content, CancellationToken cancellationToken)
		{
			return _httpClient.PostAsync(requestUri, content, cancellationToken);
		}

		public Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content, CancellationToken cancellationToken)
		{
			return _httpClient.PutAsync(requestUri, content, cancellationToken);
		}
	}
}
