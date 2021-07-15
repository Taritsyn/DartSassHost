using System;
using System.Collections.Generic;

using NUnit.Framework;

namespace DartSassHost.Tests.Simple
{
	[TestFixture]
	public sealed class SassWarningTests : WarningTestsBase
	{
		public SassWarningTests()
			: base(SyntaxType.Sass)
		{ }


		#region Code

		[Test]
		public void MappingSassDeprecationWarningDuringCompilationOfCode()
		{
			// Arrange
			string inputPath = GenerateSassFilePath("deprecated-division", "style");
			string input = GetFileContent(inputPath);

			// Act
			IList<ProblemInfo> warnings;

			using (var sassCompiler = CreateSassCompiler())
			{
				warnings = sassCompiler.Compile(input, inputPath).Warnings;
			}

			// Assert
			Assert.AreEqual(2, warnings.Count);

			Assert.AreEqual(
				"Deprecation Warning: Using / for division is deprecated and will be removed in Dart Sass 2.0.0.\n\n" +
				"Recommendation: math.div(map-get($grid-gutter-widths, xs), 2)\n\n" +
				"More info and automated migrator: https://sass-lang.com/d/slash-div" + Environment.NewLine +
				"   at root stylesheet (Files/simple/warnings/deprecated-division/sass/style.sass:3:19)",
				warnings[0].Message
			);
			Assert.AreEqual(
				"Using / for division is deprecated and will be removed in Dart Sass 2.0.0.\n\n" +
				"Recommendation: math.div(map-get($grid-gutter-widths, xs), 2)\n\n" +
				"More info and automated migrator: https://sass-lang.com/d/slash-div",
				warnings[0].Description
			);
			Assert.AreEqual(true, warnings[0].IsDeprecation);
			Assert.AreEqual(inputPath, warnings[0].File);
			Assert.AreEqual(3, warnings[0].LineNumber);
			Assert.AreEqual(19, warnings[0].ColumnNumber);
			Assert.AreEqual(
				"Line 3: $col-padding-xs:  map-get($grid-gutter-widths, xs) / 2" + Environment.NewLine +
				"--------------------------^",
				warnings[0].SourceFragment
			);
			Assert.AreEqual(
				"   at root stylesheet (Files/simple/warnings/deprecated-division/sass/style.sass:3:19)",
				warnings[0].CallStack
			);

			Assert.AreEqual(
				"Deprecation Warning: Using / for division is deprecated and will be removed in Dart Sass 2.0.0.\n\n" +
				"Recommendation: math.div($col-padding-xs, 2)\n\n" +
				"More info and automated migrator: https://sass-lang.com/d/slash-div" + Environment.NewLine +
				"   at root stylesheet (Files/simple/warnings/deprecated-division/sass/style.sass:6:18)",
				warnings[1].Message
			);
			Assert.AreEqual(
				"Using / for division is deprecated and will be removed in Dart Sass 2.0.0.\n\n" +
				"Recommendation: math.div($col-padding-xs, 2)\n\n" +
				"More info and automated migrator: https://sass-lang.com/d/slash-div",
				warnings[1].Description
			);
			Assert.AreEqual(true, warnings[1].IsDeprecation);
			Assert.AreEqual(inputPath, warnings[1].File);
			Assert.AreEqual(6, warnings[1].LineNumber);
			Assert.AreEqual(18, warnings[1].ColumnNumber);
			Assert.AreEqual(
				"Line 5: div" + Environment.NewLine +
				"Line 6:   padding-right: $col-padding-xs / 2" + Environment.NewLine +
				"-------------------------^",
				warnings[1].SourceFragment
			);
			Assert.AreEqual(
				"   at root stylesheet (Files/simple/warnings/deprecated-division/sass/style.sass:6:18)",
				warnings[1].CallStack
			);
		}

		[Test]
		public void MappingSassCustomWarningDuringCompilationOfCode()
		{
			// Arrange
			string inputPath = GenerateSassFilePath("custom-warning", "style");
			string input = GetFileContent(inputPath);

			// Act
			IList<ProblemInfo> warnings;

			using (var sassCompiler = CreateSassCompiler())
			{
				warnings = sassCompiler.Compile(input, inputPath).Warnings;
			}

			// Assert
			Assert.AreEqual(1, warnings.Count);

			Assert.AreEqual(
				"Warning: Unknown prefix wekbit." + Environment.NewLine +
				"   at prefix (Files/simple/warnings/custom-warning/sass/style.sass:6:7)" + Environment.NewLine +
				"   at root stylesheet (Files/simple/warnings/custom-warning/sass/style.sass:15:3)",
				warnings[0].Message
			);
			Assert.AreEqual(
				"Unknown prefix wekbit.",
				warnings[0].Description
			);
			Assert.AreEqual(false, warnings[0].IsDeprecation);
			Assert.AreEqual(inputPath, warnings[0].File);
			Assert.AreEqual(6, warnings[0].LineNumber);
			Assert.AreEqual(7, warnings[0].ColumnNumber);
			Assert.AreEqual(
				"Line 5:     @if not index($known-prefixes, $prefix)" + Environment.NewLine +
				"Line 6:       @warn \"Unknown prefix #{$prefix}.\"" + Environment.NewLine +
				"--------------^",
				warnings[0].SourceFragment
			);
			Assert.AreEqual(
				"   at prefix (Files/simple/warnings/custom-warning/sass/style.sass:6:7)" + Environment.NewLine +
				"   at root stylesheet (Files/simple/warnings/custom-warning/sass/style.sass:15:3)",
				warnings[0].CallStack
			);
		}

