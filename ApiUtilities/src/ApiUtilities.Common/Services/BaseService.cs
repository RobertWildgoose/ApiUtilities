using ApiUtilities.Common.Handlers;
using ApiUtilities.Common.Interfaces;
using ApiUtilities.Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUtilities.Common.Services
{
	public class BaseService
	{
		private readonly IRequestHandler _requestHandler;
		private readonly IApiConfig _apiConfig;

		public BaseService(IApiConfig apiConfig, IRequestHandler requestHandler) 
		{
			_requestHandler = requestHandler;
			_apiConfig = apiConfig;
		}

		public async Task<ResponseContainer<T>> Get<T>(string url) where T : BaseResponse
		{
			if (!string.IsNullOrWhiteSpace(_apiConfig.BaseUrl) && !string.IsNullOrWhiteSpace(url))
			{
				_requestHandler.RefreshHandler();
				var response = await _requestHandler.GetAsync($"{_apiConfig.BaseUrl}{url}");
				if (!string.IsNullOrWhiteSpace(response))
				{
					try
					{
						var data = JsonConvert.DeserializeObject<T>(response ?? string.Empty);
						return new ResponseContainer<T>() { Data = data, Error = null };
					}
					catch (Exception ex)
					{
						return new ResponseContainer<T>() { Data = null, Error = ex.ToString() };
					}
				}
				else
				{
					return new ResponseContainer<T>() { Data = null, Error = "InvalidDataException" };
				}
			}
			else
			{
				return new ResponseContainer<T>() { Data = null, Error = "InvalidUrl" };
			}
		}

		public async Task<ResponseContainer<List<T>>> GetEnumerable<T>(string url) where T : BaseResponse
		{
			if (!string.IsNullOrWhiteSpace(_apiConfig.BaseUrl) && !string.IsNullOrWhiteSpace(url))
			{
				_requestHandler.RefreshHandler();
				var response = await _requestHandler.GetAsync($"{_apiConfig.BaseUrl}{url}");
				if (!string.IsNullOrWhiteSpace(response))
				{
					try
					{
						var data = JsonConvert.DeserializeObject<List<T>>(response ?? string.Empty);
						return new ResponseContainer<List<T>>() { Data = data, Error = null };
					}
					catch (Exception ex)
					{
						return new ResponseContainer<List<T>>() { Data = null, Error = ex.ToString() };
					}
				}
				else
				{
					return new ResponseContainer<List<T>>() { Data = null, Error = "InvalidDataException" };
				}
			}
			else
			{
				return new ResponseContainer<List<T>>() { Data = null, Error = "InvalidUrl" };
			}
		}

		public async Task<ResponseContainer<T>> Post<T>(string url, object data) where T : BaseResponse
		{
			if (!string.IsNullOrWhiteSpace(_apiConfig.BaseUrl) && !string.IsNullOrWhiteSpace(url))
			{
				if (data != null)
				{
					var payload = JsonConvert.SerializeObject(data);
					if (!string.IsNullOrEmpty(payload))
					{
						_requestHandler.RefreshHandler();
						var response = await _requestHandler.PostAsync($"{_apiConfig.BaseUrl}{url}", payload);
						if (!string.IsNullOrWhiteSpace(response))
						{
							try
							{
								var returnedData = JsonConvert.DeserializeObject<T>(response ?? string.Empty);
								return new ResponseContainer<T>() { Data = returnedData, Error = null };
							}
							catch (Exception ex)
							{
								return new ResponseContainer<T>() { Data = null, Error = ex.ToString() };
							}
						}
						else
						{
							return new ResponseContainer<T>() { Data = null, Error = "InvalidDataException" };
						}
					}
					else
					{
						return new ResponseContainer<T>() { Data = null, Error = "PostDataInvalid" };
					}
				}
				else
				{
					return new ResponseContainer<T>() { Data = null, Error = "PostDataInvalid" };
				}
			}
			else
			{
				return new ResponseContainer<T>() { Data = null, Error = "InvalidUrl" };
			}
		}
	}
}
