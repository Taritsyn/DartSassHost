using NUnit.Framework;
using System;

namespace SassHost.Tests
{
	[TestFixture]
	public sealed class ScssErrorTests : ErrorTestsBase
	{
		public ScssErrorTests()
			: base(SyntaxType.Scss)
		{ }


		#region Code

		[Test]
		public void MappingSassImportErrorDuringCompilationOfCode()
		{
			// Arrange
			string inputPath = ToAbsolutePath(GenerateSassFilePath("non-existing-files", "base"));
			string input = GetFileContent(inputPath);

			// Act
			string output;
			SassCompilationException exception = null;

			try
			{
				using (var sassCompiler = new SassCompiler())
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
				"Error: Can't find stylesheet to import." + Environment.NewLine +
				"   at base.scss:6:9",
				exception.Message
			);
			Assert.AreEqual("Can't find stylesheet to import.", exception.Description);
			Assert.AreEqual(1, exception.Status);
			Assert.AreEqual(inputPath, exception.File);
			Assert.AreEqual(6, exception.LineNumber);
			Assert.AreEqual(9, exception.ColumnNumber);
			Assert.IsEmpty(exception.SourceFragment);
		}


		[Test]
		public void MappingSassSyntaxErrorDuringCompilationOfCode()
		{
			// Arrange
			string inputPath = ToAbsolutePath(GenerateSassFilePath("invalid-syntax", "style"));
			string input = GetFileContent(inputPath);

			// Act
			string output;
			SassCompilationException exception = null;

			try
			{
				using (var sassCompiler = new SassCompiler())
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
				"   at style.scss:3:36 ->     family: \"Open Sans, sans-serif;",
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
		}

		#endregion

		#region Files

		[Test]
		public void MappingSassImportErrorDuringCompilationOfFile()
		{
			// Arrange
			string inputPath = ToAbsolutePath(GenerateSassFilePath("non-existing-files", "base"));

			// Act
			string output;
			SassCompilationException exception = null;

			try
			{
				using (var sassCompiler = new SassCompiler())
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
				"Error: Can't find stylesheet to import." + Environment.NewLine +
				"   at base.scss:6:9",
				exception.Message
			);
			Assert.AreEqual("Can't find stylesheet to import.", exception.Description);
			Assert.AreEqual(1, exception.Status);
			Assert.AreEqual(inputPath, exception.File);
			Assert.AreEqual(6, exception.LineNumber);
			Assert.AreEqual(9, exception.ColumnNumber);
			Assert.IsEmpty(exception.SourceFragment);
		}

		[Test]
		public void MappingSassSyntaxDuringCompilationOfFile()
		{
			// Arrange
			string inputPath = ToAbsolutePath(GenerateSassFilePath("invalid-syntax", "style"));

			// Act
			string output;
			SassCompilationException exception = null;

			try
			{
				using (var sassCompiler = new SassCompiler())
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
				"   at style.scss:3:36 ->     family: \"Open Sans, sans-serif;",
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
		}

		#endregion
	}
}