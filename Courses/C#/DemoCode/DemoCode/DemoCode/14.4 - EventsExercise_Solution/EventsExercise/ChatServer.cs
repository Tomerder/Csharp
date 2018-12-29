using System;

namespace EventsExercise
{
    class MessageArrivedEventArgs : EventArgs
    {
        private string _senderName;
        private string _messageText;

        public MessageArrivedEventArgs(string senderName, string messageText)
        {
            _senderName = senderName;
            _messageText = messageText;
        }

        public string SenderName { get { return _senderName; } }
        public string MessageText { get { return _messageText; } }
    }

    delegate void MessageArrivedEventHandler(object sender, MessageArrivedEventArgs args);

    class ChatServer
    {
        public event MessageArrivedEventHandler MessageArrived;

        public void SendMessage(string sender, string message)
        {
            if (MessageArrived != null)
            {
                MessageArrived(this, new MessageArrivedEventArgs(sender, message));
            }
        }
    }
}
