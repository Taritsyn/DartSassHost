using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
#if NETSTANDARD1_3

using Microsoft.Extensions.PlatformAbstractions;
#endif

using DartSassHost.Helpers;

namespace DartSassHost.Sample.Logic
{
	public abstract class CompilationExampleBase
	{
		private static readonly string _filesDirectoryPath;


		/// <summary>
		/// Static constructor
		/// </summary>
		static CompilationExampleBase()
		{
#if NETSTANDARD1_3
			var appEnv = PlatformServices.Default.Application;
			string appDirectoryPath = appEnv.ApplicationBasePath;
#else
			string appDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;
#endif
			_filesDirectoryPath = Path.GetFullPath(Path.Combine(appDirectoryPath, "Files"));
		}


		protected static void CompileContent()
		{
			WriteHeader("Compilation of SCSS code");

			const string inputContent = @"$font-stack: Helvetica, sans-serif;
$primary-color: #333;

body {
  font: 100% $font-stack;
  color: $primary-color;
}

/* Стрелка вниз */
.down-arrow:before {
  content: ""▼"";
}";

			try
			{
				using (var sassCompiler = new SassCompiler(new CompilationOptions { SourceMap = true }))
				{
					CompilationResult result = sassCompiler.Compile(inputContent, "input.scss", "output.css");
					WriteVersion(sassCompiler.Version);
					WriteOutput(result);
				}
			}
			catch (SassException e)
			{
				WriteError("During compilation of SCSS code an error occurred.", e);
			}
		}

		protected static void CompileFile()
		{
			WriteHeader("Compilation of SCSS file");

			string inputFilePath = Path.Combine(_filesDirectoryPath, "style.scss");
			string outputFilePath = Path.Combine(_filesDirectoryPath, "style.css");

			try
			{
				using (var sassCompiler = new SassCompiler(new CompilationOptions { SourceMap = true }))
				{
					CompilationResult result = sassCompiler.CompileFile(inputFilePath, outputFilePath);
					WriteVersion(sassCompiler.Version);
					WriteOutput(result);
				}
			}
			catch (SassException e)
			{
				WriteError("During compilation of SCSS file an error occurred.", e);
			}
		}

		private static void WriteHeader(string text)
		{
			string separator = new string('-', 80);

			Console.WriteLine(separator);
			Console.WriteLine(text);
			Console.WriteLine(separator);
			Console.WriteLine();
		}

		private static void WriteVersion(string version)
		{
			Console.WriteLine("Version: {0}", version);
		}

		private static void WriteOutput(CompilationResult result)
		{
			Console.WriteLine("Compiled content:{1}{1}{0}{1}", result.CompiledContent, Environment.NewLine);
			Console.WriteLine("Source map:{1}{1}{0}{1}", result.SourceMap, Environment.NewLine);

			IList<string> includedFilePaths = result.IncludedFilePaths;

			if (includedFilePaths.Count > 0)
			{
				Console.WriteLine("Included file paths:");
				Console.WriteLine();

				foreach (string includedFilePath in includedFilePaths)
				{
					Console.WriteLine(includedFilePath);
				}

				Console.WriteLine();
			}

			IList<ProblemInfo> warnings = result.Warnings;

			if (warnings.Count > 0)
			{
				Console.WriteLine("Warnings:");
				Console.WriteLine();

				foreach (ProblemInfo warning in warnings)
				{
					Console.WriteLine(warning.Message);
					Console.WriteLine();
				}
			}

			Console.WriteLine();
		}

		private static void WriteError(string title, SassException exception)
		{
			Console.WriteLine("{0} See details:", title);
			Console.WriteLine();
			Console.Write(exception.Message);

			string errorDetails = SassErrorHelpers.GenerateErrorDetails(exception, true);
			if (errorDetails.Length > 0)
			{
				Console.WriteLine();
				Console.WriteLine();
				Console.Write(errorDetails);
			}

			Console.WriteLine();
			Console.WriteLine();
		}
	}
}