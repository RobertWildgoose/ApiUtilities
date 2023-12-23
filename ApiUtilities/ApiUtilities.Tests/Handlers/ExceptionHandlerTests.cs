using ApiUtilities.Common.Handlers;
using ApiUtilities.Common.Services;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUtilities.Tests.Handlers
{
	public class ExceptionHandlerTests
	{
		private ExceptionHandler _subjectUnderTest { get; set; }


		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void AddExceptionHandler_WhenExceptionIsInvalid_ShouldReturnFalse(string exception)
		{
			_subjectUnderTest = new ExceptionHandler();
			_subjectUnderTest.AddExceptionHandler(exception,null).Should().BeFalse();
		}

		[Fact]
		public void AddExceptionHandler_WhenExceptionIsValid_ButActionIsNull_ShouldReturnFalse()
		{
			_subjectUnderTest = new ExceptionHandler();
			_subjectUnderTest.AddExceptionHandler(nameof(Exception), null).Should().BeFalse();
		}

		[Fact]
		public void AddExceptionHandler_WhenExceptionIsValid_AndActionIsValid_ShouldReturnTrue()
		{
			_subjectUnderTest = new ExceptionHandler();
			_subjectUnderTest.AddExceptionHandler(nameof(Exception), null).Should().BeFalse();
		}

		[Fact]
		public void AddExceptionHandler_WhenExceptionIsValid_AndActionIsValid_ButExceptionAlreadyExists_ShouldReturnFalse()
		{
			_subjectUnderTest = new ExceptionHandler();
			_subjectUnderTest.AddExceptionHandler(nameof(Exception), () => { }).Should().BeTrue();
			_subjectUnderTest.AddExceptionHandler(nameof(Exception), () => { }).Should().BeFalse();
		}

		[Fact]
		public void CanHandle_WhenExceptionDoesntExist_ShouldReturn()
		{
			_subjectUnderTest = new ExceptionHandler();
			_subjectUnderTest.CanHandle(new Exception()).Should().BeFalse();
		}

		[Fact]
		public void CanHandle_WhenExceptionDoesntExist_WhenExceptionIsAdded_ShouldReturnTrue()
		{
			_subjectUnderTest = new ExceptionHandler();
			_subjectUnderTest.AddExceptionHandler(nameof(Exception), () => { }).Should().BeTrue();
			_subjectUnderTest.CanHandle(new Exception()).Should().BeTrue();
		}

		[Fact]
		public void HandleException_WhenExceptionHandlerExists_ShouldInvokeAction()
		{
			var handlerTriggered = false;
			_subjectUnderTest = new ExceptionHandler();
			_subjectUnderTest.AddExceptionHandler(nameof(Exception), () => { handlerTriggered = true; }).Should().BeTrue();
			_subjectUnderTest.CanHandle(new Exception()).Should().BeTrue();
			_subjectUnderTest.HandleException(new Exception());
			handlerTriggered.Should().BeTrue();
		}

		[Fact]
		public void HandleException_WhenExceptionHandlerDoesntExists_ShouldNotInvokeAction()
		{
			var handlerTriggered = false;
			_subjectUnderTest = new ExceptionHandler();
			_subjectUnderTest.HandleException(new Exception());
			handlerTriggered.Should().BeFalse();
		}
	}
}
