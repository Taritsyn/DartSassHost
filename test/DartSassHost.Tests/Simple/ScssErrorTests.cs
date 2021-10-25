﻿using System;

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

		[Test]
		public void MappingSassCustomErrorDuringCompilationOfCode()
		{
			// Arrange
			string inputPath = GenerateSassFilePath("custom-error", "style");
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
				"Error: \"Property top must be either left or right.\"" + Environment.NewLine +
				"   at root stylesheet (Files/simple/errors/custom-error/scss/style.scss:18:3) -> " +
				"  @include reflexive-position(top, 12px);",
				exception.Message
			);
			Assert.AreEqual("\"Property top must be either left or right.\"", exception.Description);
			Assert.AreEqual(1, exception.Status);
			Assert.AreEqual(inputPath, exception.File);
			Assert.AreEqual(18, exception.LineNumber);
			Assert.AreEqual(3, exception.ColumnNumber);
			Assert.AreEqual(
				"Line 17: .sidebar {" + Environment.NewLine +
				"Line 18:   @include reflexive-position(top, 12px);" + Environment.NewLine +
				"-----------^" + Environment.NewLine +
				"Line 19: }",
				exception.SourceFragment
			);
			Assert.AreEqual(
				"   at root stylesheet (Files/simple/errors/custom-error/scss/style.scss:18:3)",
				exception.CallStack
			);
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

		[Test]
		public void MappingSassCustomErrorDuringCompilationOfFile()
		{
			// Arrange
			string inputPath = GenerateSassFilePath("custom-error", "style");

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
				"Error: \"Property top must be either left or right.\"" + Environment.NewLine +
				"   at root stylesheet (Files/simple/errors/custom-error/scss/style.scss:18:3) -> " +
				"  @include reflexive-position(top, 12px);",
				exception.Message
			);
			Assert.AreEqual("\"Property top must be either left or right.\"", exception.Description);
			Assert.AreEqual(1, exception.Status);
			Assert.AreEqual(inputPath, exception.File);
			Assert.AreEqual(18, exception.LineNumber);
			Assert.AreEqual(3, exception.ColumnNumber);
			Assert.AreEqual(
				"Line 17: .sidebar {" + Environment.NewLine +
				"Line 18:   @include reflexive-position(top, 12px);" + Environment.NewLine +
				"-----------^" + Environment.NewLine +
				"Line 19: }",
				exception.SourceFragment
			);
			Assert.AreEqual(
				"   at root stylesheet (Files/simple/errors/custom-error/scss/style.scss:18:3)",
				exception.CallStack
			);
		}

		#endregion
	}
}