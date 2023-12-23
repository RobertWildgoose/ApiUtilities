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
		private readonly IExceptionHandler _exceptionHandler;
		private readonly IRequestHandler _requestHandler;
		private readonly IApiConfig _apiConfig;

		public BaseService(IApiConfig apiConfig, IExceptionHandler exceptionHandler, IRequestHandler requestHandler) 
		{
			_exceptionHandler = exceptionHandler;
			_requestHandler = requestHandler;
			_apiConfig = apiConfig;
		}

		public async Task<T> GetData<T>(string url) where T : BaseResponse
		{
			if(ValidateInputs(url))
			{
				var response = await ValidateResponse($"{_apiConfig.BaseUrl}{url}");
				try
				{
					return JsonConvert.DeserializeObject<T>(response ?? string.Empty);
				}
				catch (Exception ex)
				{
					if (_exceptionHandler.CanHandle(ex))
					{
						_exceptionHandler.HandleException(ex);
					}
				}
			}
			return null;
		}

		public async Task<List<T>> GetDataList<T>(string url) where T : BaseResponse
		{
			if (ValidateInputs(url))
			{
				var response = await ValidateResponse($"{_apiConfig.BaseUrl}{url}");
				try
				{
					return JsonConvert.DeserializeObject<List<T>>(response ?? string.Empty);
				}
				catch (Exception ex)
				{
					if (_exceptionHandler.CanHandle(ex))
					{
						_exceptionHandler.HandleException(ex);
					}
				}
			}
			return null;
		}

		private async Task<string> ValidateResponse(string url)
		{
			var response = await _requestHandler.GetAsync(url);
			if (string.IsNullOrWhiteSpace(response))
			{
				HandleOrThrowException(new InvalidDataException());
			}
			return response;
		}

		private bool ValidateInputs(string url)
		{
			if (string.IsNullOrWhiteSpace(_apiConfig.BaseUrl) || string.IsNullOrWhiteSpace(url)) 
			{
				HandleOrThrowException(new MissingFieldException(string.IsNullOrWhiteSpace(_apiConfig.BaseUrl) ? nameof(_apiConfig.BaseUrl) : nameof(url)));
				return false;
			}
			return true;
		}

		private void HandleOrThrowException(Exception ex)
		{
			if (_exceptionHandler.CanHandle(ex))
			{
				_exceptionHandler.HandleException(ex);
			}
			else
			{
				throw ex;
			}
		}
	}
}
