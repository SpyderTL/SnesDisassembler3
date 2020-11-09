public class SnesRom
{
	public void 008C3B_Reset()
	{
		I = 1;
		C = 0;
		temp = C; C = E; E = temp;
		return this.808C42_Start();
	}

	public void 80559C()
	{
		Cpu.Break();
	}

	public void 808202()
	{
		Stack.Push(P);
		Stack.Push(B);
		Stack.Push(K);
		B = Stack.Pop();
		P |= 0x30;
		A = 0x01;
		[0x014B] = A;
	}

	public void 80820D()
	{
		A = [0x014B];
		
		if (Z == 0)
			return this.80820D();

		B = Stack.Pop();
		P = Stack.Pop();
		return;
	}

	public void 808215()
	{
		Stack.Push(P);
		Stack.Push(B);
		Stack.Push(K);
		B = Stack.Pop();
		P |= 0x20;
		A = [0x013C];
		A |= 0x80;
		[0x4200] = A;
		[0x013C] = A;
		B = Stack.Pop();
		P = Stack.Pop();
		return;
	}

	public void 808229()
	{
		Stack.Push(P);
		Stack.Push(B);
		Stack.Push(K);
		B = Stack.Pop();
		P |= 0x20;
		A = [0x013C];
		A &= 0x7F;
		[0x4200] = A;
		[0x013C] = A;
		B = Stack.Pop();
		P = Stack.Pop();
		return;
	}

	public void 80823D()
	{
		Stack.Push(P);
		Stack.Push(B);
		Stack.Push(K);
		B = Stack.Pop();
		P |= 0x20;
		A = [0x0100];
		A |= 0x80;
		[0x0100] = A;
		this.808202();
		B = Stack.Pop();
		P = Stack.Pop();
		return;
	}

	public void 808252()
	{
		Stack.Push(P);
		Stack.Push(B);
		Stack.Push(K);
		B = Stack.Pop();
		P |= 0x20;
		A = [0x0100];
		A &= 0x7F;
		[0x0100] = A;
		this.808202();
		B = Stack.Pop();
		P = Stack.Pop();
		return;
	}

	public void 80827B()
	{
		Stack.Push(P);
		Stack.Push(B);
		Stack.Push(K);
		B = Stack.Pop();
		P |= 0x20;
		A = [0x013C];
		A &= 0xEF;
		[0x4200] = A;
		[0x013C] = A;
		B = Stack.Pop();
		P = Stack.Pop();
		return;
	}

	public void 8082A3()
	{
		Stack.Push(P);
		Stack.Push(B);
		Stack.Push(K);
		B = Stack.Pop();
		P |= 0x20;
		A = [0x013C];
		A &= 0xDF;
		[0x4200] = A;
		[0x013C] = A;
		B = Stack.Pop();
		P = Stack.Pop();
		return;
	}

	public void 808306()
	{
		Stack.Push(P);
		Stack.Push(B);
		Stack.Push(K);
		B = Stack.Pop();
		P |= 0x20;
		A = [0x0154];
		
		if (Z == 1)
			return this.808345();

		[0x0154] = 0;
		[0x420B] = 0;
		[0x420C] = 0;
		P &= ~0x30;
		[0x0152] = 0;
		Y = 0x0000;
		temp = Y - 0x0100;
		
		if (C == 0)
			return this.808329();

	}

	public void 808327()
	{
		return this.808327();
	}

	public void 808329()
	{
		A = [0x0155 + Y];
		A &= 0x00FF;
		temp = A - 0x0005;
		
		if (C == 0)
			return this.808336();

	}

	public void 808334()
	{
		return this.808334();
	}

	public void 808336()
	{
		A <<= 1;
		X = A;
		return [(0x833B + X)]();
		A ^= [0x83];
		A ^= [0x8B83];
		[0xD7] = A;
		[0x29] = A;
		[0xE2] = Y;
		this.80559C();
		A |= [(0xAB + X)];
		P = Stack.Pop();
		return;
	}

	public void 808345()
	{
		P |= 0x20;
		[0x0155] = 0;
		B = Stack.Pop();
		P = Stack.Pop();
		return;
	}

	public void 80850F()
	{
		Stack.Push(P);
		Stack.Push(B);
		Stack.Push(K);
		B = Stack.Pop();
		P &= ~0x30;
	}

	public void 808515()
	{
		[0x7E0000 + X] = A;
		X++;
		X++;
		Y--;
		Y--;
		
		if (Z == 0)
			return this.808515();

		B = Stack.Pop();
		P = Stack.Pop();
		return;
	}

	public void 808744()
	{
		Stack.Push(P);
		Stack.Push(B);
		Stack.Push(K);
		B = Stack.Pop();
		P &= ~0x30;
		A = 0x8000;
		[0x01] = A;
		A = 0x875B;
		[0x00] = A;
		this.8087A4();
		B = Stack.Pop();
		P = Stack.Pop();
		return;
	}

	public void 808764()
	{
		Stack.Push(P);
		Stack.Push(B);
		Stack.Push(K);
		B = Stack.Pop();
		P &= ~0x30;
		A = 0x8000;
		[0x01] = A;
		A = 0x877B;
		[0x00] = A;
		this.8087A4();
		B = Stack.Pop();
		P = Stack.Pop();
		return;
	}

	public void 808784()
	{
		Stack.Push(P);
		Stack.Push(B);
		Stack.Push(K);
		B = Stack.Pop();
		P &= ~0x30;
		A = 0x8000;
		[0x01] = A;
		A = 0x879B;
		[0x00] = A;
		this.8087A4();
		B = Stack.Pop();
		P = Stack.Pop();
		return;
	}

	public void 8087A4()
	{
		Stack.Push(P);
		P &= ~0x30;
		X = [0x0152];
		Y = 0x0000;
		P |= 0x20;
		A = [[0x00] + Y];
		Y++;
		[0x0155 + X] = A;
		A--;
		
		if (Z == 0)
			return this.8087BB();

		return this.808822();
	}

	public void 8087BB()
	{
		A--;
		
		if (Z == 0)
			return this.8087C1();

		return this.8087CD();
	}

	public void 8087C1()
	{
		A--;
		
		if (Z == 0)
			return this.8087C7();

		return this.8087CD();
	}

	public void 8087C7()
	{
		A--;
		
		if (Z == 0)
			return this.8087CD();

		return this.8087F3();
	}

	public void 8087CD()
	{
		[0x015E + X] = 0;
		P &= ~0x20;
		A = [[0x00] + Y];
		[0x0156 + X] = A;
		Y++;
		Y++;
		A = [[0x00] + Y];
		[0x0158 + X] = A;
		Y++;
		Y++;
		A = [[0x00] + Y];
		[0x015A + X] = A;
		Y++;
		Y++;
		A = [[0x00] + Y];
		[0x015C + X] = A;
		A = X;
		C = 0;
		A += 0x0009 + C;
		return this.80883F();
	}

	public void 8087F3()
	{
		[0x0160 + X] = 0;
		P &= ~0x20;
		A = [[0x00] + Y];
		[0x0156 + X] = A;
		Y++;
		Y++;
		A = [[0x00] + Y];
		[0x0158 + X] = A;
		Y++;
		Y++;
		A = [[0x00] + Y];
		[0x015A + X] = A;
		Y++;
		Y++;
		A = [[0x00] + Y];
		[0x015C + X] = A;
		Y++;
		Y++;
		A = [[0x00] + Y];
		[0x015E + X] = A;
		Y++;
		Y++;
		A = X;
		C = 0;
		A += 0x000B + C;
		return this.80883F();
	}

	public void 808822()
	{
		[0x015C + X] = 0;
		P &= ~0x20;
		A = [[0x00] + Y];
		[0x0156 + X] = A;
		Y++;
		Y++;
		A = [[0x00] + Y];
		[0x0158 + X] = A;
		Y++;
		Y++;
		A = [[0x00] + Y];
		[0x015A + X] = A;
		A = X;
		C = 0;
		A += 0x0007 + C;
	}

	public void 80883F()
	{
		[0x0152] = A;
		P |= 0x30;
		A = 0x01;
		[0x0154] = A;
		A = [0x02D6];
		
		if (N == 0)
			return this.808852();

		this.808306();
	}

	public void 808852()
	{
		P = Stack.Pop();
		return;
	}

	public void 808C42_Start()
	{
		P |= 0x20;
		A = 0x80;
		[0x0100] = A;
		[0x2100] = A;
		A = 0x00;
		[0x7FF7] = A;
		P |= 0x30;
		[0x420B] = 0;
		[0x420C] = 0;
		[0x2140] = 0;
		[0x2141] = 0;
		[0x2142] = 0;
		[0x2143] = 0;
		P &= ~0x30;
		X = 0x1FFF;
		S = X;
		Y = 0x0000;
		Stack.Push(Y);
		D = Stack.Pop();
		Stack.Push(K);
		B = Stack.Pop();
		return this.808EE3_Start2();
	}

	public void 808C76()
	{
		Stack.Push(P);
		P |= 0x30;
		A = 0x01;
		[0x4200] = A;
		[0x013C] = A;
		A = 0x80;
		[0x4201] = A;
		[0x4202] = 0;
		[0x4203] = 0;
		[0x4204] = 0;
		[0x4205] = 0;
		[0x4206] = 0;
		[0x4207] = 0;
		[0x013F] = 0;
		[0x4208] = 0;
		[0x0140] = 0;
		[0x4209] = 0;
		[0x013D] = 0;
		[0x420A] = 0;
		[0x013E] = 0;
		[0x420B] = 0;
		[0x420C] = 0;
		[0x0141] = 0;
		A = 0x01;
		[0x420D] = A;
		[0x0142] = A;
		P = Stack.Pop();
		return;
	}

	public void 808CC0()
	{
		Stack.Push(P);
		P |= 0x30;
		A = 0x8F;
		[0x2100] = A;
		[0x0100] = A;
		A = 0x01;
		[0x2101] = A;
		[0x0101] = A;
		[0x2102] = 0;
		[0x0102] = 0;
		A = 0x80;
		[0x2103] = A;
		[0x0103] = A;
		[0x2104] = 0;
		[0x2104] = 0;
		A = 0x09;
		[0x2105] = A;
		[0x0104] = A;
		[0x2106] = 0;
		[0x0105] = 0;
		A = 0x00;
		[0x2107] = A;
		[0x0106] = A;
		A = 0x08;
		[0x2108] = A;
		[0x0107] = A;
		A = 0x1C;
		[0x2109] = A;
		[0x0108] = A;
		A = 0x00;
		[0x210A] = 0;
		[0x0109] = 0;
		A = 0x06;
		[0x210B] = A;
		[0x010A] = A;
		A = 0x01;
		[0x210C] = A;
		[0x010B] = A;
		[0x210D] = 0;
		[0x210D] = 0;
		[0x210E] = 0;
		[0x210E] = 0;
		[0x210F] = 0;
		[0x210F] = 0;
		[0x2110] = 0;
		[0x2110] = 0;
		[0x2111] = 0;
		[0x2111] = 0;
		[0x2112] = 0;
		[0x2112] = 0;
		[0x2113] = 0;
		[0x2113] = 0;
		[0x2114] = 0;
		[0x2114] = 0;
		[0x2115] = 0;
		[0x211A] = 0;
		[0x010C] = 0;
		[0x211B] = 0;
		[0x211C] = 0;
		[0x211D] = 0;
		[0x211E] = 0;
		[0x211F] = 0;
		[0x2120] = 0;
		A = 0x00;
		[0x2123] = A;
		[0x010D] = A;
		A = 0x00;
		[0x2124] = A;
		[0x010E] = A;
		[0x2125] = 0;
		[0x011F] = 0;
		A = 0x00;
		[0x2126] = A;
		[0x0120] = A;
		A = 0xF8;
		[0x2127] = A;
		[0x0121] = A;
		[0x2128] = 0;
		[0x0122] = 0;
		[0x2129] = 0;
		[0x0123] = 0;
		[0x212A] = 0;
		[0x0124] = 0;
		[0x212B] = 0;
		[0x0125] = 0;
		A = 0x11;
		[0x212C] = A;
		[0x0126] = A;
		[0x212E] = A;
		[0x0128] = A;
		A = 0x02;
		[0x212D] = A;
		[0x0127] = A;
		[0x212F] = A;
		[0x0129] = A;
		A = 0x02;
		[0x2130] = A;
		[0x012A] = A;
		A = 0xA1;
		[0x2131] = A;
		[0x012B] = A;
		A = 0x20;
		[0x2132] = A;
		[0x012E] = A;
		A = 0x40;
		[0x2132] = A;
		[0x012D] = A;
		A = 0x80;
		[0x2132] = A;
		[0x012C] = A;
		A = 0x00;
		[0x2133] = A;
		[0x012F] = A;
		P = Stack.Pop();
		return;
	}

	public void 808E19()
	{
		P &= ~0x30;
		A = 0x1C2F;
		this.808E3F();
		A = 0x1C2F;
		this.808E52();
		A = 0x1C2F;
		this.808E65();
		P |= 0x30;
		this.808744();
		this.808764();
		this.808784();
		return;
	}

	public void 808E3F()
	{
		Stack.Push(P);
		Stack.Push(B);
		Stack.Push(K);
		B = Stack.Pop();
		P &= ~0x30;
		X = 0x2000;
		Y = 0x0800;
		this.80850F();
		B = Stack.Pop();
		P = Stack.Pop();
		return;
	}

	public void 808E52()
	{
		Stack.Push(P);
		Stack.Push(B);
		Stack.Push(K);
		B = Stack.Pop();
		P &= ~0x30;
		X = 0x2800;
		Y = 0x0800;
		this.80850F();
		B = Stack.Pop();
		P = Stack.Pop();
		return;
	}

	public void 808E65()
	{
		Stack.Push(P);
		Stack.Push(B);
		Stack.Push(K);
		B = Stack.Pop();
		P &= ~0x30;
		X = 0x3000;
		Y = 0x0800;
		this.80850F();
		B = Stack.Pop();
		P = Stack.Pop();
		return;
	}

	public void 808E78()
	{
		Stack.Push(P);
		P &= ~0x30;
		this.808EA7();
		A = [0x0255];
		temp = A - 0x0005;
		
		if (C == 0)
			return this.808E8E();

		A = 0x0001;
		[0x0257] = A;
		return this.808E91();
	}

	public void 808E8E()
	{
		[0x0257] = 0;
	}

	public void 808E91()
	{
		A = [0x0259];
		C = 0;
		A += [0x025D] + C;
		[0x0261] = A;
		A = [0x025B];
		C = 0;
		A += [0x025F] + C;
		[0x0263] = A;
		P = Stack.Pop();
		return;
	}

	public void 808EA7()
	{
		Stack.Push(P);
		P |= 0x20;
		A = [0x213F];
		temp = A & 0x40;
		
		if (Z == 1)
			return this.808ED9();

		A = [0x00213C];
		[0x0259] = A;
		A = [0x00213C];
		A &= 0x01;
		[0x025A] = A;
		A = [0x00213D];
		[0x025B] = A;
		A = [0x00213D];
		A &= 0x01;
		[0x025C] = A;
		[0x0255] = 0;
	}

	public void 808ED4()
	{
		[0x0256] = 0;
		P = Stack.Pop();
		return;
	}

	public void 808ED9()
	{
		[0x0255]++;
		
		if (Z == 0)
			return this.808ED4();

		[0x0255]--;
		return this.808ED4();
	}

	public void 808EE3_Start2()
	{
		P |= 0x30;
		A = 0xA1;
		[0x7FF5] = A;
		A = 0x0A;
		[0x7FF7] = A;
		P &= ~0x30;
		X = 0x01FE;
	}

	public void 808EF4()
	{
		[0x00 + X] = 0;
		[0x0200 + X] = 0;
		[0x0400 + X] = 0;
		[0x0600 + X] = 0;
		[0x0800 + X] = 0;
		[0x0A00 + X] = 0;
		[0x0C00 + X] = 0;
		[0x0E00 + X] = 0;
		[0x1000 + X] = 0;
		[0x1200 + X] = 0;
		[0x1400 + X] = 0;
		[0x1600 + X] = 0;
		[0x1800 + X] = 0;
		[0x1A00 + X] = 0;
		[0x1C00 + X] = 0;
		[0x1E00 + X] = 0;
		X--;
		X--;
		
		if (N == 0)
			return this.808EF4();

		return this.808F49();
	}

	public void 808F49()
	{
		P |= 0x30;
		[0x4200] = 0;
		[0x013C] = 0;
		A = 0x8F;
		[0x2100] = A;
		[0x0100] = A;
		this.808C76();
		this.808CC0();
		this.808E19();
		this.809D46();
		this.809BAB();
		this.808215();
		P |= 0x20;
		A = 0x54;
		[0x36] = A;
		[0x37] = 0;
		[0x38] = 0;
		A = 0x6B;
		[0x39] = A;
		A = 0x44;
		[0x3A] = A;
		[0x3B] = 0;
		[0x3C] = 0;
		A = 0x6B;
		[0x3D] = A;
		A = 0x01;
		[0x4B] = A;
		P &= ~0x30;
		this.8CC0E8();
		this.808252();
		P &= ~0x30;
		this.80942D();
		P &= ~0x30;
		Y = 0x003C;
	}

	public void 808FA1()
	{
		this.808202();
		Y--;
		
		if (Z == 0)
			return this.808FA1();

		P |= 0x30;
		A = 0x7E;
		Stack.Push(A);
		B = Stack.Pop();
		P &= ~0x30;
		X = 0x0FFE;
		A = 0x0000;
	}

	public void 808FB6()
	{
		[0x2000 + X] = A;
		[0x3000 + X] = A;
		[0x4000 + X] = A;
		[0x5000 + X] = A;
		[0x6000 + X] = A;
		[0x7000 + X] = A;
		[0x8000 + X] = A;
		[0x9000 + X] = A;
		[0xA000 + X] = A;
		[0xB000 + X] = A;
		[0xC000 + X] = A;
		[0xD000 + X] = A;
		[0xE000 + X] = A;
		[0xF000 + X] = A;
		X--;
		X--;
		
		if (N == 0)
			return this.808FB6();

		P |= 0x30;
		A = 0x7F;
		Stack.Push(A);
		B = Stack.Pop();
		P &= ~0x30;
		X = 0x0FFE;
		A = 0x0000;
	}

	public void 808FF2()
	{
		[0x0000 + X] = A;
		[0x1000 + X] = A;
		[0x2000 + X] = A;
		[0x3000 + X] = A;
		[0x4000 + X] = A;
		[0x5000 + X] = A;
		[0x6000 + X] = A;
		[0x7000 + X] = A;
		[0x8000 + X] = A;
		[0x9000 + X] = A;
		[0xA000 + X] = A;
		[0xB000 + X] = A;
		[0xC000 + X] = A;
		[0xD000 + X] = A;
		[0xE000 + X] = A;
		[0xF000 + X] = A;
		X--;
		X--;
		
		if (N == 0)
			return this.808FF2();

		Stack.Push(K);
		B = Stack.Pop();
		P |= 0x20;
		A = 0x0F;
		[0x0100] = A;
	}

	public void 80902F()
	{
		this.808202();
		[0x0100]--;
		
		if (Z == 0)
			return this.80902F();

		P &= ~0x20;
		this.80823D();
		A = 0x8000;
		[0x01] = A;
		A = 0x904E;
		[0x00] = A;
		this.8087A4();
		return this.809059();
	}

	public void 809059()
	{
		A = 0x8000;
		[0x01] = A;
		A = 0x9069;
		[0x00] = A;
		this.8087A4();
		return this.809074();
	}

	public void 809074()
	{
		return this.809078();
	}

	public void 809078()
	{
		P &= ~0x30;
		[0x026D] = 0;
		[0x0265] = 0;
		[0x0267] = 0;
		this.809559();
		this.808202();
	}

	public void 80908B_Loop()
	{
		P &= ~0x30;
		this.8090A1();
		this.808E78();
		this.8090CD();
		this.8090B8();
		this.808202();
		return this.80908B_Loop();
	}

	public void 8090A1()
	{
		this.809570();
		this.809A94();
		this.809B30();
		this.809FDF();
		P &= ~0x30;
		this.809D46();
		return;
	}

	public void 8090B8()
	{
		this.809BAB();
		this.80A005();
		P &= ~0x30;
		A = [0x5A];
		[0x0265] = A;
		A = [0x52];
		[0x0267] = A;
		return;
	}

	public void 8090CD()
	{
		P &= ~0x30;
		Stack.Push(B);
		A = [0x026D];
		A <<= 1;
		A += [0x026D] + C;
		X = A;
		P |= 0x20;
		A = [0x809107];
		temp = A - 0x7D;
		
		if (Z == 1)
			return this.8090F6();

		[0x02] = A;
		Stack.Push(A);
		B = Stack.Pop();
		P &= ~0x20;
		A = [0x809105];
		[0x00] = A;
		this.809102();
	}

	public void 8090F2()
	{
		P &= ~0x30;
		B = Stack.Pop();
		return;
	}

	public void 8090F6()
	{
		P &= ~0x20;
		A = [0x809105];
		[0x026D] = A;
		return this.8090F2();
	}

	public void 809102()
	{
		return [[0x0000]]();	//24-Bit Address
	}

	public void 80942D()
	{
		Stack.Push(P);
		Stack.Push(B);
		Stack.Push(K);
		B = Stack.Pop();
		P &= ~0x30;
		A = 0x0001;
		[0x0322] = A;
		Stack.Push(X);
		Stack.Push(Y);
		A = [0x00];
		Stack.Push(A);
		A = [0x01];
		Stack.Push(A);
		A = 0xB900;
		[0x01] = A;
		Y = 0x8000;
		[0x00] = 0;
		this.8098E6();
		P |= 0x30;
		A = 0xFE;
	}

	public void 809452()
	{
		[0x2142] = A;
		temp = A - [0x2142];
		
		if (Z == 0)
			return this.809452();

		P &= ~0x30;
		X = 0x005E;
	}

	public void 80945F()
	{
		[0x02D7 + X] = 0;
		X--;
		X--;
		
		if (N == 1)
			return this.809468();

		return this.80945F();
	}

	public void 809468()
	{
		P &= ~0x30;
		A = Stack.Pop();
		[0x01] = A;
		A = Stack.Pop();
		[0x00] = A;
		Y = Stack.Pop();
		X = Stack.Pop();
		A = 0x0000;
		[0x0322] = A;
		B = Stack.Pop();
		P = Stack.Pop();
		return;
	}

	public void 809492()
	{
		Stack.Push(P);
		Stack.Push(B);
		Stack.Push(K);
		B = Stack.Pop();
		P &= ~0x30;
		Stack.Push(X);
		X = A;
		A = [0x02F7];
		A &= 0x0001;
		
		if (Z == 0)
			return this.8094B6();

		Stack.Push(X);
		X = 0x0000;
	}

	public void 8094A6()
	{
		A = [0x02D7 + X];
		
		if (Z == 1)
			return this.8094B2();

		X++;
		X++;
		temp = X - 0x0006;
		
		if (Z == 0)
			return this.8094A6();

	}

	public void 8094B2()
	{
		A = Stack.Pop();
		[0x02D7 + X] = A;
	}

	public void 8094B6()
	{
		X = Stack.Pop();
		B = Stack.Pop();
		P = Stack.Pop();
		return;
	}

	public void 8094BA()
	{
		Stack.Push(P);
		Stack.Push(B);
		Stack.Push(K);
		B = Stack.Pop();
		P &= ~0x30;
		Stack.Push(X);
		X = A;
		A = [0x02F7];
		A &= 0x0001;
		
		if (Z == 0)
			return this.8094DE();

		Stack.Push(X);
		X = 0x0000;
	}

	public void 8094CE()
	{
		A = [0x02DF + X];
		
		if (Z == 1)
			return this.8094DA();

		X++;
		X++;
		temp = X - 0x0006;
		
		if (Z == 0)
			return this.8094CE();

	}

	public void 8094DA()
	{
		A = Stack.Pop();
		[0x02DF + X] = A;
	}

	public void 8094DE()
	{
		X = Stack.Pop();
		B = Stack.Pop();
		P = Stack.Pop();
		return;
	}

	public void 8094E2()
	{
		Stack.Push(P);
		Stack.Push(B);
		Stack.Push(K);
		B = Stack.Pop();
		P &= ~0x30;
		Stack.Push(X);
		X = A;
		temp = A - 0x00F0;
		
		if (C == 1)
			return this.809541();

		temp = A - 0x0000;
		
		if (Z == 1)
			return this.809555();

		Stack.Push(Y);
		X = [0x00];
		Stack.Push(X);
		X = [0x01];
		Stack.Push(X);
		X = A;
		A = [0x030B];
		
		if (Z == 0)
			return this.80950E();

		this.808229();
		this.8097B2();
		this.808215();
		P &= ~0x30;
	}

	public void 80950E()
	{
		X = Stack.Pop();
		[0x01] = X;
		X = Stack.Pop();
		[0x00] = X;
		Y = Stack.Pop();
		return this.809555();
	}

	public void 809517()
	{
		Stack.Push(P);
		Stack.Push(B);
		Stack.Push(K);
		B = Stack.Pop();
		P &= ~0x30;
		Stack.Push(X);
		X = A;
		A &= 0x00FF;
		temp = A - 0x00F0;
		
		if (C == 1)
			return this.809555();

		temp = A - 0x00E0;
		
		if (C == 0)
			return this.809555();

		temp = A - 0x00E5;
		
		if (C == 0)
			return this.809534();

		[0x02F9] = X;
	}

	public void 809534()
	{
		A = [0x030B];
		
		if (Z == 0)
			return this.809555();

		A = [0x02F7];
		A &= 0x0002;
		
		if (Z == 0)
			return this.809555();

	}

	public void 809541()
	{
		Stack.Push(X);
		X = 0x0000;
	}

	public void 809545()
	{
		A = [0x02D7 + X];
		
		if (Z == 1)
			return this.809551();

		X++;
		X++;
		temp = X - 0x0006;
		
		if (Z == 0)
			return this.809545();

	}

	public void 809551()
	{
		A = Stack.Pop();
		[0x02D7 + X] = A;
	}

	public void 809555()
	{
		X = Stack.Pop();
		B = Stack.Pop();
		P = Stack.Pop();
		return;
	}

	public void 809559()
	{
		Stack.Push(P);
		Stack.Push(B);
		Stack.Push(K);
		B = Stack.Pop();
		P &= ~0x20;
		Stack.Push(A);
		A = 0xFFFF;
		[0x02EB] = A;
		[0x02EF] = A;
		[0x02F3] = A;
		A = Stack.Pop();
		B = Stack.Pop();
		P = Stack.Pop();
		return;
	}

	public void 809570()
	{
		Stack.Push(P);
		Stack.Push(B);
		Stack.Push(K);
		B = Stack.Pop();
		P &= ~0x30;
		Stack.Push(A);
		Stack.Push(X);
		Stack.Push(Y);
		A = 0xBA00;
		[0x01] = A;
		A = 0x80C0;
		[0x00] = A;
		X = 0x0000;
	}

	public void 809586()
	{
		A = [0x02EB + X];
		
		if (N == 0)
			return this.8095A2();

		temp = A - 0xFFFF;
		
		if (Z == 1)
			return this.8095AA();

		Stack.Push(A);
		A = Stack.Pop();
		A &= 0x00FF;
		A <<= 1;
		Y = X;
		X = A;
		A = [0xBA80C0];
		[0x02EB + Y] = A;
		X = Y;
		return this.8095A7();
	}

	public void 8095A2()
	{
		[0x02ED + X]--;
		
		if (Z == 0)
			return this.8095AA();

	}

	public void 8095A7()
	{
		this.8095BB();
	}

	public void 8095AA()
	{
		temp = X - 0x0005;
		
		if (C == 1)
			return this.8095B5();

		X++;
		X++;
		X++;
		X++;
		return this.809586();
	}

	public void 8095B5()
	{
		Y = Stack.Pop();
		X = Stack.Pop();
		A = Stack.Pop();
		B = Stack.Pop();
		P = Stack.Pop();
		return;
	}

	public void 8095BB()
	{
		Stack.Push(X);
		A = [0x02EB + X];
		Y = A;
	}

	public void 8095C0()
	{
		this.80968B();
		X = A;
		
		if (Z == 0)
			return this.8095CB();

		A--;
		Y = A;
		return this.809682();
	}

	public void 8095CB()
	{
		this.80968B();
		temp = X - 0x2000;
		
		if (C == 0)
			return this.80962F();

		temp = X - 0x3000;
		
		if (C == 0)
			return this.809635();

		temp = X - 0x4000;
		
		if (C == 0)
			return this.80963B();

		temp = X - 0x5000;
		
		if (C == 0)
			return this.809641();

		temp = X - 0x6000;
		
		if (C == 0)
			return this.809647();

		temp = X - 0x7000;
		
		if (C == 0)
			return this.809625();

		temp = X - 0x8000;
		
		if (C == 0)
			return this.809666();

		temp = X - 0x9000;
		
		if (C == 0)
			return this.80961A();

		temp = X - 0xA000;
		
		if (C == 0)
			return this.80960F();

		temp = X - 0xB000;
		
		if (C == 0)
			return this.80964A();

		temp = X - 0xC000;
		
		if (C == 0)
			return this.80964F();

		temp = X - 0xD000;
		
		if (C == 0)
			return this.80965A();

		temp = X - 0xE000;
		
		if (C == 0)
			return this.809660();

	}

	public void 80960F()
	{
		temp = A - 0xFFFF;
		
		if (Z == 1)
			return this.809620();

		A = 0x8000;
		temp = A & [0x031A]; [0x031A] |= A;
	}

	public void 80961A()
	{
		A = [0x0318];
		this.8096FB();
	}

	public void 809620()
	{
		A |= [0x031A];
		return this.809635();
	}

	public void 809625()
	{
		A = [0x02F9];
		this.809517();
		return this.809679();
	}

	public void 80962F()
	{
		this.809492();
		return this.809679();
	}

	public void 809635()
	{
		this.8094BA();
		return this.809679();
	}

	public void 80963B()
	{
		this.809517();
		return this.809679();
	}

	public void 809641()
	{
		this.8094E2();
		return this.809679();
	}

	public void 809647()
	{
		Y = A;
		return this.809679();
	}

	public void 80964A()
	{
		this.809690();
		return this.809679();
	}

	public void 80964F()
	{
		this.809690();
		A = [0x030B];
		
		if (Z == 0)
			return this.809672();

		return this.8095C0();
	}

	public void 80965A()
	{
		this.809711();
		return this.8095C0();
	}

	public void 809660()
	{
		this.809715();
		return this.8095C0();
	}

	public void 809666()
	{
		A = X;
		A &= 0x00FF;
		A &= [0x02FB];
		
		if (Z == 0)
			return this.809672();

		return this.8095C0();
	}

	public void 809672()
	{
		Y--;
		Y--;
		Y--;
		Y--;
		X = 0x0001;
	}

	public void 809679()
	{
		A = X;
		A &= 0x0FFF;
		
		if (Z == 0)
			return this.809682();

		return this.8095C0();
	}

	public void 809682()
	{
		X = Stack.Pop();
		[0x02ED + X] = A;
		A = Y;
		[0x02EB + X] = A;
		return;
	}

	public void 80968B()
	{
		A = [[0x00] + Y];
		Y++;
		Y++;
		return;
	}

	public void 809690()
	{
		Stack.Push(P);
		Stack.Push(B);
		Stack.Push(K);
		B = Stack.Pop();
		P &= ~0x30;
		Stack.Push(X);
		X = A;
		Stack.Push(Y);
		X = [0x00];
		Stack.Push(X);
		X = [0x01];
		Stack.Push(X);
		X = A;
		A = [0x030B];
		
		if (Z == 0)
			return this.8096AB();

		A = 0x0001;
		[0x030B] = A;
	}

	public void 8096AB()
	{
		this.8097B2();
		A = [0x02E7];
		[0x2140] = A;
		A = [0x02D7];
		[0x02E7] = A;
		A = [0x02D9];
		[0x02D7] = A;
		A = [0x02DB];
		[0x02D9] = A;
		A = [0x02DD];
		[0x02DB] = A;
		[0x02DD] = 0;
		A = [0x02E9];
		[0x2142] = A;
		A = [0x02DF];
		[0x02E9] = A;
		A = [0x02E1];
		[0x02DF] = A;
		A = [0x02E3];
		[0x02E1] = A;
		A = [0x02E5];
		[0x02E3] = A;
		[0x02E5] = 0;
		X = Stack.Pop();
		[0x01] = X;
		X = Stack.Pop();
		[0x00] = X;
		Y = Stack.Pop();
		X = Stack.Pop();
		B = Stack.Pop();
		P = Stack.Pop();
		return;
	}

	public void 8096FB()
	{
		A &= 0x00FF;
		[0x031C] = A;
		A = (A >> 4) | (A << 4);
		A >>= 1;
		A >>= 1;
		A >>= 1;
		C = 1;
		A -= 0x031C - !C;
		C = 0;
		A += 0x0100 + C;
		A &= 0x1F00;
		return;
	}

	public void 809711()
	{
		[0x0402] = A;
		return;
	}

	public void 809715()
	{
		temp = A - 0xFFF1;
		
		if (Z == 0)
			return this.80971F();

		this.80973C();
		return this.80973B();
	}

	public void 80971F()
	{
		Stack.Push(A);
		A &= 0xFF00;
		
		if (Z == 0)
			return this.809731();

		A = Stack.Pop();
		A &= 0x00FF;
		A ^= 0xFFFF;
		A &= [0x02F7];
		return this.809738();
	}

	public void 809731()
	{
		A = Stack.Pop();
		A &= 0x00FF;
		A |= [0x02F7];
	}

	public void 809738()
	{
		[0x02F7] = A;
	}

	public void 80973B()
	{
		return;
	}

	public void 80973C()
	{
		Stack.Push(X);
		A = [0x036D];
		A &= 0x0003;
		[0x031C] = A;
		A = [0x0381];
		A &= 0x0001;
		
		if (Z == 1)
			return this.80975A();

		A = 0x0010;
	}

	public void 809751()
	{
		C = 0;
		A += [0x031C] + C;
		[0x031C] = A;
		return this.80976D();
	}

	public void 80975A()
	{
		A = 0x000E;
		[0x0367] = A;
		this.94EB88();
		A = [0x2C];
		
		if (Z == 1)
			return this.80976D();

		A = 0x0008;
		return this.809751();
	}

	public void 80976D()
	{
		A = [0x150C];
		A &= 0x0010;
		
		if (Z == 1)
			return this.80977F();

		A = 0x0004;
		C = 0;
		A += [0x031C] + C;
		[0x031C] = A;
	}

	public void 80977F()
	{
		A = [0x031C];
		temp = A - 0x0018;
		
		if (C == 0)
			return this.80978A();

		A = 0x0000;
	}

	public void 80978A()
	{
		X = A;
		A = [0x80979A];
		A &= 0x00FF;
		A |= 0x8000;
		[0x02F3] = A;
		X = Stack.Pop();
		return;
	}

	public void 8097B2()
	{
		P &= ~0x30;
		[0x0303] = 0;
		A = [0x030B];
		
		if (Z == 1)
			return this.809801();

		[0x0322] = A;
		Y = 0x0040;
		[0x0303] = Y;
		temp = A - 0x0001;
		
		if (Z == 0)
			return this.8097D2();

		[0x030B]++;
		A = 0x00FC;
		return this.809804();
	}

	public void 8097D2()
	{
		A = [0x030D];
		Y = 0x1234;
	}

	public void 8097D8()
	{
		temp = Y - [0x2140];
		
		if (Z == 0)
			return this.8097D8();

		[0x2140] = A;
		Y = 0x007F;
		[0x2142] = Y;
	}

	public void 8097E6()
	{
		temp = A - [0x2140];
		
		if (Z == 0)
			return this.8097E6();

		A = [0x030F];
		[0x00] = A;
		A = [0x0310];
		[0x01] = A;
		A = 0x007F;
		X = [0x0307];
		Y = [0x0309];
		return this.80988B();
	}

	public void 809801()
	{
		A = 0x00FE;
	}

	public void 809804()
	{
		[0x2140] = A;
		[0x2142] = 0;
		A = X;
		A--;
		A <<= 1;
		X = A;
		A = [0xBA8010];
		[0x02FD] = A;
		X = 0x0000;
		return this.809898();
	}

	public void 80981B()
	{
		[0x2142] = X;
		X = A;
		A = [0xBA8201];
		C = 0;
		A += 0xB900 + C;
		[0x01] = A;
		[0x00] = 0;
		A = [0xBA8203];
		A++;
		A >>= 1;
		[0x02FF] = A;
		A = [0xBA8205];
		Y = 0x1234;
	}

	public void 80983B()
	{
		temp = Y - [0x2140];
		
		if (Z == 0)
			return this.80983B();

		[0x2140] = A;
		[0x030D] = A;
		Y = 0x007F;
		[0x2142] = Y;
	}

	public void 80984C()
	{
		temp = A - [0x2140];
		
		if (Z == 0)
			return this.80984C();

		A = [0xBA8200];
		Y = A;
		[0x0301] = 0;
		A = [[0x00] + Y];
		X = A;
		A = 0x007F;
	}

	public void 80985F()
	{
		temp = A - [0x2142];
		
		if (Z == 0)
			return this.80985F();

		[0x2140] = X;
		A = [0x0301];
		[0x2142] = A;
		Y++;
		Y++;
		Stack.Push(A);
		A = [[0x00] + Y];
		X = A;
		[0x0301]++;
		A = 0xFF80;
		temp = A & [0x0301]; [0x0301] &= ~A;
		A = Stack.Pop();
		[0x030D]++;
		[0x030D]++;
		[0x0303]--;
		
		if (Z == 0)
			return this.80988B();

		return this.8098D1();
	}

	public void 80988B()
	{
		[0x02FF]--;
		
		if (Z == 0)
			return this.80985F();

		X = 0x7FFF;
	}

	public void 809893()
	{
		temp = A - [0x2142];
		
		if (Z == 0)
			return this.809893();

	}

	public void 809898()
	{
		Y = [0x02FD];
		A = 0xBA00;
		[0x01] = A;
		[0x00] = 0;
		A = [[0x00] + Y];
		
		if (Z == 1)
			return this.8098AE();

		Y++;
		Y++;
		[0x02FD] = Y;
		return this.80981B();
	}

	public void 8098AE()
	{
		[0x030B] = 0;
		[0x0305] = 0;
		A = 0x00FF;
	}

	public void 8098B7()
	{
		[0x2142] = A;
	}

	public void 8098BA()
	{
		A = [0x2140];
		
		if (Z == 0)
			return this.8098BA();

		A = [0x2142];
		
		if (Z == 0)
			return this.8098BA();

		[0x2142] = A;
		[0x2140] = A;
		A = 0x0000;
		[0x0322] = A;
		return;
	}

	public void 8098D1()
	{
		[0x0307] = X;
		[0x0309] = Y;
		A = [0x00];
		[0x030F] = A;
		A = [0x01];
		[0x0310] = A;
		A = 0xFFFF;
		return this.8098B7();
	}

	public void 8098E6()
	{
		Stack.Push(P);
		P &= ~0x30;
		A = 0xBBAA;
		return this.8098F4();
	}

	public void 8098EE()
	{
		X = 0x00FD;
		[0x2140] = X;
	}

	public void 8098F4()
	{
		temp = A - [0x2140];
		
		if (Z == 0)
			return this.8098EE();

		P |= 0x20;
		A = 0xCC;
		return this.809925();
	}

	public void 8098FF()
	{
		A = [[0x00] + Y];
		Y++;
		A = (A >> 4) | (A << 4);
		A = 0x00;
		return this.809912();
	}

	public void 809907()
	{
		A = (A >> 4) | (A << 4);
		A = [[0x00] + Y];
		Y++;
		A = (A >> 4) | (A << 4);
	}

	public void 80990C()
	{
		temp = A - [0x2140];
		
		if (Z == 0)
			return this.80990C();

		A++;
	}

	public void 809912()
	{
		P &= ~0x20;
		[0x2140] = A;
		P |= 0x20;
		X--;
		
		if (Z == 0)
			return this.809907();

	}

	public void 80991C()
	{
		temp = A - [0x2140];
		
		if (Z == 0)
			return this.80991C();

	}

	public void 809921()
	{
		A += 0x03 + C;
		
		if (Z == 1)
			return this.809921();

	}

	public void 809925()
	{
		Stack.Push(A);
		P &= ~0x20;
		A = [[0x00] + Y];
		Y++;
		Y++;
		X = A;
		A = [[0x00] + Y];
		Y++;
		Y++;
		[0x2142] = A;
		P |= 0x20;
		temp = X - 0x0001;
		A = 0x00;
		Cpu.ROL();
		[0x2141] = A;
		A += 0x7F + C;
		A = Stack.Pop();
		[0x2140] = A;
	}

	public void 809945()
	{
		temp = A - [0x2140];
		
		if (Z == 0)
			return this.809945();

		
		if (F.BVS)
			return this.8098FF();

		P = Stack.Pop();
		return;
	}

	public void 809A94()
	{
		Stack.Push(P);
		P |= 0x20;
		A = [0x030B];
		
		if (Z == 0)
			return this.809ADF();

		A = [0x0100];
		temp = A & 0x80;
		
		if (Z == 0)
			return this.809ADF();

		A ^= 0xFF;
		temp = A & 0x0F;
		
		if (Z == 0)
			return this.809ADF();

		A = [0x026B];
		
		if (Z == 1)
			return this.809ADF();

		A = [0x0269];
		A++;
		
		if (Z == 1)
			return this.809ADF();

		A--;
		
		if (Z == 0)
			return this.809AE1();

		A = [0x53];
		temp = A & 0x40;
		
		if (Z == 1)
			return this.809ADF();

		A = [0x0266];
		temp = A & 0x80;
		
		if (Z == 1)
			return this.809ADF();

		A = 0x3C;
		[0x0269] = A;
		A = [0x0268];
		A &= 0x7F;
		[0x0268] = A;
		A = [0x0266];
		A &= 0x7F;
		[0x0266] = A;
		A = [0x63];
		A &= 0x7F;
		[0x63] = A;
	}

	public void 809ADF()
	{
		P = Stack.Pop();
		return;
	}

	public void 809AE1()
	{
		[0x0269]--;
		A = [0x53];
		temp = A & 0x40;
		
		if (Z == 1)
			return this.809AF5();

		A = [0x0266];
		temp = A & 0x80;
		
		if (Z == 1)
			return this.809ADF();

		this.809B58();
	}

	public void 809AF5()
	{
		A = [0x0266];
		temp = A & 0x90;
		
		if (Z == 1)
			return this.809ADF();

		[0x0269] = 0;
		return this.809ADF();
	}

	public void 809B30()
	{
		Stack.Push(P);
		P &= ~0x30;
		A = [0x030B];
		
		if (Z == 0)
			return this.809B56();

		A = [0x50];
		temp = A - 0x4080;
		
		if (Z == 0)
			return this.809B56();

		A = [0x58];
		temp = A & 0x4000;
		
		if (Z == 1)
			return this.809B56();

		[0x0255] = 0;
		[0x0257] = 0;
		A = 0x00FF;
		temp = A & [0x52]; [0x52] |= A;
		A = 0x8000;
		temp = A & [0x5A]; [0x5A] |= A;
	}

	public void 809B56()
	{
		P = Stack.Pop();
		return;
	}

	public void 809B58()
	{
		Stack.Push(P);
		P &= ~0x30;
		this.80823D();
		P |= 0x20;
		A = 0x81;
		[0x4200] = A;
		[0x013C] = A;
		P &= ~0x20;
		this.80827B();
		this.8082A3();
		P |= 0x20;
		[0x49] = 0;
		[0x4A] = 0;
		[0x0141] = 0;
		[0x0154] = 0;
		P &= ~0x20;
		[0x0152] = 0;
		this.808202();
		[0x026B] = 0;
		[0x14CE] = 0;
		[0x14CC] = 0;
		A = 0x0002;
		[0x026D] = A;
		P = Stack.Pop();
		return;
	}

	public void 809BAB()
	{
		Stack.Push(P);
		P &= ~0x20;
		A = [0xA6];
		A >>= 1;
		[0x00] = A;
		A >>= 1;
		C = 0;
		A += [0x00] + C;
		C = 0;
		A += 0x9BC4 + C;
		[0x00] = A;
		P |= 0x20;
		A = 0xEF;
		return [(0x0000)]();
	}

	public void 809D46()
	{
		Stack.Push(P);
		P &= ~0x30;
		[0x7A00] = 0;
		[0x7A02] = 0;
		[0x7A04] = 0;
		[0x7A06] = 0;
		[0x7A08] = 0;
		[0x7A0A] = 0;
		[0x7A0C] = 0;
		[0x7A0E] = 0;
		[0x7A10] = 0;
		[0x7A12] = 0;
		[0x7A14] = 0;
		[0x7A16] = 0;
		[0x7A18] = 0;
		[0x7A1A] = 0;
		[0x7A1C] = 0;
		[0x7A1E] = 0;
		[0xA6] = 0;
		P = Stack.Pop();
		return;
	}

	public void 809FDF()
	{
		Stack.Push(P);
		P &= ~0x30;
		A = [0x14CE];
		
		if (Z == 1)
			return this.80A003();

		A = [0x0265];
		temp = A & 0x1000;
		
		if (Z == 1)
			return this.80A003();

		A = [0x14CC];
		
		if (Z == 0)
			return this.80A003();

		[0x0265] = 0;
		A = [0x026D];
		[0x14CA] = A;
		A = 0x001B;
		[0x026D] = A;
	}

	public void 80A003()
	{
		P = Stack.Pop();
		return;
	}

	public void 80A005()
	{
		A = [0x58];
		temp = A & 0x1000;
		
		if (Z == 0)
			return this.80A00F();

		return this.80A093();
	}

	public void 80A00F()
	{
		A = [0x026D];
		temp = A - 0x001A;
		
		if (Z == 0)
			return this.80A020();

		A = [0x0371];
		A--;
		
		if (Z == 1)
			return this.80A022();

		A--;
		
		if (Z == 1)
			return this.80A022();

	}

	public void 80A020()
	{
		return this.80A093();
	}

	public void 80A022()
	{
		P |= 0x20;
		A = [0x4A];
		Stack.Push(A);
		P &= ~0x20;
		this.808202();
		P |= 0x20;
		A = Stack.Pop();
		[0x4A] = A;
		P &= ~0x20;
		A = [0x026D];
		temp = A - 0x001A;
		
		if (Z == 0)
			return this.80A05C();

		this.909993();
		this.80A094();
		A = 0x8000;
		[0x01] = A;
		A = 0xA053;
		[0x00] = A;
		this.8087A4();
		return this.80A05C();
	}

	public void 80A05C()
	{
		P |= 0x20;
		A = [0x4A];
		Stack.Push(A);
		P &= ~0x20;
		this.808202();
		P |= 0x20;
		A = Stack.Pop();
		[0x4A] = A;
		P &= ~0x20;
		A = [0x58];
		temp = A & 0x1000;
		
		if (Z == 0)
			return this.80A07E();

		A = [0x5A];
		temp = A & 0x1000;
		
		if (Z == 0)
			return this.80A07E();

		return this.80A05C();
	}

	public void 80A07E()
	{
		this.80A0C6();
		P |= 0x20;
		A = [0x4A];
		Stack.Push(A);
		P &= ~0x20;
		this.808202();
		P |= 0x20;
		A = Stack.Pop();
		[0x4A] = A;
		P &= ~0x20;
	}

	public void 80A093()
	{
		return;
	}

	public void 80A094()
	{
		A = 0x2199;
		[0x7E3318] = A;
		A = 0x2194;
		[0x7E331A] = A;
		A = 0x2195;
		[0x7E331C] = A;
		A = 0x2196;
		[0x7E331E] = A;
		A = 0x2197;
		[0x7E3320] = A;
		A = 0x2198;
		[0x7E3322] = A;
		A = 0x2199;
		[0x7E3324] = A;
		return;
	}

	public void 80A0C6()
	{
		A = 0x0080;
		[0x7E3318] = A;
		[0x7E331A] = A;
		[0x7E331C] = A;
		[0x7E331E] = A;
		[0x7E3320] = A;
		[0x7E3322] = A;
		[0x7E3324] = A;
		return;
	}

	public void 8CC0E8()
	{
		P &= ~0x30;
		this.80823D();
		P |= 0x20;
		A = 0x8F;
		[0x2100] = A;
		[0x0100] = A;
		A = 0x10;
		[0x2102] = A;
		[0x0102] = A;
		A = 0x00;
		[0x2103] = A;
		[0x0103] = A;
		A = 0x09;
		[0x2105] = A;
		[0x0104] = A;
		A = 0x10;
		[0x2109] = A;
		[0x0108] = A;
		A = 0x00;
		[0x210C] = A;
		[0x010B] = A;
		A = 0x04;
		[0x212C] = A;
		[0x0126] = A;
		A = 0x00;
		[0x212D] = A;
		[0x0127] = A;
		A = 0x81;
		[0x4200] = A;
		[0x013C] = A;
		P &= ~0x20;
		A = 0x8C00;
		[0x01] = A;
		A = 0xC1C2;
		[0x00] = A;
		this.8087A4();
		A = 0x8C00;
		[0x01] = A;
		A = 0xC1D4;
		[0x00] = A;
		this.8087A4();
		this.808202();
		A = 0x0000;
		Y = 0x0800;
		X = 0x0000;
	}

	public void 8CC163()
	{
		[0x7E2000 + X] = A;
		X++;
		Y--;
		X++;
		Y--;
		
		if (Z == 0)
			return this.8CC163();

		Y = 0x0050;
		X = 0x0000;
	}

	public void 8CC173()
	{
		A = [0x8CC1DB];
		[0x7E2318 + X] = A;
		X++;
		Y--;
		X++;
		Y--;
		
		if (Z == 0)
			return this.8CC173();

		A = 0x8C00;
		[0x01] = A;
		A = 0xC1CB;
		[0x00] = A;
		this.8087A4();
		this.808202();
		this.808252();
		return;
	}

	public void 909993()
	{
		Stack.Push(B);
		Stack.Push(P);
		Stack.Push(K);
		B = Stack.Pop();
		A = 0xFFFF;
		[0x037F] = A;
		X = 0x0216;
		this.909ACF();
		X = 0x04C6;
		this.909ACF();
		A = 0xFFFF;
		[0x02F3] = A;
		P = Stack.Pop();
		B = Stack.Pop();
		return;
	}

	public void 909ACF()
	{
		A = 0x0080;
		[0x7E3000 + X] = A;
		[0x7E3002 + X] = A;
		[0x7E3004 + X] = A;
		[0x7E3006 + X] = A;
		[0x7E3008 + X] = A;
		[0x7E300A + X] = A;
		[0x7E300C + X] = A;
		[0x7E300E + X] = A;
		[0x7E3010 + X] = A;
		[0x7E3012 + X] = A;
		[0x7E3040 + X] = A;
		[0x7E3042 + X] = A;
		[0x7E3044 + X] = A;
		[0x7E3046 + X] = A;
		[0x7E3048 + X] = A;
		[0x7E304A + X] = A;
		[0x7E304C + X] = A;
		[0x7E304E + X] = A;
		[0x7E3050 + X] = A;
		[0x7E3052 + X] = A;
		return;
	}

	public void 94EB88()
	{
		Stack.Push(B);
		Stack.Push(P);
		Stack.Push(K);
		B = Stack.Pop();
		A = [0x0371];
		
		if (Z == 0)
			return this.94EBBE();

		A = [0x0367];
		A <<= 1;
		A <<= 1;
		X = A;
		A = [0x7060B5];
		A &= 0x00FF;
		[0x26] = A;
		A = [0x7060B6];
		A &= 0x00FF;
		[0x28] = A;
		A = [0x7060B7];
		A &= 0x00FF;
		[0x2A] = A;
		A = [0x7060B8];
		A &= 0x00FF;
		[0x2C] = A;
	}

	public void 94EBBB()
	{
		P = Stack.Pop();
		B = Stack.Pop();
		return;
	}

	public void 94EBBE()
	{
		A = [0x0369];
		A <<= 1;
		A <<= 1;
		C = 0;
		A += [0x0365] + C;
		A <<= 1;
		A <<= 1;
		X = A;
		A = [0x7060F5];
		A &= 0x00FF;
		[0x26] = A;
		A = [0x7060F6];
		A &= 0x00FF;
		[0x28] = A;
		A = [0x7060F7];
		A &= 0x00FF;
		[0x2A] = A;
		A = [0x7060F8];
		A &= 0x00FF;
		[0x2C] = A;
		return this.94EBBB();
	}

}
