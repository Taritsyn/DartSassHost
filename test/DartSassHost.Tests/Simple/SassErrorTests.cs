using System;

using NUnit.Framework;

namespace DartSassHost.Tests.Simple
{
	[TestFixture]
	public sealed class SassErrorTests : ErrorTestsBase
	{
		public SassErrorTests()
			: base(SyntaxType.Sass)
		{ }


		[Test]
		public void MappingSassSyntaxErrorDuringCompilation([Values]bool fromFile)
		{
			// Arrange
			string inputPath = GenerateSassFilePath("invalid-syntax", "style");
			string input = !fromFile ? GetFileContent(inputPath) : string.Empty;

			// Act
			string output;
			SassCompilationException exception = null;

			try
			{
				using (var sassCompiler = CreateSassCompiler())
				{
					if (fromFile)
					{
						output = sassCompiler.CompileFile(inputPath).CompiledContent;
					}
					else
					{
						output = sassCompiler.Compile(input, inputPath).CompiledContent;
					}
				}
			}
			catch (SassCompilationException e)
			{
				exception = e;
			}

			// Assert
			Assert.NotNull(exception);
			Assert.AreEqual(
				"Error: Expected \"." + Environment.NewLine +
				"   at root stylesheet (Files/simple/errors/invalid-syntax/sass/style.sass:3:35) -> " +
				"    family: \"Open Sans, sans-serif",
				exception.Message
			);
			Assert.AreEqual("Expected \".", exception.Description);
			Assert.AreEqual(1, exception.Status);
			Assert.AreEqual(inputPath, exception.File);
			Assert.AreEqual(3, exception.LineNumber);
			Assert.AreEqual(35, exception.ColumnNumber);
			Assert.AreEqual(
				"Line 2:   font: italic 20px/24px" + Environment.NewLine +
				"Line 3:     family: \"Open Sans, sans-serif" + Environment.NewLine +
				"------------------------------------------^",
				exception.SourceFragment
			);
			Assert.AreEqual(
				"   at root stylesheet (Files/simple/errors/invalid-syntax/sass/style.sass:3:35)",
				exception.CallStack
			);
		}

		[Test]
		public void MappingSassCustomErrorDuringCompilation([Values]bool fromFile)
		{
			// Arrange
			string inputPath = GenerateSassFilePath("custom-error", "style");
			string input = !fromFile ? GetFileContent(inputPath) : string.Empty;

			// Act
			string output;
			SassCompilationException exception = null;

			try
			{
				using (var sassCompiler = CreateSassCompiler())
				{
					if (fromFile)
					{
						output = sassCompiler.CompileFile(inputPath).CompiledContent;
					}
					else
					{
						output = sassCompiler.Compile(input, inputPath).CompiledContent;
					}
				}
			}
			catch (SassCompilationException e)
			{
				exception = e;
			}

			// Assert
			Assert.NotNull(exception);
			Assert.AreEqual(
				"Error: \"Property top must be either left or right.\"" + Environment.NewLine +
				"   at root stylesheet (Files/simple/errors/custom-error/sass/style.sass:16:3) -> " +
				"  @include reflexive-position(top, 12px)",
				exception.Message
			);
			Assert.AreEqual("\"Property top must be either left or right.\"", exception.Description);
			Assert.AreEqual(1, exception.Status);
			Assert.AreEqual(inputPath, exception.File);
			Assert.AreEqual(16, exception.LineNumber);
			Assert.AreEqual(3, exception.ColumnNumber);
			Assert.AreEqual(
				"Line 15: .sidebar" + Environment.NewLine +
				"Line 16:   @include reflexive-position(top, 12px)" + Environment.NewLine +
				"-----------^",
				exception.SourceFragment
			);
			Assert.AreEqual(
				"   at root stylesheet (Files/simple/errors/custom-error/sass/style.sass:16:3)",
				exception.CallStack
			);
		}
	}
}