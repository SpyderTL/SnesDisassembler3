using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SnesDisassembler
{
	internal static class ProgramConsole
	{
		internal static void Start()
		{
			RomFile.Load(Arguments.Source);

			SnesRom.Load();

			var functions = new List<Function>();
			var branches = new List<FunctionReader.Branch>();

			branches.Add(new FunctionReader.Branch { Address = Arguments.Addresses[0], Flags = 0x00 });

			while (branches.Any())
			{
				var branch = branches.First();
				branches.RemoveAt(0);

				if (functions.Any(x => x.Address == branch.Address))
					continue;

				InstructionReader.Position = branch.Address;
				InstructionReader.Flags = branch.Flags;

				FunctionReader.Read();

				functions.Add(new Function
				{
					Address = branch.Address,
					Flags = branch.Flags,
					Instructions = FunctionReader.Instructions
				});

				branches.AddRange(FunctionReader.Branches);
			}

			functions.Sort((a, b) => a.Address - b.Address);

			//AssemblyConsole.Write(functions);
			CSharpConsole.Write(functions);
		}


		internal struct Function
		{
			internal int Address;
			internal int Flags;
			internal FunctionReader.Instruction[] Instructions;
		}
	}
}
