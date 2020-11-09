using System;
using System.Collections.Generic;
using System.Linq;

namespace SnesDisassembler
{
	internal class CSharpConsole
	{
		internal static void Write(List<ProgramConsole.Function> functions)
		{
			Console.WriteLine("public class SnesRom");
			Console.WriteLine("{");

			for (var x = 0; x < functions.Count; x++)
			{
				var function = functions[x];

				Function(function);

				foreach (var instruction in function.Instructions)
				{
					if (instruction.Address != function.Address &&
						functions.Any(x => x.Address == instruction.Address))
						break;

					var opCode = InstructionReader.OpCodes[instruction.Code];

					Indent();
					Indent();

					//Address(instruction);
					//Code(instruction);
					//Mnemonic(opCode);
					//Parameter(instruction, opCode);
					Instruction(instruction, opCode);

					Console.WriteLine();
				}

				Indent();
				Console.WriteLine("}");

				Console.WriteLine();
			}

			Console.WriteLine("}");
		}

		private static void Instruction(FunctionReader.Instruction instruction, InstructionReader.OpCode opCode)
		{
			switch (opCode.Type)
			{
				case InstructionReader.InstructionType.None:
					None(opCode);
					break;

				case InstructionReader.InstructionType.Branch:
					Console.WriteLine();
					Indent();
					Indent();
					Branch(opCode);
					Indent();
					Indent();
					Indent();
					Console.WriteLine("return this." + instruction.Parameter.ToString("X6") + "();");
					break;

				case InstructionReader.InstructionType.Call:
					Console.Write("this." + instruction.Parameter.ToString("X6") + "();");
					break;

				case InstructionReader.InstructionType.Jump:
				case InstructionReader.InstructionType.JumpRelative:
					Console.Write("return this." + instruction.Parameter.ToString("X6") + "();");
					break;

				case InstructionReader.InstructionType.JumpPointer:
					Console.Write("return [");
					Parameter(instruction, opCode);
					Console.Write("]();");
					break;

				case InstructionReader.InstructionType.JumpPointer24:
					Console.Write("return [");
					Parameter(instruction, opCode);
					Console.Write("]();\t//24-Bit Address");
					break;

				case InstructionReader.InstructionType.Immediate:
					Immediate(instruction, opCode);
					//Console.Write("Cpu." + opCode.Name + "(");
					//Parameter(instruction, opCode);
					//Console.Write(");");
					break;

				case InstructionReader.InstructionType.Variable:
				case InstructionReader.InstructionType.StackVariable:
					Variable(instruction, opCode);
					//Console.Write("Cpu." + opCode.Name + "(");
					//Parameter(instruction, opCode);
					//Console.Write(");");
					break;

				case InstructionReader.InstructionType.Table:
					Table(instruction, opCode);
					break;

				case InstructionReader.InstructionType.Pointer:
					Pointer(instruction, opCode);
					break;

				case InstructionReader.InstructionType.PointerTable:
					PointerTable(instruction, opCode);
					break;

				case InstructionReader.InstructionType.TablePointer:
				case InstructionReader.InstructionType.StackTablePointer:
					TablePointer(instruction, opCode);
					break;

				case InstructionReader.InstructionType.TablePointer24:
					TablePointer24(instruction, opCode);
					break;

				case InstructionReader.InstructionType.Pointer24:
					Pointer24(instruction, opCode);
					break;

				case InstructionReader.InstructionType.BankCopy:
					BankCopy(instruction, opCode);
					break;

				case InstructionReader.InstructionType.Return:
					Console.Write("return;");
					break;

				default:
					Console.Write("// " + opCode.Type);
					break;
			}
		}

		private static void BankCopy(FunctionReader.Instruction instruction, InstructionReader.OpCode opCode)
		{
			switch (opCode.Name)
			{
				case "MVP":
					Console.Write("Cpu.BankCopyAndIncrement(0x" + (instruction.Parameter & 0xff).ToString("X2") + ", 0x" + (instruction.Parameter >> 8).ToString("X2") + ");");
					break;

				case "MVN":
					Console.Write("Cpu.BankCopyAndDecrement(0x" + (instruction.Parameter & 0xff).ToString("X2") + ", 0x" + (instruction.Parameter >> 8).ToString("X2") + ");");
					break;

				default:
					Console.Write("// " + opCode.Type);
					break;
			}
		}

