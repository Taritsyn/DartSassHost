using System;
using System.Collections.Generic;

using NUnit.Framework;

namespace DartSassHost.Tests.Modules
{
	[TestFixture]
	public sealed class SassWarningTests : WarningTestsBase
	{
		public SassWarningTests()
			: base(SyntaxType.Sass)
		{ }


		[Test]
		public void MappingSassWarningDuringCompilation([Values]bool fromFile)
		{
			// Arrange
			string inputPath = GenerateSassFilePath("division-with-non-numeric-args", "base");
			string input = !fromFile ? GetFileContent(inputPath) : string.Empty;
			string gridMixinFilePath = GenerateSassFilePath("division-with-non-numeric-args", "mixins/_grid");

			// Act
			IList<ProblemInfo> warnings;

			using (var sassCompiler = CreateSassCompiler())
			{
				if (fromFile)
				{
					warnings = sassCompiler.CompileFile(inputPath).Warnings;
				}
				else
				{
					warnings = sassCompiler.Compile(input, inputPath).Warnings;
				}
			}

			// Assert
			Assert.AreEqual(3, warnings.Count);

			Assert.AreEqual(
				"Warning: " + WarningConstants.MathDivOnlySupportNumberArguments + Environment.NewLine +
				"   at make-column (Files/modules/warnings/division-with-non-numeric-args/sass/mixins/_grid.sass:15:10) -> " +
				"  width: math.div(100% * $columns - $grid-gutter-width * ($grid-columns - $columns), \"#{$grid-column" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/division-with-non-numeric-args/sass/base.sass:7:5)",
				warnings[0].Message
			);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings[0].Description);
			Assert.AreEqual(false, warnings[0].IsDeprecation);
			Assert.Null(warnings[0].DeprecationId);
			Assert.AreEqual(gridMixinFilePath, warnings[0].File);
			Assert.AreEqual(15, warnings[0].LineNumber);
			Assert.AreEqual(10, warnings[0].ColumnNumber);
			Assert.AreEqual(
				"Line 14:   float: left" + Environment.NewLine +
				"Line 15:   width: math.div(100% * $columns - $grid-gutter-width * ($grid-columns - $columns), \"#{$grid…" + Environment.NewLine +
				"------------------^" + Environment.NewLine +
				"Line 16:   margin-right: math.div(\"#{$grid-gutter-width}\", 2)",
				warnings[0].SourceFragment
			);
			Assert.AreEqual(
				"   at make-column (Files/modules/warnings/division-with-non-numeric-args/sass/mixins/_grid.sass:15:10)" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/division-with-non-numeric-args/sass/base.sass:7:5)",
				warnings[0].CallStack
			);

			Assert.AreEqual(
				"Warning: " + WarningConstants.MathDivOnlySupportNumberArguments + Environment.NewLine +
				"   at make-column (Files/modules/warnings/division-with-non-numeric-args/sass/mixins/_grid.sass:16:17) -> " +
				"  margin-right: math.div(\"#{$grid-gutter-width}\", 2)" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/division-with-non-numeric-args/sass/base.sass:7:5)",
				warnings[1].Message
			);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings[1].Description);
			Assert.AreEqual(false, warnings[1].IsDeprecation);
			Assert.Null(warnings[1].DeprecationId);
			Assert.AreEqual(gridMixinFilePath, warnings[1].File);
			Assert.AreEqual(16, warnings[1].LineNumber);
			Assert.AreEqual(17, warnings[1].ColumnNumber);
			Assert.AreEqual(
				"Line 15:   width: math.div(100% * $columns - $grid-gutter-width * ($grid-columns - $columns), \"#{$grid-c…" + Environment.NewLine +
				"Line 16:   margin-right: math.div(\"#{$grid-gutter-width}\", 2)" + Environment.NewLine +
				"-------------------------^" + Environment.NewLine +
				"Line 17:   margin-left: math.div(\"\" + $grid-gutter-width, 2)",
				warnings[1].SourceFragment
			);
			Assert.AreEqual(
				"   at make-column (Files/modules/warnings/division-with-non-numeric-args/sass/mixins/_grid.sass:16:17)" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/division-with-non-numeric-args/sass/base.sass:7:5)",
				warnings[1].CallStack
			);

			Assert.AreEqual(
				"Warning: " + WarningConstants.MathDivOnlySupportNumberArguments + Environment.NewLine +
				"   at make-column (Files/modules/warnings/division-with-non-numeric-args/sass/mixins/_grid.sass:17:16) -> " +
				"  margin-left: math.div(\"\" + $grid-gutter-width, 2)" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/division-with-non-numeric-args/sass/base.sass:7:5)",
				warnings[2].Message
			);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings[2].Description);
			Assert.AreEqual(false, warnings[2].IsDeprecation);
			Assert.Null(warnings[2].DeprecationId);
			Assert.AreEqual(gridMixinFilePath, warnings[2].File);
			Assert.AreEqual(17, warnings[2].LineNumber);
			Assert.AreEqual(16, warnings[2].ColumnNumber);
			Assert.AreEqual(
				"Line 16:   margin-right: math.div(\"#{$grid-gutter-width}\", 2)" + Environment.NewLine +
				"Line 17:   margin-left: math.div(\"\" + $grid-gutter-width, 2)" + Environment.NewLine +
				"------------------------^",
				warnings[2].SourceFragment
			);
			Assert.AreEqual(
				"   at make-column (Files/modules/warnings/division-with-non-numeric-args/sass/mixins/_grid.sass:17:16)" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/division-with-non-numeric-args/sass/base.sass:7:5)",
				warnings[2].CallStack
			);
		}

		[Test]
		public void MappingSassDeprecationWarningDuringCompilation([Values]bool fromFile)
		{
			// Arrange
			string inputPath = GenerateSassFilePath("deprecated-division", "base");
			string input = !fromFile ? GetFileContent(inputPath) : string.Empty;
			string importedFilePath = GenerateSassFilePath("deprecated-division", "_mixins");

			// Act
			IList<ProblemInfo> warnings;

			using (var sassCompiler = CreateSassCompiler())
			{
				if (fromFile)
				{
					warnings = sassCompiler.CompileFile(inputPath).Warnings;
				}
				else
				{
					warnings = sassCompiler.Compile(input, inputPath).Warnings;
				}
			}

			// Assert
			Assert.AreEqual(1, warnings.Count);

			string description = string.Format(WarningConstants.DeprecatedDivisionWithSimpleRecommendation,
				"$y", "$x");

			Assert.AreEqual(
				"Deprecation Warning [slash-div]: " + description + Environment.NewLine +
				"   at responsive-ratio (Files/modules/warnings/deprecated-division/sass/_mixins.sass:7:22) -> " +
				"  $padding: unquote(($y / $x) * 100 + \"%\")" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/deprecated-division/sass/base.sass:11:3)",
				warnings[0].Message
			);
			Assert.AreEqual(description, warnings[0].Description);
			Assert.AreEqual(true, warnings[0].IsDeprecation);
			Assert.AreEqual("slash-div", warnings[0].DeprecationId);
			Assert.AreEqual(importedFilePath, warnings[0].File);
			Assert.AreEqual(7, warnings[0].LineNumber);
			Assert.AreEqual(22, warnings[0].ColumnNumber);
			Assert.AreEqual(
				"Line 6: @mixin responsive-ratio($x, $y, $pseudo: false)" + Environment.NewLine +
				"Line 7:   $padding: unquote(($y / $x) * 100 + \"%\")" + Environment.NewLine +
				"-----------------------------^" + Environment.NewLine +
				"Line 8:   @if $pseudo",
				warnings[0].SourceFragment
			);
			Assert.AreEqual(
				"   at responsive-ratio (Files/modules/warnings/deprecated-division/sass/_mixins.sass:7:22)" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/deprecated-division/sass/base.sass:11:3)",
				warnings[0].CallStack
			);
		}

		[Test]
		public void MappingSassCustomWarningDuringCompilation([Values]bool fromFile)
		{
			// Arrange
			string inputPath = GenerateSassFilePath("custom-warning", "base");
			string input = !fromFile ? GetFileContent(inputPath) : string.Empty;
			string importedFilePath = GenerateSassFilePath("custom-warning", "_mixins");

			// Act
			IList<ProblemInfo> warnings;

			using (var sassCompiler = CreateSassCompiler())
			{
				if (fromFile)
				{
					warnings = sassCompiler.CompileFile(inputPath).Warnings;
				}
				else
				{
					warnings = sassCompiler.Compile(input, inputPath).Warnings;
				}
			}

			// Assert
			Assert.AreEqual(1, warnings.Count);

			string description = string.Format(WarningConstants.UnknownVendorPrefix, "wekbit");

			Assert.AreEqual(
				"Warning: " + description + Environment.NewLine +
				"   at prefix (Files/modules/warnings/custom-warning/sass/_mixins.sass:6:7) -> " +
				"      @warn 'Unknown prefix #{$prefix}.'" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/custom-warning/sass/base.sass:4:3)",
				warnings[0].Message
			);
			Assert.AreEqual(description, warnings[0].Description);
			Assert.AreEqual(false, warnings[0].IsDeprecation);
			Assert.Null(warnings[0].DeprecationId);
			Assert.AreEqual(importedFilePath, warnings[0].File);
			Assert.AreEqual(6, warnings[0].LineNumber);
			Assert.AreEqual(7, warnings[0].ColumnNumber);
			Assert.AreEqual(
				"Line 5:     @if not index($known-prefixes, $prefix)" + Environment.NewLine +
				"Line 6:       @warn 'Unknown prefix #{$prefix}.'" + Environment.NewLine +
				"--------------^",
				warnings[0].SourceFragment
			);
			Assert.AreEqual(
				"   at prefix (Files/modules/warnings/custom-warning/sass/_mixins.sass:6:7)" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/custom-warning/sass/base.sass:4:3)",
				warnings[0].CallStack
			);
		}
	}
}