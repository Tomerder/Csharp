#include "stdafx.h"
#include "ChatServer.h"

namespace Chat
{
	void ChatServer::SendMessage(System::String^ from, System::String^ message)
	{
		MessageArrived(this, gcnew MessageArrivedEventArgs(from, message));
	}
}
