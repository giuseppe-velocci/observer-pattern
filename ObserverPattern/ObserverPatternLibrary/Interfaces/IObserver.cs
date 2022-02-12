namespace ObserverPatternLibrary.Interfaces
{
    public interface IObserver<TEventType> where TEventType : System.Enum 
    {      
        void AddSubscriber(TEventType eventType, ISubscriber subscriber);
        void RemoveSubscriber(TEventType eventType, ISubscriber subscriber);

        void Notify(TEventType eventType, IMessage message);
    }
}
