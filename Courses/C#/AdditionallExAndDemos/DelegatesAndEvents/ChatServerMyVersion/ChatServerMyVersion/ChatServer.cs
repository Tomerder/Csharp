using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatServerMyVersion
{
    class ChatServer
    {
        // delegate type for event handling
        public delegate void MsgArrivedEventHandler(Object sender, MsgArrivedEventArgs eventInfo);

        // Event handler for clients to register to
        public event MsgArrivedEventHandler MsgArrivedEvent;
        private void OnMessageArrived(ChatClient sender, string msg)
        {

           
            if (MsgArrivedEvent == null)
                return;

            // else - we have listners
            MsgArrivedEventArgs eventArgs = new  MsgArrivedEventArgs(sender,msg);
            //MsgArrivedEvent(this, eventArgs);
            var invokeList = MsgArrivedEvent.GetInvocationList();
            foreach (var item in invokeList)
            {
                if (item.Target != sender)
                    ((MsgArrivedEventHandler)item)(this, eventArgs);
            }
        }
        public void sendMessage(ChatClient client, string message)
        {
            Console.WriteLine("This is the server printout");
            Console.WriteLine("---------------------------");
            Console.WriteLine("Got a message from: " + client.Name + " message is: " + message);
            Console.WriteLine("End of server printout");
            Console.WriteLine("---------------------------");
            OnMessageArrived(client, message);

        }

    }
}
