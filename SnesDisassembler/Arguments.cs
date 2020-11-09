using System;
using System.Linq;

namespace SnesDisassembler
{
	internal class Arguments
	{
		internal static string Source;
		internal static int[] Addresses;

		internal static bool Parse(string[] args)
		{
			if (args.Length < 2)
			{
				Console.WriteLine("usage: SnesDisassembler.exe source address [address2] [address3] ...");

				return false;
			}

			Source = args[0];
			Addresses = args.Skip(1).Select(x => int.Parse(x, System.Globalization.NumberStyles.HexNumber)).ToArray();

			return true;
		}
	}
}