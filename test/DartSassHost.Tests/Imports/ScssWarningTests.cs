using System;
using System.Collections.Generic;

using NUnit.Framework;

namespace DartSassHost.Tests.Imports
{
	[TestFixture]
	public sealed class ScssWarningTests : WarningTestsBase
	{
		public ScssWarningTests()
			: base(SyntaxType.Scss)
		{ }


		[Test]
		public void MappingSassWarningDuringCompilation([Values]bool fromFile)
		{
			// Arrange
			var options = new CompilationOptions { SilenceDeprecations = new List<string> { "import" } };

			string inputPath = GenerateSassFilePath("division-with-non-numeric-args", "base");
			string input = !fromFile ? GetFileContent(inputPath) : string.Empty;
			string gridMixinFilePath = GenerateSassFilePath("division-with-non-numeric-args", "mixins/_grid");

			// Act
			IList<ProblemInfo> warnings;

			using (var sassCompiler = CreateSassCompiler(options))
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
				"   at make-column (Files/imports/warnings/division-with-non-numeric-args/scss/mixins/_grid.scss:13:10) -> " +
				"  width: math.div(100% * $columns - $grid-gutter-width * ($grid-columns - $columns), \"#{$grid-column" + Environment.NewLine +
				"   at root stylesheet (Files/imports/warnings/division-with-non-numeric-args/scss/base.scss:8:5)",
				warnings[0].Message
			);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings[0].Description);
			Assert.AreEqual(false, warnings[0].IsDeprecation);
			Assert.Null(warnings[0].DeprecationId);
			Assert.AreEqual(gridMixinFilePath, warnings[0].File);
			Assert.AreEqual(13, warnings[0].LineNumber);
			Assert.AreEqual(10, warnings[0].ColumnNumber);
			Assert.AreEqual(
				"Line 12:   float: left;" + Environment.NewLine +
				"Line 13:   width: math.div(100% * $columns - $grid-gutter-width * ($grid-columns - $columns), \"#{$grid…" + Environment.NewLine +
				"------------------^" + Environment.NewLine +
				"Line 14:   margin-right: math.div(\"#{$grid-gutter-width}\", 2);",
				warnings[0].SourceFragment
			);
			Assert.AreEqual(
				"   at make-column (Files/imports/warnings/division-with-non-numeric-args/scss/mixins/_grid.scss:13:10)" + Environment.NewLine +
				"   at root stylesheet (Files/imports/warnings/division-with-non-numeric-args/scss/base.scss:8:5)",
				warnings[0].CallStack
			);

