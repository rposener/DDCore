using DDCore.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DDCore.Tests
{
    [TestClass]
    public class MessagesTests
    {
        IServiceProvider provider;
        ILogger<Messages> logger;
        Messages messages;

        [TestInitialize]
        public void Setup()
        {
            provider = Mock.Of<IServiceProvider>();
            logger = Mock.Of<ILogger<Messages>>();
            messages = new Messages(provider, logger);
        }

        [TestMethod]
        public async Task DispatchesCommand()
        {
            // Setup
            var handler = Mock.Of<ICommandHandler<CmdA>>();
            Mock.Get(provider).Setup(p => p.GetService(typeof(ICommandHandler<CmdA>))).Returns(handler);
            Mock.Get(handler).Setup(h => h.HandleAsync(It.IsAny<CmdA>())).ReturnsAsync(Result.Success());

            // Test
            await messages.DispatchAsync(new CmdA());

            // Asserts
            Mock.Get(provider).Verify(p => p.GetService(typeof(ICommandHandler<CmdA>)), Times.Once());
            Mock.Get(handler).Verify(h => h.HandleAsync(It.IsAny<CmdA>()), Times.Once());
        }

        [TestMethod]
        public async Task Dispatch_Logs_When_Failure()
        {
            // Setup
            var handler = Mock.Of<ICommandHandler<CmdA>>();
            Mock.Get(provider).Setup(p => p.GetService(typeof(ICommandHandler<CmdA>))).Returns(handler);
            Mock.Get(handler).Setup(h => h.HandleAsync(It.IsAny<CmdA>())).ReturnsAsync(Result.Failure("AA==failed"));

            // Test
            await messages.DispatchAsync(new CmdA());

            // Asserts
            Mock.Get(provider).Verify(p => p.GetService(typeof(ICommandHandler<CmdA>)), Times.Once());
            Mock.Get(handler).Verify(h => h.HandleAsync(It.IsAny<CmdA>()), Times.Once());
        }

        [TestMethod]
        public async Task Dispatch_Throws_On_Exception()
        {
            // Setup
            var handler = Mock.Of<ICommandHandler<CmdA>>();
            Mock.Get(provider).Setup(p => p.GetService(typeof(ICommandHandler<CmdA>))).Returns(handler);
            Mock.Get(handler).Setup(h => h.HandleAsync(It.IsAny<CmdA>())).ThrowsAsync(new ArgumentOutOfRangeException());

            // Test
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(async () =>
           {
               await messages.DispatchAsync(new CmdA());
           }, "Did not throw exception");

            // Asserts
            Mock.Get(provider).Verify(p => p.GetService(typeof(ICommandHandler<CmdA>)), Times.Once());
            Mock.Get(handler).Verify(h => h.HandleAsync(It.IsAny<CmdA>()), Times.Once());
        }


        [TestMethod]
        public async Task DispatchesQuery()
        {
            // Setup
            var handler = Mock.Of<IQueryHandler<QueryB, ResultB>>();
            Mock.Get(provider).Setup(p => p.GetService(typeof(IQueryHandler<QueryB, ResultB>))).Returns(handler);

            var providedResult = new ResultB();
            Mock.Get(handler).Setup(h => h.ExecuteAsync(It.IsAny<QueryB>())).ReturnsAsync(providedResult);

            // Test
            var actualResult = await messages.DispatchAsync(new QueryB());

            // Asserts
            Mock.Get(provider).Verify(p => p.GetService(typeof(IQueryHandler<QueryB, ResultB>)), Times.Once());
            Mock.Get(handler).Verify(h => h.ExecuteAsync(It.IsAny<QueryB>()), Times.Once());
            Assert.AreSame(providedResult, actualResult, "results were not the same object");
        }


        [TestMethod]
        public async Task Dispatch_Throws_On_Query_Exception()
        {
            // Setup
            var handler = Mock.Of<IQueryHandler<QueryB, ResultB>>();
            Mock.Get(provider).Setup(p => p.GetService(typeof(IQueryHandler<QueryB, ResultB>))).Returns(handler);
            Mock.Get(handler).Setup(h => h.ExecuteAsync(It.IsAny<QueryB>())).ThrowsAsync(new ArgumentOutOfRangeException());

            // Test
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(async () =>
            {
                await messages.DispatchAsync(new QueryB());
            }, "Did not throw exception");

            // Asserts
            Mock.Get(provider).Verify(p => p.GetService(typeof(IQueryHandler<QueryB, ResultB>)), Times.Once());
            Mock.Get(handler).Verify(h => h.ExecuteAsync(It.IsAny<QueryB>()), Times.Once());
        }


        // helper Classes
        public class CmdA:ICommand { }

        public class ResultB { }

        public class QueryB: IQuery<ResultB> { }
    }
}
