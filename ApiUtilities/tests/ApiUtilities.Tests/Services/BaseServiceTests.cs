using ApiUtilities.Common.Interfaces;
using ApiUtilities.Common.Models;
using ApiUtilities.Common.Services;
using ApiUtilities.Tests.Models;
using ApiUtilities.Tests.TestData;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUtilities.Tests.Services
{
	public class BaseServiceTests
	{
		private BaseService _subjectUnderTest { get; set; }
		private Mock<IRequestHandler> _requestHandlerMock { get; set; }
		private Mock<IApiConfig> _apiConfigMock { get; set; }


		private void ResetMocks()
		{
			_requestHandlerMock = new Mock<IRequestHandler>();
			_apiConfigMock = new Mock<IApiConfig>();
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public async Task Get_WhenBaseUrlIsInvalid_ShouldReturnMissingFieldError(string baseUrl)
		{
			ResetMocks();
			_apiConfigMock.SetupProperty(a => a.BaseUrl, baseUrl);
			_subjectUnderTest = new BaseService(_apiConfigMock.Object, _requestHandlerMock.Object);

			var responseData = await _subjectUnderTest.Get<TestResponseModel>("/invalid_base_url");

			Assert.False(responseData.Success);
			Assert.Null(responseData.Data);
			Assert.Equal("InvalidUrl", responseData.Error);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public async Task GetEnumerable_WhenBaseUrlIsInvalid_ShouldReturnMissingFieldError(string baseUrl)
		{
			ResetMocks();
			_apiConfigMock.SetupProperty(a => a.BaseUrl, baseUrl);
			_subjectUnderTest = new BaseService(_apiConfigMock.Object, _requestHandlerMock.Object);

			var responseData = await _subjectUnderTest.GetEnumerable<TestResponseModel>("/invalid_base_url");

			Assert.False(responseData.Success);
			Assert.Null(responseData.Data);
			Assert.Equal("InvalidUrl", responseData.Error);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public async Task Get_WhenUrlIsInvalid_ShouldReturnMissingFieldError(string url)
		{
			ResetMocks();
			_apiConfigMock.SetupProperty(a => a.BaseUrl, "http://www.baseurl.com");
			_subjectUnderTest = new BaseService(_apiConfigMock.Object, _requestHandlerMock.Object);

			var responseData = await _subjectUnderTest.Get<TestResponseModel>(url);

			Assert.False(responseData.Success);
			Assert.Null(responseData.Data);
			Assert.Equal("InvalidUrl", responseData.Error);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public async Task GetEnumerable_WhenUrlIsInvalid_ShouldReturnMissingFieldError(string url)
		{
			ResetMocks();
			_apiConfigMock.SetupProperty(a => a.BaseUrl, "http://www.baseurl.com");
			_subjectUnderTest = new BaseService(_apiConfigMock.Object, _requestHandlerMock.Object);

			var responseData = await _subjectUnderTest.GetEnumerable<TestResponseModel>(url);

			Assert.False(responseData.Success);
			Assert.Null(responseData.Data);
			Assert.Equal("InvalidUrl", responseData.Error);
		}

		[Fact]
		public async Task Get_WhenResponseDataIsEmpty_ShouldReturnError()
		{
			ResetMocks();
			_apiConfigMock.SetupProperty(a => a.BaseUrl, "http://www.baseurl.com");
			_requestHandlerMock.Setup(a => a.GetAsync("http://www.baseurl.com/empty_response")).ReturnsAsync(TestResponseModel_Data.EmptyDataObject);
			_subjectUnderTest = new BaseService(_apiConfigMock.Object, _requestHandlerMock.Object);

			var responseData = await _subjectUnderTest.Get<TestResponseModel>("/empty_response");

			Assert.False(responseData.Success);
			Assert.Null(responseData.Data);
			Assert.Equal("InvalidDataException", responseData.Error);
		}

		[Fact]
		public async Task GetEnumerable_WhenResponseDataIsEmpty_ShouldReturnError()
		{
			ResetMocks();
			_apiConfigMock.SetupProperty(a => a.BaseUrl, "http://www.baseurl.com");
			_requestHandlerMock.Setup(a => a.GetAsync("http://www.baseurl.com/empty_response")).ReturnsAsync(TestResponseModel_Data.EmptyDataObject);
			_subjectUnderTest = new BaseService(_apiConfigMock.Object, _requestHandlerMock.Object);

			var responseData = await _subjectUnderTest.GetEnumerable<TestResponseModel>("/empty_response");

			Assert.False(responseData.Success);
			Assert.Null(responseData.Data);
			Assert.Equal("InvalidDataException", responseData.Error);
		}

		[Fact]
		public async Task Get_WhenResponseDataIsValid_ShouldReturnValidObject()
		{
			ResetMocks();
			_apiConfigMock.SetupProperty(a => a.BaseUrl, "http://www.baseurl.com");
			_requestHandlerMock.Setup(a => a.GetAsync("http://www.baseurl.com/valid")).ReturnsAsync(TestResponseModel_Data.ValidDataObject);
			_subjectUnderTest = new BaseService(_apiConfigMock.Object, _requestHandlerMock.Object);

			var responseData = await _subjectUnderTest.Get<TestResponseModel>("/valid");

			Assert.True(responseData.Success);
			Assert.NotNull(responseData.Data);
			Assert.Null(responseData.Error);
		}

		[Fact]
		public async Task GetEnumerable_WhenResponseDataIsValid_ShouldReturnValidObject()
		{
			ResetMocks();
			_apiConfigMock.SetupProperty(a => a.BaseUrl, "http://www.baseurl.com");
			_requestHandlerMock.Setup(a => a.GetAsync("http://www.baseurl.com/valid")).ReturnsAsync(TestResponseModel_Data.ValidListDataObject);
			_subjectUnderTest = new BaseService(_apiConfigMock.Object, _requestHandlerMock.Object);

			var responseData = await _subjectUnderTest.GetEnumerable<TestResponseModel>("/valid");

			Assert.True(responseData.Success);
			Assert.NotNull(responseData.Data);
			Assert.Equal(4, responseData.Data.Count);
			Assert.Null(responseData.Error);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public async Task Post_WhenBaseUrlIsInvalid_ShouldReturnMissingFieldError(string baseUrl)
		{
			ResetMocks();
			_apiConfigMock.SetupProperty(a => a.BaseUrl, baseUrl);
			_subjectUnderTest = new BaseService(_apiConfigMock.Object, _requestHandlerMock.Object);

			var responseData = await _subjectUnderTest.Post<TestResponseModel>("/invalid_base_url","postData");

			Assert.False(responseData.Success);
			Assert.Null(responseData.Data);
			Assert.Equal("InvalidUrl", responseData.Error);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public async Task Post_WhenUrlIsInvalid_ShouldReturnMissingFieldError(string url)
		{
			ResetMocks();
			_apiConfigMock.SetupProperty(a => a.BaseUrl, "http://www.baseurl.com");
			_subjectUnderTest = new BaseService(_apiConfigMock.Object, _requestHandlerMock.Object);

			var responseData = await _subjectUnderTest.Post<TestResponseModel>(url,"postData");

			Assert.False(responseData.Success);
			Assert.Null(responseData.Data);
			Assert.Equal("InvalidUrl", responseData.Error);
		}

		[Fact]
		public async Task Post_WhenPostDataIsnull_ShouldReturnError()
		{
			ResetMocks();
			_apiConfigMock.SetupProperty(a => a.BaseUrl, "http://www.baseurl.com");
			_requestHandlerMock.Setup(a => a.PostAsync("http://www.baseurl.com/empty_response", null)).ReturnsAsync(TestResponseModel_Data.EmptyDataObject);
			_subjectUnderTest = new BaseService(_apiConfigMock.Object, _requestHandlerMock.Object);

			var responseData = await _subjectUnderTest.Post<TestResponseModel>("/empty_response", null);

			Assert.False(responseData.Success);
			Assert.Null(responseData.Data);
			Assert.Equal("PostDataInvalid", responseData.Error);
		}

		[Fact]
		public async Task Post_WhenResponseDataIsEmpty_ShouldReturnError()
		{
			ResetMocks();
			_apiConfigMock.SetupProperty(a => a.BaseUrl, "http://www.baseurl.com");
			_requestHandlerMock.Setup(a => a.PostAsync("http://www.baseurl.com/empty_response", It.IsAny<string>())).ReturnsAsync(TestResponseModel_Data.EmptyDataObject);
			_subjectUnderTest = new BaseService(_apiConfigMock.Object, _requestHandlerMock.Object);

			var responseData = await _subjectUnderTest.Post<TestResponseModel>("/empty_response","postData");

			Assert.False(responseData.Success);
			Assert.Null(responseData.Data);
			Assert.Equal("InvalidDataException", responseData.Error);
		}

		[Fact]
		public async Task Post_WhenResponseDataIsValid_ShouldReturnValidObject()
		{
			ResetMocks();
			_apiConfigMock.SetupProperty(a => a.BaseUrl, "http://www.baseurl.com");
			_requestHandlerMock.Setup(a => a.PostAsync("http://www.baseurl.com/valid",It.IsAny<string>())).ReturnsAsync(TestResponseModel_Data.ValidDataObject);
			_subjectUnderTest = new BaseService(_apiConfigMock.Object, _requestHandlerMock.Object);

			var responseData = await _subjectUnderTest.Post<TestResponseModel>("/valid","postData");

			Assert.True(responseData.Success);
			Assert.NotNull(responseData.Data);
			Assert.Null(responseData.Error);
		}

	}
}
