namespace ObserverPatternLibrary.Interfaces
{
    public interface ISubscriber
    {
        void Update(IMessage message);
    }
}