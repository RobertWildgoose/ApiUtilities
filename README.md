# ApiUtilities NuGet Package

## Overview

The `ApiUtilities` package provides a simplified way to interact with HTTP APIs in .NET applications. It encapsulates common HTTP methods (GET, POST, PUT, DELETE) and manages configuration, authentication, and error handling, making it easy to perform API requests.

## Installation

To install the package, use the NuGet Package Manager Console:

```sh
Install-Package ApiUtilities
```

# Usage
## Configuration Interface
First, implement the IBaseConfiguration interface to provide the necessary configuration for your HTTP client.

```c#
public class ApiConfiguration : IBaseConfiguration
{
    public string BaseUrl { get; set; }
    public string AuthToken { get; set; }
    public IDictionary<string, string> Headers { get; set; }
    public int TimeoutSeconds { get; set; } = 30; // Default timeout
}
```

## Service Registration
Register the necessary services in your dependency injection container.

```c#
public class ServiceRegistration : BaseServiceRegistration
{
    public ServiceRegistration(IServiceCollection serviceCollection) : base(serviceCollection)
    {
        services.AddSingleton<IBaseConfiguration>(configuration);
    }

    protected override void RegisterOverride()
    {
        // Add custom service registrations here if needed
    }
}

---

var services = new ServiceCollection();
var configuration = new ApiConfiguration
{
    BaseUrl = "https://api.example.com",
    AuthToken = "your_token",
    Headers = new Dictionary<string, string>
    {
        { "Custom-Header", "value" }
    }
};

services.AddSingleton<IBaseConfiguration>(configuration);
new ServiceRegistration(services);
var serviceProvider = services.BuildServiceProvider();

```

## Using the BaseService

```c#
public class MyService
{
    private readonly BaseService _baseService;

    public MyService(BaseService baseService)
    {
        _baseService = baseService;
    }

    public async Task DoSomethingAsync()
    {
        var response = await _baseService.Get<MyResponse>("endpoint");
        if (response.Error == null)
        {
            // Handle successful response
        }
        else
        {
            // Handle error
        }
    }
}
```

## Models
Define your response models inheriting from BaseResponse.

```c#
public class MyResponse : BaseResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
}
```

## API Methods
### Get Request

```c#
var response = await _baseService.Get<MyResponse>("endpoint");
```

### Post Request

```c#
var data = new { Property1 = "value", Property2 = "value" };
var response = await _baseService.Post<MyResponse>("endpoint", data);
```

### Put Request

```c#
var data = new { Property1 = "value", Property2 = "value" };
var response = await _baseService.Put<MyResponse>("endpoint", data);
```

### Delete Request

```c#
var response = await _baseService.Delete<MyResponse>("endpoint");
```

## Error Handling

If the request fails and requireSuccess is set to true, an HttpRequestException will be thrown. 
If requireSuccess is set to false, the error will be set in the ResponseContainer.

## Cancellation and Timeout

All methods accept a CancellationToken which can be used to cancel the request. The default timeout for requests can be configured via the TimeoutSeconds property in IBaseConfiguration.

## Unit Testing

Mocking HTTP requests and responses can be achieved using the provided IHttpClientService. Here is an example using Moq:

```c#
public class BaseServiceTests
{
    [Fact]
    public async Task Get_SuccessfulRequest_ReturnsResponseContainerWithData()
    {
        // Arrange
        var httpClientServiceMock = new Mock<IHttpClientService>();
        var config = new ApiConfiguration { BaseUrl = "https://example.com", AuthToken = "dummy_token", TimeoutSeconds = 30 };

        var responseData = new MyResponse { Id = 1, Name = "Test" };
        var json = JsonConvert.SerializeObject(responseData);

        httpClientServiceMock.Setup(c => c.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            });

        var baseService = new BaseService(httpClientServiceMock.Object, config);

        // Act
        var responseContainer = await baseService.Get<MyResponse>("test");

        // Assert
        Assert.NotNull(responseContainer);
        Assert.Null(responseContainer.Error);
        Assert.NotNull(responseContainer.Data);
        Assert.Equal(responseData.Id, responseContainer.Data.Id);
        Assert.Equal(responseData.Name, responseContainer.Data.Name);
    }
}
```

## Conclusion
ApiUtilities simplifies HTTP API interactions in .NET applications by providing a configurable and extendable base service. By following the steps outlined in this documentation, you can quickly integrate and use the package in your projects.

# Contributing
Contributions are welcome! Please follow the contribution guidelines when submitting pull requests.

# License
This project is licensed under the MIT License.