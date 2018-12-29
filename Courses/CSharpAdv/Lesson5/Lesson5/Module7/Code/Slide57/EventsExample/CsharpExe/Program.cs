using Chat;

namespace CsharpExe
{
    class Program
    {
        static void Main(string[] args)
        {
            ChatServer server = new ChatServer();
            ChatClient joe = new ChatClient("Joe", server);
            ChatClient kate = new ChatClient("Kate", server);
            joe.SendMessage("Hey there!");
            kate.SendMessage("I'm just about to leave...");
            joe.SendMessage("See you later then.");
        }
    }
}
