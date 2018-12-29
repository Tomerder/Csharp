// CliExe.cpp : main project file.

//Demonstrates the use of events in a client-server local chat emulation.
//The client objects register for the server's event in their constructor.

#include "stdafx.h"

using namespace System;
using namespace Chat;

int main(array<System::String ^> ^args)
{
	ChatServer^ server = gcnew ChatServer();
	ChatClient^ joe = gcnew ChatClient("Joe", server);
	ChatClient^ kate = gcnew ChatClient("Kate", server);

	joe->SendMessage("Hey there!");
	kate->SendMessage("I'm just about to leave...");
	joe->SendMessage("See you later then.");
	return 0;
}
