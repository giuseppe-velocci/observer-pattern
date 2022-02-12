using ObserverPatternLibrary.Interfaces;

namespace ObserverPatternLibrary
{
    public class Message : IMessage
    {
        private readonly object Value;

        public Message(object value)
        {
            Value = value;
        }

        public object GetMessage()
        {
            return Value;
        }
    }
}
