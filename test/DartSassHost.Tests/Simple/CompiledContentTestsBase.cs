using NUnit.Framework;

namespace DartSassHost.Tests.Simple
{
	public abstract class CompiledContentTestsBase : PhysicalFileSystemTestsBase
	{
		public override string BaseDirectoryPath => "simple/compiled-content";


		protected CompiledContentTestsBase(SyntaxType syntaxType)
			: base(syntaxType)
		{ }


		[Test]
		public void UsageOfOutputStylePropertyDuringCompilation([Values]bool fromFile)
		{
			// Arrange
			var expandedOutputStyleOptions = new CompilationOptions { OutputStyle = OutputStyle.Expanded };
			var compressedOutputStyleOptions = new CompilationOptions { OutputStyle = OutputStyle.Compressed };

			string inputPath = GenerateSassFilePath("ordinary", "variables");
			string input = !fromFile ? GetFileContent(inputPath) : string.Empty;

			string targetOutputPath1 = GenerateCssFilePath("ordinary", "variables-with-output-style-option-expanded");
			string targetOutput1 = GetFileContent(targetOutputPath1);

			string targetOutputPath2 = GenerateCssFilePath("ordinary",
				"variables-with-output-style-option-compressed");
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
					output1 = compiler.Compile(input, this.IndentedSyntax, expandedOutputStyleOptions).CompiledContent;
					output2 = compiler.Compile(input, this.IndentedSyntax, compressedOutputStyleOptions).CompiledContent;
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

			string inputPath = GenerateSassFilePath("ютф-8", "символы");
			string input = !fromFile ? GetFileContent(inputPath) : string.Empty;

			string targetOutputPath1 = GenerateCssFilePath("ютф-8", "символы-без-кодировки");
			string targetOutput1 = GetFileContent(targetOutputPath1);

			string targetOutputPath2 = GenerateCssFilePath("ютф-8", "символы-с-кодировкой");
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
					output1 = compiler.Compile(input, this.IndentedSyntax, options: charsetDisabledOptions).CompiledContent;
					output2 = compiler.Compile(input, this.IndentedSyntax, options: charsetEnabledOptions).CompiledContent;
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

			string inputPath = GenerateSassFilePath("ютф-16", "символы");
			string input = !fromFile ? GetFileContent(inputPath) : string.Empty;

			string targetOutputPath1 = GenerateCssFilePath("ютф-16", "символы-без-кодировки");
			string targetOutput1 = GetFileContent(targetOutputPath1);

			string targetOutputPath2 = GenerateCssFilePath("ютф-16", "символы-с-кодировкой");
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
					output1 = compiler.Compile(input, this.IndentedSyntax, options: charsetDisabledOptions).CompiledContent;
					output2 = compiler.Compile(input, this.IndentedSyntax, options: charsetEnabledOptions).CompiledContent;
				}
			}

			// Assert
			Assert.AreEqual(targetOutput1, output1);
			Assert.AreEqual(targetOutput2, output2);
		}
	}
}