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
		public void MappingSassWarningDuringCompilationOfCode()
		{
			// Arrange
			string inputPath = GenerateSassFilePath("division-with-non-numeric-args", "style");
			string input = GetFileContent(inputPath);

			// Act
			IList<ProblemInfo> warnings;

			using (var sassCompiler = CreateSassCompiler())
			{
				warnings = sassCompiler.Compile(input, inputPath).Warnings;
			}

			// Assert
			Assert.AreEqual(3, warnings.Count);

			Assert.AreEqual(
				"Warning: " + WarningConstants.MathDivOnlySupportNumberArguments + Environment.NewLine +
				"   at root stylesheet (Files/simple/warnings/division-with-non-numeric-args/sass/style.sass:19:17) -> " +
				"  margin-right: math.div(\"#{$grid-gutter-width}\", 2)",
				warnings[0].Message
			);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings[0].Description);
			Assert.AreEqual(false, warnings[0].IsDeprecation);
			Assert.AreEqual(inputPath, warnings[0].File);
			Assert.AreEqual(19, warnings[0].LineNumber);
			Assert.AreEqual(17, warnings[0].ColumnNumber);
			Assert.AreEqual(
				"Line 18:   float: left" + Environment.NewLine +
				"Line 19:   margin-right: math.div(\"#{$grid-gutter-width}\", 2)" + Environment.NewLine +
				"-------------------------^" + Environment.NewLine +
				"Line 20:   margin-left: math.div(\"\" + $grid-gutter-width, 2)",
				warnings[0].SourceFragment
			);
			Assert.AreEqual(
				"   at root stylesheet (Files/simple/warnings/division-with-non-numeric-args/sass/style.sass:19:17)",
				warnings[0].CallStack
			);

			Assert.AreEqual(
				"Warning: " + WarningConstants.MathDivOnlySupportNumberArguments + Environment.NewLine +
				"   at root stylesheet (Files/simple/warnings/division-with-non-numeric-args/sass/style.sass:20:16) -> " +
				"  margin-left: math.div(\"\" + $grid-gutter-width, 2)",
				warnings[1].Message
			);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments,	warnings[1].Description);
			Assert.AreEqual(false, warnings[1].IsDeprecation);
			Assert.AreEqual(inputPath, warnings[1].File);
			Assert.AreEqual(20, warnings[1].LineNumber);
			Assert.AreEqual(16, warnings[1].ColumnNumber);
			Assert.AreEqual(
				"Line 19:   margin-right: math.div(\"#{$grid-gutter-width}\", 2)" + Environment.NewLine +
				"Line 20:   margin-left: math.div(\"\" + $grid-gutter-width, 2)" + Environment.NewLine +
				"------------------------^" + Environment.NewLine + "Line 21:   -webkit-box-sizing: border-box",
				warnings[1].SourceFragment
			);
			Assert.AreEqual(
				"   at root stylesheet (Files/simple/warnings/division-with-non-numeric-args/sass/style.sass:20:16)",
				warnings[1].CallStack
			);

			Assert.AreEqual(
				"Warning: " + WarningConstants.MathDivOnlySupportNumberArguments + Environment.NewLine +
				"   at root stylesheet (Files/simple/warnings/division-with-non-numeric-args/sass/style.sass:33:12) -> " +
				"    width: math.div(100% * $num - $grid-gutter-width * ($grid-columns - $num), \"#{$grid-columns}\")",
				warnings[2].Message
			);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments,	warnings[2].Description);
			Assert.AreEqual(false, warnings[2].IsDeprecation);
			Assert.AreEqual(inputPath, warnings[2].File);
			Assert.AreEqual(33, warnings[2].LineNumber);
			Assert.AreEqual(12, warnings[2].ColumnNumber);
			Assert.AreEqual(
				"Line 32:   .col-#{$num}" + Environment.NewLine +
				"Line 33:     width: math.div(100% * $num - $grid-gutter-width * ($grid-columns - $num), \"#{$grid-colum…" + Environment.NewLine +
				"--------------------^",
				warnings[2].SourceFragment
			);
			Assert.AreEqual(
				"   at root stylesheet (Files/simple/warnings/division-with-non-numeric-args/sass/style.sass:33:12)",
				warnings[2].CallStack
			);
		}

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

			string description1 = string.Format(WarningConstants.DeprecatedDivisionWithSimpleRecommendation,
				"map-get($grid-gutter-widths, xs)", 2);

			Assert.AreEqual(
				"Deprecation Warning: " + description1 + Environment.NewLine +
				"   at root stylesheet (Files/simple/warnings/deprecated-division/sass/style.sass:3:19) -> " +
				"$col-padding-xs:  map-get($grid-gutter-widths, xs) / 2",
				warnings[0].Message
			);
			Assert.AreEqual(description1, warnings[0].Description);
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

			string description2 = string.Format(WarningConstants.DeprecatedDivisionWithSimpleRecommendation,
				"$col-padding-xs", 2);

			Assert.AreEqual(
				"Deprecation Warning: " + description2 + Environment.NewLine +
				"   at root stylesheet (Files/simple/warnings/deprecated-division/sass/style.sass:6:18) -> " +
				"  padding-right: $col-padding-xs / 2",
				warnings[1].Message
			);
			Assert.AreEqual(description2, warnings[1].Description);
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

			string description = string.Format(WarningConstants.UnknownVendorPrefix, "wekbit");

			Assert.AreEqual(
				"Warning: " + description + Environment.NewLine +
				"   at prefix (Files/simple/warnings/custom-warning/sass/style.sass:6:7) -> " +
				"      @warn \"Unknown prefix #{$prefix}.\"" + Environment.NewLine +
				"   at root stylesheet (Files/simple/warnings/custom-warning/sass/style.sass:15:3)",
				warnings[0].Message
			);
			Assert.AreEqual(description, warnings[0].Description);
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
		public void MappingSassWarningDuringCompilationOfFile()
		{
			// Arrange
			string inputPath = GenerateSassFilePath("division-with-non-numeric-args", "style");

			// Act
			IList<ProblemInfo> warnings;

			using (var sassCompiler = CreateSassCompiler())
			{
				warnings = sassCompiler.CompileFile(inputPath).Warnings;
			}

			// Assert
			Assert.AreEqual(3, warnings.Count);

			Assert.AreEqual(
				"Warning: " + WarningConstants.MathDivOnlySupportNumberArguments + Environment.NewLine +
				"   at root stylesheet (Files/simple/warnings/division-with-non-numeric-args/sass/style.sass:19:17) -> " +
				"  margin-right: math.div(\"#{$grid-gutter-width}\", 2)",
				warnings[0].Message
			);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings[0].Description);
			Assert.AreEqual(false, warnings[0].IsDeprecation);
			Assert.AreEqual(inputPath, warnings[0].File);
			Assert.AreEqual(19, warnings[0].LineNumber);
			Assert.AreEqual(17, warnings[0].ColumnNumber);
			Assert.AreEqual(
				"Line 18:   float: left" + Environment.NewLine +
				"Line 19:   margin-right: math.div(\"#{$grid-gutter-width}\", 2)" + Environment.NewLine +
				"-------------------------^" + Environment.NewLine +
				"Line 20:   margin-left: math.div(\"\" + $grid-gutter-width, 2)",
				warnings[0].SourceFragment
			);
			Assert.AreEqual(
				"   at root stylesheet (Files/simple/warnings/division-with-non-numeric-args/sass/style.sass:19:17)",
				warnings[0].CallStack
			);

			Assert.AreEqual(
				"Warning: " + WarningConstants.MathDivOnlySupportNumberArguments + Environment.NewLine +
				"   at root stylesheet (Files/simple/warnings/division-with-non-numeric-args/sass/style.sass:20:16) -> " +
				"  margin-left: math.div(\"\" + $grid-gutter-width, 2)",
				warnings[1].Message
			);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings[1].Description);
			Assert.AreEqual(false, warnings[1].IsDeprecation);
			Assert.AreEqual(inputPath, warnings[1].File);
			Assert.AreEqual(20, warnings[1].LineNumber);
			Assert.AreEqual(16, warnings[1].ColumnNumber);
			Assert.AreEqual(
				"Line 19:   margin-right: math.div(\"#{$grid-gutter-width}\", 2)" + Environment.NewLine +
				"Line 20:   margin-left: math.div(\"\" + $grid-gutter-width, 2)" + Environment.NewLine +
				"------------------------^" + Environment.NewLine + "Line 21:   -webkit-box-sizing: border-box",
				warnings[1].SourceFragment
			);
			Assert.AreEqual(
				"   at root stylesheet (Files/simple/warnings/division-with-non-numeric-args/sass/style.sass:20:16)",
				warnings[1].CallStack
			);

			Assert.AreEqual(
				"Warning: " + WarningConstants.MathDivOnlySupportNumberArguments + Environment.NewLine +
				"   at root stylesheet (Files/simple/warnings/division-with-non-numeric-args/sass/style.sass:33:12) -> " +
				"    width: math.div(100% * $num - $grid-gutter-width * ($grid-columns - $num), \"#{$grid-columns}\")",
				warnings[2].Message
			);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings[2].Description);
			Assert.AreEqual(false, warnings[2].IsDeprecation);
			Assert.AreEqual(inputPath, warnings[2].File);
			Assert.AreEqual(33, warnings[2].LineNumber);
			Assert.AreEqual(12, warnings[2].ColumnNumber);
			Assert.AreEqual(
				"Line 32:   .col-#{$num}" + Environment.NewLine +
				"Line 33:     width: math.div(100% * $num - $grid-gutter-width * ($grid-columns - $num), \"#{$grid-colum…" + Environment.NewLine +
				"--------------------^",
				warnings[2].SourceFragment
			);
			Assert.AreEqual(
				"   at root stylesheet (Files/simple/warnings/division-with-non-numeric-args/sass/style.sass:33:12)",
				warnings[2].CallStack
			);
		}

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

			string description1 = string.Format(WarningConstants.DeprecatedDivisionWithSimpleRecommendation,
				"map-get($grid-gutter-widths, xs)", 2);

			Assert.AreEqual(
				"Deprecation Warning: " + description1 + Environment.NewLine +
				"   at root stylesheet (Files/simple/warnings/deprecated-division/sass/style.sass:3:19) -> " +
				"$col-padding-xs:  map-get($grid-gutter-widths, xs) / 2",
				warnings[0].Message
			);
			Assert.AreEqual(description1, warnings[0].Description);
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

			string description2 = string.Format(WarningConstants.DeprecatedDivisionWithSimpleRecommendation,
				"$col-padding-xs", 2);

			Assert.AreEqual(
				"Deprecation Warning: " + description2 + Environment.NewLine +
				"   at root stylesheet (Files/simple/warnings/deprecated-division/sass/style.sass:6:18) -> " +
				"  padding-right: $col-padding-xs / 2",
				warnings[1].Message
			);
			Assert.AreEqual(description2, warnings[1].Description);
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

			string description = string.Format(WarningConstants.UnknownVendorPrefix, "wekbit");

			Assert.AreEqual(
				"Warning: " + description + Environment.NewLine +
				"   at prefix (Files/simple/warnings/custom-warning/sass/style.sass:6:7) -> " +
				"      @warn \"Unknown prefix #{$prefix}.\"" + Environment.NewLine +
				"   at root stylesheet (Files/simple/warnings/custom-warning/sass/style.sass:15:3)",
				warnings[0].Message
			);
			Assert.AreEqual(description, warnings[0].Description);
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
