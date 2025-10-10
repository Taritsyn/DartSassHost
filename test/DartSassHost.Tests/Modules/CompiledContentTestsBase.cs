using System.Collections.Generic;

using NUnit.Framework;

namespace DartSassHost.Tests.Modules
{
	public abstract class CompiledContentTestsBase : PhysicalFileSystemTestsBase
	{
		public override string BaseDirectoryPath => "modules/compiled-content";


		protected CompiledContentTestsBase(SyntaxType syntaxType)
			: base(syntaxType)
		{ }


		[Test]
		public void UsageOfIncludePathsPropertyDuringCompilation([Values]bool fromFile)
		{
			// Arrange
			var withoutIncludedPathOptions = new CompilationOptions
			{
				IncludePaths = new List<string>()
			};
			var withIncludedPathOptions = new CompilationOptions
			{
				IncludePaths = new List<string> { GenerateSassDirectoryPath("additional-path", "vendor") }
			};

			string inputPath = GenerateSassFilePath("additional-path", "base");
			string input = !fromFile ? GetFileContent(inputPath) : string.Empty;

			string targetOutputPath2 = GenerateCssFilePath("additional-path", "base");
			string targetOutput2 = GetFileContent(targetOutputPath2);

			// Act
			SassCompilationException exception1 = null;
			string output2;

			using (var compiler = CreateSassCompiler())
			{
				try
				{
					if (fromFile)
					{
						compiler.CompileFile(inputPath, options: withoutIncludedPathOptions);
					}
					else
					{
						compiler.Compile(input, inputPath, options: withoutIncludedPathOptions);
					}
				}
				catch (SassCompilationException e)
				{
					exception1 = e;
				}

				if (fromFile)
				{
					output2 = compiler.CompileFile(inputPath, options: withIncludedPathOptions).CompiledContent;
				}
				else
				{
					output2 = compiler.Compile(input, inputPath, options: withIncludedPathOptions).CompiledContent;
				}
			}

			// Assert
			Assert.IsNotNull(exception1);
			Assert.AreEqual(targetOutput2, output2);
		}

		[Test]
		public void UsageOfOutputStylePropertyDuringCompilation([Values]bool fromFile)
		{
			// Arrange
			var expandedOutputStyleOptions = new CompilationOptions { OutputStyle = OutputStyle.Expanded };
			var compressedOutputStyleOptions = new CompilationOptions { OutputStyle = OutputStyle.Compressed };

			string inputPath = GenerateSassFilePath("ordinary", "base");
			string input = !fromFile ? GetFileContent(inputPath) : string.Empty;

			string targetOutputPath1 = GenerateCssFilePath("ordinary", "base-with-output-style-option-expanded");
			string targetOutput1 = GetFileContent(targetOutputPath1);

			string targetOutputPath2 = GenerateCssFilePath("ordinary", "base-with-output-style-option-compressed");
			string targetOutput2 = GetFileContent(targetOutputPath2);

			// Act
			string output1;
			string output2;

			using (var compiler = CreateSassCompiler())
			{
				if (fromFile)
				{
					output1 = compiler.CompileFile(inputPath, options: expandedOutputStyleOptions).CompiledContent;
					output2 = compiler.CompileFile(inputPath, options: compressedOutputStyleOptions).CompiledContent;
				}
				else
				{
					output1 = compiler.Compile(input, inputPath, options: expandedOutputStyleOptions).CompiledContent;
					output2 = compiler.Compile(input, inputPath, options: compressedOutputStyleOptions).CompiledContent;
				}
			}

			// Assert
			Assert.AreEqual(targetOutput1, output1);
			Assert.AreEqual(targetOutput2, output2);
		}

		[Test]
		public void UsageOfCharsetPropertyDuringCompilationWithUtf8Characters([Values]bool fromFile)
		{
			// Arrange
			var charsetDisabledOptions = new CompilationOptions { Charset = false };
			var charsetEnabledOptions = new CompilationOptions { Charset = true };

			string inputPath = GenerateSassFilePath("ютф-8", "главный");
			string input = !fromFile ? GetFileContent(inputPath) : string.Empty;

			string targetOutputPath1 = GenerateCssFilePath("ютф-8", "главный-без-кодировки");
			string targetOutput1 = GetFileContent(targetOutputPath1);

			string targetOutputPath2 = GenerateCssFilePath("ютф-8", "главный-с-кодировкой");
			string targetOutput2 = GetFileContent(targetOutputPath2);

			// Act
			string output1;
			string output2;

			using (var compiler = CreateSassCompiler())
			{
				if (fromFile)
				{
					output1 = compiler.CompileFile(inputPath, options: charsetDisabledOptions).CompiledContent;
					output2 = compiler.CompileFile(inputPath, options: charsetEnabledOptions).CompiledContent;
				}
				else
				{
					output1 = compiler.Compile(input, inputPath, options: charsetDisabledOptions).CompiledContent;
					output2 = compiler.Compile(input, inputPath, options: charsetEnabledOptions).CompiledContent;
				}
			}

			// Assert
			Assert.AreEqual(targetOutput1, output1);
			Assert.AreEqual(targetOutput2, output2);
		}

		[Test]
		public void UsageOfCharsetPropertyDuringCompilationWithUtf16Characters([Values]bool fromFile)
		{
			// Arrange
			var charsetDisabledOptions = new CompilationOptions { Charset = false };
			var charsetEnabledOptions = new CompilationOptions { Charset = true };

			string inputPath = GenerateSassFilePath("ютф-16", "главный");
			string input = !fromFile ? GetFileContent(inputPath) : string.Empty;

			string targetOutputPath1 = GenerateCssFilePath("ютф-16", "главный-без-кодировки");
			string targetOutput1 = GetFileContent(targetOutputPath1);

			string targetOutputPath2 = GenerateCssFilePath("ютф-16", "главный-с-кодировкой");
			string targetOutput2 = GetFileContent(targetOutputPath2);

			// Act
			string output1;
			string output2;

			using (var compiler = CreateSassCompiler())
			{
				if (fromFile)
				{
					output1 = compiler.CompileFile(inputPath, options: charsetDisabledOptions).CompiledContent;
					output2 = compiler.CompileFile(inputPath, options: charsetEnabledOptions).CompiledContent;
				}
				else
				{
					output1 = compiler.Compile(input, inputPath, options: charsetDisabledOptions).CompiledContent;
					output2 = compiler.Compile(input, inputPath, options: charsetEnabledOptions).CompiledContent;
				}
			}

			// Assert
			Assert.AreEqual(targetOutput1, output1);
			Assert.AreEqual(targetOutput2, output2);
		}
	}
}