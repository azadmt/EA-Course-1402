using Castle.Components.DictionaryAdapter.Xml;
using Framework.ApplicationBus;
using Framework.Core.Bus;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Framework.Test
{
    public class ApplicationBusTest
    {
        [Fact]
        public void HandleCommand()
        {
            var mockHandler = new Mock<ICommandHandler<TestCommand>>();
            var serviceCollection = (IServiceCollection)new Microsoft.Extensions.DependencyInjection.ServiceCollection();
            serviceCollection.AddScoped<ICommandHandler<TestCommand>>(p => mockHandler.Object);

            var command = new TestCommand { Content = "test" };
            var sut = new BusControl(serviceCollection.BuildServiceProvider());

            sut.Send(command);
            mockHandler.Verify(p => p.Handle(command), Times.Exactly(1));
        }
    }

    public class TestCommand : ICommand
    {
        public Guid TraceId => Guid.NewGuid();

        public string Content { get; set; }
    }
}