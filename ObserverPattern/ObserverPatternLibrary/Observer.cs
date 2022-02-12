using ObserverPatternLibrary.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ObserverPatternLibrary
{
    public class Observer<TEventType> : IObserver<TEventType> where TEventType : System.Enum
    {
        private readonly IList<EventSubscriber<TEventType>> Subscribers;

        public Observer()
        {
            Subscribers = new List<EventSubscriber<TEventType>>();
        }

        public void AddSubscriber(TEventType eventType, ISubscriber subscriber)
        {
            Subscribers.Add(new EventSubscriber<TEventType>(eventType, subscriber));
        }

        public void RemoveSubscriber(TEventType eventType, ISubscriber subscriber)
        {
            var elemToBeRemoved = Subscribers.FirstOrDefault(x => x.EventType.Equals(eventType) && x.Subscriber == subscriber);
            Subscribers.Remove(elemToBeRemoved);
        }

        public void Notify(TEventType eventType, IMessage message)
        {
            var subscribersToNotify = Subscribers
                .Where(x => x.EventType.Equals(eventType))
                .Select(x => x.Subscriber);
            foreach (ISubscriber sub in subscribersToNotify)
            {
                sub.Update(message);
            }
        }
    }
}
