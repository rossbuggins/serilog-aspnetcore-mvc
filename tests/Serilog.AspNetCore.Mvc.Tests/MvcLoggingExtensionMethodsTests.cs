using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;
using System;
using Xunit;
using Moq;
using Moq.Protected;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Serilog.AspNetCore.Mvc.Tests
{
    public class MvcLoggingExtensionMethodsTests
    {
        [Fact]
        public void MvcRequestLoggingFilter_Constructor_DiagContext_Default()
        {
            //Arrange
            var diagContext = Mock.Of<IDiagnosticContext>();
            var logger = Mock.Of<ILogger<MvcRequestLoggingFilter>>();
            var dicHelper = Mock.Of<IDictionaryHelper>();

            //Act
            //System Under Test (SUT)
            var sut = new MvcRequestLoggingFilter(
                diagContext,
                logger,
                dicHelper);

            //Assert
            var expected = diagContext;
            var actual = sut.DiagnosticContext;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MvcRequestLoggingFilter_Constructor_Logger_Default()
        {
            //Arrange
            var diagContext = Mock.Of<IDiagnosticContext>();
            var logger = Mock.Of<ILogger<MvcRequestLoggingFilter>>();
            var dicHelper = Mock.Of<IDictionaryHelper>();

            //Act
            //System Under Test (SUT)
            var sut = new MvcRequestLoggingFilter(
                diagContext,
                logger,
                dicHelper);

            //Assert
            var expected = logger;
            var actual = sut.Logger;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MvcRequestLoggingFilter_Constructor_DictionaryHelper_Default()
        {
            //Arrange
            var diagContext = Mock.Of<IDiagnosticContext>();
            var logger = Mock.Of<ILogger<MvcRequestLoggingFilter>>();
            var dicHelper = Mock.Of<IDictionaryHelper>();

            //Act
            //System Under Test (SUT)
            var sut = new MvcRequestLoggingFilter(
                diagContext,
                logger,
                dicHelper);

            //Assert
            var expected = dicHelper;
            var actual = sut.DictionaryHelper;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MvcRequestLoggingFilter_Constructor_DiagContext_Null()
        {
            //Arrange
            IDiagnosticContext diagContext = null;
            var logger = Mock.Of<ILogger<MvcRequestLoggingFilter>>();
            var dicHelper = Mock.Of<IDictionaryHelper>();

            //Act
            var ex = Assert.Throws<ArgumentNullException>(
                () => new MvcRequestLoggingFilter(
                    diagContext,
                    logger,
                    dicHelper));

            //Assert
            var expected = "Value cannot be null. (Parameter 'diag')";
            var actual = ex.Message;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MvcRequestLoggingFilter_Constructor_Logger_Null()
        {
            //Arrange
            var diagContext = Mock.Of<IDiagnosticContext>();
            ILogger <MvcRequestLoggingFilter> logger = null;
            var dicHelper = Mock.Of<IDictionaryHelper>();

            //Act
            var ex = Assert.Throws<ArgumentNullException>(
                () => new MvcRequestLoggingFilter(
                    diagContext,
                    logger,
                    dicHelper));

            //Assert
            var expected = "Value cannot be null. (Parameter 'logger')";
            var actual = ex.Message;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MvcRequestLoggingFilter_Constructor_DicHelper_Null()
        {
            //Arrange
            var diagContext = Mock.Of<IDiagnosticContext>();
            var logger = Mock.Of<ILogger<MvcRequestLoggingFilter>>();
            IDictionaryHelper dicHelper = null;

            //Act
            var ex = Assert.Throws<ArgumentNullException>(
                () => new MvcRequestLoggingFilter(
                    diagContext,
                    logger,
                    dicHelper));

            //Assert
            var expected = "Value cannot be null. (Parameter 'dictionaryHelper')";
            var actual = ex.Message;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MvcRequestLoggingFilter_OnActionExecuting_HostValue()
        {
            var hostValueString = "https://www.testingIsGreat:4444";
            var hostKeyString = "HttpContext.Request.Host.Value";

            var controllerName = "MyControllerName";
            //Arrange
            var store = new Dictionary<string, object>();

            var diagContext = Mock.Of<IDiagnosticContext>();
            var logger = Mock.Of<ILogger<MvcRequestLoggingFilter>>();
            var dicHelperLogger = Mock.Of<ILogger<DictionaryHelper>>();
            var dicHelper = new Mock<DictionaryHelper>(dicHelperLogger);
            dicHelper
                .Setup(x => x.InitializeValueStore())
                .Returns(store);
            dicHelper.CallBase = true;

            var test = dicHelper.Object.InitializeValueStore();

            var httpContext = new DefaultHttpContext();

            var uri = new Uri(hostValueString);
            var hostString = HostString.FromUriComponent(uri);
            httpContext.Request.Host = hostString;

            IList<IFilterMetadata> filters = new List<IFilterMetadata>();
            IDictionary<string, object> actionArguments = new Dictionary<string, object>();
            object controller = Mock.Of<ControllerBase>();

            var routeData = new RouteData();
            var ctrlDescriptor = new ControllerActionDescriptor();
            ctrlDescriptor.ActionName = "MyActionName";
            ctrlDescriptor.ControllerName = "ControllerName";
            ctrlDescriptor.DisplayName = controllerName;
            ctrlDescriptor.ControllerTypeInfo = controller.GetType().GetTypeInfo();
            ctrlDescriptor.AttributeRouteInfo = new AttributeRouteInfo();
            ctrlDescriptor.AttributeRouteInfo.Template = "/abc/this/isroute";

            var actionContext = new ActionContext(
                httpContext,
                routeData,
                ctrlDescriptor);

            var actionExecutingContext = new Mock<ActionExecutingContext>(
                actionContext,
                filters,
                actionArguments,
                controller);

            //System Under Test (SUT)
            var sut = new MvcRequestLoggingFilter(
                diagContext,
                logger,
                dicHelper.Object);

            //Act
            sut.OnActionExecuting(actionExecutingContext.Object);

            //Assert
            var expected = hostString.Value;
            var actual = store[hostKeyString];
            Assert.Equal(expected, actual);
        }
    }
}
