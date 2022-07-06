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


		[Test]
		public void MappingSassImportErrorDuringCompilation([Values]bool fromFile)
		{
			// Arrange
			string inputPath = GenerateSassFilePath("non-existing-files", "base");
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
				"Error: Can't find stylesheet to import." + Environment.NewLine +
				"   at root stylesheet (Files/imports/errors/non-existing-files/scss/base.scss:6:9) -> " +
				"@import 'normalize';",
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
			Assert.AreEqual(
				"   at root stylesheet (Files/imports/errors/non-existing-files/scss/base.scss:6:9)",
				exception.CallStack
			);
		}

		[Test]
		public void MappingSassSyntaxErrorDuringCompilation([Values]bool fromFile)
		{
			// Arrange
			string inputPath = GenerateSassFilePath("invalid-syntax", "base");
			string input = !fromFile ? GetFileContent(inputPath) : string.Empty;
			string importedFilePath = GenerateSassFilePath("invalid-syntax", "_reset");

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
				"Error: expected \"{\"." + Environment.NewLine +
				"   at @import (Files/imports/errors/invalid-syntax/scss/_reset.scss:5:9) -> " +
				"  margin; 0;" + Environment.NewLine +
				"   at root stylesheet (Files/imports/errors/invalid-syntax/scss/base.scss:6:9)",
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
			Assert.AreEqual(
				"   at @import (Files/imports/errors/invalid-syntax/scss/_reset.scss:5:9)" + Environment.NewLine +
				"   at root stylesheet (Files/imports/errors/invalid-syntax/scss/base.scss:6:9)",
				exception.CallStack
			);
		}

		[Test]
		public void MappingSassCustomErrorDuringCompilation([Values]bool fromFile)
		{
			// Arrange
			string inputPath = GenerateSassFilePath("custom-error", "base");
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
				"   at root stylesheet (Files/imports/errors/custom-error/scss/base.scss:4:3) -> " +
				"  @include reflexive-position(top, 12px);",
				exception.Message
			);
			Assert.AreEqual("\"Property top must be either left or right.\"", exception.Description);
			Assert.AreEqual(1, exception.Status);
			Assert.AreEqual(inputPath, exception.File);
			Assert.AreEqual(4, exception.LineNumber);
			Assert.AreEqual(3, exception.ColumnNumber);
			Assert.AreEqual(
				"Line 3: .sidebar {" + Environment.NewLine +
				"Line 4:   @include reflexive-position(top, 12px);" + Environment.NewLine +
				"----------^" + Environment.NewLine +
				"Line 5: }",
				exception.SourceFragment
			);
			Assert.AreEqual(
				"   at root stylesheet (Files/imports/errors/custom-error/scss/base.scss:4:3)",
				exception.CallStack
			);
		}
	}
}