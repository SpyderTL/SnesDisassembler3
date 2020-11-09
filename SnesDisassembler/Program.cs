using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SnesDisassembler
{
	class Program
	{
		static void Main(string[] args)
		{
			Arguments.Parse(args);

			ProgramConsole.Start();
		}
	}
}
