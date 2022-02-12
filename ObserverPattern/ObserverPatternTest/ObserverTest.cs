using Moq;
using ObserverPatternLibrary;
using ObserverPatternLibrary.Interfaces;
using Xunit;

namespace ObserverPatternTest
{
    public class ObserverTest
    {
        private readonly IObserver<EventEnum> Sut;
        private readonly Mock<ISubscriber> Subscriber;
        private readonly Mock<IMessage> Message;

        public ObserverTest()
        {
            Sut = new Observer<EventEnum>();
            Subscriber = new Mock<ISubscriber>();
            Message = new Mock<IMessage>();
        }

        [Fact]
        public void Observer_ShouldNotifyOneSubscriber_Success()
        {
            Subscriber.Setup(x => x.Update(It.IsAny<IMessage>()))
                .Verifiable();

            Sut.AddSubscriber(EventEnum.EventA, Subscriber.Object);
            Sut.Notify(EventEnum.EventA, Message.Object);

            Subscriber.Verify(x => x.Update(It.IsAny<IMessage>()), Times.Once);
        }
        
        [Fact]
        public void Observer_ShouldNotNotifyOneSubscriberForUnrelatedEvents_Success()
        {
            Subscriber.Setup(x => x.Update(It.IsAny<IMessage>()))
                .Verifiable();

            Sut.AddSubscriber(EventEnum.EventA, Subscriber.Object);
            Sut.Notify(EventEnum.EventB, Message.Object);

            Subscriber.Verify(x => x.Update(It.IsAny<IMessage>()), Times.Never);
        }
        
        [Fact]
        public void Observer_ShouldNotifyMultipleSubscribers_Success()
        {
            Subscriber.Setup(x => x.Update(It.IsAny<IMessage>()))
                .Verifiable();
            var subscriber1 = new Mock<ISubscriber>();
            subscriber1.Setup(x => x.Update(It.IsAny<IMessage>()))
                .Verifiable();

            Sut.AddSubscriber(EventEnum.EventA, Subscriber.Object);
            Sut.AddSubscriber(EventEnum.EventA, subscriber1.Object);
            Sut.Notify(EventEnum.EventA, Message.Object);

            Subscriber.Verify(x => x.Update(It.IsAny<IMessage>()), Times.Once);
            subscriber1.Verify(x => x.Update(It.IsAny<IMessage>()), Times.Once);
        }
        
        [Fact]
        public void Observer_ShouldNotNotifyManySubscribersForUnrelatedEvents_Success()
        {
            Subscriber.Setup(x => x.Update(It.IsAny<IMessage>()))
                .Verifiable();
            var subscriber1 = new Mock<ISubscriber>();
            subscriber1.Setup(x => x.Update(It.IsAny<IMessage>()))
                .Verifiable();

            Sut.AddSubscriber(EventEnum.EventA, Subscriber.Object);
            Sut.AddSubscriber(EventEnum.EventB, subscriber1.Object);
            Sut.Notify(EventEnum.EventA, Message.Object);

            Subscriber.Verify(x => x.Update(It.IsAny<IMessage>()), Times.Once);
            subscriber1.Verify(x => x.Update(It.IsAny<IMessage>()), Times.Never);
        }

        [Fact]
        public void Observer_ShouldBePossibleToRemoveSubscribers_Success()
        {
            Subscriber.Setup(x => x.Update(It.IsAny<IMessage>()))
                .Verifiable();

            Sut.AddSubscriber(EventEnum.EventA, Subscriber.Object);
            Sut.RemoveSubscriber(EventEnum.EventA, Subscriber.Object);
            Sut.Notify(EventEnum.EventA, Message.Object);

            Subscriber.Verify(x => x.Update(It.IsAny<IMessage>()), Times.Never);
        }
        
        [Fact]
        public void Observer_ShouldNotThrowErrorWhenRemovingNotFoundSubscribers_Success()
        {
            Subscriber.Setup(x => x.Update(It.IsAny<IMessage>()))
                .Verifiable();

            Sut.RemoveSubscriber(EventEnum.EventA, Subscriber.Object);
        }

        [Fact]
        public void Observer_ShouldNotNotifyAnyoneWhenNoSubscribers_Success()
        {
            Subscriber.Setup(x => x.Update(It.IsAny<IMessage>()))
                .Verifiable();

            Sut.Notify(EventEnum.EventA, Message.Object);

            Subscriber.Verify(x => x.Update(It.IsAny<IMessage>()), Times.Never);
        }
    }

    enum EventEnum
    {
        EventA,
        EventB
    }
}
