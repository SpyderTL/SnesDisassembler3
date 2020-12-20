using System;
using System.Collections.Generic;
using System.Text;

namespace SnesDisassembler
{
	internal static class InstructionReader
	{
		internal static int Position;
		internal static int Flags;
		internal static int Address;
		internal static int Code;
		internal static int Length;
		internal static InstructionType Type;
		internal static int Parameter;

		internal static void Read()
		{
			var memory16 = (Flags & 0x20) == 0;
			var index16 = (Flags & 0x10) == 0;

			Address = Position;

			Code = Snes.Memory[Position++];
			Length = OpCodes[Code].Length;

			if (memory16 && OpCodes[Code].LengthA16 > Length)
				Length = OpCodes[Code].LengthA16;

			if (index16 && OpCodes[Code].LengthX16 > Length)
				Length = OpCodes[Code].LengthX16;

			Type = OpCodes[Code].Type;

			if (Type == InstructionType.Branch)
			{
				Parameter = (sbyte)Snes.Memory[Position++];
			}
			else if (Type == InstructionType.JumpRelative)
			{
				if (Length == 2)
				{
					Parameter = (sbyte)Snes.Memory[Position++];
				}
				else
				{
					Parameter = BitConverter.ToInt16(Snes.Memory, Position);
					Position += 2;
				}
			}
			else
			{
				switch (Length)
				{
					case 2:
						Parameter = Snes.Memory[Position++];
						break;

					case 3:
						Parameter = Snes.Memory[Position++] | (Snes.Memory[Position++] << 8);
						break;

					case 4:
						Parameter = Snes.Memory[Position++] | (Snes.Memory[Position++] << 8) | (Snes.Memory[Position++] << 16);
						break;

					default:
						Parameter = -1;
						break;
				}
			}
		}

		internal enum InstructionType
		{
			None,
			Jump,
			Branch,
			Call,
			Return,
			Variable,
			Table,
			Pointer24,
			Immediate,
			TablePointer,
			PointerTable,
			StackVariable,
			Pointer,
			StackTablePointer,
			TablePointer24,
			JumpRelative,
			BankCopy,
			JumpPointer,
			JumpPointer24,
			JumpPointerTable
		}

		internal struct OpCode
		{
			internal string Name;
			internal string Description;
			internal int Length;
			internal int LengthA16;
			internal int LengthX16;
			internal InstructionType Type;
			internal IndexRegister Index;
		}

		internal enum IndexRegister
		{
			None,
			X,
			Y
		}

