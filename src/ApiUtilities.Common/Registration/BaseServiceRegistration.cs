using ApiUtilities.Common.Interfaces;
using ApiUtilities.Common.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiUtilities.Common.Registration
{
	public abstract partial class BaseServiceRegistration : IServiceRegistration
	{
		private readonly IServiceCollection _serviceCollection;

		public BaseServiceRegistration(IServiceCollection serviceCollection)
		{
			_serviceCollection = serviceCollection;
			RegisterServices();
		}

		public void RegisterServices()
		{
			_serviceCollection.AddSingleton<IHttpClientService, HttpClientService>();
			RegisterOverride();

		}

		protected abstract void RegisterOverride();
	}
}
