using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http.Headers;

namespace ApiUtilities.Common.Interfaces
{
	public interface IHttpClientService
	{
		void ConfigureHttpClient(IBaseConfiguration config);
		Task<HttpResponseMessage> GetAsync(string requestUri, CancellationToken cancellationToken);
		Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content, CancellationToken cancellationToken);
		Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content, CancellationToken cancellationToken);
		Task<HttpResponseMessage> DeleteAsync(string requestUri, CancellationToken cancellationToken);
	}
}