		private static void None(InstructionReader.OpCode opCode)
		{
			switch (opCode.Name)
			{
				case "INC":
					Console.Write("A++;");
					break;

				case "INX":
					Console.Write("X++;");
					break;

				case "INY":
					Console.Write("Y++;");
					break;

				case "DEC":
					Console.Write("A--;");
					break;

				case "DEX":
					Console.Write("X--;");
					break;

				case "DEY":
					Console.Write("Y--;");
					break;

				case "TAX":
					Console.Write("X = A;");
					break;

				case "TAY":
					Console.Write("Y = A;");
					break;

				case "TSX":
					Console.Write("X = S;");
					break;

				case "TXA":
					Console.Write("A = X;");
					break;

				case "TXS":
					Console.Write("S = X;");
					break;

				case "TXY":
					Console.Write("Y = X;");
					break;

				case "TYA":
					Console.Write("A = Y;");
					break;

				case "TYX":
					Console.Write("X = Y;");
					break;

				case "SEC":
					Console.Write("C = 1;");
					break;

				case "CLC":
					Console.Write("C = 0;");
					break;

				case "SEI":
					Console.Write("I = 1;");
					break;

				case "BRK":
					Console.Write("Cpu.Break();");
					break;

				case "CLI":
					Console.Write("I = 0;");
					break;

				case "ASL":
					Console.Write("A <<= 1;");
					break;

				case "LSR":
					Console.Write("A >>= 1;");
					break;

				case "PHA":
					Console.Write("Stack.Push(A);");
					break;

				case "PHX":
					Console.Write("Stack.Push(X);");
					break;

				case "PHY":
					Console.Write("Stack.Push(Y);");
					break;

				case "PHB":
					Console.Write("Stack.Push(B);");
					break;

				case "PHD":
					Console.Write("Stack.Push(D);");
					break;

				case "PHP":
					Console.Write("Stack.Push(P);");
					break;

				case "PHK":
					Console.Write("Stack.Push(K);");
					break;

				case "PLA":
					Console.Write("A = Stack.Pop();");
					break;

				case "PLX":
					Console.Write("X = Stack.Pop();");
					break;

				case "PLY":
					Console.Write("Y = Stack.Pop();");
					break;

				case "PLB":
					Console.Write("B = Stack.Pop();");
					break;

				case "PLD":
					Console.Write("D = Stack.Pop();");
					break;

				case "PLP":
					Console.Write("P = Stack.Pop();");
					break;

				case "XBA":
					Console.Write("A = (A >> 4) | (A << 4);");
					break;

				case "XCE":
					Console.Write("temp = C; C = E; E = temp;");
					break;

				default:
					Console.Write("Cpu." + opCode.Name + "();");
					break;
			}
		}

