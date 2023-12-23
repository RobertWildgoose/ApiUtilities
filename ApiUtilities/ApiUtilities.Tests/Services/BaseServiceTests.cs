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


		private void ResetMocks()
		{
			_requestHandlerMock = new Mock<IRequestHandler>();
			_exceptionHandlerMock = new Mock<IExceptionHandler>();
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public async Task GetData_WhenBaseUrlIsInvalid_ShouldThrowException(string baseUrl)
		{
			ResetMocks();
			_exceptionHandlerMock.Setup(a => a.CanHandle(It.IsAny<MissingFieldException>())).Returns(true);
			_subjectUnderTest = new BaseService(_exceptionHandlerMock.Object, _requestHandlerMock.Object);
			_subjectUnderTest.BaseUrl = baseUrl;
			var responseData = await _subjectUnderTest.GetData<TestResponseModel>("/valid");
			_exceptionHandlerMock.Verify(a => a.HandleException(It.IsAny<MissingFieldException>()),Times.Once);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public async Task GetData_WhenUrlIsInvalid_ShouldThrowException(string url)
		{
			ResetMocks();
			_exceptionHandlerMock.Setup(a => a.CanHandle(It.IsAny<MissingFieldException>())).Returns(true);
			_subjectUnderTest = new BaseService(_exceptionHandlerMock.Object, _requestHandlerMock.Object);
			_subjectUnderTest.BaseUrl = "https://www.validUrl.com";
			var responseData = await _subjectUnderTest.GetData<TestResponseModel>(url);
			_exceptionHandlerMock.Verify(a => a.HandleException(It.IsAny<MissingFieldException>()), Times.Once);
		}

		[Fact]
		public async Task GetData_WhenDataIsEmpty_ShouldThrowException()
		{
			ResetMocks();
			_exceptionHandlerMock.Setup(a => a.CanHandle(It.IsAny<InvalidDataException>())).Returns(true);
			_requestHandlerMock.Setup(a => a.GetAsync("https://www.validUrl.com/empty")).ReturnsAsync(TestResponseModel_Data.EmptyDataObject);
			_subjectUnderTest = new BaseService(_exceptionHandlerMock.Object, _requestHandlerMock.Object);
			_subjectUnderTest.BaseUrl = "https://www.validUrl.com";
			var responseData = await _subjectUnderTest.GetData<TestResponseModel>("/empty");
			_exceptionHandlerMock.Verify(a => a.HandleException(It.IsAny<InvalidDataException>()), Times.Once);
		}

		[Fact]
		public async Task GetData_WhenDataIsInvalid_ShouldThrowException()
		{
			ResetMocks();
			_exceptionHandlerMock.Setup(a => a.CanHandle(It.IsAny<InvalidDataException>())).Returns(true);
			_requestHandlerMock.Setup(a => a.GetAsync("https://www.validUrl.com/empty")).ReturnsAsync(TestResponseModel_Data.InvalidDataObject);
			_subjectUnderTest = new BaseService(_exceptionHandlerMock.Object, _requestHandlerMock.Object);
			_subjectUnderTest.BaseUrl = "https://www.validUrl.com";
			var responseData = await _subjectUnderTest.GetData<TestResponseModel>("/invalid");
			_exceptionHandlerMock.Verify(a => a.HandleException(It.IsAny<InvalidDataException>()), Times.Once);
		}

		[Fact]
		public async Task GetData_WhenDataIsValid_ShouldReturnValidObject()
		{
			ResetMocks();
			_requestHandlerMock.Setup(a => a.GetAsync("https://www.validUrl.com/empty")).ReturnsAsync(TestResponseModel_Data.ValidDataObject);
			_subjectUnderTest = new BaseService(_exceptionHandlerMock.Object, _requestHandlerMock.Object);
			_subjectUnderTest.BaseUrl = "https://www.validUrl.com";
			var responseData = await _subjectUnderTest.GetData<TestResponseModel>("/valid");
			responseData.Should().BeNull();
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public async Task GetDataList_WhenBaseUrlIsInvalid_ShouldThrowException(string baseUrl)
		{
			ResetMocks();
			_exceptionHandlerMock.Setup(a => a.CanHandle(It.IsAny<MissingFieldException>())).Returns(true);
			_subjectUnderTest = new BaseService(_exceptionHandlerMock.Object, _requestHandlerMock.Object);
			_subjectUnderTest.BaseUrl = baseUrl;
			var responseData = await _subjectUnderTest.GetDataList<TestResponseModel>("/valid");
			_exceptionHandlerMock.Verify(a => a.HandleException(It.IsAny<MissingFieldException>()), Times.Once);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public async Task GetDataList_WhenUrlIsInvalid_ShouldThrowException(string url)
		{
			ResetMocks();
			_exceptionHandlerMock.Setup(a => a.CanHandle(It.IsAny<MissingFieldException>())).Returns(true);
			_subjectUnderTest = new BaseService(_exceptionHandlerMock.Object, _requestHandlerMock.Object);
			_subjectUnderTest.BaseUrl = "https://www.validUrl.com";
			var responseData = await _subjectUnderTest.GetDataList<TestResponseModel>(url);
			_exceptionHandlerMock.Verify(a => a.HandleException(It.IsAny<MissingFieldException>()), Times.Once);
		}

		[Fact]
		public async Task GetDataList_WhenDataIsEmpty_ShouldThrowException()
		{
			ResetMocks();
			_exceptionHandlerMock.Setup(a => a.CanHandle(It.IsAny<InvalidDataException>())).Returns(true);
			_requestHandlerMock.Setup(a => a.GetAsync("https://www.validUrl.com/empty")).ReturnsAsync(TestResponseModel_Data.EmptyDataObject);
			_subjectUnderTest = new BaseService(_exceptionHandlerMock.Object, _requestHandlerMock.Object);
			_subjectUnderTest.BaseUrl = "https://www.validUrl.com";
			var responseData = await _subjectUnderTest.GetDataList<TestResponseModel>("/empty");
			_exceptionHandlerMock.Verify(a => a.HandleException(It.IsAny<InvalidDataException>()), Times.Once);
		}

		[Fact]
		public async Task GetDataList_WhenDataIsInvalid_ShouldThrowException()
		{
			ResetMocks();
			_exceptionHandlerMock.Setup(a => a.CanHandle(It.IsAny<InvalidDataException>())).Returns(true);
			_requestHandlerMock.Setup(a => a.GetAsync("https://www.validUrl.com/invalid")).ReturnsAsync(TestResponseModel_Data.InvalidDataObject);
			_subjectUnderTest = new BaseService(_exceptionHandlerMock.Object, _requestHandlerMock.Object);
			_subjectUnderTest.BaseUrl = "https://www.validUrl.com";
			var responseData = await _subjectUnderTest.GetDataList<TestResponseModel>("/invalid");
			_exceptionHandlerMock.Verify(a => a.HandleException(It.IsAny<InvalidDataException>()), Times.Once);
		}

		[Fact]
		public async Task GetDataList_WhenDataIsValid_ShouldReturnValidObject()
		{
			ResetMocks();
			_requestHandlerMock.Setup(a => a.GetAsync("https://www.validUrl.com/valid")).ReturnsAsync(TestResponseModel_Data.ValidListDataObject);
			_subjectUnderTest = new BaseService(_exceptionHandlerMock.Object, _requestHandlerMock.Object);
			_subjectUnderTest.BaseUrl = "https://www.validUrl.com";
			var responseData = await _subjectUnderTest.GetDataList<TestResponseModel>("/valid");
			responseData.Count.Should().Be(4);
		}
	}
}
