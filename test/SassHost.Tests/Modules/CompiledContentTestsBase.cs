using System;
using System.Collections.Generic;

using NUnit.Framework;

namespace SassHost.Tests.Modules
{
	public abstract class CompiledContentTestsBase : FileSystemTestsBase
	{
		public override string BaseDirectoryPath => "modules/compiled-content";


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

			try
			{
				using (var withoutIncludedPathCompiler = new SassCompiler(withoutIncludedPathOptions))
				{
					withoutIncludedPathCompiler.Compile(input, inputPath);
				}
			}
			catch (SassCompilationException e)
			{
				exception1 = e;
			}

			string output2;

			using (var withIncludedPathCompiler = new SassCompiler(withIncludedPathOptions))
			{
				output2 = withIncludedPathCompiler.Compile(input, inputPath).CompiledContent;
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

			using (var twoSpaceIndentCompiler = new SassCompiler(twoSpaceIndentOptions))
			{
				output1 = twoSpaceIndentCompiler.Compile(input, inputPath).CompiledContent;
			}

			using (var fourSpaceIndentCompiler = new SassCompiler(fourSpaceIndentOptions))
			{
				output2 = fourSpaceIndentCompiler.Compile(input, inputPath).CompiledContent;
			}

			using (var oneTabIndentCompiler = new SassCompiler(oneTabIndentOptions))
			{
				output3 = oneTabIndentCompiler.Compile(input, inputPath).CompiledContent;
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

			using (var crLineBreakCompiler = new SassCompiler(crLineBreakOptions))
			{
				output1 = crLineBreakCompiler.Compile(input, inputPath).CompiledContent;
			}

			using (var crLfLineBreakCompiler = new SassCompiler(crLfLineBreakOptions))
			{
				output2 = crLfLineBreakCompiler.Compile(input, inputPath).CompiledContent;
			}

			using (var lfLineBreakCompiler = new SassCompiler(lfLineBreakOptions))
			{
				output3 = lfLineBreakCompiler.Compile(input, inputPath).CompiledContent;
			}

			using (var lfCrLineBreakCompiler = new SassCompiler(lfCrLineBreakOptions))
			{
				output4 = lfCrLineBreakCompiler.Compile(input, inputPath).CompiledContent;
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

			using (var expandedOutputStyleCompiler = new SassCompiler(expandedOutputStyleOptions))
			{
				output1 = expandedOutputStyleCompiler.Compile(input, inputPath).CompiledContent;
			}

			using (var compressedOutputStyleCompiler = new SassCompiler(compressedOutputStyleOptions))
			{
				output2 = compressedOutputStyleCompiler.Compile(input, inputPath).CompiledContent;
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

			try
			{
				using (var withoutIncludedPathCompiler = new SassCompiler(withoutIncludedPathOptions))
				{
					withoutIncludedPathCompiler.CompileFile(inputPath);
				}
			}
			catch (SassCompilationException e)
			{
				exception1 = e;
			}

			string output2;

			using (var withIncludedPathCompiler = new SassCompiler(withIncludedPathOptions))
			{
				output2 = withIncludedPathCompiler.CompileFile(inputPath).CompiledContent;
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

			using (var twoSpaceIndentCompiler = new SassCompiler(twoSpaceIndentOptions))
			{
				output1 = twoSpaceIndentCompiler.CompileFile(inputPath).CompiledContent;
			}

			using (var fourSpaceIndentCompiler = new SassCompiler(fourSpaceIndentOptions))
			{
				output2 = fourSpaceIndentCompiler.CompileFile(inputPath).CompiledContent;
			}

			using (var oneTabIndentCompiler = new SassCompiler(oneTabIndentOptions))
			{
				output3 = oneTabIndentCompiler.CompileFile(inputPath).CompiledContent;
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

			using (var crLineBreakCompiler = new SassCompiler(crLineBreakOptions))
			{
				output1 = crLineBreakCompiler.CompileFile(inputPath).CompiledContent;
			}

			using (var crLfLineBreakCompiler = new SassCompiler(crLfLineBreakOptions))
			{
				output2 = crLfLineBreakCompiler.CompileFile(inputPath).CompiledContent;
			}

			using (var lfLineBreakCompiler = new SassCompiler(lfLineBreakOptions))
			{
				output3 = lfLineBreakCompiler.CompileFile(inputPath).CompiledContent;
			}

			using (var lfCrLineBreakCompiler = new SassCompiler(lfCrLineBreakOptions))
			{
				output4 = lfCrLineBreakCompiler.CompileFile(inputPath).CompiledContent;
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

			using (var expandedOutputStyleCompiler = new SassCompiler(expandedOutputStyleOptions))
			{
				output1 = expandedOutputStyleCompiler.CompileFile(inputPath).CompiledContent;
			}

			using (var compressedOutputStyleCompiler = new SassCompiler(compressedOutputStyleOptions))
			{
				output2 = compressedOutputStyleCompiler.CompileFile(inputPath).CompiledContent;
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