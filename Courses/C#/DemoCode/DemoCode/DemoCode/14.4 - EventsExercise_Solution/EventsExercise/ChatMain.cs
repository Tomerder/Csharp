using System;

namespace EventsExercise
{
    class ChatMain
    {
        static void Main(string[] args)
        {
            ChatServer server = new ChatServer();

            ChatClient john = new ChatClient("John", server);
            ChatClient mary = new ChatClient("Mary", server);

            john.Say("Hello");
            mary.Say("Oh hi!");
            john.Say("How are you?");
            mary.Say("I'm just fine, how are you?");
        }
    }
}
