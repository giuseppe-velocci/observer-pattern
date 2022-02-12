using ObserverPatternLibrary.Interfaces;

namespace ObserverPatternLibrary
{
    class EventSubscriber<TEventType> where TEventType : System.Enum
    {
        public TEventType EventType { get; private set; }
        public ISubscriber Subscriber { get; private set; }

        public EventSubscriber(TEventType eventType, ISubscriber subscriber)
        {
            EventType = eventType;
            Subscriber = subscriber;
        }
    }
}
