#pragma once

namespace Chat
{
	public ref class MessageArrivedEventArgs : System::EventArgs
	{
	private:
		System::String^ _message;
		System::String^ _from;
	public:
		MessageArrivedEventArgs(System::String^ from, System::String^ message)
		{
			_from = from;
			_message = message;
		}
		property System::String^ Message
		{
			System::String^ get() { return _message; }
		}
		property System::String^ From
		{
			System::String^ get() { return _from; }
		}
	};

	public delegate void MessageArrivedEventHandler(System::Object^ sender, MessageArrivedEventArgs^ e);

	public ref class ChatServer
	{
	public:
		void SendMessage(System::String^ from, System::String^ message);
		event MessageArrivedEventHandler^ MessageArrived;
	};
}
