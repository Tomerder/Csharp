#pragma once

namespace Chat
{
	ref class ChatServer;
	ref class MessageArrivedEventArgs;

	public ref class ChatClient
	{
	private:
		void OnMessageArrived(System::Object^ sender, MessageArrivedEventArgs^ args);
		System::String^ _name;
		ChatServer^ _server;
	public:
		ChatClient(System::String^ name, ChatServer^ server);
		void SendMessage(System::String^ message);
	};
}