		internal static OpCode[] OpCodes = new OpCode[]
		{
			// 0x00
			new OpCode { Name = "BRK", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "ORA", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.PointerTable, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "COP", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Immediate, Description = "" },
			new OpCode { Name = "ORA", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.StackVariable, Description = "" },
			new OpCode { Name = "TSB", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "ORA", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "ASL", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "ORA", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Pointer24, Description = "" },

			new OpCode { Name = "PHP", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "ORA", Length = 2, LengthA16 = 3, LengthX16 = 2, Type = InstructionType.Immediate, Description = "" },
			new OpCode { Name = "ASL", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "PHD", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "TSB", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "ORA", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "ASL", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "ORA", Length = 4, LengthA16 = 4, LengthX16 = 4, Type = InstructionType.Variable, Description = "" },

			// 0x10
			new OpCode { Name = "BPL", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Branch, Description = "" },
			new OpCode { Name = "ORA", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.TablePointer, Index = IndexRegister.Y, Description = "" },
			new OpCode { Name = "ORA", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Pointer, Description = "" },
			new OpCode { Name = "ORA", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.StackTablePointer, Index = IndexRegister.Y, Description = "" },
			new OpCode { Name = "TRB", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "ORA", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "ASL", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "ORA", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.TablePointer24, Index = IndexRegister.Y, Description = "" },

			new OpCode { Name = "CLC", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "ORA", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Table, Index = IndexRegister.Y, Description = "" },
			new OpCode { Name = "INC", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "TCS", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "TRB", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "ORA", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "ASL", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "ORA", Length = 4, LengthA16 = 4, LengthX16 = 4, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },

			// 0x20
			new OpCode { Name = "JSR", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Call, Description = "" },
			new OpCode { Name = "AND", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.PointerTable, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "JSL", Length = 4, LengthA16 = 4, LengthX16 = 4, Type = InstructionType.Call, Description = "" },
			new OpCode { Name = "AND", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.StackVariable, Description = "" },
			new OpCode { Name = "BIT", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "AND", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "ROL", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "AND", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Pointer24, Description = "" },

			new OpCode { Name = "PLP", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "AND", Length = 2, LengthA16 = 3, LengthX16 = 2, Type = InstructionType.Immediate, Description = "" },
			new OpCode { Name = "ROL", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "PLD", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "BIT", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "AND", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "ROL", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "AND", Length = 4, LengthA16 = 4, LengthX16 = 4, Type = InstructionType.Variable, Description = "" },

			// 0x30
			new OpCode { Name = "BMI", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Branch, Description = "" },
			new OpCode { Name = "AND", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.TablePointer, Index = IndexRegister.Y, Description = "" },
			new OpCode { Name = "AND", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Pointer, Description = "" },
			new OpCode { Name = "AND", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.StackTablePointer, Index = IndexRegister.Y, Description = "" },
			new OpCode { Name = "BIT", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "AND", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "ROL", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "AND", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.TablePointer24, Index = IndexRegister.Y, Description = "" },

			new OpCode { Name = "SEC", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "AND", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Table, Index = IndexRegister.Y, Description = "" },
			new OpCode { Name = "DEC", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "TSC", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "BIT", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "AND", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "ROL", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "AND", Length = 4, LengthA16 = 4, LengthX16 = 4, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },

			// 0x40
			new OpCode { Name = "RTI", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.Return, Description = "" },
			new OpCode { Name = "EOR", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.PointerTable, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "WDM", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "EOR", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.StackVariable, Description = "" },
			new OpCode { Name = "MVP", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.BankCopy, Description = "" },
			new OpCode { Name = "EOR", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "LSR", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "EOR", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Pointer24, Description = "" },

			new OpCode { Name = "PHA", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "EOR", Length = 2, LengthA16 = 3, LengthX16 = 2, Type = InstructionType.Immediate, Description = "" },
			new OpCode { Name = "LSR", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "PHK", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "JMP", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Jump, Description = "" },
			new OpCode { Name = "EOR", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "LSR", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "EOR", Length = 4, LengthA16 = 4, LengthX16 = 4, Type = InstructionType.Variable, Description = "" },

			// 0x50
			new OpCode { Name = "BVC", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Branch, Description = "" },
			new OpCode { Name = "EOR", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.TablePointer, Index = IndexRegister.Y, Description = "" },
			new OpCode { Name = "EOR", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Pointer, Description = "" },
			new OpCode { Name = "EOR", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.StackTablePointer, Index = IndexRegister.Y, Description = "" },
			new OpCode { Name = "MVN", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.BankCopy, Description = "" },
			new OpCode { Name = "EOR", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "LSR", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "EOR", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.TablePointer24, Index = IndexRegister.Y, Description = "" },

			new OpCode { Name = "CLI", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "EOR", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Table, Index = IndexRegister.Y, Description = "" },
			new OpCode { Name = "PHY", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "TCD", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "JMP", Length = 4, LengthA16 = 4, LengthX16 = 4, Type = InstructionType.Jump, Description = "" },
			new OpCode { Name = "EOR", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "LSR", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "EOR", Length = 4, LengthA16 = 4, LengthX16 = 4, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },

			// 0x60
			new OpCode { Name = "RTS", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.Return, Description = "" },
			new OpCode { Name = "ADC", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.PointerTable, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "PER", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Immediate, Description = "" },
			new OpCode { Name = "ADC", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.StackVariable, Description = "" },
			new OpCode { Name = "STZ", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "ADC", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "ROR", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "ADC", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Pointer24, Description = "" },

			new OpCode { Name = "PLA", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "ADC", Length = 2, LengthA16 = 3, LengthX16 = 2, Type = InstructionType.Immediate, Description = "" },
			new OpCode { Name = "ROR", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "RTL", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.Return, Description = "" },
			new OpCode { Name = "JMP", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.JumpPointer, Description = "" },
			new OpCode { Name = "ADC", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "ROR", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "ADC", Length = 4, LengthA16 = 4, LengthX16 = 4, Type = InstructionType.Variable, Description = "" },

			// 0x70
			new OpCode { Name = "BVS", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Branch, Description = "" },
			new OpCode { Name = "ADC", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.TablePointer, Index = IndexRegister.Y, Description = "" },
			new OpCode { Name = "ADC", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Pointer, Description = "" },
			new OpCode { Name = "ADC", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.StackTablePointer, Index = IndexRegister.Y, Description = "" },
			new OpCode { Name = "STZ", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "ADC", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "ROR", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "ADC", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.TablePointer24, Index = IndexRegister.Y, Description = "" },

			new OpCode { Name = "SEI", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "ADC", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Table, Index = IndexRegister.Y, Description = "" },
			new OpCode { Name = "PLY", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "TDC", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "JMP", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.JumpPointerTable, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "ADC", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "ROR", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "ADC", Length = 4, LengthA16 = 4, LengthX16 = 4, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },

			// 0x80
			new OpCode { Name = "BRA", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.JumpRelative, Description = "" },
			new OpCode { Name = "STA", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.PointerTable, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "BRL", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.JumpRelative, Description = "" },
			new OpCode { Name = "STA", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.StackVariable, Description = "" },
			new OpCode { Name = "STY", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "STA", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "STX", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "STA", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.Pointer24, Description = "" },

			new OpCode { Name = "DEY", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "BIT", Length = 2, LengthA16 = 3, LengthX16 = 2, Type = InstructionType.Immediate, Description = "" },
			new OpCode { Name = "TXA", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "PHB", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "STY", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "STA", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "STX", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "STA", Length = 4, LengthA16 = 4, LengthX16 = 4, Type = InstructionType.Variable, Description = "" },

			// 0x90
			new OpCode { Name = "BCC", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Branch, Description = "" },
			new OpCode { Name = "STA", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.TablePointer, Index = IndexRegister.Y, Description = "" },
			new OpCode { Name = "STA", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Pointer, Description = "" },
			new OpCode { Name = "STA", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.StackTablePointer, Index = IndexRegister.Y, Description = "" },
			new OpCode { Name = "STX", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "STA", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "STY", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Table, Index = IndexRegister.Y, Description = "" },
			new OpCode { Name = "STA", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.TablePointer24, Index = IndexRegister.Y, Description = "" },

			new OpCode { Name = "TYA", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "STA", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Table, Index = IndexRegister.Y, Description = "" },
			new OpCode { Name = "TXS", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "TXY", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "STZ", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "STA", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "STZ", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "STA", Length = 4, LengthA16 = 4, LengthX16 = 4, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },

			// 0xA0
			new OpCode { Name = "LDY", Length = 2, LengthA16 = 2, LengthX16 = 3, Type = InstructionType.Immediate, Description = "" },
			new OpCode { Name = "LDA", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.PointerTable, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "LDX", Length = 2, LengthA16 = 2, LengthX16 = 3, Type = InstructionType.Immediate, Description = "" },
			new OpCode { Name = "LDA", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.StackVariable, Description = "" },
			new OpCode { Name = "LDY", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "LDA", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "LDX", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "LDA", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Pointer24, Description = "" },

			new OpCode { Name = "TAY", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "LDA", Length = 2, LengthA16 = 3, LengthX16 = 2, Type = InstructionType.Immediate, Description = "" },
			new OpCode { Name = "TAX", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "PLB", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "LDY", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "LDA", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "LDX", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "LDA", Length = 4, LengthA16 = 4, LengthX16 = 4, Type = InstructionType.Variable, Description = "" },

			// 0xB0
			new OpCode { Name = "BCS", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Branch, Description = "" },
			new OpCode { Name = "LDA", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.TablePointer, Index = IndexRegister.Y, Description = "" },
			new OpCode { Name = "LDA", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Pointer, Description = "" },
			new OpCode { Name = "LDA", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.StackTablePointer, Index = IndexRegister.Y, Description = "" },
			new OpCode { Name = "LDY", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "LDA", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "LDX", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Table, Index = IndexRegister.Y, Description = "" },
			new OpCode { Name = "LDA", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.TablePointer24, Index = IndexRegister.Y, Description = "" },

			new OpCode { Name = "CLV", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "LDA", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Table, Index = IndexRegister.Y, Description = "" },
			new OpCode { Name = "TSX", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "TYX", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "LDY", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "LDA", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "LDX", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Table, Index = IndexRegister.Y, Description = "" },
			new OpCode { Name = "LDA", Length = 4, LengthA16 = 4, LengthX16 = 4, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },

			// 0xC0
			new OpCode { Name = "CPY", Length = 2, LengthA16 = 2, LengthX16 = 3, Type = InstructionType.Immediate, Description = "" },
			new OpCode { Name = "CMP", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.PointerTable, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "REP", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Immediate, Description = "" },
			new OpCode { Name = "CMP", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.StackVariable, Description = "" },
			new OpCode { Name = "CPY", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "CMP", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "DEC", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "CMP", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Pointer24, Description = "" },

			new OpCode { Name = "INY", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "CMP", Length = 2, LengthA16 = 3, LengthX16 = 2, Type = InstructionType.Immediate, Description = "" },
			new OpCode { Name = "DEX", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "WAI", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "CPY", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "CMP", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "DEC", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "CMP", Length = 4, LengthA16 = 4, LengthX16 = 4, Type = InstructionType.Variable, Description = "" },

			// 0xD0
			new OpCode { Name = "BNE", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Branch, Description = "" },
			new OpCode { Name = "CMP", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.TablePointer, Index = IndexRegister.Y, Description = "" },
			new OpCode { Name = "CMP", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Pointer, Description = "" },
			new OpCode { Name = "CMP", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.StackTablePointer, Index = IndexRegister.Y, Description = "" },
			new OpCode { Name = "PEI", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Immediate, Description = "" },
			new OpCode { Name = "CMP", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "DEC", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "CMP", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.TablePointer24, Index = IndexRegister.Y, Description = "" },

			new OpCode { Name = "CLD", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "CMP", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "PHX", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "STP", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "JMP", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.JumpPointer24, Description = "" },
			new OpCode { Name = "CMP", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "DEC", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "CMP", Length = 4, LengthA16 = 4, LengthX16 = 4, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },

			// 0xE0
			new OpCode { Name = "CPX", Length = 2, LengthA16 = 2, LengthX16 = 3, Type = InstructionType.Immediate, Description = "" },
			new OpCode { Name = "SBC", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.PointerTable, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "SEP", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Immediate, Description = "" },
			new OpCode { Name = "SBC", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.StackVariable, Description = "" },
			new OpCode { Name = "CPX", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "SBC", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "INC", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "SBC", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Pointer24, Description = "" },

			new OpCode { Name = "INX", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "SBC", Length = 2, LengthA16 = 3, LengthX16 = 2, Type = InstructionType.Immediate, Description = "" },
			new OpCode { Name = "NOP", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "XBA", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "CPX", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "SBC", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "INC", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Variable, Description = "" },
			new OpCode { Name = "SBC", Length = 4, LengthA16 = 4, LengthX16 = 4, Type = InstructionType.Variable, Description = "" },

			// 0xF0
			new OpCode { Name = "BEQ", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Branch, Description = "" },
			new OpCode { Name = "SBC", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.TablePointer, Index = IndexRegister.Y, Description = "" },
			new OpCode { Name = "SBC", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Pointer, Description = "" },
			new OpCode { Name = "SBC", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.StackTablePointer, Index = IndexRegister.Y, Description = "" },
			new OpCode { Name = "PEA", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Immediate, Description = "" },
			new OpCode { Name = "SBC", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "INC", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "SBC", Length = 2, LengthA16 = 2, LengthX16 = 2, Type = InstructionType.TablePointer24, Index = IndexRegister.Y, Description = "" },

			new OpCode { Name = "SED", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "SBC", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Table, Index = IndexRegister.Y, Description = "" },
			new OpCode { Name = "PLX", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "XCE", Length = 1, LengthA16 = 1, LengthX16 = 1, Type = InstructionType.None, Description = "" },
			new OpCode { Name = "JSR", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.JumpPointerTable, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "SBC", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "INC", Length = 3, LengthA16 = 3, LengthX16 = 3, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },
			new OpCode { Name = "SBC", Length = 4, LengthA16 = 4, LengthX16 = 4, Type = InstructionType.Table, Index = IndexRegister.X, Description = "" },
		};
	}
}
