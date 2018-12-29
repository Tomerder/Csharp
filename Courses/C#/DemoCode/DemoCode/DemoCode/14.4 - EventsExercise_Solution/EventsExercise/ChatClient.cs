using System;

namespace EventsExercise
{
    class ChatClient
    {
        private string _name;
        private ChatServer _server;

        public ChatClient(string name, ChatServer server)
        {
            _name = name;
            _server = server;

            _server.MessageArrived += OnMessageArrived;
        }

        public void Say(string text)
        {
            _server.SendMessage(_name, text);
        }

        private void OnMessageArrived(object sender, MessageArrivedEventArgs args)
        {
            if (args.SenderName != _name)
            {
                Console.WriteLine("<{0}> {1} says: {2}", _name, args.SenderName, args.MessageText);
            }
        }
    }
}