		private static void Variable(FunctionReader.Instruction instruction, InstructionReader.OpCode opCode)
		{
			switch (opCode.Name)
			{
				case "ADC":
					Console.Write("A += [");
					Parameter(instruction, opCode);
					Console.Write("] + C;");
					break;

				case "AND":
					Console.Write("A &= [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "ORA":
					Console.Write("A |= [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "EOR":
					Console.Write("A ^= [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "SBC":
					Console.Write("A -= ");
					Parameter(instruction, opCode);
					Console.Write(" - !C;");
					break;

				case "INC":
					Console.Write("[");
					Parameter(instruction, opCode);
					Console.Write("]++;");
					break;

				case "DEC":
					Console.Write("[");
					Parameter(instruction, opCode);
					Console.Write("]--;");
					break;

				case "BIT":
					Console.Write("temp = A & [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "TSB":
					Console.Write("temp = A & [");
					Parameter(instruction, opCode);
					Console.Write("]; [");
					Parameter(instruction, opCode);
					Console.Write("] |= A;");
					break;

				case "TRB":
					Console.Write("temp = A & [");
					Parameter(instruction, opCode);
					Console.Write("]; [");
					Parameter(instruction, opCode);
					Console.Write("] &= ~A;");
					break;

				case "CMP":
					Console.Write("temp = A - [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "CPX":
					Console.Write("temp = X - [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "CPY":
					Console.Write("temp = Y - [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "LDA":
					Console.Write("A = [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "LDX":
					Console.Write("X = [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "LDY":
					Console.Write("Y = [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "STA":
					Console.Write("[");
					Parameter(instruction, opCode);
					Console.Write("] = A;");
					break;

				case "STX":
					Console.Write("[");
					Parameter(instruction, opCode);
					Console.Write("] = X;");
					break;

				case "STY":
					Console.Write("[");
					Parameter(instruction, opCode);
					Console.Write("] = Y;");
					break;

				case "STZ":
					Console.Write("[");
					Parameter(instruction, opCode);
					Console.Write("] = 0;");
					break;

				case "ASL":
					Console.Write("[");
					Parameter(instruction, opCode);
					Console.Write("] <<= 1;");
					break;

				case "LSR":
					Console.Write("[");
					Parameter(instruction, opCode);
					Console.Write("] >>= 1;");
					break;

				default:
					Console.Write("Cpu." + opCode.Name + "(");
					Parameter(instruction, opCode);
					Console.Write(");");
					break;
			}
		}

		private static void Immediate(FunctionReader.Instruction instruction, InstructionReader.OpCode opCode)
		{
			switch (opCode.Name)
			{
				case "ADC":
					Console.Write("A += ");
					Parameter(instruction, opCode);
					Console.Write(" + C;");
					break;

				case "AND":
					Console.Write("A &= ");
					Parameter(instruction, opCode);
					Console.Write(";");
					break;

				case "ORA":
					Console.Write("A |= ");
					Parameter(instruction, opCode);
					Console.Write(";");
					break;

				case "EOR":
					Console.Write("A ^= ");
					Parameter(instruction, opCode);
					Console.Write(";");
					break;

				case "SBC":
					Console.Write("A -= ");
					Parameter(instruction, opCode);
					Console.Write(" - !C;");
					break;

				case "BIT":
					Console.Write("temp = A & ");
					Parameter(instruction, opCode);
					Console.Write(";");
					break;

				case "CMP":
					Console.Write("temp = A - ");
					Parameter(instruction, opCode);
					Console.Write(";");
					break;

				case "CPX":
					Console.Write("temp = X - ");
					Parameter(instruction, opCode);
					Console.Write(";");
					break;

				case "CPY":
					Console.Write("temp = Y - ");
					Parameter(instruction, opCode);
					Console.Write(";");
					break;

				case "LDA":
					Console.Write("A = ");
					Parameter(instruction, opCode);
					Console.Write(";");
					break;

				case "LDX":
					Console.Write("X = ");
					Parameter(instruction, opCode);
					Console.Write(";");
					break;

				case "LDY":
					Console.Write("Y = ");
					Parameter(instruction, opCode);
					Console.Write(";");
					break;

				case "SEP":
					Console.Write("P |= ");
					Parameter(instruction, opCode);
					Console.Write(";");
					break;

				case "REP":
					Console.Write("P &= ~");
					Parameter(instruction, opCode);
					Console.Write(";");
					break;

				case "COP":
					Console.Write("Coprocessor.Execute(");
					Parameter(instruction, opCode);
					Console.Write(");");
					break;

				default:
					Console.Write("Cpu." + opCode.Name + "(");
					Parameter(instruction, opCode);
					Console.Write("); // ??");
					break;
			}
		}

		private static void Table(FunctionReader.Instruction instruction, InstructionReader.OpCode opCode)
		{
			switch (opCode.Name)
			{
				case "ADC":
					Console.Write("A += [");
					Parameter(instruction, opCode);
					Console.Write("] + C;");
					break;

				case "AND":
					Console.Write("A &= [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "ORA":
					Console.Write("A |= [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "EOR":
					Console.Write("A ^= [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "SBC":
					Console.Write("A -= [");
					Parameter(instruction, opCode);
					Console.Write("] - !C;");
					break;

				case "BIT":
					Console.Write("temp = A & [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "CMP":
					Console.Write("temp = A - [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "CPX":
					Console.Write("temp = X - [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "CPY":
					Console.Write("temp = Y - [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "INC":
					Console.Write("[");
					Parameter(instruction, opCode);
					Console.Write("]++;");
					break;

				case "DEC":
					Console.Write("[");
					Parameter(instruction, opCode);
					Console.Write("]--;");
					break;

				case "LDA":
					Console.Write("A = [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "LDX":
					Console.Write("X = [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "LDY":
					Console.Write("Y = [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "STA":
					Console.Write("[");
					Parameter(instruction, opCode);
					Console.Write("] = A;");
					break;

				case "STX":
					Console.Write("[");
					Parameter(instruction, opCode);
					Console.Write("] = X;");
					break;

				case "STY":
					Console.Write("[");
					Parameter(instruction, opCode);
					Console.Write("] = Y;");
					break;

				case "STZ":
					Console.Write("[");
					Parameter(instruction, opCode);
					Console.Write("] = 0;");
					break;

				case "JMP":
					Console.Write("return [");
					Parameter(instruction, opCode);
					Console.Write("]();");
					break;

				default:
					Console.Write("Cpu." + opCode.Name + "(");
					Parameter(instruction, opCode);
					Console.Write(");");
					break;
			}
		}

		private static void Pointer(FunctionReader.Instruction instruction, InstructionReader.OpCode opCode)
		{
			switch (opCode.Name)
			{
				case "ADC":
					Console.Write("A += [");
					Parameter(instruction, opCode);
					Console.Write("] + C;");
					break;

				case "AND":
					Console.Write("A &= [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "ORA":
					Console.Write("A |= [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "EOR":
					Console.Write("A ^= [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "SBC":
					Console.Write("A -= [");
					Parameter(instruction, opCode);
					Console.Write("] - !C;");
					break;

				case "BIT":
					Console.Write("temp = A & [");
					Parameter(instruction, opCode);
					Console.Write(";");
					break;

				case "CMP":
					Console.Write("temp = A - [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "CPX":
					Console.Write("temp = X - [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "CPY":
					Console.Write("temp = Y - [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "LDA":
					Console.Write("A = [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "LDX":
					Console.Write("X = [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "LDY":
					Console.Write("Y = [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "STA":
					Console.Write("[");
					Parameter(instruction, opCode);
					Console.Write("] = A;");
					break;

				case "STX":
					Console.Write("[");
					Parameter(instruction, opCode);
					Console.Write("] = X;");
					break;

				case "STY":
					Console.Write("[");
					Parameter(instruction, opCode);
					Console.Write("] = Y;");
					break;

				case "STZ":
					Console.Write("[");
					Parameter(instruction, opCode);
					Console.Write("] = 0;");
					break;

				default:
					Console.Write("Cpu." + opCode.Name + "(");
					Parameter(instruction, opCode);
					Console.Write(");");
					break;
			}
		}

		private static void PointerTable(FunctionReader.Instruction instruction, InstructionReader.OpCode opCode)
		{
			switch (opCode.Name)
			{
				case "ADC":
					Console.Write("A += [");
					Parameter(instruction, opCode);
					Console.Write("] + C;");
					break;

				case "AND":
					Console.Write("A &= [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "ORA":
					Console.Write("A |= [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "EOR":
					Console.Write("A ^= [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "SBC":
					Console.Write("A -= [");
					Parameter(instruction, opCode);
					Console.Write("] - !C;");
					break;

				case "BIT":
					Console.Write("temp = A & [");
					Parameter(instruction, opCode);
					Console.Write(";");
					break;

				case "CMP":
					Console.Write("temp = A - [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "CPX":
					Console.Write("temp = X - [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "CPY":
					Console.Write("temp = Y - [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "LDA":
					Console.Write("A = [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "LDX":
					Console.Write("X = [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "LDY":
					Console.Write("Y = [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "STA":
					Console.Write("[");
					Parameter(instruction, opCode);
					Console.Write("] = A;");
					break;

				case "STX":
					Console.Write("[");
					Parameter(instruction, opCode);
					Console.Write("] = X;");
					break;

				case "STY":
					Console.Write("[");
					Parameter(instruction, opCode);
					Console.Write("] = Y;");
					break;

				case "STZ":
					Console.Write("[");
					Parameter(instruction, opCode);
					Console.Write("] = 0;");
					break;

				case "JMP":
					Console.Write("return [");
					Parameter(instruction, opCode);
					Console.Write("]();");
					break;

				default:
					Console.Write("Cpu." + opCode.Name + "(");
					Parameter(instruction, opCode);
					Console.Write(");");
					break;
			}
		}

		private static void TablePointer(FunctionReader.Instruction instruction, InstructionReader.OpCode opCode)
		{
			switch (opCode.Name)
			{
				case "ADC":
					Console.Write("A += [");
					Parameter(instruction, opCode);
					Console.Write("] + C;");
					break;

				case "AND":
					Console.Write("A &= [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "ORA":
					Console.Write("A |= [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "EOR":
					Console.Write("A ^= [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "SBC":
					Console.Write("A -= [");
					Parameter(instruction, opCode);
					Console.Write("] - !C;");
					break;

				case "BIT":
					Console.Write("temp = A & [");
					Parameter(instruction, opCode);
					Console.Write(";");
					break;

				case "CMP":
					Console.Write("temp = A - [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "CPX":
					Console.Write("temp = X - [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "CPY":
					Console.Write("temp = Y - [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "LDA":
					Console.Write("A = [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "LDX":
					Console.Write("X = [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "LDY":
					Console.Write("Y = [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "STA":
					Console.Write("[");
					Parameter(instruction, opCode);
					Console.Write("] = A;");
					break;

				case "STX":
					Console.Write("[");
					Parameter(instruction, opCode);
					Console.Write("] = X;");
					break;

				case "STY":
					Console.Write("[");
					Parameter(instruction, opCode);
					Console.Write("] = Y;");
					break;

				case "STZ":
					Console.Write("[");
					Parameter(instruction, opCode);
					Console.Write("] = 0;");
					break;

				default:
					Console.Write("Cpu." + opCode.Name + "(");
					Parameter(instruction, opCode);
					Console.Write(");");
					break;
			}
		}

		private static void TablePointer24(FunctionReader.Instruction instruction, InstructionReader.OpCode opCode)
		{
			switch (opCode.Name)
			{
				case "ADC":
					Console.Write("A += [");
					Parameter(instruction, opCode);
					Console.Write("] + C;");
					break;

				case "AND":
					Console.Write("A &= [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "ORA":
					Console.Write("A |= [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "EOR":
					Console.Write("A ^= [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "SBC":
					Console.Write("A -= [");
					Parameter(instruction, opCode);
					Console.Write("] - !C;");
					break;

				case "BIT":
					Console.Write("temp = A & [");
					Parameter(instruction, opCode);
					Console.Write(";");
					break;

				case "CMP":
					Console.Write("temp = A - [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "CPX":
					Console.Write("temp = X - [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "CPY":
					Console.Write("temp = Y - [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "LDA":
					Console.Write("A = [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "LDX":
					Console.Write("X = [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "LDY":
					Console.Write("Y = [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "STA":
					Console.Write("[");
					Parameter(instruction, opCode);
					Console.Write("] = A;");
					break;

				case "STX":
					Console.Write("[");
					Parameter(instruction, opCode);
					Console.Write("] = X;");
					break;

				case "STY":
					Console.Write("[");
					Parameter(instruction, opCode);
					Console.Write("] = Y;");
					break;

				case "STZ":
					Console.Write("[");
					Parameter(instruction, opCode);
					Console.Write("] = 0;");
					break;

				default:
					Console.Write("Cpu." + opCode.Name + "(");
					Parameter(instruction, opCode);
					Console.Write(");");
					break;
			}
		}

		private static void Pointer24(FunctionReader.Instruction instruction, InstructionReader.OpCode opCode)
		{
			switch (opCode.Name)
			{
				case "ADC":
					Console.Write("A += [");
					Parameter(instruction, opCode);
					Console.Write("] + C;");
					break;

				case "AND":
					Console.Write("A &= [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "ORA":
					Console.Write("A |= [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "EOR":
					Console.Write("A ^= [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "SBC":
					Console.Write("A -= [");
					Parameter(instruction, opCode);
					Console.Write("] - !C;");
					break;

				case "BIT":
					Console.Write("temp = A & [");
					Parameter(instruction, opCode);
					Console.Write(";");
					break;

				case "CMP":
					Console.Write("temp = A - [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "CPX":
					Console.Write("temp = X - [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "CPY":
					Console.Write("temp = Y - [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "LDA":
					Console.Write("A = [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "LDX":
					Console.Write("X = [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "LDY":
					Console.Write("Y = [");
					Parameter(instruction, opCode);
					Console.Write("];");
					break;

				case "STA":
					Console.Write("[");
					Parameter(instruction, opCode);
					Console.Write("] = A;");
					break;

				case "STX":
					Console.Write("[");
					Parameter(instruction, opCode);
					Console.Write("] = X;");
					break;

				case "STY":
					Console.Write("[");
					Parameter(instruction, opCode);
					Console.Write("] = Y;");
					break;

				case "STZ":
					Console.Write("[");
					Parameter(instruction, opCode);
					Console.Write("] = 0;");
					break;

				case "JMP":
					Console.Write("return [");
					Parameter(instruction, opCode);
					Console.Write("]();\t// 24-Bit Address");
					break;

				default:
					Console.Write("Cpu." + opCode.Name + "(");
					Parameter(instruction, opCode);
					Console.Write(");");
					break;
			}
		}

		private static void Branch(InstructionReader.OpCode opCode)
		{
			switch (opCode.Name)
			{
				case "BEQ":
					Console.WriteLine("if (Z == 1)");
					break;

				case "BNE":
					Console.WriteLine("if (Z == 0)");
					break;

				case "BCS":
					Console.WriteLine("if (C == 1)");
					break;

				case "BCC":
					Console.WriteLine("if (C == 0)");
					break;

				case "BPL":
					Console.WriteLine("if (N == 0)");
					break;

				case "BMI":
					Console.WriteLine("if (N == 1)");
					break;

				default:
					Console.WriteLine("if (F." + opCode.Name + ")");
					break;
			}
		}

		private static void Function(ProgramConsole.Function function)
		{
			Indent();
			Console.WriteLine("public void " + function.Address.ToString("X6") + "()");
			Indent();
			Console.WriteLine("{");
		}

		private static void Indent()
		{
			Console.Write("\t");
		}

		private static void Address(FunctionReader.Instruction instruction)
		{
			Console.Write(instruction.Address.ToString("X6") + " ");
		}

		private static void Code(FunctionReader.Instruction instruction)
		{
			Console.Write(instruction.Code.ToString("X2") + " ");
		}

		private static void Mnemonic(InstructionReader.OpCode opCode)
		{
			Console.Write(opCode.Name + " ");
		}

		private static void Parameter(FunctionReader.Instruction instruction, InstructionReader.OpCode opCode)
		{
			if (opCode.Type == InstructionReader.InstructionType.BankCopy)
			{
				Console.Write("0x" + (instruction.Parameter & 0xff).ToString("X2"));
				Console.Write(", ");
				Console.Write("0x" + (instruction.Parameter >> 8).ToString("X2"));
			}
			else
			{
				switch (opCode.Type)
				{
					case InstructionReader.InstructionType.Pointer:
					case InstructionReader.InstructionType.PointerTable:
					case InstructionReader.InstructionType.TablePointer:
					case InstructionReader.InstructionType.JumpPointer:
						Console.Write("(");
						break;

					case InstructionReader.InstructionType.Pointer24:
					case InstructionReader.InstructionType.TablePointer24:
					case InstructionReader.InstructionType.JumpPointer24:
						Console.Write("[");
						break;
				}

				switch (instruction.Length)
				{
					case 2:
						Console.Write("0x" + instruction.Parameter.ToString("X2"));
						break;

					case 3:
						Console.Write("0x" + instruction.Parameter.ToString("X4"));
						break;

					case 4:
						Console.Write("0x" + instruction.Parameter.ToString("X6"));
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

					case InstructionReader.InstructionType.Pointer24:
						Console.Write("]");
						break;

					case InstructionReader.InstructionType.TablePointer24:
						Console.Write("]");
						Index(opCode);
						break;

					case InstructionReader.InstructionType.JumpPointer24:
						Console.Write("]");
						Index(opCode);
						break;

					default:
						Index(opCode);
						break;
				}
			}
		}

		private static void Index(InstructionReader.OpCode opCode)
		{
			if (opCode.Index != InstructionReader.IndexRegister.None)
				Console.Write(" + " + opCode.Index);
		}
	}
}