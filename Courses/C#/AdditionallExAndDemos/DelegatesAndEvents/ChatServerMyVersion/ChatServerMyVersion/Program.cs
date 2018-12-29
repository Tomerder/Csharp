using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatServerMyVersion
{
    class Program
    {
        static void Main(string[] args)
        {
            ChatServer server = new ChatServer();
            ChatClient client1 = new ChatClient("client1", server);
            ChatClient client2 = new ChatClient("client2", server);
            ChatClient client3 = new ChatClient("client3", server);

            client1.chat("Hi this is client 1 calling");
            client2.chat("Hi this is client 2 calling");
            client3.chat("Hi this is client 3 calling");




        }
    }
}
