using System;

using NUnit.Framework;

namespace DartSassHost.Tests
{
	[TestFixture]
	public sealed class SassErrorTests : ErrorTestsBase
	{
		public SassErrorTests()
			: base(SyntaxType.Sass)
		{ }


		#region Code

		[Test]
		public void MappingSassImportErrorDuringCompilationOfCode()
		{
			// Arrange
			string inputPath = GenerateSassFilePath("non-existing-files", "base");
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
				"   at base.sass:5:9",
				exception.Message
			);
			Assert.AreEqual("Can't find stylesheet to import.", exception.Description);
			Assert.AreEqual(1, exception.Status);
			Assert.AreEqual(ToAbsolutePath(inputPath), exception.File);
			Assert.AreEqual(5, exception.LineNumber);
			Assert.AreEqual(9, exception.ColumnNumber);
			Assert.IsEmpty(exception.SourceFragment);
		}

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
				"   at style.sass:3:35 ->     family: \"Open Sans, sans-serif",
				exception.Message
			);
			Assert.AreEqual("Expected \".", exception.Description);
			Assert.AreEqual(1, exception.Status);
			Assert.AreEqual(ToAbsolutePath(inputPath), exception.File);
			Assert.AreEqual(3, exception.LineNumber);
			Assert.AreEqual(35, exception.ColumnNumber);
			Assert.AreEqual(
				"Line 2:   font: italic 20px/24px" + Environment.NewLine +
				"Line 3:     family: \"Open Sans, sans-serif" + Environment.NewLine +
				"------------------------------------------^",
				exception.SourceFragment
			);
		}

		#endregion

		#region Files

		[Test]
		public void MappingSassImportErrorDuringCompilationOfFile()
		{
			// Arrange
			string inputPath = GenerateSassFilePath("non-existing-files", "base");

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
				"   at base.sass:5:9",
				exception.Message
			);
			Assert.AreEqual("Can't find stylesheet to import.", exception.Description);
			Assert.AreEqual(1, exception.Status);
			Assert.AreEqual(ToAbsolutePath(inputPath), exception.File);
			Assert.AreEqual(5, exception.LineNumber);
			Assert.AreEqual(9, exception.ColumnNumber);
			Assert.IsEmpty(exception.SourceFragment);
		}

		[Test]
		public void MappingSassSyntaxErrorDuringCompilationOfFile()
		{
			// Arrange
			string inputPath = GenerateSassFilePath("invalid-syntax", "style");

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
				"   at style.sass:3:35 ->     family: \"Open Sans, sans-serif",
				exception.Message
			);
			Assert.AreEqual("Expected \".", exception.Description);
			Assert.AreEqual(1, exception.Status);
			Assert.AreEqual(ToAbsolutePath(inputPath), exception.File);
			Assert.AreEqual(3, exception.LineNumber);
			Assert.AreEqual(35, exception.ColumnNumber);
			Assert.AreEqual(
				"Line 2:   font: italic 20px/24px" + Environment.NewLine +
				"Line 3:     family: \"Open Sans, sans-serif" + Environment.NewLine +
				"------------------------------------------^",
				exception.SourceFragment
			);
		}

		#endregion
	}
}