			Assert.AreEqual(
				"Warning: " + WarningConstants.MathDivOnlySupportNumberArguments + Environment.NewLine +
				"   at make-column (Files/imports/warnings/division-with-non-numeric-args/scss/mixins/_grid.scss:14:17) -> " +
				"  margin-right: math.div(\"#{$grid-gutter-width}\", 2);" + Environment.NewLine +
				"   at root stylesheet (Files/imports/warnings/division-with-non-numeric-args/scss/base.scss:8:5)",
				warnings[1].Message
			);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings[1].Description);
			Assert.AreEqual(false, warnings[1].IsDeprecation);
			Assert.Null(warnings[1].DeprecationId);
			Assert.AreEqual(gridMixinFilePath, warnings[1].File);
			Assert.AreEqual(14, warnings[1].LineNumber);
			Assert.AreEqual(17, warnings[1].ColumnNumber);
			Assert.AreEqual(
				"Line 13:   width: math.div(100% * $columns - $grid-gutter-width * ($grid-columns - $columns), \"#{$grid-c…" + Environment.NewLine +
				"Line 14:   margin-right: math.div(\"#{$grid-gutter-width}\", 2);" + Environment.NewLine +
				"-------------------------^" + Environment.NewLine +
				"Line 15:   margin-left: math.div(\"\" + $grid-gutter-width, 2);",
				warnings[1].SourceFragment
			);
			Assert.AreEqual(
				"   at make-column (Files/imports/warnings/division-with-non-numeric-args/scss/mixins/_grid.scss:14:17)" + Environment.NewLine +
				"   at root stylesheet (Files/imports/warnings/division-with-non-numeric-args/scss/base.scss:8:5)",
				warnings[1].CallStack
			);

			Assert.AreEqual(
				"Warning: " + WarningConstants.MathDivOnlySupportNumberArguments + Environment.NewLine +
				"   at make-column (Files/imports/warnings/division-with-non-numeric-args/scss/mixins/_grid.scss:15:16) -> " +
				"  margin-left: math.div(\"\" + $grid-gutter-width, 2);" + Environment.NewLine +
				"   at root stylesheet (Files/imports/warnings/division-with-non-numeric-args/scss/base.scss:8:5)",
				warnings[2].Message
			);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings[2].Description);
			Assert.AreEqual(false, warnings[2].IsDeprecation);
			Assert.Null(warnings[2].DeprecationId);
			Assert.AreEqual(gridMixinFilePath, warnings[2].File);
			Assert.AreEqual(15, warnings[2].LineNumber);
			Assert.AreEqual(16, warnings[2].ColumnNumber);
			Assert.AreEqual(
				"Line 14:   margin-right: math.div(\"#{$grid-gutter-width}\", 2);" + Environment.NewLine +
				"Line 15:   margin-left: math.div(\"\" + $grid-gutter-width, 2);" + Environment.NewLine +
				"------------------------^",
				warnings[2].SourceFragment
			);
			Assert.AreEqual(
				"   at make-column (Files/imports/warnings/division-with-non-numeric-args/scss/mixins/_grid.scss:15:16)" + Environment.NewLine +
				"   at root stylesheet (Files/imports/warnings/division-with-non-numeric-args/scss/base.scss:8:5)",
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
			Assert.AreEqual(3, warnings.Count);

			Assert.AreEqual(
				"Deprecation Warning [import]: " + WarningConstants.SassImportRulesDeprecated + Environment.NewLine +
				"   at root stylesheet (Files/imports/warnings/deprecated-division/scss/base.scss:1:9) -> " +
				"@import 'mixins';",
				warnings[0].Message
			);
			Assert.AreEqual(WarningConstants.SassImportRulesDeprecated, warnings[0].Description);
			Assert.AreEqual(true, warnings[0].IsDeprecation);
			Assert.AreEqual("import", warnings[0].DeprecationId);
			Assert.AreEqual(inputPath, warnings[0].File);
			Assert.AreEqual(1, warnings[0].LineNumber);
			Assert.AreEqual(9, warnings[0].ColumnNumber);
			Assert.AreEqual(
				"Line 1: @import 'mixins';" + Environment.NewLine +
				"----------------^",
				warnings[0].SourceFragment
			);
			Assert.AreEqual(
				"   at root stylesheet (Files/imports/warnings/deprecated-division/scss/base.scss:1:9)",
				warnings[0].CallStack
			);

			string description2 = string.Format(WarningConstants.DeprecatedDivisionWithSimpleRecommendation,
				"$y", "$x");

			Assert.AreEqual(
				"Deprecation Warning [slash-div]: " + description2 + Environment.NewLine +
				"   at responsive-ratio (Files/imports/warnings/deprecated-division/scss/_mixins.scss:8:22) -> " +
				"  $padding: unquote(($y / $x) * 100 + '%');" + Environment.NewLine +
				"   at root stylesheet (Files/imports/warnings/deprecated-division/scss/base.scss:12:3)",
				warnings[1].Message
			);
			Assert.AreEqual(description2, warnings[1].Description);
			Assert.AreEqual(true, warnings[1].IsDeprecation);
			Assert.AreEqual("slash-div", warnings[1].DeprecationId);
			Assert.AreEqual(importedFilePath, warnings[1].File);
			Assert.AreEqual(8, warnings[1].LineNumber);
			Assert.AreEqual(22, warnings[1].ColumnNumber);
			Assert.AreEqual(
				"Line 7: @mixin responsive-ratio($x, $y, $pseudo: false) {" + Environment.NewLine +
				"Line 8:   $padding: unquote(($y / $x) * 100 + '%');" + Environment.NewLine +
				"-----------------------------^" + Environment.NewLine +
				"Line 9:   @if $pseudo {",
				warnings[1].SourceFragment
			);
			Assert.AreEqual(
				"   at responsive-ratio (Files/imports/warnings/deprecated-division/scss/_mixins.scss:8:22)" + Environment.NewLine +
				"   at root stylesheet (Files/imports/warnings/deprecated-division/scss/base.scss:12:3)",
				warnings[1].CallStack
			);

			string description3 = string.Format(WarningConstants.GlobalBuiltinFunctionDeprecated, "string.unquote");

			Assert.AreEqual(
				"Deprecation Warning [global-builtin]: " + description3 + Environment.NewLine +
				"   at responsive-ratio (Files/imports/warnings/deprecated-division/scss/_mixins.scss:8:13) -> " +
				"  $padding: unquote(($y / $x) * 100 + '%');" + Environment.NewLine +
				"   at root stylesheet (Files/imports/warnings/deprecated-division/scss/base.scss:12:3)",
				warnings[2].Message
			);
			Assert.AreEqual(description3, warnings[2].Description);
			Assert.AreEqual(true, warnings[2].IsDeprecation);
			Assert.AreEqual("global-builtin", warnings[2].DeprecationId);
			Assert.AreEqual(importedFilePath, warnings[2].File);
			Assert.AreEqual(8, warnings[2].LineNumber);
			Assert.AreEqual(13, warnings[2].ColumnNumber);
			Assert.AreEqual(
				"Line 7: @mixin responsive-ratio($x, $y, $pseudo: false) {" + Environment.NewLine +
				"Line 8:   $padding: unquote(($y / $x) * 100 + '%');" + Environment.NewLine +
				"--------------------^" + Environment.NewLine +
				"Line 9:   @if $pseudo {",
				warnings[2].SourceFragment
			);
			Assert.AreEqual(
				"   at responsive-ratio (Files/imports/warnings/deprecated-division/scss/_mixins.scss:8:13)" + Environment.NewLine +
				"   at root stylesheet (Files/imports/warnings/deprecated-division/scss/base.scss:12:3)",
				warnings[2].CallStack
			);
		}

		[Test]
		public void MappingSassCustomWarningDuringCompilation([Values]bool fromFile)
		{
			// Arrange
			var options = new CompilationOptions { SilenceDeprecations = new List<string> { "global-builtin", "import" } };

			string inputPath = GenerateSassFilePath("custom-warning", "base");
			string input = !fromFile ? GetFileContent(inputPath) : string.Empty;
			string importedFilePath = GenerateSassFilePath("custom-warning", "_mixins");

			// Act
			IList<ProblemInfo> warnings;

			using (var sassCompiler = CreateSassCompiler(options))
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
				"   at prefix (Files/imports/warnings/custom-warning/scss/_mixins.scss:6:7) -> " +
				"      @warn \"Unknown prefix #{$prefix}.\";" + Environment.NewLine +
				"   at root stylesheet (Files/imports/warnings/custom-warning/scss/base.scss:4:3)",
				warnings[0].Message
			);
			Assert.AreEqual(description, warnings[0].Description);
			Assert.AreEqual(false, warnings[0].IsDeprecation);
			Assert.Null(warnings[0].DeprecationId);
			Assert.AreEqual(importedFilePath, warnings[0].File);
			Assert.AreEqual(6, warnings[0].LineNumber);
			Assert.AreEqual(7, warnings[0].ColumnNumber);
			Assert.AreEqual(
				"Line 5:     @if not index($known-prefixes, $prefix) {" + Environment.NewLine +
				"Line 6:       @warn \"Unknown prefix #{$prefix}.\";" + Environment.NewLine +
				"--------------^" + Environment.NewLine +
				"Line 7:     }",
				warnings[0].SourceFragment
			);
			Assert.AreEqual(
				"   at prefix (Files/imports/warnings/custom-warning/scss/_mixins.scss:6:7)" + Environment.NewLine +
				"   at root stylesheet (Files/imports/warnings/custom-warning/scss/base.scss:4:3)",
				warnings[0].CallStack
			);
		}
	}
}