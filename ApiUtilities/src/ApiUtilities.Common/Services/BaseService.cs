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

		public string? BaseUrl { get; set; }

		public BaseService(IExceptionHandler exceptionHandler, IRequestHandler requestHandler) 
		{
			_exceptionHandler = exceptionHandler;
			_requestHandler = requestHandler;
		}

		public async Task<T> GetData<T>(string url) where T : BaseResponse
		{
			if (!string.IsNullOrWhiteSpace(BaseUrl))
			{
				if (!string.IsNullOrWhiteSpace(url))
				{
					var response = await _requestHandler.GetAsync($"{BaseUrl}{url}");
					if(!string.IsNullOrWhiteSpace(response))
					{
						try
						{
							var deserialisedObject = JsonConvert.DeserializeObject<T>(response ?? string.Empty);
							return deserialisedObject;
						}
						catch(Exception ex) 
						{
							if (_exceptionHandler.CanHandle(ex))
							{
								_exceptionHandler.HandleException(ex);
							}
						}
					}
					else 
					{
						if (_exceptionHandler.CanHandle(new InvalidDataException()))
						{
							_exceptionHandler.HandleException(new InvalidDataException());
						}
					}
				}
				else
				{
					if (_exceptionHandler.CanHandle(new MissingFieldException()))
					{
						_exceptionHandler.HandleException(new MissingFieldException(nameof(url)));
					}
				}
			}
			else
			{
				if (_exceptionHandler.CanHandle(new MissingFieldException()))
				{
					_exceptionHandler.HandleException(new MissingFieldException(nameof(BaseUrl)));
				}
			}
			return null;
		}

		public async Task<List<T>> GetDataList<T>(string url) where T : BaseResponse
		{
			if (!string.IsNullOrWhiteSpace(BaseUrl))
			{
				if (!string.IsNullOrWhiteSpace(url))
				{
					var response = await _requestHandler.GetAsync($"{BaseUrl}{url}");
					if (!string.IsNullOrWhiteSpace(response))
					{
						try
						{
							var deserialisedObject = JsonConvert.DeserializeObject<List<T>>(response ?? string.Empty);
							return deserialisedObject;
						}
						catch (Exception ex)
						{
							if (_exceptionHandler.CanHandle(ex))
							{
								_exceptionHandler.HandleException(ex);
							}
						}
					}
					if (_exceptionHandler.CanHandle(new InvalidDataException()))
					{
						_exceptionHandler.HandleException(new InvalidDataException());
					}
				}
				else
				{
					if (_exceptionHandler.CanHandle(new MissingFieldException()))
					{
						_exceptionHandler.HandleException(new MissingFieldException(nameof(url)));
					}
				}
			}
			else
			{
				if (_exceptionHandler.CanHandle(new MissingFieldException()))
				{
					_exceptionHandler.HandleException(new MissingFieldException(nameof(BaseUrl)));
				}
			}
			return null;
		}
	}
}
