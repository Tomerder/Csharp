using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatServerMyVersion
{
    class MsgArrivedEventArgs : EventArgs
    {
        public ChatClient Client {get; private set;}
        public string Message {get; private set;}
        public MsgArrivedEventArgs(ChatClient client, string msg)
        {
            Client = client;
            Message = msg;
        }
        public override string ToString() 
        {
            return ( "Client: " + Client.Name + " , Message: " + Message);
        }
    }
}
