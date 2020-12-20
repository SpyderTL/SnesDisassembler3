using System;
using System.Collections.Generic;
using System.Linq;

namespace SnesDisassembler
{
	internal class AssemblyConsole
	{
		internal static void Write(List<ProgramConsole.Function> functions)
		{
			for (var x = 0; x < functions.Count; x++)
			{
				var function = functions[x];

				Label(function);

				foreach (var instruction in function.Instructions)
				{
					if (instruction.Address != function.Address &&
						functions.Any(x => x.Address == instruction.Address))
						break;

					var opCode = InstructionReader.OpCodes[instruction.Code];

					Indent();
					Address(instruction);
					Code(instruction);
					Mnemonic(opCode);
					Parameter(instruction, opCode);

					Console.WriteLine();

				}

				Console.WriteLine();
			}
		}

		internal static void Label(ProgramConsole.Function function)
		{
			Console.WriteLine(function.Address.ToString("X6") + ":");
		}

		internal static void Indent()
		{
			Console.Write("\t");
		}

		internal static void Address(FunctionReader.Instruction instruction)
		{
			Console.Write(instruction.Address.ToString("X6") + " ");
		}

		internal static void Code(FunctionReader.Instruction instruction)
		{
			Console.Write(instruction.Code.ToString("X2") + " ");
		}

		internal static void Mnemonic(InstructionReader.OpCode opCode)
		{
			Console.Write(opCode.Name + " ");
		}

		internal static void Parameter(FunctionReader.Instruction instruction, InstructionReader.OpCode opCode)
		{
			if (opCode.Type == InstructionReader.InstructionType.BankCopy)
			{
				Symbol(instruction, opCode);
				Console.Write((instruction.Parameter & 0xff).ToString("X2"));
				Console.Write(" ");
				Symbol(instruction, opCode);
				Console.Write((instruction.Parameter >> 8).ToString("X2"));
			}
			else
			{
				switch (opCode.Type)
				{
					case InstructionReader.InstructionType.Pointer:
					case InstructionReader.InstructionType.PointerTable:
					case InstructionReader.InstructionType.TablePointer:
					case InstructionReader.InstructionType.JumpPointer:
					case InstructionReader.InstructionType.JumpPointerTable:
						Console.Write("(");
						break;

					case InstructionReader.InstructionType.Pointer24:
					case InstructionReader.InstructionType.TablePointer24:
					case InstructionReader.InstructionType.JumpPointer24:
						Console.Write("[");
						break;
				}

				Symbol(instruction, opCode);

				switch (instruction.Length)
				{
					case 2:
						Console.Write(instruction.Parameter.ToString("X2"));
						break;

					case 3:
						Console.Write(instruction.Parameter.ToString("X4"));
						break;

					case 4:
						Console.Write(instruction.Parameter.ToString("X6"));
						break;
				}

				switch (opCode.Type)
				{
					case InstructionReader.InstructionType.Pointer:
						Console.Write(")");
						break;

					case InstructionReader.InstructionType.PointerTable:
						Index(opCode);
						Console.Write(")");
						break;

					case InstructionReader.InstructionType.TablePointer:
						Console.Write(")");
						Index(opCode);
						break;

					case InstructionReader.InstructionType.JumpPointer:
						Console.Write(")");
						break;

					case InstructionReader.InstructionType.JumpPointerTable:
						Index(opCode);
						Console.Write(")");
						break;

					case InstructionReader.InstructionType.JumpPointer24:
						Console.Write("]");
						break;

					case InstructionReader.InstructionType.Pointer24:
						Console.Write("]");
						break;

					case InstructionReader.InstructionType.TablePointer24:
						Console.Write("]");
						Index(opCode);
						break;

					default:
						Index(opCode);
						break;
				}
			}
		}

		internal static void Symbol(FunctionReader.Instruction instruction, InstructionReader.OpCode opCode)
		{
			if (instruction.Length == 1)
				return;

			switch (opCode.Type)
			{
				case InstructionReader.InstructionType.Immediate:
				case InstructionReader.InstructionType.BankCopy:
					Console.Write("#$");
					break;

				default:
					Console.Write("$");
					break;
			}
		}

		internal static void Index(InstructionReader.OpCode opCode)
		{
			if (opCode.Index != InstructionReader.IndexRegister.None)
				Console.Write(", " + opCode.Index);
		}
	}
}