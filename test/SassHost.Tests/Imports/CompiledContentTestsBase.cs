using System;
using System.Collections.Generic;

using NUnit.Framework;

namespace SassHost.Tests.Imports
{
	public abstract class CompiledContentTestsBase : FileSystemTestsBase
	{
		public override string BaseDirectoryPath => "imports/compiled-content";


		protected CompiledContentTestsBase(SyntaxType syntaxType)
			: base(syntaxType)
		{ }


		[SetUp]
		public void Init()
		{
			JsEngineSwitcherInitializer.Initialize();
		}

		#region Code

		[Test]
		public void UsageOfIncludePathsPropertyDuringCompilationOfCode()
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

			string inputPath = ToAbsolutePath(GenerateSassFilePath("additional-path", "base"));
			string input = GetFileContent(inputPath);

			string targetOutputPath2 = GenerateCssFilePath("additional-path", "base");
			string targetOutput2 = GetFileContent(targetOutputPath2);

			// Act
			SassCompilationException exception1 = null;
			string output2;

			using (var compiler = new SassCompiler())
			{
				try
				{
					compiler.Compile(input, inputPath, options: withoutIncludedPathOptions);
				}
				catch (SassCompilationException e)
				{
					exception1 = e;
				}

				output2 = compiler.Compile(input, inputPath, options: withIncludedPathOptions).CompiledContent;
			}

			// Assert
			Assert.IsNotNull(exception1);
			Assert.AreEqual(targetOutput2, output2);
		}

		[Test]
		public void UsageOfIndentPropertiesDuringCompilationOfCode()
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

			string inputPath = ToAbsolutePath(GenerateSassFilePath("ordinary", "base"));
			string input = GetFileContent(inputPath);

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

			using (var compiler = new SassCompiler())
			{
				output1 = compiler.Compile(input, inputPath, options: twoSpaceIndentOptions).CompiledContent;
				output2 = compiler.Compile(input, inputPath, options: fourSpaceIndentOptions).CompiledContent;
				output3 = compiler.Compile(input, inputPath, options: oneTabIndentOptions).CompiledContent;
			}

