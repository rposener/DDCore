using DDCore.DefaultProviders;
using DDCore.IntegrationEvents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDCore.Tests
{
    [TestClass]
    public class IntegrationQueueTests
    {
        IntegrationQueue queue;

        [TestInitialize]
        public void SetupTests()
        {
            queue = new IntegrationQueue();
        }

        [TestMethod]
        public void AddToQueue_Validation()
        {
            // Setup
            var IntEvent = new AIntegrationEvent();

            // Test
            queue.QueueEvent(IntEvent);

            // Assert
            var availableEvents = queue.IntegrationEvents;
            Assert.IsNotNull(availableEvents);
            Assert.IsInstanceOfType(availableEvents, typeof(IReadOnlyCollection<IIntegrationEvent>));
            Assert.AreEqual(1, availableEvents.Count);
            Assert.AreSame(IntEvent, availableEvents.First());
        }

        public class AIntegrationEvent : IIntegrationEvent
        {

        }
    }
}
