using System;
using System.IO;
using System.Linq;

namespace SnesDisassembler
{
	internal class RomFile
	{
		internal static void Load(string path)
		{
			switch (Path.GetExtension(path))
			{
				case ".smc":
					LoadSmc(path);
					break;

				case ".smf":
					LoadSmf(path);
					break;
			}
		}

		internal static void LoadSmc(string path)
		{
			Rom.Data = File.ReadAllBytes(path).Skip(0x200).ToArray();
		}

		internal static void LoadSmf(string path)
		{
			Rom.Data = File.ReadAllBytes(path);
		}
	}
}