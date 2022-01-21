using System;
using System.Collections.Generic;

using NUnit.Framework;

namespace DartSassHost.Tests.Modules
{
	[TestFixture]
	public sealed class ScssWarningTests : WarningTestsBase
	{
		public ScssWarningTests()
			: base(SyntaxType.Scss)
		{ }


		#region Code

		[Test]
		public void MappingSassWarningDuringCompilationOfCode()
		{
			// Arrange
			string inputPath = GenerateSassFilePath("division-with-non-numeric-args", "base");
			string input = GetFileContent(inputPath);
			string gridMixinFilePath = GenerateSassFilePath("division-with-non-numeric-args", "mixins/_grid");

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
				"   at make-column (Files/modules/warnings/division-with-non-numeric-args/scss/mixins/_grid.scss:17:10) -> " +
				"  width: math.div(100% * $columns - $grid-gutter-width * ($grid-columns - $columns), \"#{$grid-column" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/division-with-non-numeric-args/scss/base.scss:7:5)",
				warnings[0].Message
			);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings[0].Description);
			Assert.AreEqual(false, warnings[0].IsDeprecation);
			Assert.AreEqual(gridMixinFilePath, warnings[0].File);
			Assert.AreEqual(17, warnings[0].LineNumber);
			Assert.AreEqual(10, warnings[0].ColumnNumber);
			Assert.AreEqual(
				"Line 16:   float: left;" + Environment.NewLine +
				"Line 17:   width: math.div(100% * $columns - $grid-gutter-width * ($grid-columns - $columns), \"#{$grid…" + Environment.NewLine +
				"------------------^" + Environment.NewLine +
				"Line 18:   margin-right: math.div(\"#{$grid-gutter-width}\", 2);",
				warnings[0].SourceFragment
			);
			Assert.AreEqual(
				"   at make-column (Files/modules/warnings/division-with-non-numeric-args/scss/mixins/_grid.scss:17:10)" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/division-with-non-numeric-args/scss/base.scss:7:5)",
				warnings[0].CallStack
			);

			Assert.AreEqual(
				"Warning: " + WarningConstants.MathDivOnlySupportNumberArguments + Environment.NewLine +
				"   at make-column (Files/modules/warnings/division-with-non-numeric-args/scss/mixins/_grid.scss:18:17) -> " +
				"  margin-right: math.div(\"#{$grid-gutter-width}\", 2);" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/division-with-non-numeric-args/scss/base.scss:7:5)",
				warnings[1].Message
			);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings[1].Description);
			Assert.AreEqual(false, warnings[1].IsDeprecation);
			Assert.AreEqual(gridMixinFilePath, warnings[1].File);
			Assert.AreEqual(18, warnings[1].LineNumber);
			Assert.AreEqual(17, warnings[1].ColumnNumber);
			Assert.AreEqual(
				"Line 17:   width: math.div(100% * $columns - $grid-gutter-width * ($grid-columns - $columns), \"#{$grid-c…" + Environment.NewLine +
				"Line 18:   margin-right: math.div(\"#{$grid-gutter-width}\", 2);" + Environment.NewLine +
				"-------------------------^" + Environment.NewLine +
				"Line 19:   margin-left: math.div(\"\" + $grid-gutter-width, 2);",
				warnings[1].SourceFragment
			);
			Assert.AreEqual(
				"   at make-column (Files/modules/warnings/division-with-non-numeric-args/scss/mixins/_grid.scss:18:17)" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/division-with-non-numeric-args/scss/base.scss:7:5)",
				warnings[1].CallStack
			);

			Assert.AreEqual(
				"Warning: " + WarningConstants.MathDivOnlySupportNumberArguments + Environment.NewLine +
				"   at make-column (Files/modules/warnings/division-with-non-numeric-args/scss/mixins/_grid.scss:19:16) -> " +
				"  margin-left: math.div(\"\" + $grid-gutter-width, 2);" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/division-with-non-numeric-args/scss/base.scss:7:5)",
				warnings[2].Message
			);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings[2].Description);
			Assert.AreEqual(false, warnings[2].IsDeprecation);
			Assert.AreEqual(gridMixinFilePath, warnings[2].File);
			Assert.AreEqual(19, warnings[2].LineNumber);
			Assert.AreEqual(16, warnings[2].ColumnNumber);
			Assert.AreEqual(
				"Line 18:   margin-right: math.div(\"#{$grid-gutter-width}\", 2);" + Environment.NewLine +
				"Line 19:   margin-left: math.div(\"\" + $grid-gutter-width, 2);" + Environment.NewLine +
				"------------------------^",
				warnings[2].SourceFragment
			);
			Assert.AreEqual(
				"   at make-column (Files/modules/warnings/division-with-non-numeric-args/scss/mixins/_grid.scss:19:16)" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/division-with-non-numeric-args/scss/base.scss:7:5)",
				warnings[2].CallStack
			);
		}

		[Test]
		public void MappingSassDeprecationWarningDuringCompilationOfCode()
		{
			// Arrange
			string inputPath = GenerateSassFilePath("deprecated-division", "base");
			string input = GetFileContent(inputPath);
			string importedFilePath = GenerateSassFilePath("deprecated-division", "_mixins");

			// Act
			IList<ProblemInfo> warnings;

			using (var sassCompiler = CreateSassCompiler())
			{
				warnings = sassCompiler.Compile(input, inputPath).Warnings;
			}

			// Assert
			Assert.AreEqual(1, warnings.Count);

			string description = string.Format(WarningConstants.DeprecatedDivisionWithSimpleRecommendation,
				"$y", "$x");

			Assert.AreEqual(
				"Deprecation Warning: " + description + Environment.NewLine +
				"   at responsive-ratio (Files/modules/warnings/deprecated-division/scss/_mixins.scss:8:22) -> " +
				"  $padding: unquote(($y / $x) * 100 + \"%\");" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/deprecated-division/scss/base.scss:12:3)",
				warnings[0].Message
			);
			Assert.AreEqual(description, warnings[0].Description);
			Assert.AreEqual(true, warnings[0].IsDeprecation);
			Assert.AreEqual(importedFilePath, warnings[0].File);
			Assert.AreEqual(8, warnings[0].LineNumber);
			Assert.AreEqual(22, warnings[0].ColumnNumber);
			Assert.AreEqual(
				"Line 7: @mixin responsive-ratio($x, $y, $pseudo: false) {" + Environment.NewLine +
				"Line 8:   $padding: unquote(($y / $x) * 100 + \"%\");" + Environment.NewLine +
				"-----------------------------^" + Environment.NewLine +
				"Line 9:   @if $pseudo {",
				warnings[0].SourceFragment
			);
			Assert.AreEqual(
				"   at responsive-ratio (Files/modules/warnings/deprecated-division/scss/_mixins.scss:8:22)" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/deprecated-division/scss/base.scss:12:3)",
				warnings[0].CallStack
			);
		}

		[Test]
		public void MappingSassCustomWarningDuringCompilationOfCode()
		{
			// Arrange
			string inputPath = GenerateSassFilePath("custom-warning", "base");
			string input = GetFileContent(inputPath);
			string importedFilePath = GenerateSassFilePath("custom-warning", "_mixins");

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
				"   at prefix (Files/modules/warnings/custom-warning/scss/_mixins.scss:6:7) -> " +
				"      @warn 'Unknown prefix #{$prefix}.';" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/custom-warning/scss/base.scss:4:3)",
				warnings[0].Message
			);
			Assert.AreEqual(description, warnings[0].Description);
			Assert.AreEqual(false, warnings[0].IsDeprecation);
			Assert.AreEqual(importedFilePath, warnings[0].File);
			Assert.AreEqual(6, warnings[0].LineNumber);
			Assert.AreEqual(7, warnings[0].ColumnNumber);
			Assert.AreEqual(
				"Line 5:     @if not index($known-prefixes, $prefix) {" + Environment.NewLine +
				"Line 6:       @warn 'Unknown prefix #{$prefix}.';" + Environment.NewLine +
				"--------------^" + Environment.NewLine +
				"Line 7:     }",
				warnings[0].SourceFragment
			);
			Assert.AreEqual(
				"   at prefix (Files/modules/warnings/custom-warning/scss/_mixins.scss:6:7)" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/custom-warning/scss/base.scss:4:3)",
				warnings[0].CallStack
			);
		}

		#endregion

		#region Files

		[Test]
		public void MappingSassWarningDuringCompilationOfFile()
		{
			// Arrange
			string inputPath = GenerateSassFilePath("division-with-non-numeric-args", "base");
			string gridMixinFilePath = GenerateSassFilePath("division-with-non-numeric-args", "mixins/_grid");

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
				"   at make-column (Files/modules/warnings/division-with-non-numeric-args/scss/mixins/_grid.scss:17:10) -> " +
				"  width: math.div(100% * $columns - $grid-gutter-width * ($grid-columns - $columns), \"#{$grid-column" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/division-with-non-numeric-args/scss/base.scss:7:5)",
				warnings[0].Message
			);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings[0].Description);
			Assert.AreEqual(false, warnings[0].IsDeprecation);
			Assert.AreEqual(gridMixinFilePath, warnings[0].File);
			Assert.AreEqual(17, warnings[0].LineNumber);
			Assert.AreEqual(10, warnings[0].ColumnNumber);
			Assert.AreEqual(
				"Line 16:   float: left;" + Environment.NewLine +
				"Line 17:   width: math.div(100% * $columns - $grid-gutter-width * ($grid-columns - $columns), \"#{$grid…" + Environment.NewLine +
				"------------------^" + Environment.NewLine +
				"Line 18:   margin-right: math.div(\"#{$grid-gutter-width}\", 2);",
				warnings[0].SourceFragment
			);
			Assert.AreEqual(
				"   at make-column (Files/modules/warnings/division-with-non-numeric-args/scss/mixins/_grid.scss:17:10)" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/division-with-non-numeric-args/scss/base.scss:7:5)",
				warnings[0].CallStack
			);

			Assert.AreEqual(
				"Warning: " + WarningConstants.MathDivOnlySupportNumberArguments + Environment.NewLine +
				"   at make-column (Files/modules/warnings/division-with-non-numeric-args/scss/mixins/_grid.scss:18:17) -> " +
				"  margin-right: math.div(\"#{$grid-gutter-width}\", 2);" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/division-with-non-numeric-args/scss/base.scss:7:5)",
				warnings[1].Message
			);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings[1].Description);
			Assert.AreEqual(false, warnings[1].IsDeprecation);
			Assert.AreEqual(gridMixinFilePath, warnings[1].File);
			Assert.AreEqual(18, warnings[1].LineNumber);
			Assert.AreEqual(17, warnings[1].ColumnNumber);
			Assert.AreEqual(
				"Line 17:   width: math.div(100% * $columns - $grid-gutter-width * ($grid-columns - $columns), \"#{$grid-c…" + Environment.NewLine +
				"Line 18:   margin-right: math.div(\"#{$grid-gutter-width}\", 2);" + Environment.NewLine +
				"-------------------------^" + Environment.NewLine +
				"Line 19:   margin-left: math.div(\"\" + $grid-gutter-width, 2);",
				warnings[1].SourceFragment
			);
			Assert.AreEqual(
				"   at make-column (Files/modules/warnings/division-with-non-numeric-args/scss/mixins/_grid.scss:18:17)" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/division-with-non-numeric-args/scss/base.scss:7:5)",
				warnings[1].CallStack
			);

			Assert.AreEqual(
				"Warning: " + WarningConstants.MathDivOnlySupportNumberArguments + Environment.NewLine +
				"   at make-column (Files/modules/warnings/division-with-non-numeric-args/scss/mixins/_grid.scss:19:16) -> " +
				"  margin-left: math.div(\"\" + $grid-gutter-width, 2);" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/division-with-non-numeric-args/scss/base.scss:7:5)",
				warnings[2].Message
			);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings[2].Description);
			Assert.AreEqual(false, warnings[2].IsDeprecation);
			Assert.AreEqual(gridMixinFilePath, warnings[2].File);
			Assert.AreEqual(19, warnings[2].LineNumber);
			Assert.AreEqual(16, warnings[2].ColumnNumber);
			Assert.AreEqual(
				"Line 18:   margin-right: math.div(\"#{$grid-gutter-width}\", 2);" + Environment.NewLine +
				"Line 19:   margin-left: math.div(\"\" + $grid-gutter-width, 2);" + Environment.NewLine +
				"------------------------^",
				warnings[2].SourceFragment
			);
			Assert.AreEqual(
				"   at make-column (Files/modules/warnings/division-with-non-numeric-args/scss/mixins/_grid.scss:19:16)" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/division-with-non-numeric-args/scss/base.scss:7:5)",
				warnings[2].CallStack
			);
		}

		[Test]
		public void MappingSassDeprecationWarningDuringCompilationOfFile()
		{
			// Arrange
			string inputPath = GenerateSassFilePath("deprecated-division", "base");
			string importedFilePath = GenerateSassFilePath("deprecated-division", "_mixins");

			// Act
			IList<ProblemInfo> warnings;

			using (var sassCompiler = CreateSassCompiler())
			{
				warnings = sassCompiler.CompileFile(inputPath).Warnings;
			}

			// Assert
			Assert.AreEqual(1, warnings.Count);

			string description = string.Format(WarningConstants.DeprecatedDivisionWithSimpleRecommendation,
				"$y", "$x");

			Assert.AreEqual(
				"Deprecation Warning: " + description + Environment.NewLine +
				"   at responsive-ratio (Files/modules/warnings/deprecated-division/scss/_mixins.scss:8:22) -> " +
				"  $padding: unquote(($y / $x) * 100 + \"%\");" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/deprecated-division/scss/base.scss:12:3)",
				warnings[0].Message
			);
			Assert.AreEqual(description, warnings[0].Description);
			Assert.AreEqual(true, warnings[0].IsDeprecation);
			Assert.AreEqual(importedFilePath, warnings[0].File);
			Assert.AreEqual(8, warnings[0].LineNumber);
			Assert.AreEqual(22, warnings[0].ColumnNumber);
			Assert.AreEqual(
				"Line 7: @mixin responsive-ratio($x, $y, $pseudo: false) {" + Environment.NewLine +
				"Line 8:   $padding: unquote(($y / $x) * 100 + \"%\");" + Environment.NewLine +
				"-----------------------------^" + Environment.NewLine +
				"Line 9:   @if $pseudo {",
				warnings[0].SourceFragment
			);
			Assert.AreEqual(
				"   at responsive-ratio (Files/modules/warnings/deprecated-division/scss/_mixins.scss:8:22)" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/deprecated-division/scss/base.scss:12:3)",
				warnings[0].CallStack
			);
		}

		[Test]
		public void MappingSassCustomWarningDuringCompilationOfFile()
		{
			// Arrange
			string inputPath = GenerateSassFilePath("custom-warning", "base");
			string importedFilePath = GenerateSassFilePath("custom-warning", "_mixins");

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
				"   at prefix (Files/modules/warnings/custom-warning/scss/_mixins.scss:6:7) -> " +
				"      @warn 'Unknown prefix #{$prefix}.';" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/custom-warning/scss/base.scss:4:3)",
				warnings[0].Message
			);
			Assert.AreEqual(description, warnings[0].Description);
			Assert.AreEqual(false, warnings[0].IsDeprecation);
			Assert.AreEqual(importedFilePath, warnings[0].File);
			Assert.AreEqual(6, warnings[0].LineNumber);
			Assert.AreEqual(7, warnings[0].ColumnNumber);
			Assert.AreEqual(
				"Line 5:     @if not index($known-prefixes, $prefix) {" + Environment.NewLine +
				"Line 6:       @warn 'Unknown prefix #{$prefix}.';" + Environment.NewLine +
				"--------------^" + Environment.NewLine +
				"Line 7:     }",
				warnings[0].SourceFragment
			);
			Assert.AreEqual(
				"   at prefix (Files/modules/warnings/custom-warning/scss/_mixins.scss:6:7)" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/custom-warning/scss/base.scss:4:3)",
				warnings[0].CallStack
			);
		}

		#endregion
	}
}