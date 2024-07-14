using ApiUtilities.Common.Interfaces;
using ApiUtilities.Common.Models;
using ApiUtilities.Common.Services;
using ApiUtilities.Tests.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApiUtilities.Tests.Tests
{
	public class BaseServiceTests
	{
		private readonly Mock<IHttpClientService> _mockHttpClientService;
		private readonly Mock<IBaseConfiguration> _mockConfig;

		public BaseServiceTests()
		{
			_mockHttpClientService = new Mock<IHttpClientService>();
			_mockConfig = new Mock<IBaseConfiguration>();
		}

		[Fact]
		public async Task Get_Successful_ShouldReturnResponseContainer()
		{
			// Arrange
			var service = new BaseService(_mockHttpClientService.Object, _mockConfig.Object);
			var expectedResponseData = new TestResponse
			{
				Title = "TestTitle",
				Body = "TestBody",
				Message = "TestMessage",
				Success = true
			};

			var expectedResponseContainer = new ResponseContainer<TestResponse>
			{
				Data = expectedResponseData
			};

			// Configure mocks
			_mockConfig.Setup(c => c.BaseUrl).Returns("https://example.com");
			_mockHttpClientService.Setup(c => c.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
				{
					Content = new StringContent(JsonConvert.SerializeObject(expectedResponseData), Encoding.UTF8, "application/json")
				});

			// Act
			var response = await service.Get<TestResponse>("endpoint");

			// Assert
			Assert.NotNull(response);
			Assert.NotNull(response.Data);
			Assert.Equal(expectedResponseContainer.Data.Title, response.Data.Title);
			Assert.Equal(expectedResponseContainer.Data.Body, response.Data.Body);
			Assert.Equal(expectedResponseContainer.Data.Message, response.Data.Message);
			Assert.Equal(expectedResponseContainer.Data.Success, response.Data.Success);
		}

		[Fact]
		public async Task Post_Successful_ShouldReturnResponseContainer()
		{
			// Arrange
			var service = new BaseService(_mockHttpClientService.Object, _mockConfig.Object);
			var requestData = new { Name = "Test" };
			var expectedResponseData = new TestResponse
			{
				Title = "TestTitle",
				Body = "TestBody",
				Message = "TestMessage",
				Success = true
			};

			var expectedResponseContainer = new ResponseContainer<TestResponse>
			{
				Data = expectedResponseData
			};

			// Configure mocks
			_mockConfig.Setup(c => c.BaseUrl).Returns("https://example.com");
			_mockHttpClientService.Setup(c => c.PostAsync(It.IsAny<string>(), It.IsAny<StringContent>(), It.IsAny<CancellationToken>()))
								  .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
								  {
									  Content = new StringContent(JsonConvert.SerializeObject(expectedResponseData), Encoding.UTF8, "application/json")
								  });

			// Act
			var response = await service.Post<TestResponse>("endpoint", requestData);

			// Assert
			Assert.NotNull(response);
			Assert.NotNull(response.Data);
			Assert.Equal(expectedResponseContainer.Data.Title, response.Data.Title);
			Assert.Equal(expectedResponseContainer.Data.Body, response.Data.Body);
			Assert.Equal(expectedResponseContainer.Data.Message, response.Data.Message);
			Assert.Equal(expectedResponseContainer.Data.Success, response.Data.Success);
		}

		[Fact]
		public async Task Delete_Successful_ShouldReturnResponseContainer()
		{
			// Arrange
			var service = new BaseService(_mockHttpClientService.Object, _mockConfig.Object);
			var expectedResponseData = new TestResponse
			{
				Title = "TestTitle",
				Body = "TestBody",
				Message = "TestMessage",
				Success = true
			};

			var expectedResponseContainer = new ResponseContainer<TestResponse>
			{
				Data = expectedResponseData
			};

			// Configure mocks
			_mockConfig.Setup(c => c.BaseUrl).Returns("https://example.com");
			_mockHttpClientService.Setup(c => c.DeleteAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
								  .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
								  {
									  Content = new StringContent(JsonConvert.SerializeObject(expectedResponseData), Encoding.UTF8, "application/json")
								  });

			// Act
			var response = await service.Delete<TestResponse>("endpoint");

			// Assert
			Assert.NotNull(response);
			Assert.NotNull(response.Data);
			Assert.Equal(expectedResponseContainer.Data.Title, response.Data.Title);
			Assert.Equal(expectedResponseContainer.Data.Body, response.Data.Body);
			Assert.Equal(expectedResponseContainer.Data.Message, response.Data.Message);
			Assert.Equal(expectedResponseContainer.Data.Success, response.Data.Success);
		}

		[Fact]
		public async Task Put_Successful_ShouldReturnResponseContainer()
		{
			// Arrange
			var service = new BaseService(_mockHttpClientService.Object, _mockConfig.Object);
			var requestData = new { Id = 1, Name = "Updated" };
			var expectedResponseData = new TestResponse
			{
				Title = "TestTitle",
				Body = "TestBody",
				Message = "TestMessage",
				Success = true
			};

			var expectedResponseContainer = new ResponseContainer<TestResponse>
			{
				Data = expectedResponseData
			};

			// Configure mocks
			_mockConfig.Setup(c => c.BaseUrl).Returns("https://example.com");
			_mockHttpClientService.Setup(c => c.PutAsync(It.IsAny<string>(), It.IsAny<StringContent>(), It.IsAny<CancellationToken>()))
								  .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
								  {
									  Content = new StringContent(JsonConvert.SerializeObject(expectedResponseData), Encoding.UTF8, "application/json")
								  });

			// Act
			var response = await service.Put<TestResponse>("endpoint", requestData);

			// Assert
			Assert.NotNull(response);
			Assert.NotNull(response.Data);
			Assert.Equal(expectedResponseContainer.Data.Title, response.Data.Title);
			Assert.Equal(expectedResponseContainer.Data.Body, response.Data.Body);
			Assert.Equal(expectedResponseContainer.Data.Message, response.Data.Message);
			Assert.Equal(expectedResponseContainer.Data.Success, response.Data.Success);
		}

		[Fact]
		public async Task Get_UnSuccessful_ShouldThrowHttpRequestException()
		{
			// Arrange
			var service = new BaseService(_mockHttpClientService.Object, _mockConfig.Object);

			// Configure mocks
			_mockConfig.Setup(c => c.BaseUrl).Returns("https://example.com");
			_mockHttpClientService.Setup(c => c.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
								  .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.BadRequest));

			// Act & Assert
			await Assert.ThrowsAsync<HttpRequestException>(async () => await service.Get<TestResponse>("endpoint"));
		}

		[Fact]
		public async Task Post_UnSuccessful_ShouldThrowHttpRequestException()
		{
			// Arrange
			var service = new BaseService(_mockHttpClientService.Object, _mockConfig.Object);
			var requestData = new { Name = "Test" };

			// Configure mocks
			_mockConfig.Setup(c => c.BaseUrl).Returns("https://example.com");
			_mockHttpClientService.Setup(c => c.PostAsync(It.IsAny<string>(), It.IsAny<StringContent>(), It.IsAny<CancellationToken>()))
								  .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.InternalServerError));

			// Act & Assert
			await Assert.ThrowsAsync<HttpRequestException>(async () => await service.Post<TestResponse>("endpoint", requestData));
		}

		[Fact]
		public async Task Delete_UnSuccessful_ShouldThrowHttpRequestException()
		{
			// Arrange
			var service = new BaseService(_mockHttpClientService.Object, _mockConfig.Object);

			// Configure mocks
			_mockConfig.Setup(c => c.BaseUrl).Returns("https://example.com");
			_mockHttpClientService.Setup(c => c.DeleteAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
								  .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.NotFound));

			// Act & Assert
			await Assert.ThrowsAsync<HttpRequestException>(async () => await service.Delete<TestResponse>("endpoint"));
		}

		[Fact]
		public async Task Put_UnSuccessful_ShouldThrowHttpRequestException()
		{
			// Arrange
			var service = new BaseService(_mockHttpClientService.Object, _mockConfig.Object);
			var requestData = new { Id = 1, Name = "Updated" };

			// Configure mocks
			_mockConfig.Setup(c => c.BaseUrl).Returns("https://example.com");
			_mockHttpClientService.Setup(c => c.PutAsync(It.IsAny<string>(), It.IsAny<StringContent>(), It.IsAny<CancellationToken>()))
								  .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.ServiceUnavailable));

			// Act & Assert
			await Assert.ThrowsAsync<HttpRequestException>(async () => await service.Put<TestResponse>("endpoint", requestData));
		}

		[Fact]
		public async Task Get_ExceptionThrown_ShouldThrowTaskCanceledException()
		{
			// Arrange
			var service = new BaseService(_mockHttpClientService.Object, _mockConfig.Object);

			// Configure mocks
			_mockConfig.Setup(c => c.BaseUrl).Returns("https://example.com");
			_mockHttpClientService.Setup(c => c.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
								  .ThrowsAsync(new OperationCanceledException());

			// Act & Assert
			await Assert.ThrowsAsync<TaskCanceledException>(async () => await service.Get<TestResponse>("endpoint"));
		}

		[Fact]
		public async Task UnableToConvertModel_ShouldThrowException()
		{
			// Arrange
			var service = new BaseService(_mockHttpClientService.Object, _mockConfig.Object);

			// Configure mocks
			_mockConfig.Setup(c => c.BaseUrl).Returns("https://example.com");
			_mockHttpClientService.Setup(c => c.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
				{
					Content = new StringContent("[{ 'invalid': 'json' }]", Encoding.UTF8, "application/json") // Invalid JSON format for TestResponse
				});

			// Act & Assert
			await Assert.ThrowsAsync<HttpRequestException>(async () => await service.Get<TestResponse>("endpoint"));
		}

		[Fact]
		public async Task Get_Timeout_ShouldThrowTaskCanceledException()
		{
			// Arrange
			var service = new BaseService(_mockHttpClientService.Object, _mockConfig.Object);

			// Configure mocks
			_mockConfig.Setup(c => c.BaseUrl).Returns("https://example.com");

			// Simulate timeout by canceling the token after a short delay
			var timeoutCts = new CancellationTokenSource(TimeSpan.FromSeconds(1));
			_mockHttpClientService.Setup(c => c.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
								  .ReturnsAsync((string url, CancellationToken cancellationToken) =>
								  {
									  // Simulate long-running operation that exceeds timeout
									  Task.Delay(TimeSpan.FromSeconds(2), cancellationToken).Wait(cancellationToken);
									  return new HttpResponseMessage(HttpStatusCode.OK);
								  });

			// Act & Assert
			await Assert.ThrowsAsync<TaskCanceledException>(async () => await service.Get<TestResponse>("endpoint", cancellationToken: timeoutCts.Token));
		}

		[Fact]
		public async Task Post_Timeout_ShouldThrowTaskCanceledException()
		{
			// Arrange
			var service = new BaseService(_mockHttpClientService.Object, _mockConfig.Object);
			var requestData = new { Name = "Test" };

			// Configure mocks
			_mockConfig.Setup(c => c.BaseUrl).Returns("https://example.com");

			// Simulate timeout by canceling the token after a short delay
			var timeoutCts = new CancellationTokenSource(TimeSpan.FromSeconds(1));
			_mockHttpClientService.Setup(c => c.PostAsync(It.IsAny<string>(), It.IsAny<StringContent>(), It.IsAny<CancellationToken>()))
								  .ReturnsAsync((string url, StringContent content, CancellationToken cancellationToken) =>
								  {
									  // Simulate long-running operation that exceeds timeout
									  Task.Delay(TimeSpan.FromSeconds(2), cancellationToken).Wait(cancellationToken);
									  return new HttpResponseMessage(HttpStatusCode.OK);
								  });

			// Act & Assert
			await Assert.ThrowsAsync<TaskCanceledException>(async () => await service.Post<TestResponse>("endpoint", requestData, cancellationToken: timeoutCts.Token));
		}

		[Fact]
		public async Task Delete_Timeout_ShouldThrowTaskCanceledException()
		{
			// Arrange
			var service = new BaseService(_mockHttpClientService.Object, _mockConfig.Object);

			// Configure mocks
			_mockConfig.Setup(c => c.BaseUrl).Returns("https://example.com");

			// Simulate timeout by canceling the token after a short delay
			var timeoutCts = new CancellationTokenSource(TimeSpan.FromSeconds(1));
			_mockHttpClientService.Setup(c => c.DeleteAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
								  .ReturnsAsync((string url, CancellationToken cancellationToken) =>
								  {
									  // Simulate long-running operation that exceeds timeout
									  Task.Delay(TimeSpan.FromSeconds(2), cancellationToken).Wait(cancellationToken);
									  return new HttpResponseMessage(HttpStatusCode.OK);
								  });

			// Act & Assert
			await Assert.ThrowsAsync<TaskCanceledException>(async () => await service.Delete<TestResponse>("endpoint", cancellationToken: timeoutCts.Token));
		}

		[Fact]
		public async Task Put_Timeout_ShouldThrowTaskCanceledException()
		{
			// Arrange
			var service = new BaseService(_mockHttpClientService.Object, _mockConfig.Object);
			var requestData = new { Name = "Test" };

			// Configure mocks
			_mockConfig.Setup(c => c.BaseUrl).Returns("https://example.com");

			// Simulate timeout by canceling the token after a short delay
			var timeoutCts = new CancellationTokenSource(TimeSpan.FromSeconds(1));
			_mockHttpClientService.Setup(c => c.PutAsync(It.IsAny<string>(), It.IsAny<StringContent>(), It.IsAny<CancellationToken>()))
			.ReturnsAsync((string url, StringContent content, CancellationToken cancellationToken) =>
			{
				// Simulate long-running operation that exceeds timeout
				Task.Delay(TimeSpan.FromSeconds(2), cancellationToken).Wait(cancellationToken);
				return new HttpResponseMessage(HttpStatusCode.OK);
			});

			// Act & Assert
			await Assert.ThrowsAsync<TaskCanceledException>(async () => await service.Put<TestResponse>("endpoint", requestData, cancellationToken: timeoutCts.Token));
		}
	}
}