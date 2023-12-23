using ApiUtilities.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUtilities.Common.Handlers
{
	public class ExceptionHandler : IExceptionHandler
	{
		private Dictionary<string, Action> _exceptionHandlers { get; set; } = new Dictionary<string, Action>();
		public bool AddExceptionHandler(string exception, Action handler)
		{
			if(!string.IsNullOrWhiteSpace(exception) && !_exceptionHandlers.ContainsKey(exception) && handler != null)
			{
				_exceptionHandlers.Add(exception, handler);
				return true;
			}
			return false;
		}

		public bool CanHandle(Exception exception)
		{
			return _exceptionHandlers.ContainsKey(exception.GetType().Name);
		}

		public void HandleException(Exception exception)
		{
			var handler = _exceptionHandlers.GetValueOrDefault(exception.GetType().Name);
			if(handler != null)
			{
				handler.Invoke();
			}
		}
	}
}
