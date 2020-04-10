using DDCore.Abstractions;
using DDCore.Commands;
using DDCore.Configuration;
using DDCore.DefaultProviders;
using DDCore.IntegrationEvents;
using DDCore.Queries;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDCore.Tests
{
    [TestClass]
    public class DispatcherTests
    {
        IServiceProvider provider;
        IIntegrationQueue integrationQueue;
        ILogger<Dispatcher> logger;
        Dispatcher dispatcher;

        [TestInitialize]
        public void Setup()
        {
            provider = Mock.Of<IServiceProvider>();
            integrationQueue = Mock.Of<IIntegrationQueue>();
            logger = Mock.Of<ILogger<Dispatcher>>();
            var options = Microsoft.Extensions.Options.Options.Create(new DDCoreOptions());
            dispatcher = new Dispatcher(provider, integrationQueue, logger, options);
        }

        [TestMethod]
        public async Task DispatchesCommand()
        {
            // Setup
            var handler = Mock.Of<ICommandHandler<CmdA>>();
            Mock.Get(provider).Setup(p => p.GetService(typeof(ICommandHandler<CmdA>))).Returns(handler);
            Mock.Get(handler).Setup(h => h.HandleAsync(It.IsAny<CmdA>())).ReturnsAsync(Result.Success());

            // Test
            await dispatcher.DispatchAsync(new CmdA());

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
            await dispatcher.DispatchAsync(new CmdA());

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
                await dispatcher.DispatchAsync(new CmdA());
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
            var actualResult = await dispatcher.DispatchAsync(new QueryB());

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
                await dispatcher.DispatchAsync(new QueryB());
            }, "Did not throw exception");

            // Asserts
            Mock.Get(provider).Verify(p => p.GetService(typeof(IQueryHandler<QueryB, ResultB>)), Times.Once());
            Mock.Get(handler).Verify(h => h.ExecuteAsync(It.IsAny<QueryB>()), Times.Once());
        }


        [TestMethod]
        public async Task Command_Dispatches_Integration_Events()
        {
            // Setup
            var handler = Mock.Of<ICommandHandler<CmdA>>();
            Mock.Get(provider).Setup(p => p.GetService(typeof(ICommandHandler<CmdA>))).Returns(handler);
            Mock.Get(handler).Setup(h => h.HandleAsync(It.IsAny<CmdA>())).ReturnsAsync(Result.Success());

            var list = new List<IIntegrationEvent>();
            list.Add(new TestIntegrationEvent());
            Mock.Get(integrationQueue).SetupGet(q => q.IntegrationEvents).Returns(list.AsReadOnly());

            var ihandler = Mock.Of<IIntegrationEventHandler<TestIntegrationEvent>>();
            Mock.Get(provider).Setup(p => p.GetService(typeof(IEnumerable<IIntegrationEventHandler<TestIntegrationEvent>>))).Returns(new[] { ihandler });
            Mock.Get(ihandler).Setup(h => h.HandleEventAsync(It.IsAny<TestIntegrationEvent>())).Returns(Task.CompletedTask);


            // Test
            await dispatcher.DispatchAsync(new CmdA());

            // Asserts
            Mock.Get(provider).Verify(p => p.GetService(typeof(IEnumerable<IIntegrationEventHandler<TestIntegrationEvent>>)), Times.Once());
            Mock.Get(ihandler).Verify(h => h.HandleEventAsync(It.IsAny<TestIntegrationEvent>()), Times.Once());
        }




        [TestMethod]
        public async Task Command_Dispatches_Integration_Events_Ignores_Throwing_Handler()
        {
            // Setup
            var handler = Mock.Of<ICommandHandler<CmdA>>();
            Mock.Get(provider).Setup(p => p.GetService(typeof(ICommandHandler<CmdA>))).Returns(handler);
            Mock.Get(handler).Setup(h => h.HandleAsync(It.IsAny<CmdA>())).ReturnsAsync(Result.Success());
            var list = new List<IIntegrationEvent>();
            list.Add(new ThrowingIntegrationEvent());
            list.Add(new TestIntegrationEvent());
            Mock.Get(integrationQueue).SetupGet(q => q.IntegrationEvents).Returns(list.AsReadOnly());

            var ihandler = Mock.Of<IIntegrationEventHandler<TestIntegrationEvent>>();
            Mock.Get(provider).Setup(p => p.GetService(typeof(IEnumerable<IIntegrationEventHandler<TestIntegrationEvent>>))).Returns(new[] { ihandler });
            Mock.Get(ihandler).Setup(h => h.HandleEventAsync(It.IsAny<TestIntegrationEvent>())).Returns(Task.CompletedTask);

            var throwing_handler = Mock.Of<IIntegrationEventHandler<ThrowingIntegrationEvent>>();
            var non_throwing_handler = Mock.Of<IIntegrationEventHandler<ThrowingIntegrationEvent>>();
            Mock.Get(provider).Setup(p => p.GetService(typeof(IEnumerable<IIntegrationEventHandler<ThrowingIntegrationEvent>>))).Returns(new[] { throwing_handler, non_throwing_handler });
            Mock.Get(throwing_handler).Setup(h => h.HandleEventAsync(It.IsAny<ThrowingIntegrationEvent>())).ThrowsAsync(new Exception("exception under test"));
            Mock.Get(non_throwing_handler).Setup(h => h.HandleEventAsync(It.IsAny<ThrowingIntegrationEvent>())).Returns(Task.CompletedTask);


            // Test
            await dispatcher.DispatchAsync(new CmdA());

            // Asserts
            Mock.Get(throwing_handler).Verify(h => h.HandleEventAsync(It.IsAny<ThrowingIntegrationEvent>()), Times.Once());
            Mock.Get(non_throwing_handler).Verify(h => h.HandleEventAsync(It.IsAny<ThrowingIntegrationEvent>()), Times.Once());
            Mock.Get(ihandler).Verify(h => h.HandleEventAsync(It.IsAny<TestIntegrationEvent>()), Times.Once());
        }


        // helper Classes
        public class TestIntegrationEvent : IIntegrationEvent { }

        public class ThrowingIntegrationEvent : IIntegrationEvent { }

        public class CmdA : ICommand { }

        public class ResultB { }

        public class QueryB : IQuery<ResultB> { }
    }
}
