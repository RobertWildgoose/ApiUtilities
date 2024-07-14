using ApiUtilities.Common.Interfaces;
using ApiUtilities.Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApiUtilities.Common.Services
{
	public class BaseService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly IBaseConfiguration _config;

        public BaseService(IHttpClientService httpClientService, IBaseConfiguration config)
        {
            _httpClientService = httpClientService;
            _config = config;

			_httpClientService.ConfigureHttpClient(_config);
        }

        public async Task<ResponseContainer<T>> Get<T>(string url, bool requireSuccess = true, CancellationToken cancellationToken = default) where T : BaseResponse
        {
            return await SendRequest<T>(HttpMethod.Get, url, null, requireSuccess, cancellationToken);
        }

        public async Task<ResponseContainer<T>> Post<T>(string url, object data, bool requireSuccess = true, CancellationToken cancellationToken = default) where T : BaseResponse
        {
            return await SendRequest<T>(HttpMethod.Post, url, data, requireSuccess, cancellationToken);
        }

        public async Task<ResponseContainer<T>> Put<T>(string url, object data, bool requireSuccess = true, CancellationToken cancellationToken = default) where T : BaseResponse
        {
            return await SendRequest<T>(HttpMethod.Put, url, data, requireSuccess, cancellationToken);
        }

        public async Task<ResponseContainer<T>> Delete<T>(string url, bool requireSuccess = true, CancellationToken cancellationToken = default) where T : BaseResponse
        {
            return await SendRequest<T>(HttpMethod.Delete, url, null, requireSuccess, cancellationToken);
        }

        private async Task<ResponseContainer<T>> SendRequest<T>(HttpMethod method, string url, object? data, bool requireSuccess, CancellationToken cancellationToken) where T : BaseResponse
        {
            if (string.IsNullOrWhiteSpace(_config.BaseUrl) || string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("Invalid URL.");
            }

            var requestUrl = $"{_config.BaseUrl.TrimEnd('/')}/{url.TrimStart('/')}";

            try
            {
                HttpResponseMessage response;

                // Handle cancellation token and timeout
                using (var timeoutCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
                {
                    timeoutCts.CancelAfter(TimeSpan.FromSeconds(_config.TimeoutSeconds));

                    if (method == HttpMethod.Get)
                    {
                        response = await _httpClientService.GetAsync(requestUrl, timeoutCts.Token);
                    }
                    else if (method == HttpMethod.Post)
                    {
                        var jsonContent = JsonConvert.SerializeObject(data);
                        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                        response = await _httpClientService.PostAsync(requestUrl, content, timeoutCts.Token);
                    }
                    else if (method == HttpMethod.Put)
                    {
                        var jsonContent = JsonConvert.SerializeObject(data);
                        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                        response = await _httpClientService.PutAsync(requestUrl, content, timeoutCts.Token);
                    }
                    else if (method == HttpMethod.Delete)
                    {
                        response = await _httpClientService.DeleteAsync(requestUrl, timeoutCts.Token);
                    }
                    else
                    {
                        throw new NotSupportedException($"HTTP method '{method}' is not supported.");
                    }
                }

                return await ProcessResponse<T>(response, requireSuccess);
            }
            catch (OperationCanceledException)
            {
                throw new TaskCanceledException("Request was canceled due to timeout.");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate
                throw new HttpRequestException($"Request failed: {ex.Message}", ex);
            }
        }

		private async Task<ResponseContainer<T>> ProcessResponse<T>(HttpResponseMessage response, bool requireSuccess) where T : BaseResponse
		{
			var responseContainer = new ResponseContainer<T>();

			try
			{
				if (response.IsSuccessStatusCode)
				{
					var responseBody = await response.Content.ReadAsStringAsync();

					// Deserialization logic
					responseContainer.Data = JsonConvert.DeserializeObject<T>(responseBody);
				}
				else
				{
					responseContainer.Error = $"{(int)response.StatusCode} - {response.ReasonPhrase}";

					if (requireSuccess)
					{
						throw new HttpRequestException($"Request failed with status code {(int)response.StatusCode}: {response.ReasonPhrase}");
					}
				}
			}
			catch (Exception ex)
			{
                throw new Exception("Cannot Convert Data Object");				responseContainer.Error = $"Request failed: {ex.Message}";
			}

			return responseContainer;
		}
	}
}