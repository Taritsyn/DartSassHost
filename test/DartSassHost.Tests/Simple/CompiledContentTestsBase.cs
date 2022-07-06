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
		public void UsageOfIndentPropertiesDuringCompilation([Values]bool fromFile)
		{
			// Arrange
			var twoSpaceIndentOptions = new CompilationOptions
			{
				IndentType = IndentType.Space,
				IndentWidth = 2
			};
			var fourSpaceIndentOptions = new CompilationOptions
			{
				IndentType = IndentType.Space,
				IndentWidth = 4
			};
			var oneTabIndentOptions = new CompilationOptions
			{
				IndentType = IndentType.Tab,
				IndentWidth = 1
			};

			string inputPath = GenerateSassFilePath("ordinary", "variables");
			string input = !fromFile ? GetFileContent(inputPath) : string.Empty;

			string targetOutputPath1 = GenerateCssFilePath("ordinary", "variables-with-indent-options-two-space");
			string targetOutput1 = GetFileContent(targetOutputPath1);

			string targetOutputPath2 = GenerateCssFilePath("ordinary", "variables-with-indent-options-four-space");
			string targetOutput2 = GetFileContent(targetOutputPath2);

			string targetOutputPath3 = GenerateCssFilePath("ordinary", "variables-with-indent-options-one-tab");
			string targetOutput3 = GetFileContent(targetOutputPath3);

			// Act
			string output1;
			string output2;
			string output3;

			using (var compiler = CreateSassCompiler())
			{
				if (fromFile)
				{
					output1 = compiler.CompileFile(inputPath, options: twoSpaceIndentOptions).CompiledContent;
					output2 = compiler.CompileFile(inputPath, options: fourSpaceIndentOptions).CompiledContent;
					output3 = compiler.CompileFile(inputPath, options: oneTabIndentOptions).CompiledContent;
				}
				else
				{
					output1 = compiler.Compile(input, this.IndentedSyntax, twoSpaceIndentOptions).CompiledContent;
					output2 = compiler.Compile(input, this.IndentedSyntax, fourSpaceIndentOptions).CompiledContent;
					output3 = compiler.Compile(input, this.IndentedSyntax, oneTabIndentOptions).CompiledContent;
				}
			}

			// Assert
			Assert.AreEqual(targetOutput1, output1);
			Assert.AreEqual(targetOutput2, output2);
			Assert.AreEqual(targetOutput3, output3);
		}

		[Test]
		public void UsageOfLineFeedTypePropertyDuringCompilation([Values]bool fromFile)
		{
			// Arrange
			var crLineBreakOptions = new CompilationOptions { LineFeedType = LineFeedType.Cr };
			var crLfLineBreakOptions = new CompilationOptions { LineFeedType = LineFeedType.CrLf };
			var lfLineBreakOptions = new CompilationOptions { LineFeedType = LineFeedType.Lf };
			var lfCrLineBreakOptions = new CompilationOptions { LineFeedType = LineFeedType.LfCr };

			string inputPath = GenerateSassFilePath("ordinary", "variables");
			string input = !fromFile ? GetFileContent(inputPath) : string.Empty;

			string targetOutputPath = GenerateCssFilePath("ordinary", "variables-with-line-feed-type-options");
			string targetOutput1 = GetFileContent(targetOutputPath, LineFeedType.Cr);
			string targetOutput2 = GetFileContent(targetOutputPath, LineFeedType.CrLf);
			string targetOutput3 = GetFileContent(targetOutputPath, LineFeedType.Lf);
			string targetOutput4 = GetFileContent(targetOutputPath, LineFeedType.LfCr);

			// Act
			string output1;
			string output2;
			string output3;
			string output4;

			using (var compiler = CreateSassCompiler())
			{
				if (fromFile)
				{
					output1 = compiler.CompileFile(inputPath, options: crLineBreakOptions).CompiledContent;
					output2 = compiler.CompileFile(inputPath, options: crLfLineBreakOptions).CompiledContent;
					output3 = compiler.CompileFile(inputPath, options: lfLineBreakOptions).CompiledContent;
					output4 = compiler.CompileFile(inputPath, options: lfCrLineBreakOptions).CompiledContent;
				}
				else
				{
					output1 = compiler.Compile(input, this.IndentedSyntax, crLineBreakOptions).CompiledContent;
					output2 = compiler.Compile(input, this.IndentedSyntax, crLfLineBreakOptions).CompiledContent;
					output3 = compiler.Compile(input, this.IndentedSyntax, lfLineBreakOptions).CompiledContent;
					output4 = compiler.Compile(input, this.IndentedSyntax, lfCrLineBreakOptions).CompiledContent;
				}
			}

			// Assert
			Assert.AreEqual(targetOutput1, output1);
			Assert.AreEqual(targetOutput2, output2);
			Assert.AreEqual(targetOutput3, output3);
			Assert.AreEqual(targetOutput4, output4);
		}

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