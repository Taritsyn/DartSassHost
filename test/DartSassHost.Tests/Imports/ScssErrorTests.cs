using System;

using NUnit.Framework;

namespace DartSassHost.Tests.Imports
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
				"   at Files/imports/errors/non-existing-files/scss/base.scss:6:9 -> @import 'normalize';",
				exception.Message
			);
			Assert.AreEqual("Can't find stylesheet to import.", exception.Description);
			Assert.AreEqual(1, exception.Status);
			Assert.AreEqual(inputPath, exception.File);
			Assert.AreEqual(6, exception.LineNumber);
			Assert.AreEqual(9, exception.ColumnNumber);
			Assert.AreEqual(
				"Line 6: @import 'normalize';" + Environment.NewLine +
				"----------------^" + Environment.NewLine +
				"Line 7: @import url(http://fonts.googleapis.com/css?family=Limelight&subset=latin,latin-ext);",
				exception.SourceFragment
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
				"   at Files/imports/errors/invalid-syntax/scss/_reset.scss:5:9 ->   margin; 0;",
				exception.Message
			);
			Assert.AreEqual("expected \"{\".", exception.Description);
			Assert.AreEqual(1, exception.Status);
			Assert.AreEqual(importedFilePath, exception.File);
			Assert.AreEqual(5, exception.LineNumber);
			Assert.AreEqual(9, exception.ColumnNumber);
			Assert.AreEqual(
				"Line 4: ol {" + Environment.NewLine +
				"Line 5:   margin; 0;" + Environment.NewLine +
				"----------------^" + Environment.NewLine +
				"Line 6:   padding: 0;",
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
				"   at Files/imports/errors/non-existing-files/scss/base.scss:6:9 -> @import 'normalize';",
				exception.Message
			);
			Assert.AreEqual("Can't find stylesheet to import.", exception.Description);
			Assert.AreEqual(1, exception.Status);
			Assert.AreEqual(inputPath, exception.File);
			Assert.AreEqual(6, exception.LineNumber);
			Assert.AreEqual(9, exception.ColumnNumber);
			Assert.AreEqual(
				"Line 6: @import 'normalize';" + Environment.NewLine +
				"----------------^" + Environment.NewLine +
				"Line 7: @import url(http://fonts.googleapis.com/css?family=Limelight&subset=latin,latin-ext);",
				exception.SourceFragment
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
				"   at Files/imports/errors/invalid-syntax/scss/_reset.scss:5:9 ->   margin; 0;",
				exception.Message
			);
			Assert.AreEqual("expected \"{\".", exception.Description);
			Assert.AreEqual(1, exception.Status);
			Assert.AreEqual(importedFilePath, exception.File);
			Assert.AreEqual(5, exception.LineNumber);
			Assert.AreEqual(9, exception.ColumnNumber);
			Assert.AreEqual(
				"Line 4: ol {" + Environment.NewLine +
				"Line 5:   margin; 0;" + Environment.NewLine +
				"----------------^" + Environment.NewLine +
				"Line 6:   padding: 0;",
				exception.SourceFragment
			);
		}

		#endregion
	}
}