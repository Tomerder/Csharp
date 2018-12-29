// CliDll.h

#pragma once

using namespace System;

namespace CliDll 
{
	public interface class I1 { int c(); int d(); int f(); int h(); };
	public interface class I2 { int f(); int i(); };
	public interface class I3 { int i(); int j(); };
	
	public ref class R : I1, I2, I3 {
	public:
		// virtual int e() override; //error, no virtual e()
		virtual int c() override;
		virtual int d() new; //new slot, doesn't override any d
		virtual int f() sealed; //overrides I1::f and I2::f
		virtual int g() abstract; //same as "=0“
		virtual int x() = I1::h; //overrides I1::h
		virtual int y() = I2::i; //overrides I2::I
		virtual int z() = I3::j, I3::i; //overrides I3::j and I3::I
	};
}
