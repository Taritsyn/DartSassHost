﻿using System.Collections.Generic;

using NUnit.Framework;

namespace DartSassHost.Tests.Imports
{
	public abstract class CompiledContentTestsBase : PhysicalFileSystemTestsBase
	{
		public override string BaseDirectoryPath => "imports/compiled-content";


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

			string inputPath = GenerateSassFilePath("ordinary", "base");
			string input = !fromFile ? GetFileContent(inputPath) : string.Empty;

			string targetOutputPath1 = GenerateCssFilePath("ordinary", "base-with-indent-options-two-space");
			string targetOutput1 = GetFileContent(targetOutputPath1);

			string targetOutputPath2 = GenerateCssFilePath("ordinary", "base-with-indent-options-four-space");
			string targetOutput2 = GetFileContent(targetOutputPath2);

			string targetOutputPath3 = GenerateCssFilePath("ordinary", "base-with-indent-options-one-tab");
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
					output1 = compiler.Compile(input, inputPath, options: twoSpaceIndentOptions).CompiledContent;
					output2 = compiler.Compile(input, inputPath, options: fourSpaceIndentOptions).CompiledContent;
					output3 = compiler.Compile(input, inputPath, options: oneTabIndentOptions).CompiledContent;
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

			string inputPath = GenerateSassFilePath("ordinary", "base");
			string input = !fromFile ? GetFileContent(inputPath) : string.Empty;

			string targetOutputPath = GenerateCssFilePath("ordinary", "base-with-line-feed-type-options");
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
					output1 = compiler.Compile(input, inputPath, options: crLineBreakOptions).CompiledContent;
					output2 = compiler.Compile(input, inputPath, options: crLfLineBreakOptions).CompiledContent;
					output3 = compiler.Compile(input, inputPath, options: lfLineBreakOptions).CompiledContent;
					output4 = compiler.Compile(input, inputPath, options: lfCrLineBreakOptions).CompiledContent;
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