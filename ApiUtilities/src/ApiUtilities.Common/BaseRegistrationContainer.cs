using ApiUtilities.Common.Handlers;
using ApiUtilities.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUtilities.Common
{
	public class BaseRegistrationContainer
	{
        public BaseRegistrationContainer(IServiceCollection collection)
        {
			collection.AddSingleton<IRequestHandler, RequestHandler>();
			ExtendRegistration(collection);
		}
		/// <summary>
		/// Allows Extra Dependancy Injection, For Utilising Library.
		/// </summary>
		/// <param name="collection">DI ServiceCollection</param>
		public virtual void ExtendRegistration(IServiceCollection collection)
		{

		}
    }
}
