using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUtilities.Common.Interfaces
{
	public interface IExceptionHandler
	{
		/// <summary>
		/// Adds Handler Into List Of Handlers.
		/// Returns
		/// </summary>
		/// <param name="exception">The Name Of The Exception - Use nameof(Exception)</param>
		/// <param name="handler">Action To Fire If Exception Gets Thrown</param>
		/// <returns>Boolean Representing If It Can Be Added</returns>
		public bool AddExceptionHandler(string exception, Action handler);

		/// <summary>
		/// Confirms If A Handler Exists To Deal With The Exception
		/// </summary>
		/// <param name="exception">Exception To Confirm If Handler Is Present</param>
		/// <returns>Boolean Representing If It Can Be Handled</returns>
		public bool CanHandle(Exception exception);

		/// <summary>
		/// Handles The Exception With The Attached Exception Handlers
		/// </summary>
		/// <param name="exception">Exception To Handle</param>
		public void HandleException(Exception exception);
	}
}
