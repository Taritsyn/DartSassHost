using System;

using NUnit.Framework;

namespace DartSassHost.Tests.Simple
{
	[TestFixture]
	public sealed class ScssErrorTests : ErrorTestsBase
	{
		public ScssErrorTests()
			: base(SyntaxType.Scss)
		{ }


		#region Code

		[Test]
		public void MappingSassSyntaxErrorDuringCompilationOfCode()
		{
			// Arrange
			string inputPath = GenerateSassFilePath("invalid-syntax", "style");
			string input = GetFileContent(inputPath);

			// Act
			string output;
			SassCompilationException exception = null;

			try
			{
				using (var sassCompiler = CreateSassCompiler())
				{
					output = sassCompiler.Compile(input, inputPath).CompiledContent;
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
				"   at Files/simple/errors/invalid-syntax/scss/style.scss:3:36 -> " +
				"    family: \"Open Sans, sans-serif;",
				exception.Message
			);
			Assert.AreEqual("Expected \".", exception.Description);
			Assert.AreEqual(1, exception.Status);
			Assert.AreEqual(inputPath, exception.File);
			Assert.AreEqual(3, exception.LineNumber);
			Assert.AreEqual(36, exception.ColumnNumber);
			Assert.AreEqual(
				"Line 2:   font: italic 20px/24px {" + Environment.NewLine +
				"Line 3:     family: \"Open Sans, sans-serif;" + Environment.NewLine +
				"-------------------------------------------^" + Environment.NewLine +
				"Line 4:   }",
				exception.SourceFragment
			);
			Assert.IsEmpty(exception.CallStack);
		}

		#endregion

		#region Files

		[Test]
		public void MappingSassSyntaxDuringCompilationOfFile()
		{
			// Arrange
			string inputPath = GenerateSassFilePath("invalid-syntax", "style");

			// Act
			string output;
			SassCompilationException exception = null;

			try
			{
				using (var sassCompiler = CreateSassCompiler())
				{
					output = sassCompiler.CompileFile(inputPath).CompiledContent;
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
				"   at Files/simple/errors/invalid-syntax/scss/style.scss:3:36 -> " +
				"    family: \"Open Sans, sans-serif;",
				exception.Message
			);
			Assert.AreEqual("Expected \".", exception.Description);
			Assert.AreEqual(1, exception.Status);
			Assert.AreEqual(inputPath, exception.File);
			Assert.AreEqual(3, exception.LineNumber);
			Assert.AreEqual(36, exception.ColumnNumber);
			Assert.AreEqual(
				"Line 2:   font: italic 20px/24px {" + Environment.NewLine +
				"Line 3:     family: \"Open Sans, sans-serif;" + Environment.NewLine +
				"-------------------------------------------^" + Environment.NewLine +
				"Line 4:   }",
				exception.SourceFragment
			);
			Assert.IsEmpty(exception.CallStack);
		}

		#endregion
	}
}