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
		private Mock<IExceptionHandler> _exceptionHandlerMock { get; set; }
		private Mock<IApiConfig> _apiConfigMock { get; set; }


		private void ResetMocks()
		{
			_requestHandlerMock = new Mock<IRequestHandler>();
			_exceptionHandlerMock = new Mock<IExceptionHandler>();
			_apiConfigMock = new Mock<IApiConfig>();
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public async Task GetData_WhenBaseUrlIsInvalid_ShouldHandleMissingFieldException(string baseUrl)
		{
			ResetMocks();
			_exceptionHandlerMock.Setup(a => a.CanHandle(It.IsAny<MissingFieldException>())).Returns(true);
			_apiConfigMock.SetupProperty(a => a.BaseUrl, baseUrl);
			_subjectUnderTest = new BaseService(_apiConfigMock.Object, _exceptionHandlerMock.Object, _requestHandlerMock.Object);
			var responseData = await _subjectUnderTest.GetData<TestResponseModel>("/valid");
			_exceptionHandlerMock.Verify(a => a.HandleException(It.IsAny<MissingFieldException>()),Times.Once);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public async Task GetData_WhenBaseUrlIsInvalid_ShouldThrowMissingFieldException(string baseUrl)
		{
			ResetMocks();
			_exceptionHandlerMock.Setup(a => a.CanHandle(It.IsAny<MissingFieldException>())).Returns(false);
			_apiConfigMock.SetupProperty(a => a.BaseUrl, baseUrl);
			_subjectUnderTest = new BaseService(_apiConfigMock.Object, _exceptionHandlerMock.Object, _requestHandlerMock.Object);
			var exception = await Assert.ThrowsAsync<MissingFieldException>(async () => await _subjectUnderTest.GetData<TestResponseModel>("/valid"));
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public async Task GetData_WhenUrlIsInvalid_ShouldHandleMissingFieldException(string url)
		{
			ResetMocks();
			_exceptionHandlerMock.Setup(a => a.CanHandle(It.IsAny<MissingFieldException>())).Returns(true);
			_apiConfigMock.SetupProperty(a => a.BaseUrl, null);
			_subjectUnderTest = new BaseService(_apiConfigMock.Object, _exceptionHandlerMock.Object, _requestHandlerMock.Object);
			var responseData = await _subjectUnderTest.GetData<TestResponseModel>(url);
			_exceptionHandlerMock.Verify(a => a.HandleException(It.IsAny<MissingFieldException>()), Times.Once);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public async Task GetData_WhenUrlIsInvalid_ShouldThrowMissingFieldException(string url)
		{
			ResetMocks();
			_exceptionHandlerMock.Setup(a => a.CanHandle(It.IsAny<MissingFieldException>())).Returns(false);
			_apiConfigMock.SetupProperty(a => a.BaseUrl, null);
			_subjectUnderTest = new BaseService(_apiConfigMock.Object, _exceptionHandlerMock.Object, _requestHandlerMock.Object);
			var exception = await Assert.ThrowsAsync<MissingFieldException>(async () => await _subjectUnderTest.GetData<TestResponseModel>(url));
		}

		[Fact]
		public async Task GetData_WhenDataIsEmpty_ShouldHandlesInvalidDataException()
		{
			ResetMocks();
			_exceptionHandlerMock.Setup(a => a.CanHandle(It.IsAny<InvalidDataException>())).Returns(true);
			_requestHandlerMock.Setup(a => a.GetAsync("https://www.validUrl.com/empty")).ReturnsAsync(TestResponseModel_Data.EmptyDataObject);
			_apiConfigMock.SetupProperty(a => a.BaseUrl, "https://www.validUrl.com");
			_subjectUnderTest = new BaseService(_apiConfigMock.Object, _exceptionHandlerMock.Object, _requestHandlerMock.Object);
			var responseData = await _subjectUnderTest.GetData<TestResponseModel>("/empty");
			_exceptionHandlerMock.Verify(a => a.HandleException(It.IsAny<InvalidDataException>()), Times.Once);
		}

		[Fact]
		public async Task GetData_WhenDataIsEmpty_ShouldThrowInvalidDataException()
		{
			ResetMocks();
			_exceptionHandlerMock.Setup(a => a.CanHandle(It.IsAny<InvalidDataException>())).Returns(false);
			_requestHandlerMock.Setup(a => a.GetAsync("https://www.validUrl.com/empty")).ReturnsAsync(TestResponseModel_Data.EmptyDataObject);
			_apiConfigMock.SetupProperty(a => a.BaseUrl, "https://www.validUrl.com");
			_subjectUnderTest = new BaseService(_apiConfigMock.Object, _exceptionHandlerMock.Object, _requestHandlerMock.Object);
			var exception = await Assert.ThrowsAsync<InvalidDataException>(async () => await _subjectUnderTest.GetData<TestResponseModel>("/empty"));
		}


		[Fact]
		public async Task GetData_WhenDataIsValid_ShouldReturnValidObject()
		{
			ResetMocks();
			_requestHandlerMock.Setup(a => a.GetAsync("https://www.validUrl.com/valid")).ReturnsAsync(TestResponseModel_Data.ValidDataObject);
			_apiConfigMock.SetupProperty(a => a.BaseUrl, "https://www.validUrl.com");
			_subjectUnderTest = new BaseService(_apiConfigMock.Object, _exceptionHandlerMock.Object, _requestHandlerMock.Object);
			var responseData = await _subjectUnderTest.GetData<TestResponseModel>("/valid");
			responseData.Should().NotBeNull();
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public async Task GetDataList_WhenBaseUrlIsInvalid_ShouldHandlesMissingFieldException(string baseUrl)
		{
			ResetMocks();
			_exceptionHandlerMock.Setup(a => a.CanHandle(It.IsAny<MissingFieldException>())).Returns(true);
			_apiConfigMock.SetupProperty(a => a.BaseUrl, baseUrl);
			_subjectUnderTest = new BaseService(_apiConfigMock.Object, _exceptionHandlerMock.Object, _requestHandlerMock.Object);
			var responseData = await _subjectUnderTest.GetDataList<TestResponseModel>("/valid");
			_exceptionHandlerMock.Verify(a => a.HandleException(It.IsAny<MissingFieldException>()), Times.Once);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public async Task GetDataList_WhenBaseUrlIsInvalid_ShouldThrowMissingFieldException(string baseUrl)
		{
			ResetMocks();
			_exceptionHandlerMock.Setup(a => a.CanHandle(It.IsAny<MissingFieldException>())).Returns(false);
			_apiConfigMock.SetupProperty(a => a.BaseUrl, baseUrl);
			_subjectUnderTest = new BaseService(_apiConfigMock.Object, _exceptionHandlerMock.Object, _requestHandlerMock.Object);
			var exception = await Assert.ThrowsAsync<MissingFieldException>(async () => await _subjectUnderTest.GetDataList<TestResponseModel>("/valid"));
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public async Task GetDataList_WhenUrlIsInvalid_ShouldHandlesMissingFieldException(string url)
		{
			ResetMocks();
			_exceptionHandlerMock.Setup(a => a.CanHandle(It.IsAny<MissingFieldException>())).Returns(true);
			_apiConfigMock.SetupProperty(a => a.BaseUrl, "https://www.validUrl.com");
			_subjectUnderTest = new BaseService(_apiConfigMock.Object, _exceptionHandlerMock.Object, _requestHandlerMock.Object);
			var responseData = await _subjectUnderTest.GetDataList<TestResponseModel>(url);
			_exceptionHandlerMock.Verify(a => a.HandleException(It.IsAny<MissingFieldException>()), Times.Once);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public async Task GetDataList_WhenUrlIsInvalid_ShouldThrowMissingFieldException(string url)
		{
			ResetMocks();
			_exceptionHandlerMock.Setup(a => a.CanHandle(It.IsAny<MissingFieldException>())).Returns(false);
			_apiConfigMock.SetupProperty(a => a.BaseUrl, "https://www.validUrl.com");
			_subjectUnderTest = new BaseService(_apiConfigMock.Object, _exceptionHandlerMock.Object, _requestHandlerMock.Object);
			var exception = await Assert.ThrowsAsync<MissingFieldException>(async () => await _subjectUnderTest.GetDataList<TestResponseModel>(url));
		}

		[Fact]
		public async Task GetDataList_WhenDataIsEmpty_ShouldhandlesInvalidDataException()
		{
			ResetMocks();
			_exceptionHandlerMock.Setup(a => a.CanHandle(It.IsAny<InvalidDataException>())).Returns(true);
			_requestHandlerMock.Setup(a => a.GetAsync("https://www.validUrl.com/empty")).ReturnsAsync(TestResponseModel_Data.EmptyDataObject);
			_apiConfigMock.SetupProperty(a => a.BaseUrl, "https://www.validUrl.com");
			_subjectUnderTest = new BaseService(_apiConfigMock.Object, _exceptionHandlerMock.Object, _requestHandlerMock.Object);
			var responseData = await _subjectUnderTest.GetDataList<TestResponseModel>("/empty");
			_exceptionHandlerMock.Verify(a => a.HandleException(It.IsAny<InvalidDataException>()), Times.Once);
		}

		[Fact]
		public async Task GetDataList_WhenDataIsEmpty_ShouldThrowInvalidDataException()
		{
			ResetMocks();
			_exceptionHandlerMock.Setup(a => a.CanHandle(It.IsAny<InvalidDataException>())).Returns(false);
			_requestHandlerMock.Setup(a => a.GetAsync("https://www.validUrl.com/empty")).ReturnsAsync(TestResponseModel_Data.EmptyDataObject);
			_apiConfigMock.SetupProperty(a => a.BaseUrl, "https://www.validUrl.com");
			_subjectUnderTest = new BaseService(_apiConfigMock.Object, _exceptionHandlerMock.Object, _requestHandlerMock.Object);
			var exception = await Assert.ThrowsAsync<InvalidDataException>(async () => await _subjectUnderTest.GetDataList<TestResponseModel>("/empty"));
		}


		[Fact]
		public async Task GetDataList_WhenDataIsValid_ShouldReturnValidObject()
		{
			ResetMocks();
			_requestHandlerMock.Setup(a => a.GetAsync("https://www.validUrl.com/valid")).ReturnsAsync(TestResponseModel_Data.ValidListDataObject);
			_apiConfigMock.SetupProperty(a => a.BaseUrl, "https://www.validUrl.com");
			_subjectUnderTest = new BaseService(_apiConfigMock.Object, _exceptionHandlerMock.Object, _requestHandlerMock.Object);
			var responseData = await _subjectUnderTest.GetDataList<TestResponseModel>("/valid");
			responseData.Count.Should().Be(4);
		}
	}
}
