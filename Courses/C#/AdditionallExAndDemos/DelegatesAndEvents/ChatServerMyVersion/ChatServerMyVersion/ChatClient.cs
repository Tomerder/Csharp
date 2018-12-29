using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatServerMyVersion
{
    class ChatClient
    {
        public string Name { get; private set; }
        public ChatServer Server { get; private set; }
        public ChatClient(string name, ChatServer server)
        {
            Name = name;
            Server = server;
            Server.MsgArrivedEvent += myResonse;
        }

        public void myResonse(object sender, MsgArrivedEventArgs eventInfo) {
            Console.WriteLine("I am client: " + Name + " Got an event from the server");
            Console.WriteLine("Event info is: " + eventInfo);
        }
        public void chat(string message)
        {
            Server.sendMessage(this, message);
        }
    }
}
