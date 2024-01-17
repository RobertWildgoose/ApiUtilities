# ApiUtilities
### A lightweight common api wrapper utilities nuget package.

APIUtilities is a comprehensive library designed to accelerate the development of API wrapper NuGet packages.

# Getting Started

## Installation
Install the APIUtilities NuGet package using the following command:

```
bash
Copy code
dotnet add package APIUtilities
Usage
```

## Creating a Registration Container
In order to create an api wrapper, first create a class that inherits from BaseRegistrationContainer, 
this will load in all the services the APIUtilities.Common brings in but also the services created in the package.

```
public class RegistrationContainer : BaseRegistrationContainer
	{
		public RegistrationContainer(IServiceCollection collection) : base(collection)
		{
			collection.AddSingleton<IApiConfig, ApiConfig>();
			ExtendRegistration(collection);
		}

		public override void ExtendRegistration(IServiceCollection collection)
		{
			base.ExtendRegistration(collection);
			collection.AddSingleton<IService, Service>();
		}
	}
```

## Creating an ApiConfig

In order to make API calls, APIUtilities.Common will require an ApiConfig in order to pass in properties such as the base url.

```
public class ApiConfig : IApiConfig
{
	public string BaseUrl { get => "https://www.baseurl.com/api/";  }
}
```

## Creating an Service
In order to make a service call the service needs to be registered in the container defined above. Once registered the service class must inherit BaseService
```
// Example of using BaseService
public class MyApiService : BaseService
{
    // Implement your API-specific logic here
}
```

## Requests Within the Service 
Base Service contains Get GetEnumerable and Set methods this takes in a DTO and returns a ResponseContainer.

```
// Example of using common utilities
var getRequestResponse = await Get<DTO>("/endpoint");
var getRequestEnumerableResponse = await GetEnumerable<DTO>("/endpoint");
var getRequestPostResponse = await Post<DTO>("/endpoint");
```

# Contributing
Contributions are welcome! Please follow the contribution guidelines when submitting pull requests.

# License
This project is licensed under the MIT License.
