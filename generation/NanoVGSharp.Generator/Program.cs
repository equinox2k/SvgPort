using System;
using System.IO;
using Sichem;

namespace StbSharp.StbImage.Generator
{
	class Program
	{
		static void Process()
		{
			using (var output = new StringWriter())
			{
				var parameters = new ConversionParameters
				{
					InputPath = @"nanovg\nansovg.c",
					Output = output,
					Defines = new string[0],
					Namespace = "NanoVGSharp",
					Class = "NanoSVG",
					SkipStructs = new string[0],
					SkipGlobalVariables = new string[0],
					SkipFunctions = new string[0],
					Classes = new string[]
					{
					
					},
					GlobalArrays = new string[0]
				};

				var cp = new ClangParser();

				cp.Process(parameters);
				var data = output.ToString();

				// Post processing
				Logger.Info("Post processing...");

				data = Utility.ReplaceNativeCalls(data);

				File.WriteAllText(@"..\..\..\..\..\src\NanoSVG.Generated.cs", data);
			}
		}

		static void Main(string[] args)
		{
			try
			{
				Process();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Console.WriteLine(ex.StackTrace);
			}

			Console.WriteLine("Finished. Press any key to quit.");
			Console.ReadKey();
		}
	}
}