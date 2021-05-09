using System;

using NUnit.Framework;

namespace DartSassHost.Tests.Modules
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
			string inputPath = GenerateSassFilePath("non-existing-files", "base");
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
				"Error: Can't find stylesheet to import." + Environment.NewLine +
				"   at base.scss:1:1",
				exception.Message
			);
			Assert.AreEqual("Can't find stylesheet to import.", exception.Description);
			Assert.AreEqual(1, exception.Status);
			Assert.AreEqual(inputPath, exception.File);
			Assert.AreEqual(1, exception.LineNumber);
			Assert.AreEqual(1, exception.ColumnNumber);
			Assert.IsEmpty(exception.SourceFragment);
		}

		[Test]
		public void MappingSassSyntaxErrorDuringCompilationOfCode()
		{
			// Arrange
			string inputPath = GenerateSassFilePath("invalid-syntax", "base");
			string input = GetFileContent(inputPath);
			string importedFilePath = GenerateSassFilePath("invalid-syntax", "_reset");

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
				"Error: expected \"{\"." + Environment.NewLine +
				"   at _reset.scss:6:10",
				exception.Message
			);
			Assert.AreEqual("expected \"{\".", exception.Description);
			Assert.AreEqual(1, exception.Status);
			Assert.AreEqual(importedFilePath, exception.File);
			Assert.AreEqual(6, exception.LineNumber);
			Assert.AreEqual(10, exception.ColumnNumber);
			Assert.IsEmpty(exception.SourceFragment);
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
				"Error: Can't find stylesheet to import." + Environment.NewLine +
				"   at base.scss:1:1",
				exception.Message
			);
			Assert.AreEqual("Can't find stylesheet to import.", exception.Description);
			Assert.AreEqual(1, exception.Status);
			Assert.AreEqual(inputPath, exception.File);
			Assert.AreEqual(1, exception.LineNumber);
			Assert.AreEqual(1, exception.ColumnNumber);
			Assert.IsEmpty(exception.SourceFragment);
		}

		[Test]
		public void MappingSassSyntaxDuringCompilationOfFile()
		{
			// Arrange
			string inputPath = GenerateSassFilePath("invalid-syntax", "base");
			string importedFilePath = GenerateSassFilePath("invalid-syntax", "_reset");

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
				"Error: expected \"{\"." + Environment.NewLine +
				"   at _reset.scss:6:10",
				exception.Message
			);
			Assert.AreEqual("expected \"{\".", exception.Description);
			Assert.AreEqual(1, exception.Status);
			Assert.AreEqual(importedFilePath, exception.File);
			Assert.AreEqual(6, exception.LineNumber);
			Assert.AreEqual(10, exception.ColumnNumber);
			Assert.IsEmpty(exception.SourceFragment);
		}

		#endregion
	}
}