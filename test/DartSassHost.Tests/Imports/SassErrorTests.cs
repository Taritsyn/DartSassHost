using System;

using NUnit.Framework;

namespace DartSassHost.Tests.Imports
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
				"   at root stylesheet (Files/imports/errors/non-existing-files/sass/base.sass:5:9) -> " +
				"@import normalize",
				exception.Message
			);
			Assert.AreEqual("Can't find stylesheet to import.", exception.Description);
			Assert.AreEqual(1, exception.Status);
			Assert.AreEqual(inputPath, exception.File);
			Assert.AreEqual(5, exception.LineNumber);
			Assert.AreEqual(9, exception.ColumnNumber);
			Assert.AreEqual(
				"Line 5: @import normalize" + Environment.NewLine +
				"----------------^" + Environment.NewLine +
				"Line 6: @import url(http://fonts.googleapis.com/css?family=Limelight&subset=latin,latin-ext)",
				exception.SourceFragment
			);
			Assert.AreEqual(
				"   at root stylesheet (Files/imports/errors/non-existing-files/sass/base.sass:5:9)",
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
				"Error: Expected newline." + Environment.NewLine +
				"   at @import (Files/imports/errors/invalid-syntax/sass/_reset.sass:5:9) -> " +
				"  margin; 0" + Environment.NewLine +
				"   at root stylesheet (Files/imports/errors/invalid-syntax/sass/base.sass:5:9)",
				exception.Message
			);
			Assert.AreEqual("Expected newline.", exception.Description);
			Assert.AreEqual(1, exception.Status);
			Assert.AreEqual(importedFilePath, exception.File);
			Assert.AreEqual(5, exception.LineNumber);
			Assert.AreEqual(9, exception.ColumnNumber);
			Assert.AreEqual(
				"Line 4: ol" + Environment.NewLine +
				"Line 5:   margin; 0" + Environment.NewLine +
				"----------------^" + Environment.NewLine +
				"Line 6:   padding: 0",
				exception.SourceFragment
			);
			Assert.AreEqual(
				"   at @import (Files/imports/errors/invalid-syntax/sass/_reset.sass:5:9)" + Environment.NewLine +
				"   at root stylesheet (Files/imports/errors/invalid-syntax/sass/base.sass:5:9)",
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
				"   at root stylesheet (Files/imports/errors/non-existing-files/sass/base.sass:5:9) -> " +
				"@import normalize",
				exception.Message
			);
			Assert.AreEqual("Can't find stylesheet to import.", exception.Description);
			Assert.AreEqual(1, exception.Status);
			Assert.AreEqual(inputPath, exception.File);
			Assert.AreEqual(5, exception.LineNumber);
			Assert.AreEqual(9, exception.ColumnNumber);
			Assert.AreEqual(
				"Line 5: @import normalize" + Environment.NewLine +
				"----------------^" + Environment.NewLine +
				"Line 6: @import url(http://fonts.googleapis.com/css?family=Limelight&subset=latin,latin-ext)",
				exception.SourceFragment
			);
			Assert.AreEqual(
				"   at root stylesheet (Files/imports/errors/non-existing-files/sass/base.sass:5:9)",
				exception.CallStack
			);
		}

		[Test]
		public void MappingSassSyntaxErrorDuringCompilationOfFile()
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
				"Error: Expected newline." + Environment.NewLine +
				"   at @import (Files/imports/errors/invalid-syntax/sass/_reset.sass:5:9) -> " +
				"  margin; 0" + Environment.NewLine +
				"   at root stylesheet (Files/imports/errors/invalid-syntax/sass/base.sass:5:9)",
				exception.Message
			);
			Assert.AreEqual("Expected newline.", exception.Description);
			Assert.AreEqual(1, exception.Status);
			Assert.AreEqual(importedFilePath, exception.File);
			Assert.AreEqual(5, exception.LineNumber);
			Assert.AreEqual(9, exception.ColumnNumber);
			Assert.AreEqual(
				"Line 4: ol" + Environment.NewLine +
				"Line 5:   margin; 0" + Environment.NewLine +
				"----------------^" + Environment.NewLine +
				"Line 6:   padding: 0",
				exception.SourceFragment
			);
			Assert.AreEqual(
				"   at @import (Files/imports/errors/invalid-syntax/sass/_reset.sass:5:9)" + Environment.NewLine +
				"   at root stylesheet (Files/imports/errors/invalid-syntax/sass/base.sass:5:9)",
				exception.CallStack
			);
		}

		#endregion
	}
}