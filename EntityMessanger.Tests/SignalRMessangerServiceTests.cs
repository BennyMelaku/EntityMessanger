using EntityMessanger.Domain.Models;
using EntityMessanger.EntityAPI.Services;
using Microsoft.AspNetCore.SignalR.Client;

namespace EntityMessanger.Tests
{
    [TestFixture]
    public class SignalRMessangerServiceTests
    {
        private SignalRMessangerService _service;

        [SetUp]
        public void Setup()
        {
            // Set up the SignalR connection and messenger service
            var connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7053/entitymessanger")
                .Build();
            _service = new SignalRMessangerService(connection);
        }

        [Test]
        public async Task CanConnectToSignalRHub()
        {
            // Ensure that the connection can be established
            await _service.Connect();
            Assert.IsTrue(_service.IsConnected);
        }

        [Test]
        public async Task CanSendAndReceiveEntityMessage()
        {
            // Ensure that the entity message can be sent and received
            var entity = new Entity();
            var messageReceived = false;

            _service.EntityMessageReceived += receivedEntity =>
            {
                Assert.AreEqual(entity, receivedEntity);
                messageReceived = true;
            };

            await _service.SendEntityMessage(entity);

            // Wait for the message to be received
            await Task.Delay(500);

            Assert.IsTrue(messageReceived);
        }
    }
}