		#endregion

		#region Files

		[Test]
		public void MappingSassDeprecationWarningDuringCompilationOfFile()
		{
			// Arrange
			string inputPath = GenerateSassFilePath("deprecated-division", "style");

			// Act
			IList<ProblemInfo> warnings;

			using (var sassCompiler = CreateSassCompiler())
			{
				warnings = sassCompiler.CompileFile(inputPath).Warnings;
			}

			// Assert
			Assert.AreEqual(2, warnings.Count);

			Assert.AreEqual(
				"Deprecation Warning: Using / for division is deprecated and will be removed in Dart Sass 2.0.0.\n\n" +
				"Recommendation: math.div(map-get($grid-gutter-widths, xs), 2)\n\n" +
				"More info and automated migrator: https://sass-lang.com/d/slash-div" + Environment.NewLine +
				"   at root stylesheet (Files/simple/warnings/deprecated-division/sass/style.sass:3:19)",
				warnings[0].Message
			);
			Assert.AreEqual(
				"Using / for division is deprecated and will be removed in Dart Sass 2.0.0.\n\n" +
				"Recommendation: math.div(map-get($grid-gutter-widths, xs), 2)\n\n" +
				"More info and automated migrator: https://sass-lang.com/d/slash-div",
				warnings[0].Description
			);
			Assert.AreEqual(true, warnings[0].IsDeprecation);
			Assert.AreEqual(inputPath, warnings[0].File);
			Assert.AreEqual(3, warnings[0].LineNumber);
			Assert.AreEqual(19, warnings[0].ColumnNumber);
			Assert.AreEqual(
				"Line 3: $col-padding-xs:  map-get($grid-gutter-widths, xs) / 2" + Environment.NewLine +
				"--------------------------^",
				warnings[0].SourceFragment
			);
			Assert.AreEqual(
				"   at root stylesheet (Files/simple/warnings/deprecated-division/sass/style.sass:3:19)",
				warnings[0].CallStack
			);

			Assert.AreEqual(
				"Deprecation Warning: Using / for division is deprecated and will be removed in Dart Sass 2.0.0.\n\n" +
				"Recommendation: math.div($col-padding-xs, 2)\n\n" +
				"More info and automated migrator: https://sass-lang.com/d/slash-div" + Environment.NewLine +
				"   at root stylesheet (Files/simple/warnings/deprecated-division/sass/style.sass:6:18)",
				warnings[1].Message
			);
			Assert.AreEqual(
				"Using / for division is deprecated and will be removed in Dart Sass 2.0.0.\n\n" +
				"Recommendation: math.div($col-padding-xs, 2)\n\n" +
				"More info and automated migrator: https://sass-lang.com/d/slash-div",
				warnings[1].Description
			);
			Assert.AreEqual(true, warnings[1].IsDeprecation);
			Assert.AreEqual(inputPath, warnings[1].File);
			Assert.AreEqual(6, warnings[1].LineNumber);
			Assert.AreEqual(18, warnings[1].ColumnNumber);
			Assert.AreEqual(
				"Line 5: div" + Environment.NewLine +
				"Line 6:   padding-right: $col-padding-xs / 2" + Environment.NewLine +
				"-------------------------^",
				warnings[1].SourceFragment
			);
			Assert.AreEqual(
				"   at root stylesheet (Files/simple/warnings/deprecated-division/sass/style.sass:6:18)",
				warnings[1].CallStack
			);
		}

		[Test]
		public void MappingSassCustomWarningDuringCompilationOfFile()
		{
			// Arrange
			string inputPath = GenerateSassFilePath("custom-warning", "style");

			// Act
			IList<ProblemInfo> warnings;

			using (var sassCompiler = CreateSassCompiler())
			{
				warnings = sassCompiler.CompileFile(inputPath).Warnings;
			}

			// Assert
			Assert.AreEqual(1, warnings.Count);

			Assert.AreEqual(
				"Warning: Unknown prefix wekbit." + Environment.NewLine +
				"   at prefix (Files/simple/warnings/custom-warning/sass/style.sass:6:7)" + Environment.NewLine +
				"   at root stylesheet (Files/simple/warnings/custom-warning/sass/style.sass:15:3)",
				warnings[0].Message
			);
			Assert.AreEqual(
				"Unknown prefix wekbit.",
				warnings[0].Description
			);
			Assert.AreEqual(false, warnings[0].IsDeprecation);
			Assert.AreEqual(inputPath, warnings[0].File);
			Assert.AreEqual(6, warnings[0].LineNumber);
			Assert.AreEqual(7, warnings[0].ColumnNumber);
			Assert.AreEqual(
				"Line 5:     @if not index($known-prefixes, $prefix)" + Environment.NewLine +
				"Line 6:       @warn \"Unknown prefix #{$prefix}.\"" + Environment.NewLine +
				"--------------^",
				warnings[0].SourceFragment
			);
			Assert.AreEqual(
				"   at prefix (Files/simple/warnings/custom-warning/sass/style.sass:6:7)" + Environment.NewLine +
				"   at root stylesheet (Files/simple/warnings/custom-warning/sass/style.sass:15:3)",
				warnings[0].CallStack
			);
		}

		#endregion
	}
}
