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
				"   at root stylesheet (Files/modules/errors/non-existing-files/scss/base.scss:1:1) -> " +
				"@use 'normalize';",
				exception.Message
			);
			Assert.AreEqual("Can't find stylesheet to import.", exception.Description);
			Assert.AreEqual(1, exception.Status);
			Assert.AreEqual(inputPath, exception.File);
			Assert.AreEqual(1, exception.LineNumber);
			Assert.AreEqual(1, exception.ColumnNumber);
			Assert.AreEqual(
				"Line 1: @use 'normalize';" + Environment.NewLine +
				"--------^",
				exception.SourceFragment
			);
			Assert.AreEqual(
				"   at root stylesheet (Files/modules/errors/non-existing-files/scss/base.scss:1:1)",
				exception.CallStack
			);
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
				"   at @use (Files/modules/errors/invalid-syntax/scss/_reset.scss:6:10) -> " +
				"  padding; 0;" + Environment.NewLine +
				"   at root stylesheet (Files/modules/errors/invalid-syntax/scss/base.scss:1:1)",
				exception.Message
			);
			Assert.AreEqual("expected \"{\".", exception.Description);
			Assert.AreEqual(1, exception.Status);
			Assert.AreEqual(importedFilePath, exception.File);
			Assert.AreEqual(6, exception.LineNumber);
			Assert.AreEqual(10, exception.ColumnNumber);
			Assert.AreEqual(
				"Line 5:   margin: 0;" + Environment.NewLine +
				"Line 6:   padding; 0;" + Environment.NewLine +
				"-----------------^" + Environment.NewLine +
				"Line 7: }",
				exception.SourceFragment
			);
			Assert.AreEqual(
				"   at @use (Files/modules/errors/invalid-syntax/scss/_reset.scss:6:10)" + Environment.NewLine +
				"   at root stylesheet (Files/modules/errors/invalid-syntax/scss/base.scss:1:1)",
				exception.CallStack
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
				"   at root stylesheet (Files/modules/errors/non-existing-files/scss/base.scss:1:1) -> " +
				"@use 'normalize';",
				exception.Message
			);
			Assert.AreEqual("Can't find stylesheet to import.", exception.Description);
			Assert.AreEqual(1, exception.Status);
			Assert.AreEqual(inputPath, exception.File);
			Assert.AreEqual(1, exception.LineNumber);
			Assert.AreEqual(1, exception.ColumnNumber);
			Assert.AreEqual(
				"Line 1: @use 'normalize';" + Environment.NewLine +
				"--------^",
				exception.SourceFragment
			);
			Assert.AreEqual(
				"   at root stylesheet (Files/modules/errors/non-existing-files/scss/base.scss:1:1)",
				exception.CallStack
			);
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
				"   at @use (Files/modules/errors/invalid-syntax/scss/_reset.scss:6:10) -> " +
				"  padding; 0;" + Environment.NewLine +
				"   at root stylesheet (Files/modules/errors/invalid-syntax/scss/base.scss:1:1)",
				exception.Message
			);
			Assert.AreEqual("expected \"{\".", exception.Description);
			Assert.AreEqual(1, exception.Status);
			Assert.AreEqual(importedFilePath, exception.File);
			Assert.AreEqual(6, exception.LineNumber);
			Assert.AreEqual(10, exception.ColumnNumber);
			Assert.AreEqual(
				"Line 5:   margin: 0;" + Environment.NewLine +
				"Line 6:   padding; 0;" + Environment.NewLine +
				"-----------------^" + Environment.NewLine +
				"Line 7: }",
				exception.SourceFragment
			);
			Assert.AreEqual(
				"   at @use (Files/modules/errors/invalid-syntax/scss/_reset.scss:6:10)" + Environment.NewLine +
				"   at root stylesheet (Files/modules/errors/invalid-syntax/scss/base.scss:1:1)",
				exception.CallStack
			);
		}

		#endregion
	}
}