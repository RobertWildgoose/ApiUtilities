using System;
using System.Collections.Generic;
using System.Text;

namespace ApiUtilities.Common.Models
{
	public class ResponseContainer<T> where T : BaseResponse
	{
		public string? Error { get; set; }
		public T? Data { get; set; }
		public bool Success => Data != null;
	}
}
