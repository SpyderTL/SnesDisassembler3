using System;
using System.Collections.Generic;
using System.Text;

namespace SnesDisassembler
{
	internal static class FunctionReader
	{
		internal static Branch[] Branches;
		internal static Instruction[] Instructions;

		internal static void Read()
		{
			var branches = new List<Branch>();
			var instructions = new List<Instruction>();

			var reading = true;

			while (reading)
			{
				InstructionReader.Read();

				switch (InstructionReader.Type)
				{
					case InstructionReader.InstructionType.Branch:
						instructions.Add(new Instruction { Address = InstructionReader.Address, Length = InstructionReader.Length, Code = InstructionReader.Code, Parameter = InstructionReader.Position + InstructionReader.Parameter });
						branches.Add(new Branch { Address = InstructionReader.Position + InstructionReader.Parameter, Flags = InstructionReader.Flags });
						break;

					case InstructionReader.InstructionType.Call:
						if (InstructionReader.Length == 3)
						{
							instructions.Add(new Instruction { Address = InstructionReader.Address, Length = InstructionReader.Length, Code = InstructionReader.Code, Parameter = (InstructionReader.Address & 0xff0000) | (InstructionReader.Parameter & 0xffff) });
							branches.Add(new Branch { Address = (InstructionReader.Address & 0xff0000) | (InstructionReader.Parameter & 0xffff), Flags = InstructionReader.Flags });
						}
						else
						{
							instructions.Add(new Instruction { Address = InstructionReader.Address, Length = InstructionReader.Length, Code = InstructionReader.Code, Parameter = InstructionReader.Parameter });
							branches.Add(new Branch { Address = InstructionReader.Parameter, Flags = InstructionReader.Flags });
						}
						break;

					case InstructionReader.InstructionType.Jump:
						if (InstructionReader.Length == 3)
						{
							instructions.Add(new Instruction { Address = InstructionReader.Address, Length = InstructionReader.Length, Code = InstructionReader.Code, Parameter = (InstructionReader.Address & 0xff0000) | (InstructionReader.Parameter & 0xffff) });
							branches.Add(new Branch { Address = (InstructionReader.Address & 0xff0000) | (InstructionReader.Parameter & 0xffff), Flags = InstructionReader.Flags });
						}
						else
						{
							instructions.Add(new Instruction { Address = InstructionReader.Address, Length = InstructionReader.Length, Code = InstructionReader.Code, Parameter = InstructionReader.Parameter });
							branches.Add(new Branch { Address = InstructionReader.Parameter, Flags = InstructionReader.Flags });
						}
						reading = false;
						break;

					case InstructionReader.InstructionType.JumpPointer:
					case InstructionReader.InstructionType.JumpPointer24:
						instructions.Add(new Instruction { Address = InstructionReader.Address, Length = InstructionReader.Length, Code = InstructionReader.Code, Parameter = InstructionReader.Parameter });
						reading = false;
						break;

					case InstructionReader.InstructionType.JumpRelative:
						instructions.Add(new Instruction { Address = InstructionReader.Address, Length = InstructionReader.Length, Code = InstructionReader.Code, Parameter = InstructionReader.Position + InstructionReader.Parameter });
						branches.Add(new Branch { Address = InstructionReader.Position + InstructionReader.Parameter, Flags = InstructionReader.Flags });
						reading = false;
						break;

					case InstructionReader.InstructionType.Return:
						instructions.Add(new Instruction { Address = InstructionReader.Address, Length = InstructionReader.Length, Code = InstructionReader.Code, Parameter = InstructionReader.Parameter });
						reading = false;
						break;

					default:
						instructions.Add(new Instruction { Address = InstructionReader.Address, Length = InstructionReader.Length, Code = InstructionReader.Code, Parameter = InstructionReader.Parameter });
						break;
				}

				if (InstructionReader.OpCodes[InstructionReader.Code].Name == "SEP")
					InstructionReader.Flags |= InstructionReader.Parameter;
				else if (InstructionReader.OpCodes[InstructionReader.Code].Name == "REP")
					InstructionReader.Flags &= ~InstructionReader.Parameter;
				else if (InstructionReader.OpCodes[InstructionReader.Code].Name == "BRK")
					reading = false;
				else if (InstructionReader.OpCodes[InstructionReader.Code].Name == "")
					System.Diagnostics.Debug.WriteLine("Unknown Code: " + InstructionReader.Code.ToString("X2"));
			}

			Branches = branches.ToArray();
			Instructions = instructions.ToArray();
		}

		internal struct Instruction
		{
			internal int Address;
			internal int Length;
			internal int Code;
			internal int Parameter;
		}

		internal struct Branch
		{
			internal int Address;
			internal int Flags;
		}
	}
}
