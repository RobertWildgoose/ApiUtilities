using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUtilities.Common.Models
{
	public class ResponseContainer<T> where T : class
	{
		public string Error { get; set; }
		public T Data { get; set; }
		public bool Success => Data != null;
	}
}
