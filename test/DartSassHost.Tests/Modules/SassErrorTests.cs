using System;

using NUnit.Framework;

namespace DartSassHost.Tests.Modules
{
	[TestFixture]
	public sealed class SassErrorTests : ErrorTestsBase
	{
		public SassErrorTests()
			: base(SyntaxType.Sass)
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
				"   at root stylesheet (Files/modules/errors/non-existing-files/sass/base.sass:1:1) -> " +
				"@use 'normalize'",
				exception.Message
			);
			Assert.AreEqual("Can't find stylesheet to import.", exception.Description);
			Assert.AreEqual(1, exception.Status);
			Assert.AreEqual(inputPath, exception.File);
			Assert.AreEqual(1, exception.LineNumber);
			Assert.AreEqual(1, exception.ColumnNumber);
			Assert.AreEqual(
				"Line 1: @use 'normalize'" + Environment.NewLine +
				"--------^",
				exception.SourceFragment
			);
			Assert.AreEqual(
				"   at root stylesheet (Files/modules/errors/non-existing-files/sass/base.sass:1:1)",
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
				"Error: Expected newline." + Environment.NewLine +
				"   at @use (Files/modules/errors/invalid-syntax/sass/_reset.sass:6:10) -> " +
				"  padding; 0" + Environment.NewLine +
				"   at root stylesheet (Files/modules/errors/invalid-syntax/sass/base.sass:1:1)",
				exception.Message
			);
			Assert.AreEqual("Expected newline.", exception.Description);
			Assert.AreEqual(1, exception.Status);
			Assert.AreEqual(importedFilePath, exception.File);
			Assert.AreEqual(6, exception.LineNumber);
			Assert.AreEqual(10, exception.ColumnNumber);
			Assert.AreEqual(
				"Line 5:   margin: 0" + Environment.NewLine +
				"Line 6:   padding; 0" + Environment.NewLine +
				"-----------------^",
				exception.SourceFragment
			);
			Assert.AreEqual(
				"   at @use (Files/modules/errors/invalid-syntax/sass/_reset.sass:6:10)" + Environment.NewLine +
				"   at root stylesheet (Files/modules/errors/invalid-syntax/sass/base.sass:1:1)",
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
				"   at root stylesheet (Files/modules/errors/custom-error/sass/base.sass:4:3) -> " +
				"  @include m.reflexive-position(top, 12px)",
				exception.Message
			);
			Assert.AreEqual("\"Property top must be either left or right.\"", exception.Description);
			Assert.AreEqual(1, exception.Status);
			Assert.AreEqual(inputPath, exception.File);
			Assert.AreEqual(4, exception.LineNumber);
			Assert.AreEqual(3, exception.ColumnNumber);
			Assert.AreEqual(
				"Line 3: .sidebar" + Environment.NewLine +
				"Line 4:   @include m.reflexive-position(top, 12px)" + Environment.NewLine +
				"----------^",
				exception.SourceFragment
			);
			Assert.AreEqual(
				"   at root stylesheet (Files/modules/errors/custom-error/sass/base.sass:4:3)",
				exception.CallStack
			);
		}
	}
}