			// Assert
			Assert.AreEqual(targetOutput1, output1);
			Assert.AreEqual(targetOutput2, output2);
			Assert.AreEqual(targetOutput3, output3);
		}

		[Test]
		public void UsageOfLineFeedTypePropertyDuringCompilationOfCode()
		{
			// Arrange
			var crLineBreakOptions = new CompilationOptions { LineFeedType = LineFeedType.Cr };
			var crLfLineBreakOptions = new CompilationOptions { LineFeedType = LineFeedType.CrLf };
			var lfLineBreakOptions = new CompilationOptions { LineFeedType = LineFeedType.Lf };
			var lfCrLineBreakOptions = new CompilationOptions { LineFeedType = LineFeedType.LfCr };

			string inputPath = ToAbsolutePath(GenerateSassFilePath("ordinary", "base"));
			string input = GetFileContent(inputPath);

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

			using (var compiler = new SassCompiler())
			{
				output1 = compiler.Compile(input, inputPath, options: crLineBreakOptions).CompiledContent;
				output2 = compiler.Compile(input, inputPath, options: crLfLineBreakOptions).CompiledContent;
				output3 = compiler.Compile(input, inputPath, options: lfLineBreakOptions).CompiledContent;
				output4 = compiler.Compile(input, inputPath, options: lfCrLineBreakOptions).CompiledContent;
			}

			// Assert
			Assert.AreEqual(targetOutput1, output1);
			Assert.AreEqual(targetOutput2, output2);
			Assert.AreEqual(targetOutput3, output3);
			Assert.AreEqual(targetOutput4, output4);
		}

		[Test]
		public void UsageOfOutputStylePropertyDuringCompilationOfCode()
		{
			// Arrange
			var expandedOutputStyleOptions = new CompilationOptions { OutputStyle = OutputStyle.Expanded };
			var compressedOutputStyleOptions = new CompilationOptions { OutputStyle = OutputStyle.Compressed };

			string inputPath = ToAbsolutePath(GenerateSassFilePath("ordinary", "base"));
			string input = GetFileContent(inputPath);

			string targetOutputPath1 = GenerateCssFilePath("ordinary", "base-with-output-style-option-expanded");
			string targetOutput1 = GetFileContent(targetOutputPath1);

			string targetOutputPath2 = GenerateCssFilePath("ordinary", "base-with-output-style-option-compressed");
			string targetOutput2 = GetFileContent(targetOutputPath2);

			// Act
			string output1;
			string output2;

			using (var compiler = new SassCompiler())
			{
				output1 = compiler.Compile(input, inputPath, options: expandedOutputStyleOptions).CompiledContent;
				output2 = compiler.Compile(input, inputPath, options: compressedOutputStyleOptions).CompiledContent;
			}

			// Assert
			Assert.AreEqual(targetOutput1, output1);
			Assert.AreEqual(targetOutput2, output2);
		}

		[Test]
		public void CompilationOfCodeWithUtf8Characters()
		{
			// Arrange
			string inputPath = ToAbsolutePath(GenerateSassFilePath("ютф-8", "главный"));
			string input = GetFileContent(inputPath);

			string targetOutputPath = GenerateCssFilePath("ютф-8", "главный");
			string targetOutput = GetFileContent(targetOutputPath);

			// Act
			string output;

			using (var compiler = new SassCompiler())
			{
				output = compiler.Compile(input, inputPath).CompiledContent;
			}

			// Assert
			Assert.AreEqual(targetOutput, output);
		}

		[Test]
		public void CompilationOfCodeWithUtf16Characters()
		{
			// Arrange
			string inputPath = ToAbsolutePath(GenerateSassFilePath("ютф-16", "главный"));
			string input = GetFileContent(inputPath);

			string targetOutputPath = GenerateCssFilePath("ютф-16", "главный");
			string targetOutput = GetFileContent(targetOutputPath);

			// Act
			string output;

			using (var compiler = new SassCompiler())
			{
				output = compiler.Compile(input, inputPath).CompiledContent;
			}

			// Assert
			Assert.AreEqual(targetOutput, output);
		}

		#endregion

		#region Files

		[Test]
		public void UsageOfIncludePathsPropertyDuringCompilationOfFile()
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

			string inputPath = ToAbsolutePath(GenerateSassFilePath("additional-path", "base"));

			string targetOutputPath2 = GenerateCssFilePath("additional-path", "base");
			string targetOutput2 = GetFileContent(targetOutputPath2);

			// Act
			SassCompilationException exception1 = null;
			string output2;

			using (var compiler = new SassCompiler())
			{
				try
				{
					compiler.CompileFile(inputPath, options: withoutIncludedPathOptions);
				}
				catch (SassCompilationException e)
				{
					exception1 = e;
				}

				output2 = compiler.CompileFile(inputPath, options: withIncludedPathOptions).CompiledContent;
			}

			// Assert
			Assert.IsNotNull(exception1);
			Assert.AreEqual(targetOutput2, output2);
		}

		[Test]
		public void UsageOfIndentPropertiesDuringCompilationOfFile()
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

			string inputPath = ToAbsolutePath(GenerateSassFilePath("ordinary", "base"));

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

			using (var compiler = new SassCompiler())
			{
				output1 = compiler.CompileFile(inputPath, options: twoSpaceIndentOptions).CompiledContent;
				output2 = compiler.CompileFile(inputPath, options: fourSpaceIndentOptions).CompiledContent;
				output3 = compiler.CompileFile(inputPath, options: oneTabIndentOptions).CompiledContent;
			}

			// Assert
			Assert.AreEqual(targetOutput1, output1);
			Assert.AreEqual(targetOutput2, output2);
			Assert.AreEqual(targetOutput3, output3);
		}

		[Test]
		public void UsageOfLineFeedTypePropertyDuringCompilationOfFile()
		{
			// Arrange
			var crLineBreakOptions = new CompilationOptions { LineFeedType = LineFeedType.Cr };
			var crLfLineBreakOptions = new CompilationOptions { LineFeedType = LineFeedType.CrLf };
			var lfLineBreakOptions = new CompilationOptions { LineFeedType = LineFeedType.Lf };
			var lfCrLineBreakOptions = new CompilationOptions { LineFeedType = LineFeedType.LfCr };

			string inputPath = ToAbsolutePath(GenerateSassFilePath("ordinary", "base"));

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

			using (var compiler = new SassCompiler())
			{
				output1 = compiler.CompileFile(inputPath, options: crLineBreakOptions).CompiledContent;
				output2 = compiler.CompileFile(inputPath, options: crLfLineBreakOptions).CompiledContent;
				output3 = compiler.CompileFile(inputPath, options: lfLineBreakOptions).CompiledContent;
				output4 = compiler.CompileFile(inputPath, options: lfCrLineBreakOptions).CompiledContent;
			}

			// Assert
			Assert.AreEqual(targetOutput1, output1);
			Assert.AreEqual(targetOutput2, output2);
			Assert.AreEqual(targetOutput3, output3);
			Assert.AreEqual(targetOutput4, output4);
		}

		[Test]
		public void UsageOfOutputStylePropertyDuringCompilationOfFile()
		{
			// Arrange
			var expandedOutputStyleOptions = new CompilationOptions { OutputStyle = OutputStyle.Expanded };
			var compressedOutputStyleOptions = new CompilationOptions { OutputStyle = OutputStyle.Compressed };

			string inputPath = ToAbsolutePath(GenerateSassFilePath("ordinary", "base"));

			string targetOutputPath1 = GenerateCssFilePath("ordinary", "base-with-output-style-option-expanded");
			string targetOutput1 = GetFileContent(targetOutputPath1);

			string targetOutputPath2 = GenerateCssFilePath("ordinary", "base-with-output-style-option-compressed");
			string targetOutput2 = GetFileContent(targetOutputPath2);

			// Act
			string output1;
			string output2;

			using (var compiler = new SassCompiler())
			{
				output1 = compiler.CompileFile(inputPath, options: expandedOutputStyleOptions).CompiledContent;
				output2 = compiler.CompileFile(inputPath, options: compressedOutputStyleOptions).CompiledContent;
			}

			// Assert
			Assert.AreEqual(targetOutput1, output1);
			Assert.AreEqual(targetOutput2, output2);
		}

		[Test]
		public void CompilationOfFileWithUtf8Characters()
		{
			// Arrange
			string inputPath = ToAbsolutePath(GenerateSassFilePath("ютф-8", "главный"));

			string targetOutputPath = GenerateCssFilePath("ютф-8", "главный");
			string targetOutput = GetFileContent(targetOutputPath);

			// Act
			string output;

			using (var compiler = new SassCompiler())
			{
				output = compiler.CompileFile(inputPath).CompiledContent;
			}

			// Assert
			Assert.AreEqual(targetOutput, output);
		}

		[Test]
		public void CompilationOfFileWithUtf16Characters()
		{
			// Arrange
			string inputPath = ToAbsolutePath(GenerateSassFilePath("ютф-16", "главный"));

			string targetOutputPath = GenerateCssFilePath("ютф-16", "главный");
			string targetOutput = GetFileContent(targetOutputPath);

			// Act
			string output;

			using (var compiler = new SassCompiler())
			{
				output = compiler.CompileFile(inputPath).CompiledContent;
			}

			// Assert
			Assert.AreEqual(targetOutput, output);
		}

		#endregion
	}
}